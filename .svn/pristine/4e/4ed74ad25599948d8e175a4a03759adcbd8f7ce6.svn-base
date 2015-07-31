using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Model.Chat
{
    public enum RoomMessageType
    {
        PublicChatMessage,
        PrivateChatMessage,
        StampMessage,
        GiftMessage,
        AnnoucementMessage,
        HornMessage,
        HallHornMessage,
        GlobalHornMessage
    }

    public enum MessageResult
    {
        Succeed,
        UnkownError,
        NotEnoughMoney,
        NotEnoughPrivilege
    }

	public enum SendGiftResult
	{
		Succeed,
		NotEnoughMoney,
		CannotSendGift,
		CannotReceiveGift,
		UnkownError
	}

    [Serializable]
    [DataContract]
    public class RoomMessage : Message
    {
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int SenderId { get; set; }
        [DataMember]
        public int ReceiverId { get; set; }
        [DataMember]
        public RoomMessageType MessageType { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public bool IsScroll { get; set; }
        [DataMember]
        public bool IsHorn { get; set; }
        [DataMember]
        public List<MotionImagesMessage> MotionMessages { get; set; }
		[DataMember]
		public SendGiftResult GiftResult { get; set; }
        [DataMember]
        public MessageResult MsgResult { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MotionImagesMessage : Message
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte[] Bytes { get; set; }
        [DataMember]
        public int Offset { get; set; }
        [DataMember]
        public bool IsCustomizedImage { get; set; }
        [DataMember]
        public string Path { get; set; }
    }

    public enum MicType
    {
        None,
        Public,
        Private,
        Secret,
        Max
    }

    public enum MicAction
    {
        None,
        OnMic,
        Toggle,
        DownMic
    }

    public enum ConfigType
    { 
        Audio,
        Vedio,
        Personal,
        Security
    }

    [Serializable]
    [DataContract]
    public class MicStatusMessage : Message
    {
        public const int MicStatus_Off = 0;
        //MicQueue
        public const int MicStatus_Queue = 1;
        //On Mic, but both Video and Audio are off
        public const int MicStatus_On = MicStatus_Queue << 1;
        //Video is on, Audio is off
        public const int MicStatus_Video = MicStatus_On << 1;
        //Audio is on, Video is off
        public const int MicStatus_Audio = MicStatus_Video << 1;
        //Both Video and Audio are on
        public const int MicStatus_VideoAudio = MicStatus_Video | MicStatus_Audio;

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int MicStatus { get; set; }
        [DataMember]
        public int MicIndex { get; set; }
        [DataMember]
        public string StreamGuid { get; set; }
        [DataMember]
        public MicType MicType { get; set; }
        [DataMember]
        public MicAction MicAction { get; set; }

        public MicStatusMessage()
        {
            Reset();
        }
        public void Reset()
        {
            lock (this)
            {
                MicStatus = MicStatus_Off;
                UserId = -1;
                MicAction = Chat.MicAction.None;
                MicType = Chat.MicType.None;
            }
        }
    }
}
