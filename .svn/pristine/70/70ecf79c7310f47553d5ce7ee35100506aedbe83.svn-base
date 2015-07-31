using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Client.ViewModel
{
    public partial class RoomWindowViewModel
    {
        protected override void InitializeCommand()
        {
            base.InitializeCommand();
            OnMicCommandVM = new CommandViewModel(BuiltIns.OnMicCommand, this,OnMicCommandExecute);
            ConfigCommand = new SecureCommand(ConfigCommandExecute);
            PlayMusicCommand = new SecureCommand(PlayMusicCommandExecute, CanPlayMusicCommandExecute);
            ManageMusicCommand = new SecureCommand(ManageMusicCommandExecute, CanManageMusicCommandExecute);
            //RecordCommand = new SecureCommand(RecordCommandExecute);
            //PlayCommand = new SecureCommand(PlayCommandExecute);
            //StopCommand = new SecureCommand(StopCommandExecute);
        }

        #region OnMicCommand

        public CommandViewModel OnMicCommandVM { get; set; }

        private void OnMicCommandExecute(SecureCommandArgs args)
        {
            if (args != null)
            {
                MicType micType = (MicType)args.Content;
                if (Me.MicStatus == MicStatusMessage.MicStatus_Off)
                {
                    RoomClient.OnMic(RoomVM.Id, micType, -1);
                }
                else
                {
                    switch (micType)
                    {
                        case MicType.Private:
                            PrivateMicUserVMs.Remove(Me);
                            break;
                        case MicType.Secret:
                            SecretMicUserVMs.Remove(Me);
                            break;
                        case MicType.Public:
                            switch (Me.MicIndex)
                            {
                                case 0:
                                    FirstMicUserVM = null;
                                    break;
                                case 1:
                                    SecondMicUserVM = null;
                                    break;
                                case 2:
                                    ThirdMicUserVM = null;
                                    break;
                            }
                            break;
                    }
                    RoomClient.DownMic(RoomVM.Id, micType, Me.MicIndex);
                    Me.DownMic();
                    updateMicImage(Me.Id, false);// update the local image manually as the downmic meesage will not be received by itself.
                }
            }
        }

        #endregion

        #region Configuration Command
        public SecureCommand ConfigCommand { get; set; }
        private void ConfigCommandExecute(SecureCommandArgs args)
        {
            if (args != null)
            {
                ConfigurationItemViewModel vm = null;
                ConfigType type = (ConfigType)args.Content;
                switch (type)
                {
                    case ConfigType.Audio:
                        vm = new ConfigurationItemViewModel(ApplicationVM.ProfileVM.AudioConfigurationVM);
                        break;
                    case ConfigType.Vedio:
                        vm = new ConfigurationItemViewModel(ApplicationVM.ProfileVM.VideoConfigurationVM);
                        break;
                    case ConfigType.Personal:
                        break;
                    default:
                        break;
                }
                if (vm != null)
                {
                    Messenger.Default.Send<EnumNotificationMessage<object, RoomWindowAction>>(new EnumNotificationMessage<object, RoomWindowAction>(RoomWindowAction.ShowConfigWindow, vm));
                }
            }
        }
        #endregion

        #region Music Command
        public SecureCommand PlayMusicCommand { get; set; }
        private void PlayMusicCommandExecute(SecureCommandArgs args)
        {
            bool canPlay = RoomClient.OnPlayMusic(RoomVM.Id, Me.Id);
            Messenger.Default.Send<EnumNotificationMessage<object, RoomWindowAction>>(new EnumNotificationMessage<object, RoomWindowAction>(RoomWindowAction.PlayMusic,canPlay));
        }
        private bool CanPlayMusicCommandExecute(SecureCommandArgs args)
        {
            return true;
        }

        public SecureCommand ManageMusicCommand { get; set; }
        private void ManageMusicCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, RoomWindowAction>>(new EnumNotificationMessage<object, RoomWindowAction>(RoomWindowAction.ManageMusic));
        }
        private bool CanManageMusicCommandExecute(SecureCommandArgs args)
        {
            return true;
        }

        #endregion

        public SecureCommand BroadcastCommand { get; set; }
        private void BroadcastCommandExecute(SecureCommandArgs args)
        {
            StartAudioRecording();
        }

        public SecureCommand RecordCommand { get; set; }
        private void RecordCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, RoomWindowAction>>(new EnumNotificationMessage<object, RoomWindowAction>(RoomWindowAction.RecordAudio));
        }
        public SecureCommand PlayCommand { get; set; }
        private void PlayCommandExecute(SecureCommandArgs args)
        {
            playAll = true;
        }

        public SecureCommand StopCommand { get; set; }
        private void StopCommandExecute(SecureCommandArgs args)
        {
            StopAudioRecording();
            StopAllAudioPlaying();
            playAll = false;
        }
    }
}
