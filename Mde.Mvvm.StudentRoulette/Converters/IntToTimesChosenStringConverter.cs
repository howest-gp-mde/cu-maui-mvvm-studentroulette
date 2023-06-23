using System.Globalization;

namespace Mde.Mvvm.StudentRoulette.Converters
{
    public class IntToTimesChosenStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int) throw new ArgumentException("An integer value was not supplied. Cannot convert.");

            int amount = (int)value;

            if (amount <= 0) return "(never chosen before)";
            return amount == 1 ? $"(chosen {amount} time)" : $"(chosen {amount} times)"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
