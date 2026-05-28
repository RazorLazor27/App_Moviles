using System.Globalization;
using Microsoft.Maui.Controls;

namespace E2_Maui.Converters;

public sealed class CurrencyFormatConverter : IMultiValueConverter
{
    public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length < 2)
        {
            return string.Empty;
        }

        if (values[0] is not decimal amount)
        {
            return string.Empty;
        }

        var cultureName = values[1] as string;
        var formatCulture = ResolveCulture(cultureName) ?? culture;

        return string.Format(formatCulture, "{0:C}", amount);
    }

    public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        return Array.Empty<object>();
    }

    private static CultureInfo? ResolveCulture(string? cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return null;
        }

        try
        {
            return CultureInfo.GetCultureInfo(cultureName);
        }
        catch (CultureNotFoundException)
        {
            return null;
        }
    }
}
