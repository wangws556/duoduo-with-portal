using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;
using YoYoStudio.Common.Notification;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    public partial class RoomWindowViewModel
    {
        private void MicStatusMessageReceivedEventHandler(int arg1, MicStatusMessage arg2)
        {
            if (arg1 == RoomVM.Id)
            {
                var uvm = UserVMs.FirstOrDefault(u => u.Id == arg2.UserId);
                if (uvm != null)
                {
                    switch (arg2.MicAction)
                    {
                        #region OnMic

                        case MicAction.OnMic:
                            {
                                switch (arg2.MicType)
                                {
                                    case MicType.Public:
                                        {
                                            switch (arg2.MicIndex)
                                            {
                                                case 0:
                                                    FirstMicUserVM = uvm;
                                                    break;
                                                case 1:
                                                    SecondMicUserVM = uvm;
                                                    break;
                                                case 2:
                                                    ThirdMicUserVM = uvm;
                                                    break;
                                            }
                                        }
                                        break;
                                    case MicType.Private:
                                        if (!PrivateMicUserVMs.Contains(uvm))
                                        {
                                            PrivateMicUserVMs.Add(uvm);
                                        }
                                        break;
                                    case MicType.Secret:
                                        if (!SecretMicUserVMs.Contains(uvm))
                                        {
                                            SecretMicUserVMs.Add(uvm);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                uvm.OnMic(arg2.MicType, arg2.MicIndex, arg2.StreamGuid, arg2.MicStatus);
                                updateMicImage(uvm.Id, true);
                            }
                            break;

                        #endregion

                        #region DownMic

                        case MicAction.DownMic:
                            {
                                switch (arg2.MicType)
                                {
                                    case MicType.Public:
                                        switch (arg2.MicIndex)
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
                                    case MicType.Private:
                                        PrivateMicUserVMs.Remove(uvm);
                                        break;
                                    case MicType.Secret:
                                        SecretMicUserVMs.Remove(uvm);
                                        break;
                                }
                            }
                            uvm.DownMic();
                            updateMicImage(uvm.Id, false);
                            break;

                        #endregion

                        #region Toggle

                        case MicAction.Toggle:
                            {
                                uvm.Toggle(arg2.MicStatus);
                            }
                            break;

                        #endregion
                    }
                }
            }
        }


        private void RoomMessageReceivedEventHandler(int roomId, RoomMessage msg)
        {
            //Public or Private chat message must be broadcasted in the same room
            if (msg.MessageType == RoomMessageType.PrivateChatMessage || msg.MessageType == RoomMessageType.PublicChatMessage)
            {
                if (roomId == RoomVM.Id)
                {
                    ShowChatMessage(msg);
                }
            }
            else if (msg.MessageType == RoomMessageType.GiftMessage)
            {
                if (roomId == RoomVM.Id)
                {
                    GiftSentEventHandler(msg,false);
                }
                else
                {
                    GiftViewModel gift = ApplicationVM.LocalCache.AllGiftVMs.FirstOrDefault(g => g.Id == msg.ItemId);
                    if (msg.Count >= gift.RunWay) //RunWay message can be broadcasted among the rooms
                    {
                        GiftSentEventHandler(msg,true);
                    }
                }
            }
            else if (msg.MessageType == RoomMessageType.HornMessage
                || msg.MessageType == RoomMessageType.HallHornMessage
                || msg.MessageType == RoomMessageType.GlobalHornMessage)
            {
                switch (msg.MsgResult)
                { 
                    case MessageResult.Succeed:
                        if (msg.SenderId == Me.Id)
                        {
                            if (msg.MessageType == RoomMessageType.HornMessage)
                                Me.Money -= ApplicationVM.LocalCache.HornMsgMoney;
                            if (msg.MessageType == RoomMessageType.HallHornMessage)
                                Me.Money -= ApplicationVM.LocalCache.HallHornMsgMoney;
                            if (msg.MessageType == RoomMessageType.GlobalHornMessage)
                                Me.Money -= ApplicationVM.LocalCache.GlobalHornMsgMoney;
                            CallJavaScript("InitMe", Me.GetJson(true));
                        }
                        ShowHornMsg(msg);
                        break;
                    case MessageResult.NotEnoughMoney:
                    case MessageResult.NotEnoughPrivilege:
                    case MessageResult.UnkownError:
                        CallJavaScript("AlertMessage", Messages.ResourceManager.GetString(msg.MsgResult.ToString()));
                        break;
                    default:
                        break;
                }
            }
        }
        private void GiftSentEventHandler(RoomMessage msg,bool broadcastMsg)
        {
            switch (msg.GiftResult)
            {
                case SendGiftResult.Succeed:
                    UserViewModel sender = null;
                    UserViewModel receiver = null;
                    GiftViewModel gift = ApplicationVM.LocalCache.AllGiftVMs.FirstOrDefault(g => g.Id == msg.ItemId);
                    lock (UserVMs)
                    {
                        sender = UserVMs.FirstOrDefault(u => u.Id == msg.SenderId);
                        if (sender == null) //maybe comes from other room.(e.g. RunWay message)
                        {
                            sender = ApplicationVM.LocalCache.AllUserVMs[msg.SenderId];
                            if (sender != null && !sender.IsInitialized)
                                sender.Initialize();
                        }
                        receiver = UserVMs.FirstOrDefault(u => u.Id == msg.ReceiverId);
                        if (receiver == null)
                        {
                            receiver = ApplicationVM.LocalCache.AllUserVMs[msg.ReceiverId];
                            if (receiver != null && !receiver.IsInitialized)
                                receiver.Initialize();
                        }
                    }
                    if (sender != null && receiver != null && gift != null)
                    {
                        string header = "<img title='" + sender.RoleVM.Name + "' src='" + sender.ImageVM.StaticImageFile + "'><u style='color:gold;margin-right:10px'><span  onclick='window.external.SelectUser(" + sender.Id + ")'" +
                                    " oncontextmenu='window.external.SelectUser(" + sender.Id + ")'/>" + sender.NickName + "(" + sender.Id + ")" + "</span></u></img> 送给 " +
									"<img title='" + receiver.RoleVM.Name + "' src='" + receiver.ImageVM.StaticImageFile + "'><u style='color:purple;margin-left:10px;margin-right:10px'><span onclick='window.external.SelectUser(" + receiver.Id + ")'" +
                                    "oncontextmenu='window.external.SelectUser(" + receiver.Id + ")'/>" + receiver.NickName + "(" + receiver.Id + ")" + "</span></u></img>";
                        string htmlmsg = header;

                        //for horn message
                        if (msg.Count >= gift.RoomBroadCast || msg.Count>=gift.WorldBroadCast)
                        {
							htmlmsg += "<span>" + msg.Count + gift.Unit + gift.Name + "<img src='" + gift.ImageVM.DynamicImageFile + "'/></span>";
                            msg.Content = htmlmsg;
                            ShowHornMsg(msg);
                        }

                        //for RunWay message
                        if (msg.Count >= gift.RunWay)
                        {
                            htmlmsg = string.Empty;
                            htmlmsg += "<img style='margin-left:20px;margin-right:20px;' src='" + gift.ImageVM.DynamicImageFile + "'/>";
                            htmlmsg += header + msg.Count + gift.Unit + gift.Name +  
                                "<span style='color:blue;font-size:18px'>" + msg.Time + "</span>";
                            CallJavaScript("ScrollMessage", htmlmsg);
                        }

                        if (!broadcastMsg)
                        {
                            //for public gift message
                            htmlmsg = header;
                            htmlmsg += "<span>一" + gift.Unit + gift.Name + "<img src='" + gift.ImageVM.DynamicImageFile + "'/>, 共";
                            CallJavaScript("GiftSent", htmlmsg, msg.Count, gift.Unit);

                            //for private gift message
                            if (Me.Id == msg.ReceiverId)
                            {
                                htmlmsg = header;
                                htmlmsg += "<span>一" + gift.Unit + gift.Name;
                                JavaScriptSerializer js = new JavaScriptSerializer();
                                CallJavaScript("ShowPrivateChatMessage", htmlmsg, false);
                            }


                            if (Me.Id == msg.SenderId)
                            {
                                Me.Money -= msg.Count * gift.Price;
                                CallJavaScript("InitMe", Me.GetJson(true));
                            }

                            else if (Me.Id == msg.ReceiverId)
                            {
                                Me.Score += msg.Count * gift.Score;
                                CallJavaScript("InitMe", Me.GetJson(true));
                            }
                        }
                    }
                    break;
                case SendGiftResult.CannotReceiveGift:
                case SendGiftResult.CannotSendGift:
                case SendGiftResult.NotEnoughMoney:
                case SendGiftResult.UnkownError:
					CallJavaScript("AlertMessage", Messages.ResourceManager.GetString(msg.GiftResult.ToString()));
                    break;
                default:
                    break;
            }
        }
        private void UserLeftRoomEventHandler(int arg1, int arg2)
        {
            lock (UserVMs)
            {
                UserViewModel uvm = UserVMs.FirstOrDefault(u => u.Id == arg2);
                if (uvm != null)
                {
                    uvm.DownMic();
                    lock (UserVMs)
                    {
                        UserVMs.Remove(uvm);
                    }
                    CallJavaScript("UserLeft", uvm.Id);
                }
            }
        }

        private void UserEnteredRoomEventHandler(int arg1, User arg2)
        {
            if (RoomVM.Id == arg1)
            {
                UserEntered(arg2, true);
            }
        }

        private void CommandReceivedEventHandler(int roomId, int cmdId, int sourceUserId, int targetUserId)
        {
            if (roomId == RoomVM.Id)
            {
                switch (cmdId)
                {
                    case Applications._9258App.UserCommands.KickOutOfRoomCommandId:
                        CallJavaScript("AlertMessage", string.Format(Resource.Messages.KickOut, sourceUserId));
                        Messenger.Default.Send<EnumNotificationMessage<object, HallWindowAction>>(new EnumNotificationMessage<object,HallWindowAction>(HallWindowAction.CloseRoomWindow));
                        break;
                }
            }
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            SetVideoSize();
        }
    }
}
