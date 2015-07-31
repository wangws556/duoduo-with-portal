using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Client.Chat.ValueConverters
{
    public class VideoControlVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int status = (int)value;
                if (status != MicStatusMessage.MicStatus_Off)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
