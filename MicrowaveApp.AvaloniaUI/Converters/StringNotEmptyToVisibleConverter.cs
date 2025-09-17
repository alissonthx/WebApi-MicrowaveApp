using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace MicrowaveApp.AvaloniaUI.Converters
{
    public class StringNotEmptyToVisibleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) 
            => throw new NotSupportedException();
    }
}
