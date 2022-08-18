using Hybrid.WindowsApp.ViewModels;
using System;
using System.Windows;

namespace Hybrid.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IServiceProvider serviceProvider, MainWindowViewModel viewModel)
        {
            InitializeComponent();
            Resources.Add("services", serviceProvider);
            DataContext = viewModel;
        }
    }
}
