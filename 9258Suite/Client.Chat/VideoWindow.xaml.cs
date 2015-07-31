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
using YoYoStudio.Common.Notification;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for VideoWindow.xaml
    /// </summary>
    public partial class VideoWindow 
    {
        private bool embeded = true;

        public VideoWindow(VideoWindowViewModel vm, bool isEmbedded)
        {
            DataContext = vm;
            InitializeComponent();
            vc.MoviePath = vm.FlexPath;
            vc.FlashCallback += vc_FlashCallback;
            Loaded += VideoWindow_Loaded;
            embeded = isEmbedded;
        }

        void VideoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!embeded)
            {
                WindowStyle = System.Windows.WindowStyle.ToolWindow;
            }
        }

        void vc_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            VideoWindowViewModel vm = DataContext as VideoWindowViewModel;
            if (vm != null && vm.UserVM != null)
            {
                switch (cmd)
                {
                    case YoYoStudio.Controls.Winform.FlexCallbackCommand.ZoomIn:
                        Width = Width * 1.1;
                        Height = Height * 1.1;
                        break;
                    case YoYoStudio.Controls.Winform.FlexCallbackCommand.ZoomOut:
                        Width = Width * 0.9;
                        Height = Height * 0.9;
                        break;
                    case YoYoStudio.Controls.Winform.FlexCallbackCommand.LoadComplete:
                        vc.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.ConnectRTMP, vm.UserVM.RoomWindowVM.RoomVM.RtmpUrl);
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void ProcessMessage(EnumNotificationMessage<object, VideoWindowAction> message)
        {
        }

        public override void Dispose()
        {
            vc.Dispose();
            base.Dispose();
        }
    }
}
