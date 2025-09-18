using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MicrowaveApp.AvaloniaUI.Converters
{
    public class ProgramItalicConverter : IMultiValueConverter
    {

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count > 0 && values[0] is bool isPredefined)
            {
                return isPredefined ? Avalonia.Media.FontStyle.Italic : Avalonia.Media.FontStyle.Normal;
            }
            return Avalonia.Media.FontStyle.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
