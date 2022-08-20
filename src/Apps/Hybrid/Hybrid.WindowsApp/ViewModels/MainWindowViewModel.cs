using GongSolutions.Wpf.DragDrop;
using Hybrid.WindowsApp.Extensions;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.App.Actions.NativeActions;
using Web.Core.Store.App.Actions.NativeActions.WindowAppActions;
using Web.Core.Utils.WebScopeManager;

namespace Hybrid.WindowsApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IDropTarget, IDisposable
    {
        #region Injects

        private readonly WebScopeManager _webScopeManager;

        #endregion

        #region Ctors

        public MainWindowViewModel(WebScopeManager webScopeManager, Action closeAction)
        {
            _webScopeManager = webScopeManager;
            _webScopeManager.OnInitialized += OnWebScopeInitialized;

            DragLeaveCommand = new Command(DragLeave);
 
            this.closeAction = closeAction;
            this.PropertyChanged += OnViewModelPropertyChanged;
        }

        #endregion

        #region Fields

        private WindowState windowState;
        private Action closeAction;

        private bool localDragAndDropFlag = false;
        private bool showFileDropBlock = false;
        private ObservableCollection<string> dropFiles = new();

        #endregion

        #region Props

        public WindowState WindowState { get => windowState; set => SetProperty(ref windowState, value); }

        public bool LocalDragAndDropFlag { get => localDragAndDropFlag; set => SetProperty(ref localDragAndDropFlag, value); }
        public bool ShowFileDropBlock { get => showFileDropBlock; set => SetProperty(ref showFileDropBlock, value); }
        public ObservableCollection<string> DropFiles { get => dropFiles; set => SetProperty(ref dropFiles, value); }

        #endregion

        #region Commands

        public ICommand DragLeaveCommand { get; }

        #endregion

        #region External events

        private void OnWebScopeInitialized(object? sender, EventArgs e)
        {
            _webScopeManager.ActionSubscriber.SubscribeToAction<MinimizeWindowAction>(this, OnMinimizeWindowAction);
            _webScopeManager.ActionSubscriber.SubscribeToAction<ResizeWindowAction>(this, OnResizeWindowAction);
            _webScopeManager.ActionSubscriber.SubscribeToAction<CloseWindowAction>(this, OnCloseWindowAction);

            _webScopeManager.ActionSubscriber.SubscribeToAction<HideFileDropBlockAction>(this, OnHideFileDropBlockAction);
            _webScopeManager.ActionSubscriber.SubscribeToAction<ShowFileDropBlockAction>(this, OnShowFileDropBlockAction);
        }

        private void OnMinimizeWindowAction(MinimizeWindowAction _) => WindowState = WindowState.Minimized;

        private void OnResizeWindowAction(ResizeWindowAction _)
        {
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                default:
                    this.WindowState = WindowState.Normal;  
                    break;
            }
        }

        private void OnCloseWindowAction(CloseWindowAction _) => closeAction.Invoke(); 

        private void OnHideFileDropBlockAction(HideFileDropBlockAction _) => ShowFileDropBlock = false;

        private void OnShowFileDropBlockAction(ShowFileDropBlockAction _) => ShowFileDropBlock = LocalDragAndDropFlag = true;

        #endregion

        #region Internal events

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.WindowState):
                    _webScopeManager.Dispatcher.Dispatch(new WindowStateChangedAction(this.WindowState.ToAppWindowState()));
                    break;
            }
        }

        void DragLeave()
        {
            LocalDragAndDropFlag = false;
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            LocalDragAndDropFlag = true;

            var dataObject = dropInfo.Data as DataObject;

            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            dropInfo.Effects = dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.Move;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            LocalDragAndDropFlag = false;
            var dataObject = dropInfo.Data as DataObject;
            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                var files = dataObject.GetFileDropList().Cast<string>().ToList();
                _webScopeManager.Dispatcher.Dispatch(new AddFilesFromDiskAction(_webScopeManager.CurrentContentId, false)
                {
                    FilePaths = files
                });
            }
        }

        public void Dispose()
        {
            _webScopeManager.OnInitialized -= OnWebScopeInitialized;
            _webScopeManager.ActionSubscriber.UnsubscribeFromAllActions(this);
        }

        #endregion
    }
}
