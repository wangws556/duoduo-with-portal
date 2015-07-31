using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YoYoStudio.Client.ViewModel;

namespace YoYoStudio.Client.Chat.ValueConverters
{
    public class PlayMusicLabelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RoomWindowViewModel rvm = values[0] as RoomWindowViewModel;
            if (rvm != null)
            {
                if (rvm.Me.MusicStatus == 0)
                {
                    return rvm.PlayMusicLabel;
                }
                else if (rvm.Me.MusicStatus == 1)
                {
                    return rvm.StopMusicLabel;
                }
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayMusicStatusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RoomWindowViewModel rvm = values[0] as RoomWindowViewModel;
            if (rvm != null)
            {
                if (rvm.Me.MicStatus > 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
