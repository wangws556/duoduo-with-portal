using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Media;

namespace YoYoStudio.RoomService.Client
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IRoomService", CallbackContract = typeof(IRoomServiceCallback), SessionMode = System.ServiceModel.SessionMode.Required)]
	public interface IRoomService
	{

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IRoomService/GetOnlineUserCount", ReplyAction = "http://tempuri.org/IRoomService/GetOnlineUserCountResponse")]
		int GetOnlineUserCount(int roomId);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/KeepAlive")]
		void KeepAlive();

		[System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IRoomService/EnterRoom", ReplyAction = "http://tempuri.org/IRoomService/EnterRoomResponse")]
		bool EnterRoom(int roomId, YoYoStudio.Model.Core.User user);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsTerminating = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/LeaveRoom")]
		void LeaveRoom(int roomId);

		[System.ServiceModel.OperationContractAttribute(IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetRoomUsers", ReplyAction = "http://tempuri.org/IRoomService/GetRoomUsersResponse")]
		YoYoStudio.Model.Core.User[] GetRoomUsers(int roomId);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/SendRoomMessage")]
		void SendRoomMessage(int roomId, YoYoStudio.Model.Chat.RoomMessage message);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/OnMic")]
		void OnMic(int roomId, YoYoStudio.Model.Chat.MicType micType, int suggestedIndex);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/DownMic")]
		void DownMic(int roomId, YoYoStudio.Model.Chat.MicType micType, int index);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/ToggleAudio")]
		void ToggleAudio(int roomId, YoYoStudio.Model.Chat.MicType micType);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/ToggleVideo")]
		void ToggleVideo(int roomId, YoYoStudio.Model.Chat.MicType micType);

		[System.ServiceModel.OperationContractAttribute(IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetMicUsers", ReplyAction = "http://tempuri.org/IRoomService/GetMicUsersResponse")]
		System.Collections.Generic.Dictionary<int, YoYoStudio.Model.Chat.MicStatusMessage> GetMicUsers(int roomId, YoYoStudio.Model.Chat.MicType micType);

        [System.ServiceModel.OperationContractAttribute(IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetRoomMessage", ReplyAction = "http://tempuri.org/IRoomService/GetRoomMessageResponse")]
        YoYoStudio.Model.Chat.RoomMessage GetRoomMessage(YoYoStudio.Model.Chat.RoomMessageType msgType);

		[System.ServiceModel.OperationContractAttribute(IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetMicQueue", ReplyAction = "http://tempuri.org/IRoomService/GetMicQueueResponse")]
		YoYoStudio.Model.Chat.MicStatusMessage[] GetMicQueue(int roomId);
       
        [System.ServiceModel.OperationContractAttribute(IsInitiating = false, IsOneWay = false, Action = "http://tempuri.org/IRoomService/ExecuteCommand", ReplyAction = "http://tempuri.org/IRoomService/ExecuteCommandResponse")]
        bool ExecuteCommand(int roomId, int cmdId, int targetUserId);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/VideoStateChanged")]
        void VideoStateChanged(int roomId, int state);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/AudioStateChanged")]
        void AudioStateChanged(int roomId, int state);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/SendGift")]
        void SendGift(int roomId, int receiverId, int giftId, int count);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = true, Action = "http://tempuri.org/IRoomService/AudioServiceLogin")]
        void AudioServiceLogin(string ip, int port);
        [System.ServiceModel.OperationContract(IsInitiating = false, IsOneWay = false, Action = "http://tempuri.org/IRoomService/GetAudioServiceIp")]
        string GetAudioServiceIp();
        [System.ServiceModel.OperationContract(IsInitiating = false, IsOneWay = false, Action = "http://tempuri.org/IRoomService/GetAudioServicePort")]
        int GetAudioServicePort();
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/AudioServiceLogOff")]
        void AudioServiceLogOff();
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, IsInitiating = false, Action = "http://tempuri.org/IRoomService/ScoreExchange")]
        bool ScoreExchange(int userId, int scoreToExchange, int moneyToGet);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetUser")]
        YoYoStudio.Model.Core.User GetUser(int userId);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, IsInitiating = false, Action = "http://tempuri.org/IRoomService/OnPlayMusic")]
        bool OnPlayMusic(int roomId,int userId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetMusicPlayer")]
        int GetMusicPlayer(int roomId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, IsInitiating = false, Action = "http://tempuri.org/IRoomService/GetMusicStatus")]
        MusicStatus GetMusicStatus(int roomId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/DownPlayMusic")]
        void DownPlayMusic(int roomId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/StartMusic")]
        void StartMusic(int roomId, int userId,string fileName);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/StopMusic")]
        void StopMusic(int roomId, int userId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/TogglePauseMusic")]
        void TogglePauseMusic(int roomId, int userId,bool paused);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/SetPlayPosition")]
        void SetPlayPosition(int roomId, int userId,int pos);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/SetMusicVolume")]
        void SetMusicVolume(int roomId, int userId, int volume);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/RequestMusicStatus")]
        void RequestMusicStatus(int roomId, int userId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, IsInitiating = false, Action = "http://tempuri.org/IRoomService/UpadateMusicStatus")]
        void UpadateMusicStatus(int roomId, int userId, MusicStatus status, int targetUserId);
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface IRoomServiceCallback
	{

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/UserEnteredRoom")]
		void UserEnteredRoom(int roomId, YoYoStudio.Model.Core.User user);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/UserLeftRoom")]
		void UserLeftRoom(int roomId, int userId);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/RoomMessageReceived")]
		void RoomMessageReceived(int roomId, YoYoStudio.Model.Chat.RoomMessage message);

		[System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/MicStatusMessageReceived")]
		void MicStatusMessageReceived(int roomId, YoYoStudio.Model.Chat.MicStatusMessage message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/CommandMessageReceived")]
        void CommandMessageReceived(int roomId, int cmdId, int sourceUserId, int targetUserId);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/VideoStateChanged")]
        void VideoStateChanged(int roomId, int senderId, int state);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/AudioStateChanged")]
        void AudioStateChanged(int roomId, int senderId, int state);

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true,  Action = "http://tempuri.org/IRoomService/StartMusic")]
        void StartMusic(int roomId, int userId, string fileName);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/StopMusic")]
        void StopMusic(int roomId, int userId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/TogglePauseMusic")]
        void TogglePauseMusic(int roomId, int userId, bool paused);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/SetPlayPosition")]
        void SetPlayPosition(int roomId, int userId, int pos);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/SetMusicVolume")]
        void SetMusicVolume(int roomId, int userId, int volume);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/ReportMusicStatus")]
        void ReportMusicStatus(int roomId, int requireUserId);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IRoomService/UpdateMusicStatus")]
        void UpdateMusicStatus(int roomId, MusicStatus status);
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface IRoomServiceChannel : IRoomService, System.ServiceModel.IClientChannel
	{
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public partial class RoomServiceClient : System.ServiceModel.DuplexClientBase<IRoomService>, IRoomService
	{

		public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance) :
			base(callbackInstance)
		{
		}

		public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
			base(callbackInstance, endpointConfigurationName)
		{
		}

		public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public RoomServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, binding, remoteAddress)
		{
		}

		public int GetOnlineUserCount(int roomId)
		{
			return base.Channel.GetOnlineUserCount(roomId);
		}

		public void KeepAlive()
		{
			base.Channel.KeepAlive();
		}

		public bool EnterRoom(int roomId, YoYoStudio.Model.Core.User user)
		{
			return base.Channel.EnterRoom(roomId, user);
		}

		public void LeaveRoom(int roomId)
		{
			base.Channel.LeaveRoom(roomId);
		}

		public YoYoStudio.Model.Core.User[] GetRoomUsers(int roomId)
		{
			return base.Channel.GetRoomUsers(roomId);
		}

		public void SendRoomMessage(int roomId, YoYoStudio.Model.Chat.RoomMessage message)
		{
			base.Channel.SendRoomMessage(roomId, message);
		}

		public void OnMic(int roomId, YoYoStudio.Model.Chat.MicType micType, int suggestedIndex)
		{
			base.Channel.OnMic(roomId, micType, suggestedIndex);
		}

		public void DownMic(int roomId, YoYoStudio.Model.Chat.MicType micType, int index)
		{
			base.Channel.DownMic(roomId, micType, index);
		}

		public void ToggleAudio(int roomId, YoYoStudio.Model.Chat.MicType micType)
		{
			base.Channel.ToggleAudio(roomId, micType);
		}

		public void ToggleVideo(int roomId, YoYoStudio.Model.Chat.MicType micType)
		{
			base.Channel.ToggleVideo(roomId, micType);
		}

		public System.Collections.Generic.Dictionary<int, YoYoStudio.Model.Chat.MicStatusMessage> GetMicUsers(int roomId, YoYoStudio.Model.Chat.MicType micType)
		{
			return base.Channel.GetMicUsers(roomId, micType);
		}

        public YoYoStudio.Model.Chat.RoomMessage GetRoomMessage(YoYoStudio.Model.Chat.RoomMessageType msgType)
		{
            return base.Channel.GetRoomMessage(msgType);
		}

		public YoYoStudio.Model.Chat.MicStatusMessage[] GetMicQueue(int roomId)
		{
			return base.Channel.GetMicQueue(roomId);
		}

        public bool ExecuteCommand(int roomId, int cmdId, int targetUserId)
        {
            return base.Channel.ExecuteCommand(roomId, cmdId, targetUserId);
        }

        public void VideoStateChanged(int roomId, int state)
        {
            base.Channel.VideoStateChanged(roomId, state);
        }

        public void AudioStateChanged(int roomId, int state)
        {
            base.Channel.AudioStateChanged(roomId, state);
        }


        public void SendGift(int roomId, int receiverId, int giftId, int count)
        {
            base.Channel.SendGift(roomId, receiverId, giftId, count);
        }


        public void AudioServiceLogin(string ip, int port)
        {
            base.Channel.AudioServiceLogin(ip,port);
        }


        public void AudioServiceLogOff()
        {
            base.Channel.AudioServiceLogOff();
        }


        public string GetAudioServiceIp()
        {
            return base.Channel.GetAudioServiceIp();
        }

        public int GetAudioServicePort()
        {
            return base.Channel.GetAudioServicePort();
        }
        public bool ScoreExchange(int userId, int scoreToExchange, int moneyToGet)
        {
            return base.Channel.ScoreExchange(userId, scoreToExchange, moneyToGet);
        }
        public YoYoStudio.Model.Core.User GetUser(int userId)
        {
            return base.Channel.GetUser(userId);
        }

        public bool OnPlayMusic(int roomId, int userId)
        {
            return base.Channel.OnPlayMusic(roomId, userId);
        }

        public int GetMusicPlayer(int roomId)
        {
            return base.Channel.GetMusicPlayer(roomId);
        }

        public MusicStatus GetMusicStatus(int roomId)
        {
            return base.Channel.GetMusicStatus(roomId);
        }

        public void DownPlayMusic(int roomId)
        {
            base.Channel.DownPlayMusic(roomId);
        }

        public void StartMusic(int roomId, int userId, string fileName)
        {
            base.Channel.StartMusic(roomId, userId, fileName);
        }

        public void StopMusic(int roomId, int userId)
        {
            base.Channel.StopMusic(roomId, userId);
        }

        public void TogglePauseMusic(int roomId, int userId, bool paused)
        {
            base.Channel.TogglePauseMusic(roomId, userId, paused);
        }

        public void SetPlayPosition(int roomId, int userId, int pos)
        {
            base.Channel.SetPlayPosition(roomId, userId, pos);
        }

        public void SetMusicVolume(int roomId, int userId, int volume)
        {
            base.Channel.SetMusicVolume(roomId, userId, volume);
        }
        public void RequestMusicStatus(int roomId, int userId)
        {
            base.Channel.RequestMusicStatus(roomId, userId);
        }
        public void UpadateMusicStatus(int roomId, int userId, MusicStatus status, int targetUserId)
        {
            base.Channel.UpadateMusicStatus(roomId, userId, status,targetUserId);
        }
    }

}
