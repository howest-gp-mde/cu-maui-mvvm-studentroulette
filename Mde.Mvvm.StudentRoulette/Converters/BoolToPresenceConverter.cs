using System.Globalization;
using System.Resources;

namespace Mde.Mvvm.StudentRoulette.Converters
{
    public class BoolToPresenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool) throw new ArgumentException("a boolean value must be supplied");

            bool present = (bool)value;

            Application.Current.Resources.TryGetValue("Primary", out object presentColor);
            Application.Current.Resources.TryGetValue("Gray300", out object notPresentColor);
            Color fallback = Color.FromRgb(0, 0, 0);

            if (present) return (presentColor as Color) ?? fallback;
            else return (notPresentColor as Color) ?? fallback;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
