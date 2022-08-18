using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hybrid.WindowsApp.Converters
{
    internal class BoolToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool localDragAndDropFlag = (values[0] != null && values[0] != DependencyProperty.UnsetValue)
                ? System.Convert.ToBoolean(values[0])
                : false;
            bool showFileDropBlock = (values[0] != null && values[0] != DependencyProperty.UnsetValue)
                ? System.Convert.ToBoolean(values[1])
                : false;

            if (localDragAndDropFlag || showFileDropBlock)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
