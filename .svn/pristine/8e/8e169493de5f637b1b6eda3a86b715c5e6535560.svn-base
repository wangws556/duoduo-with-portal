using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common;

namespace YoYoStudio.Client.Chat.ValueConverters
{
    public class FlexPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Singleton<ApplicationViewModel>.Instance.FlexFile;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
