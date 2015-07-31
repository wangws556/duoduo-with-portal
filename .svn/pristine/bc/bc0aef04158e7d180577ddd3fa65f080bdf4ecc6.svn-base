using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.ChatService.Client;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    public partial class ApplicationViewModel
    {
        private ChatServiceCallback callback = new ChatServiceCallback();

        public event Action<User> UserLoggedInEvent
        {
            add
            {
                callback.UserLoggedInEvent += value;
            }
            remove
            {
                callback.UserLoggedInEvent -= value;
            }
        }
        public event Action<int> UserLoggedOffEvent
        {
            add
            {
                callback.UserLoggedOffEvent += value;
            }
            remove
            {
                callback.UserLoggedOffEvent -= value;
            }
        }
        public event Action<Message> MessageReceivedEvent
        {
            add
            {
                callback.MessageReceivedEvent += value;
            }
            remove
            {
                callback.MessageReceivedEvent -= value;
            }
        }

        public event Action UserReloginEvent
        {
            add
            {
                callback.UserReloginEvent += value;
            }
            remove
            {
                callback.UserReloginEvent -= value;
            }
        }

        public event Action RoomReloginEvent
        {
            add
            {
                callback.RoomReloginEvent += value;
            }
            remove
            {
                callback.RoomReloginEvent -= value;
            }
        }
    }
}
