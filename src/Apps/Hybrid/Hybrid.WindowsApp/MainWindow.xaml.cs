using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hybrid.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDropTarget
    {
        #region Ctors

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Resources.Add("services", serviceProvider);
        }

        #endregion

        #region UI Fields

        public ObservableCollection<string> dropFiles { get; set; } = new();    

        #endregion

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;

            dropInfo.Effects = dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.Move;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            
        }
    }
}
