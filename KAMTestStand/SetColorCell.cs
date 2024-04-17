using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace KAMTestStand;

public class SetColorCell : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((ResultE)value)
        {
            case ResultE.StatusError:
                return new SolidColorBrush(Colors.Red);
            case ResultE.StatusNotStart:
                return new SolidColorBrush(Colors.Gold);
            case ResultE.StatusTesting:
                return new SolidColorBrush(Colors.BlueViolet);
            case ResultE.StatusPass:
                return new SolidColorBrush(Colors.ForestGreen);
            case ResultE.StatusNotAllow:
                return new SolidColorBrush(Colors.Black);
            case ResultE.StatusWait:
                return new SolidColorBrush(Colors.Gray);

        }
        
        return new SolidColorBrush(Colors.White);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}