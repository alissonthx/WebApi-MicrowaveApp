using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace MicrowaveApp.AvaloniaUI.Converters
{
    public class BoolNegationConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b) return !b;
            return Avalonia.Data.BindingOperations.DoNothing;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b) return !b;
            return Avalonia.Data.BindingOperations.DoNothing;
        }
    }

    public class BoolToTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter is string paramStr && paramStr.Contains('|'))
            {
                var parts = paramStr.Split('|');
                return (value is bool b && b) ? parts[0] : parts[1];
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) 
            => throw new NotSupportedException();
    }
}
