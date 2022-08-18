using GongSolutions.Wpf.DragDrop;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Web.Core.Store.AppData.Actions.ChangeStateActions;
using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Utils.WebScopeManager;

namespace Hybrid.WindowsApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IDropTarget, IDisposable
    {
        #region Injects

        private readonly WebScopeManager _webScopeManager;

        #endregion

        #region Ctors

        public MainWindowViewModel(WebScopeManager webScopeManager)
        {
            _webScopeManager = webScopeManager;
            _webScopeManager.OnInitialized += OnWebScopeInitialized;

            DragLeaveCommand = new Command(DragLeave);
        }

        #endregion

        #region Fields

        private bool localDragAndDropFlag = false;
        private bool showFileDropBlock = false;
        private ObservableCollection<string> dropFiles = new();

        #endregion

        #region Props

        public bool LocalDragAndDropFlag { get => localDragAndDropFlag; set => SetProperty(ref localDragAndDropFlag, value); }
        public bool ShowFileDropBlock { get => showFileDropBlock; set => SetProperty(ref showFileDropBlock, value); }
        public ObservableCollection<string> DropFiles { get => dropFiles; set => SetProperty(ref dropFiles, value); }

        #endregion

        #region External events

        private void OnWebScopeInitialized(object? sender, EventArgs e)
        {
            _webScopeManager.ActionSubscriber.SubscribeToAction<HideFileDropBlockAction>(this, OnHideFileDropBlockAction);
            _webScopeManager.ActionSubscriber.SubscribeToAction<ShowFileDropBlockAction>(this, OnShowFileDropBlockAction);
        }

        private void OnHideFileDropBlockAction(HideFileDropBlockAction action)
        {
            ShowFileDropBlock = false;
        }

        private void OnShowFileDropBlockAction(ShowFileDropBlockAction action)
        {
            ShowFileDropBlock = true;
            LocalDragAndDropFlag = true;
        }

        #endregion

        #region Internal events

        public ICommand DragLeaveCommand { get; }


        void DragLeave()
        {
            LocalDragAndDropFlag = false;
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {           
            LocalDragAndDropFlag = true;

            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;

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
