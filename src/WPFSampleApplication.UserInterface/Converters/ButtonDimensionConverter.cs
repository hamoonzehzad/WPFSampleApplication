using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Exceptions;
using WPFSampleApplication.UserInterface.Resources;
using System.Globalization;
using System.Windows;

namespace WPFSampleApplication.UserInterface.Converters;

/// <summary>
/// Calculates the dimension of one individual post item button.
/// </summary>
public sealed class ButtonDimensionConverter : MultiValueConverterBase
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var totalWidth = (double)values[0];

            // margin of all sides are the same so we pick top margin for instance.
            var margin = ((Thickness)values[1]).Top;

            // Calculating the size of the width (and height) including the button margin.
            var buttonWidth = (totalWidth - (20 * margin)) / 10;

            return buttonWidth;
        }
        catch (Exception)
        {
            throw new UIException(Messages.ConverterError, true);
        }
    }
}
