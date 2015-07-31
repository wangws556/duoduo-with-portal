using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model;
using YoYoStudio.Model.Chat;
using YoYoStudio.Model.Core;

namespace YoYoStudio.ChatService.Client
{
    public class ChatServiceCallback : IChatServiceCallback
    {
        public event Action<User> UserLoggedInEvent;
        public event Action<int> UserLoggedOffEvent;
        public event Action<Message> MessageReceivedEvent;
        public event Action<Room> RoomChangedEvent;
        public event Action UserReloginEvent;
        public event Action RoomReloginEvent;

        public void UserLoggedIn(Model.Core.User user)
        {
            if (UserLoggedInEvent != null)
            {
                UserLoggedInEvent(user);
            }
        }
       
        public void UserLoggedOff(int userId)
        {
            if (UserLoggedOffEvent != null)
            {
                UserLoggedOffEvent(userId);
            }
        }

        public void Receive(Message message)
        {
            if (MessageReceivedEvent != null)
            {
                MessageReceivedEvent(message);
            }
        }

        public void RoomChanged(Model.Chat.Room room)
        {
            if (RoomChangedEvent != null)
            {
                RoomChangedEvent(room);
            }
        }

        public void UserRelogin()
        {
            if (UserReloginEvent != null)
            {
                UserReloginEvent();
            }
        }

        public void RoomRelogin()
        {
            if (RoomReloginEvent != null)
            {
                RoomReloginEvent();
            }
        }

    }
}
