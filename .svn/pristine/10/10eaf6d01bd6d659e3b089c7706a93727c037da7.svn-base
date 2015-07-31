using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using YoYoStudio.Client.ViewModel;
using System.Runtime.CompilerServices;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for PlayMusicWindow.xaml
    /// </summary>
    public partial class PlayMusicWindow 
    {
        PlayMusicWindowViewModel playMusicVM = null;
        public PlayMusicWindow(PlayMusicWindowViewModel vm):base(vm)
        {
            MinHeight = ActualHeight;//overide the MiniHeight set by window base
            MaximizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;
            //MinimizeButtonState = YoYoStudio.Controls.CustomWindow.WindowButtonState.Disabled;

            if (vm != null)
            {
                playMusicVM = vm;
                playMusicVM.Initialize();
            }
            DataContext = playMusicVM;
            InitializeComponent();
            musicControl.MoviePath = vm.MusicFlexPath;
            musicControl.FlashCallback += musicControl_FlashCallback;
            Closing += PlayMusicWindow_Closing;
            ShowInTaskbar = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (playMusicVM.RoomWindowVM.RoomClient.GetMusicPlayer(playMusicVM.RoomWindowVM.RoomVM.Id) == playMusicVM.Me.Id)
            {
                musicControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.StopMusic, "1");
                playMusicVM.RoomWindowVM.RoomClient.DownPlayMusic(playMusicVM.RoomWindowVM.RoomVM.Id);
            }
            else
            {
                musicControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.StopMusic, "0");
            }
            musicControl.FlashCallback -= musicControl_FlashCallback;
            musicControl.Dispose();
            
            base.OnClosed(e);
        }

        void PlayMusicWindow_Closing(object sender, CancelEventArgs e)
        {
            //string[] results = musicControl.CallFlash(YoYoStudio.Controls.Winform.FlexCommand.GetPlayStatus);
            //if (results.Length > 0)
            //{ 
            //    string result = results[0];
            //    if (result == YoYoStudio.Controls.Winform.FlexStatusStrings.MusicPlaying
            //        || result == YoYoStudio.Controls.Winform.FlexStatusStrings.MusicPaused)
            //    {
            //        MessageBoxResult choice = MessageBox.Show(Messages.MusicPlaying, Text.Prompt, MessageBoxButton.OKCancel);
            //        if (choice == MessageBoxResult.Cancel)
            //            e.Cancel = true;
            //    }
            //}
        }

        void musicControl_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            switch (cmd)
            {
                case YoYoStudio.Controls.Winform.FlexCallbackCommand.None:
                    break;
                case YoYoStudio.Controls.Winform.FlexCallbackCommand.ReportStatus:
                    break;
                case YoYoStudio.Controls.Winform.FlexCallbackCommand.LoadComplete:
                    break;
                default:
                    break;
            }

        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, PlayMusicWindowAction> message)
        {
            //switch (message.Action)
            //{ 
            //    case PlayMusicWindowAction.PlayMusic:
            //        break;
            //    case PlayMusicWindowAction.LoadMusicComplete:
            //        PART_Loading.Visibility = System.Windows.Visibility.Collapsed;
            //        PART_Content.Visibility = System.Windows.Visibility.Visible;
            //        playBtn.IsEnabled = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        //private void playBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (playMusicVM != null && playMusicVM.SelectedMusic != null)
        //    {
        //        //to do play music
        //        DialogResult = true;
        //        Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("请先选择歌曲。");
        //    }
        //}
    }
}
