using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Media;

namespace YoYoStudio.RoomService.Library
{
    [ServiceContract(CallbackContract = (typeof(IRoomServiceCallback)), SessionMode = SessionMode.Required)]
    public interface IRoomService
    {
        [OperationContract(IsInitiating = true, IsOneWay = false)]
        int GetOnlineUserCount(int roomId);
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void KeepAlive();
        [OperationContract(IsInitiating = true, IsOneWay = false)]
        bool EnterRoom(int roomId, User user);
        [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = true)]
        void LeaveRoom(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        List<User> GetRoomUsers(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void SendRoomMessage(int roomId, RoomMessage message);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void OnMic(int roomId, MicType micType, int suggestedIndex);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void DownMic(int roomId, MicType micType, int index);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void ToggleAudio(int roomId, MicType micType);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void ToggleVideo(int roomId, MicType micType);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        Dictionary<int, MicStatusMessage> GetMicUsers(int roomId, MicType micType);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        RoomMessage GetRoomMessage(RoomMessageType msgType);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        List<MicStatusMessage> GetMicQueue(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void SendGift(int roomId, int receiverId, int giftId, int count);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        bool ExecuteCommand(int roomId, int cmdId, int targetUserId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void VideoStateChanged(int roomId, int state);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void AudioStateChanged(int roomId, int state);
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void AudioServiceLogin(string ip, int port);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        string GetAudioServiceIp();
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        int GetAudioServicePort();
        [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
        void AudioServiceLogOff();
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        bool ScoreExchange(int userId, int scoreToExchange, int moneyToGet);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        User GetUser(int userId);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        bool OnPlayMusic(int roomId, int userId);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        int GetMusicPlayer(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void DownPlayMusic(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void StartMusic(int roomId, int userId, string fileName);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void StopMusic(int roomId, int userId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void TogglePauseMusic(int roomId,int userId,bool paused);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void SetPlayPosition(int roomId, int userId, int pos);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void SetMusicVolume(int roomId, int userId, int volume);
        [OperationContract(IsInitiating = false, IsOneWay = false)]
        MusicStatus GetMusicStatus(int roomId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void RequestMusicStatus(int roomId, int userId);
        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void UpadateMusicStatus(int roomId, int userId, MusicStatus status, int targetUserId);
    }

    public interface IRoomServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserEnteredRoom(int roomId, User user);
        [OperationContract(IsOneWay = true)]
        void UserLeftRoom(int roomId, int userId);
        [OperationContract(IsOneWay = true)]
        void RoomMessageReceived(int roomId, RoomMessage message);
        [OperationContract(IsOneWay = true)]
        void MicStatusMessageReceived(int roomId, MicStatusMessage message);
        [OperationContract(IsOneWay = true)]
        void CommandMessageReceived(int roomId, int cmdId, int sourceUserId, int targetUserId);
        [OperationContract(IsOneWay = true)]
        void VideoStateChanged(int roomId, int senderId, int state);
        [OperationContract(IsOneWay = true)]
        void AudioStateChanged(int roomId, int senderId, int state);
        [OperationContract(IsOneWay = true)]
        void StartMusic(int roomId, int userId, string fileName);
        [OperationContract(IsOneWay = true)]
        void StopMusic(int roomId, int userId);
        [OperationContract(IsOneWay = true)]
        void TogglePauseMusic(int roomId, int userId,bool paused);
        [OperationContract(IsOneWay = true)]
        void SetPlayPosition(int roomId, int userId, int pos);
        [OperationContract(IsOneWay = true)]
        void SetMusicVolume(int roomId, int userId, int volume);
        [OperationContract(IsOneWay = true)]
        void ReportMusicStatus(int roomId, int requireUserId);
        [OperationContract(IsOneWay = true)]
        void UpdateMusicStatus(int roomId, MusicStatus status);
    }
}
