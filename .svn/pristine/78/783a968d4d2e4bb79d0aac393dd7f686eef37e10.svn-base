using Microsoft.Win32;
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
using YoYoStudio.Common.Wpf;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;
using YoYoStudio.Common;
using YoYoStudio.CaptureScreen;
using YoYoStudio.Common.Extensions;

namespace YoYoStudio.Client.Chat
{
	/// <summary>
	/// Interaction logic for ConfigurationWindow.xaml
	/// </summary>
    public partial class ConfigurationWindow
	{
        private readonly ScreenCaputre screenCapture = new ScreenCaputre();
        private VideoControl videoControl;
        public ConfigurationItemViewModel ActiveConfiguration { get; set; }

		public ConfigurationWindow(ConfigurationWindowViewModel vm):base(vm)
		{
			BorderStyleKeyName = "ConfigurationWindowBorderStyle";
			InitializeComponent();
            MinHeight =  ActualHeight;//overide the MiniHeight set by window base
            MinimizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
            MaximizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;

            screenCapture.ScreenCaputreCancelled += screenCapture_ScreenCaputreCancelled;
            screenCapture.ScreenCaputred += screenCapture_ScreenCaputred;
            ActiveConfiguration = vm.CurrentConfigurationItemVM;
          
		}

        void screenCapture_ScreenCaputred(object sender, ScreenCaputredEventArgs e)
        {
            Show();
            var currentVm = ((ConfigurationWindowViewModel)DataContext).CurrentConfigurationItemVM.ConfigurationVM as PhotoSelectorViewModel;
            if (currentVm != null)
            {
                currentVm.PhotoSource = e.Bmp;
            }
        }

        void screenCapture_ScreenCaputreCancelled(object sender, EventArgs e)
        {
            Show();
            Focus();
        }

        void videoControl_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            switch (cmd)
            {
                case YoYoStudio.Controls.Winform.FlexCallbackCommand.LoadComplete:
                    var cameras = new List<string> { "" };
                    string[] cams = videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.GetCameras);
                    if (cams != null && cams.Length > 0)
                    {
                        cameras.AddRange(cams);
                    }
                    VideoConfigurationViewModel cvm = videoControl.DataContext as VideoConfigurationViewModel;
                    if (cvm != null)
                    {
                        cvm.Cameras = new System.Collections.ObjectModel.ObservableCollection<string>(cameras);
                    }
                    break;
            }
        }

		protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, ViewModel.ConfigurationWindowAction> message)
		{
            switch (message.Action)
            {
                case ConfigurationWindowAction.ConfigurationStateChanged:
                    ConfigurationItemViewModel vm = message.Content as ConfigurationItemViewModel;
                    if (vm != null)
                    {
                        var cvm = DataContext as ConfigurationWindowViewModel;
                        cvm.CurrentConfigurationItemVM = vm;
						VisualStateManager.GoToState(Configurations, vm.ConfigurationVM.Name, false);
						if (vm.ConfigurationVM.Name == vm.ApplicationVM.ProfileVM.VideoConfigurationVM.Name)
						{
							if (videoControl == null)
							{
								var pv = Configurations.Template.FindName("PART_Video", Configurations) as ContentControl;
								if (pv != null)
								{
									pv.ApplyTemplate();
									videoControl = pv.Template.FindName("videoControl", pv) as VideoControl;
									if (videoControl != null)
									{
										videoControl.FlashCallback += videoControl_FlashCallback;
									}
								}
							}
						}
                    }
                    break;
                case ConfigurationWindowAction.CameraIndexChanged:
                    if (videoControl != null)
                    {
                        int index = (int)message.Content;
                        if (index > 0)
                        {
                            videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.StartCamera, new string[] { (index - 1).ToString() });
                            videoControl.IsEnabled = true;
                        }
                        else
                        {
                            videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.CloseCamera);
                            videoControl.IsEnabled = false;
                        }
                    }
                    break;
                case ConfigurationWindowAction.LocalPhotoSelect:
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = Text.PhotoType;
                    if (dialog.ShowDialog() == true)
                    {
                        var currentVm = ((ConfigurationWindowViewModel)DataContext).CurrentConfigurationItemVM.ConfigurationVM as PhotoSelectorViewModel;
                        if (currentVm != null)
                        {
                            currentVm.PhotoSource = Utility.CreateBitmapSourceFromFile(dialog.FileName);
                        }
                    }
                    break;
                case ConfigurationWindowAction.CameraPhotoSelect:
					CameraWindowViewModel cameraVM = new CameraWindowViewModel();
					cameraVM.Initialize();
					CameraWindow cameraWindow = new CameraWindow(cameraVM);
                    cameraWindow.Show();
                    break;
                case ConfigurationWindowAction.StartScreenCapture:
                    screenCapture.StartCaputre(30,new Size(Owner.ActualWidth,Owner.ActualHeight));
                    break;
                case ConfigurationWindowAction.PasswordInvalid:
                    MessageBox.Show(Text.PasswordInvalid,Text.Error,MessageBoxButton.OK);
                    break;
                case ConfigurationWindowAction.VideoRefresh:
                    string[] cams = videoControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.GetCameras);
                    if (cams != null && cams.Length > 0)
                    {
                        VideoConfigurationViewModel cvm = videoControl.DataContext as VideoConfigurationViewModel;
                        if (cvm != null)
                        {
                            cvm.Cameras.Clear();
                            cams.Foreach(c => cvm.Cameras.Add(c));
                        }
                    }
                    break;
                default:
                    break;
            }            
		}

		public override void Dispose()
		{
			if (videoControl != null)
			{
				videoControl.Dispose();
			}
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ActiveConfiguration != null)
            {
                VisualStateManager.GoToState(Configurations, ActiveConfiguration.ConfigurationVM.Name, false);
            }
        }
	}
}
