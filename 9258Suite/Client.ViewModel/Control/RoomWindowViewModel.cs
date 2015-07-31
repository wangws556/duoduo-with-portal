using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;
using YoYoStudio.RoomService.Client;
using System.IO;
using YoYoStudio.Model;
using YoYoStudio.Common.Wpf.ViewModel;
using FolderPickerLib;
using System.ComponentModel;
using System.Windows.Markup;
using Microsoft.Win32;

namespace YoYoStudio.Client.ViewModel
{
    [ComVisible(true)]
    [SnippetPropertyINPC(field = "selectedUserVM", property = "SelectedUserVM", type = "UserViewModel", defaultValue = "null")]
    //[SnippetPropertyINPC(field = "firstMicUserVM", property = "FirstMicUserVM", type = "UserViewModel", defaultValue = "null")]
    //[SnippetPropertyINPC(field = "secondMicUserVM", property = "SecondMicUserVM", type = "UserViewModel", defaultValue = "null")]
    //[SnippetPropertyINPC(field = "thirdMicUserVM", property = "ThirdMicUserVM", type = "UserViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "fontFamilies", property = "FontFamilies", type = "ObservableCollection<string>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "fontSizes", property = "FontSizes", type = "ObservableCollection<int>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "userVMs", property = "UserVMs", type = "ObservableCollection<UserViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "privateMicUserVMs", property = "PrivateMicUserVMs", type = "ObservableCollection<UserViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "secretMicUserVMs", property = "SecretMicUserVMs", type = "ObservableCollection<UserViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "giftGroupVMs", property = "GiftGroupVMs", type = "ObservableCollection<GiftGroupViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "queueMicUserVMs", property = "QueueMicUserVMs", type = "ObservableCollection<UserViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "motionImageVMs", property = "MotionImageVMs", type = "ObservableCollection<ImageViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "stampImageVMs", property = "StampImageVMs", type = "ObservableCollection<ImageViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field= "roomVM",property="RoomVM",type="RoomViewModel",defaultValue="null")]
    [SnippetPropertyINPC(field = "videoWidth", property = "VideoWidth", type = "double", defaultValue = "0")]
    [SnippetPropertyINPC(field = "videoHeight", property = "VideoHeight", type = "double", defaultValue = "0")]
    public partial class RoomWindowViewModel : WindowViewModel
    {
        public RoomServiceClient RoomClient { get; private set; }
        public RoomServiceCallback RoomCallback { get; private set; }

        #region Video

        public VideoWindowViewModel FirstVideoWindowVM { get; private set; }
        public VideoWindowViewModel SecondVideoWindowVM { get; private set; }
        public VideoWindowViewModel ThirdVideoWindowVM { get; private set; }

        public UserViewModel FirstMicUserVM
        {
            get { return FirstVideoWindowVM.UserVM; }
            set { FirstVideoWindowVM.UserVM = value; }
        }
        public UserViewModel SecondMicUserVM
        {
            get { return SecondVideoWindowVM.UserVM; }
            set { SecondVideoWindowVM.UserVM = value; }
        }
        public UserViewModel ThirdMicUserVM
        {
            get { return ThirdVideoWindowVM.UserVM; }
            set { ThirdVideoWindowVM.UserVM = value; }
        }

        #endregion

