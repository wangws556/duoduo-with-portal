using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using YoYoStudio.ChatService.Client;
using YoYoStudio.Common;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model;
using log4net;
using YoYoStudio.Model.Media;

namespace YoYoStudio.RoomService.Library
{
    internal class UserNCallback
    {
        public User User { get; set; }
        public IRoomServiceCallback Callback { get; set; }
        public int RoomId { get; set; }
		public UserApplicationInfo UserInfo { get; set; }
    }

    public partial class RoomService
    {
        static string audioServiceIp = string.Empty;
        static int audioServicePort = 0;
		static int applicationId = BuiltIns._9258ChatApplication.Id;
        static ChatServiceCache cache = new ChatServiceCache();
        static ChatServiceCallback callback = new ChatServiceCallback();
        static ChatServiceClient client = new ChatServiceClient(callback);
        //roomId, userId, 
        static SafeDictionary<int, SafeDictionary<int, UserNCallback>> userCache = new SafeDictionary<int, SafeDictionary<int, UserNCallback>>();
        static SafeDictionary<int, SafeDictionary<MicType, SafeDictionary<int, MicStatusMessage>>> micCache = new SafeDictionary<int, SafeDictionary<MicType, SafeDictionary<int, MicStatusMessage>>>();
        static SafeDictionary<int, SafeList<MicStatusMessage>> micQueue = new SafeDictionary<int, SafeList<MicStatusMessage>>();

        //roomId, RunWay message
        static RoomMessage RoomsPermanentMsg = new RoomMessage();

        //roomId, userId for music play
        static SafeDictionary<int, MusicStatus> musicCache = new SafeDictionary<int, MusicStatus>();

        static IRoomServiceCallback audioServiceCallback;
        //static Dictionary<int, object> userCacheLocker = new Dictionary<int, object>();
        //static Dictionary<int, object> micCacheLocker = new Dictionary<int, object>();

        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static Timer timer = new Timer(TimeSpan.FromSeconds(40).TotalMilliseconds);
        
        public static void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            cache.RefreshCache(null);
            cache.BuildRelationship();
            if (client.RoomLogin())
            {
                var rooms = cache.Rooms;
                //TO change to compare the ip configured
                //var rooms = cache.Rooms.Where(r => r.ServiceIp == serviceIp);
                if (rooms != null && rooms.Count() > 0)
                {
                    foreach (var r in rooms)
                    {
                        userCache[r.Id] = new SafeDictionary<int, UserNCallback>();
                        micCache[r.Id] = new SafeDictionary<MicType, SafeDictionary<int, MicStatusMessage>>();
                        if (r.PrivateChatEnabled)
                        {
                            InitMicCache(r.Id, MicType.Private, r.PrivateMicCount);
                        }
                        if (r.PublicChatEnabled)
                        {
                            InitMicCache(r.Id, MicType.Public, r.PublicMicCount);
                        }
                        InitMicCache(r.Id, MicType.Secret, r.SecretMicCount);
                        micQueue[r.Id] = new SafeList<MicStatusMessage>();
                    }
                }
                timer.Elapsed += timer_Elapsed;
                timer.Start();
            }
            else
            {
                throw new Exception("Room Initialize Failed");
            }
		}

        static void InitMicCache(int roomId, MicType micType, int count)
        {
            if (count > 0)
            {
                micCache[roomId][micType] = new SafeDictionary<int, MicStatusMessage>();
                for (int i = 0; i < count; i++)
                {
                    micCache[roomId][micType][i] = new MicStatusMessage();
                }
            }      
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //client.KeepAlive();
			Dictionary<int, int> roomUserCount = new Dictionary<int,int>();
			foreach(var pair in userCache)
			{
				roomUserCount.Add(pair.Key,pair.Value.Count);
			}
			client.UpdateRoomOnlineUserCount(roomUserCount);
        }

		static MicStatusMessage GetUserMicStatus(int roomId, int userId, MicType micType)
		{
			var mics = micCache[roomId][micType];
			foreach (var s in mics.Values)
			{
				if (s.UserId == userId)
				{
					return s;
				}
			}
			return null;
		}

        static bool IsMusicAvailabe(int roomId, int userId)
        {
            if (musicCache.ContainsKey(roomId))
            {
                if (musicCache[roomId].PlayerId != userId)
                    return false;
            }
            return true;
        }

        static int GetAvailableMicStatus(int roomId, MicType micType, int userId, int suggestedIndex, out MicStatusMessage msg)
        {
            msg = null;
            var mics = micCache[roomId][micType];
            int index = -1;
            if (suggestedIndex > -1)
            {
                if (suggestedIndex < mics.Count)
                {
                    if (mics[suggestedIndex].UserId == userId)
                    {
                        msg = mics[suggestedIndex];
                        index = suggestedIndex;
                    }
                }
            }
            if (msg == null)
            {
                foreach (var pair in mics)
                {
                    if (pair.Value.UserId == -1 || pair.Value.UserId == userId)
                    {
                        msg = pair.Value;
                        index = pair.Key;
                        break;
                    }
                }
            }
            if (msg == null && micType == MicType.Public)
            {
                msg = new MicStatusMessage
                {
                    MicType = micType,
                    MicStatus = MicStatusMessage.MicStatus_Queue,
                };
                //Mic Queue
                if (micType == MicType.Public)
                {
                    msg = micQueue[roomId].FirstOrDefault(u => u.UserId == userId);
                    if (msg == null)
                    {
                        micQueue[roomId].Add(msg);
                    }
                }
                index = micQueue[roomId].IndexOf(msg);
            }
            return index;
        }
    }
}
