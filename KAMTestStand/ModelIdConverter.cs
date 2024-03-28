using System;
using System.Globalization;
using System.Windows.Data;

namespace KAMTestStand;

public class ModelIdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((ModelId)value)
        {
            case ModelId.NoData:
                return "Нет данных";
            case ModelId.Kam14:
                return "КАМ200-14";
            case ModelId.Kam25:
                return "КАМ25";
            case ModelId.Kam25Mr:
                return "КАМ25-МР";
            default:
                return "Нет данных";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}