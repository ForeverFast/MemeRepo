using Hybrid.WindowsApp.ViewModels;
using System;
using System.Windows;
using Web.Core.Utils.WebScopeManager;

namespace Hybrid.WindowsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow(IServiceProvider serviceProvider, WebScopeManager webScopeManager)
        {
            InitializeComponent();
            Resources.Add("services", serviceProvider);
            DataContext = new MainWindowViewModel(webScopeManager, () => this.Close());
        }
    }
}
