using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Media;

namespace YoYoStudio.RoomService.Client
{
    public class RoomServiceCallback :IRoomServiceCallback
    {
        public event Action<int, User> UserEnteredRoomEvent;
        public event Action<int, int> UserLeftRoomEvent;
        public event Action<int, RoomMessage> RoomMessageReceivedEvent;
        public event Action<int, MicStatusMessage> MicStatusMessageReceivedEvent;
        public event Action<int, int, int, int> CommandReceivedEvent;
        public event Action<int, int, int> VideoStateChangedEvent;
        public event Action<int, int, int> AudioStateChangedEvent;
        public event Action<int, int, string> StartMusicEvent;
        public event Action<int, int> StopMusicEvent;
        public event Action<int, int,bool > TogglePauseMusicEvent;
        public event Action<int, int, int> SetPlayPositionEvent;
        public event Action<int, int, int> SetMusicVolumeEvent;
        public event Action<int,int> ReportMusicStatusEvent;
        public event Action<int, MusicStatus> UpdateMusicStatusEvent;

        public void UserEnteredRoom(int roomId, Model.Core.User user)
        {
            if (UserEnteredRoomEvent != null)
            {
                UserEnteredRoomEvent(roomId, user);
            }
        }

        public void UserLeftRoom(int roomId, int userId)
        {
            if (UserLeftRoomEvent != null)
            {
                UserLeftRoomEvent(roomId, userId);
            }
        }

        public void RoomMessageReceived(int roomId, Model.Chat.RoomMessage message)
        {
            if (RoomMessageReceivedEvent != null)
            {
                RoomMessageReceivedEvent(roomId, message);
            }
        }

        public void MicStatusMessageReceived(int roomId, Model.Chat.MicStatusMessage message)
        {
            if (MicStatusMessageReceivedEvent != null)
            {
                MicStatusMessageReceivedEvent(roomId, message);
            }
        }


        public void CommandMessageReceived(int roomId, int cmdId, int sourceUserId, int targetUserId)
        {
            if (CommandReceivedEvent != null)
            {
                CommandReceivedEvent(roomId, cmdId, sourceUserId, targetUserId);
            }
        }

        public void VideoStateChanged(int roomId, int senderId, int state)
        {
            if (VideoStateChangedEvent != null)
            {
                VideoStateChangedEvent(roomId, senderId, state);
            }
        }

        public void AudioStateChanged(int roomId, int senderId, int state)
        {
            if (AudioStateChangedEvent != null)
            {
                AudioStateChangedEvent(roomId, senderId, state);
            }
        }
        public void StartMusic(int roomId, int userId, string fileName)
        {
            if (StartMusicEvent != null)
            {
                StartMusicEvent(roomId, userId, fileName);
            }
        }
        public void StopMusic(int roomId, int userId)
        {
            if (StopMusicEvent != null)
            {
                StopMusicEvent(roomId, userId);
            }
        }
        public void TogglePauseMusic(int roomId, int userId,bool paused)
        {
            if (TogglePauseMusicEvent != null)
            {
                TogglePauseMusicEvent(roomId, userId, paused);
            }
        }

        public void SetPlayPosition(int roomId, int userId, int pos)
        {
            if (SetPlayPositionEvent != null)
            {
                SetPlayPositionEvent(roomId, userId, pos);
            }
        }

        public void SetMusicVolume(int roomId, int userId, int volume)
        {
            if (SetMusicVolumeEvent != null)
            {
                SetMusicVolumeEvent(roomId, userId, volume);
            }
        }

        public void ReportMusicStatus(int roomId, int requestUserId)
        {
            if (ReportMusicStatusEvent != null)
            {
                ReportMusicStatusEvent(roomId, requestUserId);
            }
        }

        public void UpdateMusicStatus(int roomId, MusicStatus status)
        {
            if (UpdateMusicStatusEvent != null)
            {
                UpdateMusicStatusEvent(roomId, status);
            }
        }
    }
}
