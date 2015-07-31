using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YoYoStudio.Client.ViewModel;

namespace YoYoStudio.Client.Chat.ValueConverters
{
    public class OnMicLabelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RoomWindowViewModel rvm = values[0] as RoomWindowViewModel;
            if (rvm != null)
            {
                if (rvm.Me.MicStatus > 0)
                {
                    return rvm.DownMicLabel;
                }
                else
                {
                    return rvm.OnMicLabel;
                }
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OnMicPopupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int mic = (int)value;
            return mic <= 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
