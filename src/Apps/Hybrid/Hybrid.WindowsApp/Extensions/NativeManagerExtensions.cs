using Domain.Core.Enums;
using System.Windows;

namespace Hybrid.WindowsApp.Extensions
{
    internal static class NativeExtensions
    {
        public static WindowState ToWindowState(this AppWindowState appWindowState)
            => appWindowState switch
            {
                AppWindowState.Normal => WindowState.Normal,
                AppWindowState.Minimized => WindowState.Minimized,
                AppWindowState.Maximized => WindowState.Maximized,
                _ => WindowState.Normal,
            };

        public static AppWindowState ToAppWindowState(this WindowState windowState)
           => windowState switch
           {
               WindowState.Normal => AppWindowState.Normal,
               WindowState.Minimized => AppWindowState.Minimized,
               WindowState.Maximized => AppWindowState.Maximized,
               _ => AppWindowState.Normal,
           };
    }
}
