using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YoYoStudio.Common.ORM;

namespace YoYoStudio.Model.Media
{
    [Serializable]
    [DataContract]
    public class MusicStatus
    {
        [DataMember]
        public string Name;
        [DataMember]
        public PlayStatus Status;
        [DataMember]
        public int Position;
        [DataMember]
        public int PlayerId;
    }


    [Serializable]
    [DataContract]
    public class MusicInfo
    {
        [DataMember]
        public string Name;

        [DataMember]
        public long Size;

        [DataMember]
        public DateTime LastModified;
    }

    public enum PlayStatus
    {
        Playing,
        Paused,
        Stoped,
        None
    }
}
