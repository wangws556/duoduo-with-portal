using Snippets;
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
using YoYoStudio.Client.Chat.Controls;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for CameraWindow.xaml
    /// </summary>
    public partial class CameraWindow
    {
        private VideoControl videoControl = null;
        private ComboBox cameraComboBox = null;

        public CameraWindow(CameraWindowViewModel vm):base(vm)
        {
            InitializeComponent();
            MinimizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
            MaximizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
        }

        public void Windows_Loaded(object sender, RoutedEventArgs arg)
        {
            if (videoControl == null)
            {
                videoControl = CameraContentControl.Template.FindName("PART_CameraControl", CameraContentControl) as VideoControl;
                if (videoControl != null)
                {
                    videoControl.FlashCallback += videoControl_FlashCallback;
                }
            }
            if (cameraComboBox == null)
            {
                cameraComboBox = CameraContentControl.Template.FindName("PART_CameraComboBox", CameraContentControl) as ComboBox;
                if (cameraComboBox != null)
                {
                    cameraComboBox.SelectionChanged += cameraComboBox_SelectionChanged; 
                }
            }
        }

        void cameraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.StartCamera, new string[] { cameraComboBox.SelectedIndex.ToString() });
        }

        void videoControl_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            switch (cmd)
            { 
                case YoYoStudio.Controls.Winform.FlexCallbackCommand.LoadComplete:
                    var cameras = videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.GetCameras).ToList();
                    CameraWindowViewModel cvm = videoControl.DataContext as CameraWindowViewModel;
                    if (cvm != null)
                        cvm.Cameras = new System.Collections.ObjectModel.ObservableCollection<string>(cameras);
                    break;
            }
        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, ViewModel.CameraWindowAction> message)
        {
            switch (message.Action)
            {
                case CameraWindowAction.TakePicture:
                    videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.TakePicture);
                    break;
                case CameraWindowAction.Save:
                    break;
                default:
                    break;
            }
        }

    }
}
