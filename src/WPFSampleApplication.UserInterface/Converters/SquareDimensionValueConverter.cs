using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Exceptions;
using WPFSampleApplication.UserInterface.Resources;
using System.Globalization;
using System.Windows;

namespace WPFSampleApplication.UserInterface.Converters;

/// <summary>
/// Calculates the dimension of posts map to keep it in square shape.
/// </summary>
public sealed class SquareDimensionValueConverter : MultiValueConverterBase
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            //The size of the square should be determine from the smaller dimension.
            var width = (double)values[0];
            var height = (double)values[1];

            return width <= height
                ? width
                : height;
        }
        catch
        {
            throw new UIException(Messages.ConverterError,true);
        }
    }
}