        public RoomWindowViewModel(RoomViewModel roomVM)
        {
            this.RoomVM = roomVM;
            userVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<UserViewModel>());
            privateMicUserVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<UserViewModel>());
            secretMicUserVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<UserViewModel>());
            fontFamilies.SetValue(new System.Collections.ObjectModel.ObservableCollection<string>());
            fontSizes.SetValue(new System.Collections.ObjectModel.ObservableCollection<int>());

            RoomCallback = new RoomServiceCallback();
            RoomClient = new RoomServiceClient(RoomCallback, roomVM.ServiceIp, ApplicationVM.LocalCache.RoomServicePort);

            SetVideoSize();
        }

        public void GetHtml(string htm)
        {
        }

        public void Enter()
        {
            if (RoomClient.EnterRoom(RoomVM.Id, ApplicationVM.LocalCache.CurrentUserVM.GetConcretEntity<User>()))
            {
                Me.RoomWindowVM = this;
                RegisterEvents();
                timer = new Timer(TimeSpan.FromSeconds(20).TotalMilliseconds);
                timer.Elapsed += timer_Elapsed;
                timer.Start();
                Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.EnterRoomSucceeded, this));
            }
            else
            {
                Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object, HallWindowAction>(HallWindowAction.EnterRoomFailed, this));
            }
        }

        public void SendGift(int giftGroupId, int giftId, int receiverId, int count)
        {
            var group = GiftGroupVMs.FirstOrDefault(g => g.Id == giftGroupId);
            if (group != null)
            {
                var gift = group.GiftVMs.FirstOrDefault(g => g.Id == giftId);
                if (gift != null)
                {
                    if (Me.Money > gift.Price * count)
                    {
                        RoomClient.SendGift(RoomVM.Id, receiverId, giftId, count);
                    }
                }
            }
        }

        public bool ScoreExchange(int userId, int toExchangeScore, int getMoney)
        {
            if (RoomClient.ScoreExchange(userId, toExchangeScore, getMoney))
            {
                Me.Score = Me.Score - toExchangeScore;
                Me.Money = Me.Money + getMoney;
                return true;
            }
            return false;
        }

        public bool CanReceiveGift(int receiverId)
        {
            lock (UserVMs)
            {
                var uvm = UserVMs.FirstOrDefault(u => u.Id == receiverId);
                if (uvm != null)
                {
                    return uvm.HasCommand(RoomVM.Id, Applications._9258App.FrontendCommands.ReceiveGiftCommandId, -1);
                }
                return false;
            }
        }

        public override void LoadAsync()
        {
            //Task.Factory.StartNew(() =>
            //    {
                    lock (UserVMs)
                    {
                        var users = RoomClient.GetRoomUsers(RoomVM.Id);
                        if (users != null && users.Length > 0)
                        {
                            foreach (var user in users)
                            {
                                UserEntered(user, false);
                            }
                        }
                    }

                    var micUsers = RoomClient.GetMicUsers(RoomVM.Id, MicType.Public);
                    if (micUsers != null && micUsers.Count > 0)
                    {
                        if (micUsers.ContainsKey(0) && micUsers[0].MicStatus != MicStatusMessage.MicStatus_Off)
                        {
                            FirstMicUserVM = UserVMs.FirstOrDefault(u => u.Id == micUsers[0].UserId);
                            FirstMicUserVM.OnMic(MicType.Public, 0, micUsers[0].StreamGuid, micUsers[0].MicStatus);
                            if ((FirstMicUserVM.MicStatus & MicStatusMessage.MicStatus_Audio) != MicStatusMessage.MicStatus_Off)
                            {
                                StartAudioPlaying(FirstMicUserVM.Id);
                            }
                        }
                        if (micUsers.ContainsKey(1) && micUsers[1].MicStatus != MicStatusMessage.MicStatus_Off)
                        {
                            SecondMicUserVM = UserVMs.FirstOrDefault(u => u.Id == micUsers[1].UserId);
                            SecondMicUserVM.OnMic(MicType.Public, 1, micUsers[1].StreamGuid, micUsers[1].MicStatus);
                            if ((SecondMicUserVM.MicStatus & MicStatusMessage.MicStatus_Audio) != MicStatusMessage.MicStatus_Off)
                            {
                                StartAudioPlaying(SecondMicUserVM.Id);
                            }
                        }
                        if (micUsers.ContainsKey(2) && micUsers[2].MicStatus != MicStatusMessage.MicStatus_Off)
                        {
                            ThirdMicUserVM = UserVMs.FirstOrDefault(u => u.Id == micUsers[2].UserId);
                            ThirdMicUserVM.OnMic(MicType.Public, 2, micUsers[2].StreamGuid, micUsers[2].MicStatus);
                            if ((ThirdMicUserVM.MicStatus & MicStatusMessage.MicStatus_Audio) != MicStatusMessage.MicStatus_Off)
                            {
                                StartAudioPlaying(ThirdMicUserVM.Id);
                            }
                        }
                    }


                    micUsers = RoomClient.GetMicUsers(RoomVM.Id, MicType.Private);
                    if (micUsers != null && micUsers.Count > 0)
                    {
                        foreach (var mic in micUsers.Values)
                        {
                            if (mic.MicStatus != MicStatusMessage.MicStatus_Off)
                            {
                                var uvm = UserVMs.FirstOrDefault(u => u.Id == mic.UserId);
                                if (uvm != null)
                                {
                                    PrivateMicUserVMs.Add(uvm);
                                    uvm.OnMic(MicType.Private, mic.MicIndex, mic.StreamGuid, mic.MicStatus);
                                    if ((uvm.MicStatus & MicStatusMessage.MicStatus_Audio) != MicStatusMessage.MicStatus_Off)
                                    {
                                        StartAudioPlaying(uvm.Id);
                                    }
                                }
                            }
                        }
                    }

                    micUsers = RoomClient.GetMicUsers(RoomVM.Id, MicType.Secret);
                    if (micUsers != null && micUsers.Count > 0)
                    {
                        foreach (var mic in micUsers.Values)
                        {
                            if (mic.MicStatus != MicStatusMessage.MicStatus_Off)
                            {
                                var uvm = UserVMs.FirstOrDefault(u => u.Id == mic.UserId);
                                if (uvm != null)
                                {
                                    SecretMicUserVMs.Add(uvm);
                                    uvm.OnMic(MicType.Secret, mic.MicIndex, mic.StreamGuid, mic.MicStatus);
                                    if ((uvm.MicStatus & MicStatusMessage.MicStatus_Audio) != MicStatusMessage.MicStatus_Off)
                                    {
                                        StartAudioPlaying(uvm.Id);
                                    }
                                }
                            }
                        }
                    }

                    RoomMessage msg = RoomClient.GetRoomMessage(RoomMessageType.GiftMessage);
                    if (msg != null)
                    {
                        UserViewModel sender = null;
                        UserViewModel receiver = null;
                        lock (UserVMs)
                        {
                            sender = UserVMs.FirstOrDefault(u => u.Id == msg.SenderId);
                            if (sender == null)
                            {
                                sender = ApplicationVM.LocalCache.AllUserVMs[msg.SenderId];
                                if (sender == null)
                                {
                                    var usr = RoomClient.GetUser(msg.SenderId);
                                    if (usr != null)
                                    {
                                        sender = new UserViewModel(usr);
                                        sender.Initialize();
                                        ApplicationVM.LocalCache.AllUserVMs[msg.SenderId] = sender;
                                    }
                                }
                                else 
                                {
                                    if (!sender.IsInitialized)
                                    {
                                        sender.Initialize();
                                    }
                                }
                            }
                            receiver = UserVMs.FirstOrDefault(u => u.Id == msg.ReceiverId);
                            if (receiver == null)
                            {
                                receiver = ApplicationVM.LocalCache.AllUserVMs[msg.ReceiverId];
                                if (receiver == null)
                                {
                                    var usr = RoomClient.GetUser(msg.ReceiverId);
                                    if (usr != null)
                                    {
                                        receiver = new UserViewModel(usr);
                                        receiver.Initialize();
                                        ApplicationVM.LocalCache.AllUserVMs[msg.ReceiverId] = receiver;
                                    }
                                }
                                else
                                {
                                    if (!receiver.IsInitialized)
                                    {
                                        receiver.Initialize();
                                    }
                                }
                            }
                        }
                        GiftViewModel gift = ApplicationVM.LocalCache.AllGiftVMs.FirstOrDefault(g => g.Id == msg.ItemId);
                        if (sender != null && receiver != null && gift != null)
                        {
                            string header = "<img title='" + sender.RoleVM.Name + "' src='" + sender.ImageVM.StaticImageFile + "'><u style='color:gold;margin-right:10px'><span  onclick='window.external.SelectUser(" + sender.Id + ")'" +
                                       " oncontextmenu='window.external.SelectUser(" + sender.Id + ")'/>" + sender.NickName + "(" + sender.Id + ")" + "</span></u></img> 送给 " +
                                       "<img title='" + receiver.RoleVM.Name + "' src='" + receiver.ImageVM.StaticImageFile + "'><u style='color:purple;margin-left:10px;margin-right:10px'><span onclick='window.external.SelectUser(" + receiver.Id + ")'" +
                                       "oncontextmenu='window.external.SelectUser(" + receiver.Id + ")'/>" + receiver.NickName + "(" + receiver.Id + ")" + "</span></u></img>";
                            string htmlmsg = string.Empty;
                            htmlmsg += "<img style='margin-left:20px;margin-right:20px;' src='" + gift.ImageVM.DynamicImageFile + "'/>";
                            htmlmsg += header + msg.Count + gift.Unit + gift.Name +
                                "<span style='color:blue'>" + msg.Time + "</span>";
                            CallJavaScript("ScrollMessage", htmlmsg);
                        }
                    }

                    XmlLanguage enus = XmlLanguage.GetLanguage("en-us");
                    XmlLanguage zhcn = XmlLanguage.GetLanguage("zh-cn");
                    string fontname = "";
                    foreach (FontFamily fontfamily in Fonts.SystemFontFamilies)
                    {
                        if (fontfamily.FamilyNames.ContainsKey(zhcn))
                        {
                            fontfamily.FamilyNames.TryGetValue(zhcn, out fontname);
                            FontFamilies.Insert(0, fontname);
                        }
                        else if (fontfamily.FamilyNames.ContainsKey(enus))
                        {
                            fontfamily.FamilyNames.TryGetValue(enus, out fontname);
                            FontFamilies.Add(fontname);
                        }
                    }

                    FontSizes.Add(14);
                    FontSizes.Add(16);
                    FontSizes.Add(18);
                    FontSizes.Add(20);
                    FontSizes.Add(22);
                    FontSizes.Add(24);
                    FontSizes.Add(28);
                    FontSizes.Add(32);
                    FontSizes.Add(36);

                    CallJavaScript("InitUsers", UsersJson);
                    CallJavaScript("InitFonts", FontFamiliesJson, FontSizesJson);
                    CallJavaScript("InitStamp", StampImagesJson);
                    for (int i = 0; i < MotionImagesJson.Count; i++)
                    {
                        CallJavaScript("InitFaceTab", MotionImagesJson[i], i == MotionImagesJson.Count - 1);
                    }
                    
                    CallJavaScript("InitMoneyForHorn", ApplicationVM.LocalCache.HornMsgMoney, ApplicationVM.LocalCache.HallHornMsgMoney, ApplicationVM.LocalCache.GlobalHornMsgMoney);

                    int scoreToMoney = 0;
                    if (ApplicationVM.LocalCache.AllExchangeRateVMs.Count > 0)
                    {
                        try
                        {
                            var exchangeVM = ApplicationVM.LocalCache.AllExchangeRateVMs.OrderBy(r => r).Where(r =>
                                Convert.ToDateTime(r.ValidTime) >= (DateTime.Now)).ToList().FirstOrDefault();
                            if (exchangeVM != null)
                            {
                                scoreToMoney = exchangeVM.ScoreToMoney;
                            }
                        }
                        catch (Exception ex)
                        { }
                    }

                    CallJavaScript("InitExchangeRate", scoreToMoney);
                    
                //});
        }

        public void SelectUser(UserViewModel uvm)
        {
            lock (UserVMs)
            {
                if (UserVMs.Contains(uvm))
                {
                    SelectedUserVM = uvm;
                }
            }
        }

        public void SelectUser(int userId)
        {
            lock (UserVMs)
            {
                var uvm = UserVMs.FirstOrDefault(u => u.Id == userId);
                if (uvm != null)
                {
                    SelectedUserVM = uvm;
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            giftGroupVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<GiftGroupViewModel>(ApplicationVM.LocalCache.AllGiftGroupVMs));
            motionImageVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<Common.Wpf.ViewModel.ImageViewModel>(
                ApplicationVM.LocalCache.AllImages[BuiltIns.SmileImageType.Id].Values));
            stampImageVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<ImageViewModel>(
                ApplicationVM.LocalCache.AllImages[BuiltIns.StampImageType.Id].Values));

            FirstVideoWindowVM = new VideoWindowViewModel();
            SecondVideoWindowVM = new VideoWindowViewModel();
            ThirdVideoWindowVM = new VideoWindowViewModel();
        }

        public void SendHornMsg(string message)
        {
            RoomMessage msg = InitAnnouncementMsg(RoomMessageType.HornMessage, message);
            RoomClient.SendRoomMessage(RoomVM.Id, msg);
        }

        public void SendHallHornMsg(string message)
        {
            RoomMessage msg = InitAnnouncementMsg(RoomMessageType.HallHornMessage, message);
            RoomClient.SendRoomMessage(RoomVM.Id, msg);
        }

        public void SendGlobalHornMsg(string message)
        {
            RoomMessage msg = InitAnnouncementMsg(RoomMessageType.GlobalHornMessage, message);
            RoomClient.SendRoomMessage(RoomVM.Id, msg);
        }

        public void ShowHornMsg(RoomMessage msg)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            CallJavaScript("ShowHornMessage", js.Serialize(msg.Content));
        }

        public void SendChatMessage(string message, int toUserId, bool isPrivate)
        {
            RoomMessage msg = new RoomMessage();
            msg.MotionMessages = new List<MotionImagesMessage>();
            msg.MessageType = isPrivate ? RoomMessageType.PrivateChatMessage : RoomMessageType.PublicChatMessage;

            msg.SenderId = Me.Id;
            msg.ReceiverId = toUserId;

            msg.Content = message;
            msg.Time = DateTime.Now.ToString();
			//int offset = 0;
			//int firstImgIndex = message.IndexOf("<IMG");
			//while (firstImgIndex != -1)
			//{
			//	message = message.Substring(firstImgIndex, message.Length - firstImgIndex);
			//	int srcStartIndex = message.IndexOf("\"");
			//	if (srcStartIndex != -1)
			//	{
			//		message = message.Substring(srcStartIndex + 1, message.Length - srcStartIndex - 1);
			//		int srcEndIndex = message.IndexOf("\"");
			//		string path = message.Substring(0, srcEndIndex);

			//		var img = ApplicationVM.LocalCache.AllImages[BuiltIns.SmileImageType.Id].Values.FirstOrDefault(
			//			r => r.StaticImageFile == path);
			//		if (img == null) // customized motion image
			//		{
			//			MotionImagesMessage mImageMsg = new MotionImagesMessage();
			//			mImageMsg.IsCustomizedImage = true;
			//			mImageMsg.Id = -1;
			//			mImageMsg.Bytes = Utility.GetImageBytesFromFile(path);
			//			mImageMsg.Offset = firstImgIndex + offset;
			//			mImageMsg.Path = path;
			//			msg.MotionMessages.Add(mImageMsg);
			//		}

			//		message = message.Substring(srcEndIndex + 1, message.Length - srcEndIndex - 1);
			//		offset += firstImgIndex + srcStartIndex + 1 + srcEndIndex + 1;
			//		firstImgIndex = message.IndexOf("<IMG");
			//	}
			//}

            RoomClient.SendRoomMessage(RoomVM.Id, msg);
        }

        public string AddMotionImages()
        {
			//FolderPickerDialog openDlg = new FolderPickerDialog();
			//if (openDlg.ShowDialog() == true)
			//{
			//	string folerName = openDlg.SelectedPath;
			//	string[] fileNames = Directory.GetFiles(folerName);
			//	List<string> imageList = new List<string>();
			//	foreach (string file in fileNames)
			//	{
			//		FileInfo info = new FileInfo(file);
			//		if (info.Extension == ".gif" || info.Extension == ".bmp" || info.Extension == ".jpg" || info.Extension == ".png")
			//		{
			//			imageList.Add(file);
			//		}
			//	}
			//	List<string> newImagePath = new List<string>();
			//	if (UploadImages(imageList, newImagePath))
			//	{
			//		ImageGroupViewModel imgGroup = new ImageGroupViewModel(Text.CustomizedMotion);
			//		foreach (string path in newImagePath)
			//		{
			//			//imgGroup.ImageVMs.Add(path);
			//		}
			//		JavaScriptSerializer js = new JavaScriptSerializer();
			//		return js.Serialize(imgGroup);
			//	}
			//	else
			//	{
			//		MessageBox.Show("导入图片失败！");
			//		return string.Empty;
			//	}
			//}
            return string.Empty;
        }

        public void MenuClicked(string userId, string cmdId)
        {
            int uid = -1;
            int cid = -1;
            if (int.TryParse(userId, out uid) && int.TryParse(cmdId, out cid))
            {
                var targetUser = GetUser(uid);
                if (targetUser != null)
                {
                    if (Me.HasCommand(RoomVM.Id, cid, targetUser.RoleVM.Id))
                    {
                        RoomClient.ExecuteCommand(RoomVM.Id, cid, targetUser.Id);
                    }
                }
            }
        }

        public bool CanSendAnnoucementMessage()
        {
            return CanSendHornMessage()
                || CanSendHallHornMessage()
                || CanSendGlobalHornMessage();
        }

        public bool CanSendHornMessage()
        {
            return Me.HasCommand(RoomVM.Id, Applications._9258App.FrontendCommands.HornCommandId, -1);
        }

        public bool CanSendHallHornMessage()
        {
            return Me.HasCommand(RoomVM.Id, Applications._9258App.FrontendCommands.HallHornCommandId, -1);
        }

        public bool CanSendGlobalHornMessage()
        {
            return Me.HasCommand(RoomVM.Id, Applications._9258App.FrontendCommands.GlobalHornCommandId, -1);
        }

        #region Private


        private Timer timer;

        private void ShowChatMessage(RoomMessage msg)
        {
            if (msg.MsgResult == MessageResult.Succeed)
            {
                //The message comes from me, just dispay it as the face (image) has the correct path
                string contentWithHeader = string.Empty;
                if (msg.SenderId != Me.Id)
                {
                    //Need to read the face(image) bytes to file
                    string root = System.IO.Path.Combine(Environment.CurrentDirectory, "Images\\ReceiveMessageImage\\" + msg.SenderId);
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    foreach (MotionImagesMessage mImageMsg in msg.MotionMessages)
                    {
                        if (mImageMsg.IsCustomizedImage) //customized motion, need to store first
                        {
                            string filePath = Path.Combine(root, mImageMsg.Path);
                            if (!File.Exists(filePath))
                            {
                                File.WriteAllBytes(filePath, mImageMsg.Bytes);
                            }
                            //msg.Content = UpdateMessageImagePath(msg.Content, mImageMsg.Offset, filePath);
                        }
                    }
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                if (msg.MessageType == RoomMessageType.PublicChatMessage)
                    CallJavaScript("ShowPublicChatMessage", js.Serialize(msg.Content), true);
                else if (msg.MessageType == RoomMessageType.PrivateChatMessage)
                    CallJavaScript("ShowPrivateChatMessage", js.Serialize(msg.Content), true);
            }
            else
            {
                msg.Content = string.Empty;
                switch (msg.MsgResult)
                {
                    case MessageResult.UnkownError:
                        msg.Content = Resource.Messages.GeneralError;
                        break;
                    case MessageResult.NotEnoughMoney:
                        msg.Content = Resource.Messages.NotEnoughMoney;
                        break;
                    case MessageResult.NotEnoughPrivilege:
                        msg.Content = Resource.Messages.NoPrivilege;
                        break;
                    default:
                        break;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                CallJavaScript("AlertMessage", js.Serialize(msg.Content));
            }

        }

        private string UpdateMessageImagePath(string message, int index, string newPath)
        {
            string result = message;
            message = message.Substring(index, message.Length - index);
            int srcStartIndex = message.IndexOf("\"");
            message = message.Substring(srcStartIndex + 1, message.Length - srcStartIndex - 1);
            int srcEndIndex = message.IndexOf("\"");
            string path = message.Substring(0, srcEndIndex);
            return result.Replace(path, newPath);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RoomClient.KeepAlive();
        }

        protected override void ReleaseManagedResource()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
            base.ReleaseManagedResource();
        }

        protected override void ReleaseUnManagedResource()
        {
            try
            {
                Me.DownMic();
                if (RoomClient != null && RoomClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    if (RoomClient.GetMusicPlayer(RoomVM.Id) == Me.Id)
                    {
                        RoomClient.DownPlayMusic(RoomVM.Id);
                    }
                    RoomClient.LeaveRoom(RoomVM.Id);
                    RoomClient.Close();
                    RoomClient = null;
                }
                if (RoomCallback != null)
                {
                    UnRegisterEvents();
                }
                Me.RoomWindowVM = null;
                ReleaseAudio();
            }
            catch { }
            base.ReleaseUnManagedResource();
        }

        private void RegisterEvents()
        {
            RoomCallback.UserEnteredRoomEvent += UserEnteredRoomEventHandler;
            RoomCallback.UserLeftRoomEvent += UserLeftRoomEventHandler;
            RoomCallback.RoomMessageReceivedEvent += RoomMessageReceivedEventHandler;
            RoomCallback.MicStatusMessageReceivedEvent += MicStatusMessageReceivedEventHandler;
            RoomCallback.CommandReceivedEvent += CommandReceivedEventHandler;

            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

        }

      

        private void UnRegisterEvents()
        {
            RoomCallback.UserEnteredRoomEvent -= UserEnteredRoomEventHandler;
            RoomCallback.UserLeftRoomEvent -= UserLeftRoomEventHandler;
            RoomCallback.RoomMessageReceivedEvent -= RoomMessageReceivedEventHandler;
            RoomCallback.MicStatusMessageReceivedEvent -= MicStatusMessageReceivedEventHandler;
            RoomCallback.CommandReceivedEvent -= CommandReceivedEventHandler;

            SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
        }

        private ImageGroupViewModel GetFaces(string path)
        {
			//DirectoryInfo dir = new DirectoryInfo(path);
			//ImageGroupViewModel faces = new ImageGroupViewModel(dir.Name);

			//foreach (FileInfo fInfo in dir.GetFiles())
			//{
			//	faces.ImageVMs.Add(fInfo.FullName);
			//}

			//return faces;
			return null;
        }

        private UserViewModel GetUser(int userId)
        {
            lock (UserVMs)
            {
                return UserVMs.FirstOrDefault(u => u.Id == userId);
            }
        }

        private void UserEntered(User user, bool notify)
        {
            UserViewModel uvm = null;
            if (!ApplicationVM.LocalCache.AllUserVMs.ContainsKey(user.Id))
            {
                uvm = new UserViewModel(user);
                uvm.Initialize();
                ApplicationVM.LocalCache.AllUserVMs.Add(user.Id, uvm);
            }
            else
            {
                uvm = ApplicationVM.LocalCache.AllUserVMs[user.Id];
                if (!uvm.IsInitialized)
                    uvm.Initialize();
            }
            lock (UserVMs)
            {
                uvm.RoomWindowVM = this;
                if (!UserVMs.Contains(uvm))
                {
                    UserVMs.Add(uvm);
                }
            }
            if (notify)
            {
                CallJavaScript("UserEntered", uvm.GetJson(false));
            }
        }

        private void updateMicImage(int userId,bool onMic)
        {
            if (onMic)
            {
                CallJavaScript("updateMicImage", userId, ApplicationVM.OnMicImageUrl);
            }
            else
            {
                CallJavaScript("updateMicImage", userId, ApplicationVM.DownMicImageUrl);
            }
        }

        private void SetVideoSize()
        {
            if (SystemParameters.FullPrimaryScreenWidth == 1920)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 5;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 4;
            }
            else if (SystemParameters.FullPrimaryScreenWidth == 1366)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 5;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 3.6;
            }

            else if (SystemParameters.FullPrimaryScreenWidth == 1280)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 4.6;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 3.6;
            }

            else if (SystemParameters.FullPrimaryScreenWidth == 1152)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 4.3;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 3.5;
            }

            else if (SystemParameters.FullPrimaryScreenWidth == 1024)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 4;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 3.5;
            }

            else if (SystemParameters.FullPrimaryScreenWidth == 800)
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 3.7;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 3.3;
            }


            else
            {
                VideoWidth = SystemParameters.FullPrimaryScreenWidth / 6;
                VideoHeight = SystemParameters.FullPrimaryScreenHeight / 4;
            }
            
        }

        public string OnMicLabel { get; private set; }
        public string PrivateMicLabel { get; private set; }
        public string SecretMicLabel { get; private set; }
        public string DownMicLabel { get; private set; }
        public string ConfigLabel { get; private set; }
        public string ConfigAudioLabel { get; private set; }
        public string ConfigVideoLabel { get; private set; }
        public string PlayMusicLabel { get; private set; }
        public string StopMusicLabel { get; private set; }
        public string MusicLabel { get; private set; }
        //public string UploadMusicLabel { get; private set; }
        public string ManageMusicLabel { get; private set; }

        protected override void InitializeResource()
        {
            OnMicLabel = Text.OnMicLabel;
            PrivateMicLabel = Text.PrivateMicLabel;
            SecretMicLabel = Text.SecretMicLabel;
            DownMicLabel = Text.DownMicLabel;
            ConfigLabel = Text.Configuration;
            PlayMusicLabel = Text.PlayMusicLabel;
            StopMusicLabel = Text.StopMusicLabel;
            MusicLabel = Text.MusicLabel;
            ManageMusicLabel = Text.Management;
            ConfigAudioLabel = Text.AudioConfiguration;
            ConfigVideoLabel = Text.VideoConfiguration;
            busyMessage.SetValue(Resource.Messages.Loading);
            title = Text._9258ChatApplication;
            welcomeMessage.SetValue("广告");
            base.InitializeResource();
        }

        private bool UploadImages(List<string> imageBytes, List<string> path)
        {
            try
            {
                string dir = ApplicationVM.GetImageGroupPath(BuiltIns.SmileImageType.Id, Text.CustomizedMotion, true);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                foreach (string file in imageBytes)
                {
                    string name = file.Substring(file.LastIndexOf("\\"));
                    File.Copy(file, dir + name, true);
                    path.Add(dir + name);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private RoomMessage InitAnnouncementMsg(RoomMessageType type, string message)
        {
            RoomMessage msg = new RoomMessage();
            msg.MessageType = type;
            msg.Content = message;
            msg.SenderId = Me.Id;
            msg.IsHorn = true;
            msg.Time = DateTime.Now.ToString();
            return msg;
        }

        #endregion
    }
}
