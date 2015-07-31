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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Controls.Winform;
using YoYoStudio.Model.Media;

namespace YoYoStudio.Client.Chat.Controls
{
    /// <summary>
    /// Interaction logic for MusicControl.xaml
    /// </summary>
    public partial class MusicControl : UserControl,IDisposable
    {
        public string MoviePath
        {
            get { return (string)GetValue(MoviePathProperty); }
            set { SetValue(MoviePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MoviePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoviePathProperty =
            DependencyProperty.Register("MoviePath", typeof(string), typeof(MusicControl), new PropertyMetadata(string.Empty, (o, e) =>
            {
                MusicControl mc = o as MusicControl;
                if (mc != null && e.NewValue != null)
                {
                    mc.flex.MoviePath = e.NewValue.ToString();
                    mc.flex.LoadFlex();
                }
            }));

        public event FlashCallbackEventHandler FlashCallback;

        public MusicControl()
        {
            InitializeComponent();
            
        }

        private void flex_FlashCallback(YoYoStudio.Controls.Winform.FlexCallbackCommand cmd, List<string> args)
        {
            PlayMusicWindowViewModel vm = DataContext as PlayMusicWindowViewModel;
            switch (cmd)
            {
                case FlexCallbackCommand.None:
                    break;
                case FlexCallbackCommand.ReportStatus:
                    if (args != null && args.Count == 1)
                    {
                        if (args[0] == FlexStatusStrings.ConnectSucceed)
                        { 
                            //the connection has been setup with Red5
                            if (vm != null)
                            {
                                vm.RoomWindowVM.RoomCallback.StartMusicEvent += RoomCallback_StartMusicEvent;
                                vm.RoomWindowVM.RoomCallback.TogglePauseMusicEvent += RoomCallback_TogglePauseMusicEvent;
                                vm.RoomWindowVM.RoomCallback.SetMusicVolumeEvent += RoomCallback_SetMusicVolumeEvent;
                                vm.RoomWindowVM.RoomCallback.SetPlayPositionEvent += RoomCallback_SetPlayPositionEvent;
                                vm.RoomWindowVM.RoomCallback.StopMusicEvent += RoomCallback_StopMusicEvent;
                                vm.RoomWindowVM.RoomCallback.ReportMusicStatusEvent += RoomCallback_ReportMusicStatusEvent;
                                vm.RoomWindowVM.RoomCallback.UpdateMusicStatusEvent += RoomCallback_UpdateMusicStatusEvent;
                            }
                        }
                    }
                    break;
                case FlexCallbackCommand.LoadComplete:
                    //flex control has been loaded. Next we will connect the Rmtp and load the musics
                    
                    if (vm != null)
                    {
                        CallFlash(FlexCommand.ConnectRTMP, vm.MusicRtmpUrl);
                        CallFlash(FlexCommand.LoadMusics);
                        SynchronizePlayStatus();
                    }
                    break;
                case FlexCallbackCommand.PlayMusic:
                    vm.RoomWindowVM.RoomClient.StartMusic(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id, args[0]);
                    break;
                case FlexCallbackCommand.StopMusic:
                    vm.RoomWindowVM.RoomClient.StopMusic(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id);
                    break;
                case FlexCallbackCommand.SetPlayPosition:
                    //vm.RoomWindowVM.RoomClient.SetPlayPosition(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id, int.Parse(args[0]));
                    break;
                case FlexCallbackCommand.SetVolume:
                    //vm.RoomWindowVM.RoomClient.SetMusicVolume(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id, int.Parse(args[0]));
                    break;
                case FlexCallbackCommand.TogglePauseMusic:
                    //if (args != null && args.Count == 1)
                    //{
                    //    if (args[0] == FlexStatusStrings.MusicPaused)
                    //    {
                    //        vm.RoomWindowVM.RoomClient.TogglePauseMusic(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id, true);
                    //    }
                    //    else
                    //    {
                    //        vm.RoomWindowVM.RoomClient.TogglePauseMusic(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id,false);
                    //    }
                    //}
                    break;
                default:
                    break;
            }

            if (FlashCallback != null)
            {
                FlashCallback(cmd, args);
            }
        }

        void RoomCallback_UpdateMusicStatusEvent(int arg1, MusicStatus arg2)
        {
            if (arg2 != null)
            {
                flex.CallFlash(FlexCommand.PlayMusic, arg2.Name);
                //flex.CallFlash(FlexCommand.SetPlayPosition, arg2.Position.ToString());
                //if (arg2.Status == PlayStatus.Paused)
                //{
                //    flex.CallFlash(FlexCommand.TogglePauseMusic);
                //}
                if (arg2.Status == PlayStatus.Stoped)
                {
                    flex.CallFlash(FlexCommand.StopMusic, "false");
                }
            }
        }

        void RoomCallback_ReportMusicStatusEvent(int arg1, int arg2)
        {
            Dispatcher.BeginInvoke((Action)(() =>
                {
                    PlayMusicWindowViewModel vm = DataContext as PlayMusicWindowViewModel;
                    string[] result = flex.CallFlash(FlexCommand.GetPlayStatus);
                    if (result != null)
                    {
                        var playStatus = flex.CallFlash(FlexCommand.GetPlayStatus).ToList();
                        if (playStatus.Count == 2)
                        {
                            MusicStatus status = new MusicStatus();
                            status.PlayerId = vm.RoomWindowVM.Me.Id;
                            status.Name = playStatus[0];
                            status.Status = (PlayStatus)Enum.Parse(typeof(PlayStatus), playStatus[1]);
                            //status.Position = (int)(Decimal.Parse(playStatus[2]))+1;
                            vm.RoomWindowVM.RoomClient.UpadateMusicStatus(vm.RoomWindowVM.RoomVM.Id, vm.RoomWindowVM.Me.Id, status, arg2);
                        }
                    }
                }));
        }

        private void SynchronizePlayStatus()
        {
            Dispatcher.BeginInvoke((Action)(() =>
                {
                    PlayMusicWindowViewModel vm = DataContext as PlayMusicWindowViewModel;
                    MusicStatus musicStatus = vm.RoomWindowVM.RoomClient.GetMusicStatus(vm.RoomWindowVM.RoomVM.Id);
                    if (musicStatus != null)//someone is playing music
                    {
                        //ask server to get latest play status
                        vm.RoomWindowVM.RoomClient.RequestMusicStatus(vm.RoomWindowVM.RoomVM.Id, vm.Me.Id);

                    }
                }));
        }

        void RoomCallback_StopMusicEvent(int arg1, int arg2)
        {
            flex.CallFlash(FlexCommand.StopMusic, "false");
        }

        void RoomCallback_SetPlayPositionEvent(int arg1, int arg2, int arg3)
        {
            //flex.CallFlash(FlexCommand.SetPlayPosition, arg3.ToString());
        }

        void RoomCallback_SetMusicVolumeEvent(int arg1, int arg2, int arg3)
        {
            //flex.CallFlash(FlexCommand.SetVolume, arg3.ToString());
        }

        void RoomCallback_TogglePauseMusicEvent(int arg1, int arg2, bool arg3)
        {
            //flex.CallFlash(FlexCommand.TogglePauseMusic);
        }

        void RoomCallback_StartMusicEvent(int arg1, int arg2, string arg3)
        {
            flex.CallFlash(FlexCommand.PlayMusic, arg3.ToString());
        }

        public string[] CallFlash(FlexCommand cmd, params string[] args)
        {
            return flex.CallFlash(cmd, args);
        }

        public void Dispose()
        {
            Dispatcher.BeginInvoke((Action)(() =>
                {
                    PlayMusicWindowViewModel vm = DataContext as PlayMusicWindowViewModel;
                    if (vm != null)
                    {
                        vm.RoomWindowVM.RoomCallback.StartMusicEvent -= RoomCallback_StartMusicEvent;
                        vm.RoomWindowVM.RoomCallback.TogglePauseMusicEvent -= RoomCallback_TogglePauseMusicEvent;
                        vm.RoomWindowVM.RoomCallback.SetMusicVolumeEvent -= RoomCallback_SetMusicVolumeEvent;
                        vm.RoomWindowVM.RoomCallback.SetPlayPositionEvent -= RoomCallback_SetPlayPositionEvent;
                        vm.RoomWindowVM.RoomCallback.ReportMusicStatusEvent -= RoomCallback_ReportMusicStatusEvent;
                        vm.RoomWindowVM.RoomCallback.UpdateMusicStatusEvent -= RoomCallback_UpdateMusicStatusEvent;
                    }
                }));
            flex.Dispose();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
