using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YoYoStudio.Exceptions
{
    public class DatabaseException : Exception
    {
    }

    [DataContract]
    public class DatabaseExceptionMsg
    { 
        public DatabaseExceptionMsg(string message)
        {
            Message = message;
        }
        [DataMember]
        public string Message { get; set; }
    }
}
