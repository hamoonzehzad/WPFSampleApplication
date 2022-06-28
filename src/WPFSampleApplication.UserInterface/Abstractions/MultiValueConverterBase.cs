using System.Globalization;
using System.Windows.Data;

namespace WPFSampleApplication.UserInterface.Abstractions;

/// <summary>
/// Base class of all multi value converters classes.
/// </summary>
public abstract class MultiValueConverterBase : IMultiValueConverter
{
    public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        // Have no use for this method in this application.
        throw new NotImplementedException();
    }
}
