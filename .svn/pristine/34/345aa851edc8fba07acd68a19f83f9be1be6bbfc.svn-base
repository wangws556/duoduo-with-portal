using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Common.Net
{
    public enum UdpPacketType : byte
    {
        Login,
        LoginSucceed,
        Logoff,
        LogoffSuccedd,
        Data
    }

    public class UdpPacket
    {
        private UdpPacket()
        {
            data = new List<byte[]>();
        }

        public UdpPacket(int userId, int pid, UdpPacketType requestType)
        {
            this.UserId = userId;
            this.PId = pid;
            this.PacketType = requestType;
            data = new List<byte[]>();
        }

        public void Append(byte[] data)
        {
            this.data.Add(data);
        }

        public static UdpPacket FromBytes(byte[] data)
        {
            UdpPacket packet = new UdpPacket();
            packet.PacketType = (UdpPacketType)Enum.ToObject(typeof(UdpPacketType), data[0]);
            packet.UserId = BitConverter.ToInt32(data, 1);
            packet.PId = BitConverter.ToInt32(data, 5);
            if (data.Length > 9)
            {
                int position = 9;
                while (position < data.Length)
                {
                    int length = BitConverter.ToInt32(data, position);
                    position += 4;
                    byte[] d = new byte[length];
                    Array.Copy(data, position, d, 0, length);
                    position += length;
                    packet.Append(d);
                }
            }
            return packet;
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[9];
            bytes[0] = (byte)this.PacketType;
            Buffer.BlockCopy(BitConverter.GetBytes(this.UserId), 0, bytes, 1, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(this.PId), 0, bytes, 5, 4);
            if (data != null && data.Count > 0)
            {
                foreach (byte[] d in data)
                {
                    if (d.Length > 0)
                    {
                        bytes = bytes.Concat(BitConverter.GetBytes(d.Length)).ToArray();
                        bytes = bytes.Concat(d).ToArray();
                    }
                }
            }
            return bytes;
        }

        public UdpPacketType PacketType
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        private List<byte[]> data;

        public List<byte[]> Data { get { return data; } }

        public int PId { get; set; }
    }
}
