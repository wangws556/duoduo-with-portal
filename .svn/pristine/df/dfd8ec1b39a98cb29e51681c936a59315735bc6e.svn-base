using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.Chat.WebPages
{
	internal static class AllWebPages
	{
        public static void Initialize()
        {
        }

        static AllWebPages()
        {
            StreamResourceInfo stream = null;
            byte[] data = null;

            stream = Application.GetResourceStream(new Uri("/WebPages/base.js", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);

            string baseJavaScript = System.Text.Encoding.UTF8.GetString(data);

            stream = Application.GetResourceStream(new Uri("/WebPages/base.css", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);

            string baseCss = System.Text.Encoding.UTF8.GetString(data);

            #region Hall Page

            HallPage = new WebPageViewModel();
            HallPage.Source = "Hall.Html";
            stream = Application.GetResourceStream(new Uri("/WebPages/Hall.html", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            HallPage.Body = System.Text.Encoding.UTF8.GetString(data);

			stream = Application.GetResourceStream(new Uri("/WebPages/Hall.js", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            HallPage.JavaScript = baseJavaScript + System.Text.Encoding.UTF8.GetString(data);

            stream = Application.GetResourceStream(new Uri("/WebPages/Hall.css", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            HallPage.Css = baseCss + System.Text.Encoding.UTF8.GetString(data);

            #endregion

            #region Room Page

            RoomPage = new WebPageViewModel();
            RoomPage.Source = "Room.Html";
            stream = Application.GetResourceStream(new Uri("/WebPages/Room.html", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            RoomPage.Body = System.Text.Encoding.UTF8.GetString(data);

            stream = Application.GetResourceStream(new Uri("/WebPages/Room.js", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            RoomPage.JavaScript = baseJavaScript + System.Text.Encoding.UTF8.GetString(data);

            stream = Application.GetResourceStream(new Uri("/WebPages/Room.css", UriKind.Relative));
            data = new byte[stream.Stream.Length];
            stream.Stream.Read(data, 0, data.Length);
            RoomPage.Css = baseCss + System.Text.Encoding.UTF8.GetString(data);

            #endregion
        }

        public static WebPageViewModel HallPage { get; private set; }
        public static WebPageViewModel RoomPage { get; private set; }


	}
}
