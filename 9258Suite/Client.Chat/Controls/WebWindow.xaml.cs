using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.Chat.Controls
{
    /// <summary>
    /// Interaction logic for WebWindow.xaml
    /// </summary>
    public partial class WebWindow 
    {
        WindowViewModel windowVM;
        public WebWindow(WindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            Topmost = true;
            windowVM = vm;
            windowVM.View = this;
            webPage.LoadCompleted += webPage_LoadCompleted;
            if (vm.Busy)
            {
                PART_Loading.Visibility = System.Windows.Visibility.Visible;
                PART_Content.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                PART_Loading.Visibility = System.Windows.Visibility.Collapsed;
                PART_Content.Visibility = System.Windows.Visibility.Visible;
            }
        }

        void webPage_LoadCompleted()
        {
            webPage.InvokeJavaScript("Init", new object[] { 
                "<script type='text/javascript' >"+windowVM.WebPageVM.JavaScript+"</script>", 
                "<style type='text/css' >" + windowVM.WebPageVM.Css + "</style>", windowVM.WebPageVM.Body });
            if (windowVM is HallWindowViewModel)
            {
                HallWindowViewModel hallVM = windowVM as HallWindowViewModel;
                if (hallVM.ApplicationVM.ProfileVM.LastLoginVM != null)
                {
                    if(hallVM.ApplicationVM.ProfileVM.LastLoginVM.Remember)
                      webPage.InvokeJavaScript(WebWindowAction.InitHallWithLogin.ToString(), hallVM.RoomGroupsJson, hallVM.ApplicationVM.ProfileVM.LastLoginVM.UserId,
                        hallVM.ApplicationVM.ProfileVM.LastLoginVM.Password,
                        hallVM.ApplicationVM.ProfileVM.LastLoginVM.Remember,
                        hallVM.ApplicationVM.ProfileVM.AutoLogin);
                    else
                        webPage.InvokeJavaScript(WebWindowAction.InitHallWithLogin.ToString(), hallVM.RoomGroupsJson, hallVM.ApplicationVM.ProfileVM.LastLoginVM.UserId,
                          string.Empty,
                          hallVM.ApplicationVM.ProfileVM.LastLoginVM.Remember,
                          hallVM.ApplicationVM.ProfileVM.AutoLogin);

                }
                else
                {
                    webPage.InvokeJavaScript(WebWindowAction.InitHall.ToString(), hallVM.RoomGroupsJson);
                }
            }
            else if (windowVM is RoomWindowViewModel)
            {
                RoomWindowViewModel roomVM = windowVM as RoomWindowViewModel;
                webPage.InvokeJavaScript(WebWindowAction.InitRoom.ToString(), roomVM.GiftGroupsJson, roomVM.Me.GetJson(true),
                    webPage.ActualWidth, webPage.ActualHeight, roomVM.ApplicationVM.LocalCache.PublicChatMessageCount,
                    roomVM.ApplicationVM.LocalCache.PrivateChatMessageCount, roomVM.ApplicationVM.LocalCache.MessagePerSecond);
            }
            windowVM.LoadAsync();
        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, WebWindowAction> message)
        {
            switch (message.Action)
            {
                case WebWindowAction.InitHall:
                    PART_Loading.Visibility = System.Windows.Visibility.Collapsed;
                    PART_Content.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public override void CallJavaScript(string functionName, params object[] args)
        {
            Dispatcher.Invoke((Action)(() => webPage.InvokeJavaScript(functionName, args)));
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            webPage.LoadHtmlFile();
        }

        public override void Dispose()
        {
            webPage.Dispose();
            base.Dispose();
        }
    }
}
