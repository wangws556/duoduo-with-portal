using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Model.Chat;

namespace YoYoStudio.Model
{
    [DataContract]
    [Serializable]
    [KnownType(typeof(RoomMessage))]
    public class Message
    {

    }

}