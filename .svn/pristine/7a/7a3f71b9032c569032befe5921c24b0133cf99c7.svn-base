using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace YoYoStudio.SocketService.Music
{
    // this class encapsulates a single packet that is either sent or received by a UDP socket
    public class TcpPacketBuffer
    {
        //size of the buffer
        public const int BUFFER_SIZE = 1024;

        // Client  socket.
        public Socket workSocket = null;


        //the buffer itself
        public byte[] Data;

        //length of data to transmit
        public int DataLength;

        // the (IP)Endpoint of the remote host
        public IPEndPoint RemoteEndPoint;

        public TcpPacketBuffer()
        {
            Data = new byte[BUFFER_SIZE];

            RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        public TcpPacketBuffer(byte[] data, IPEndPoint remoteEndPoint)
        {
            this.Data = data;
            this.DataLength = data.Length;
            this.RemoteEndPoint = remoteEndPoint;
        }
    }

    public enum TcpPacketType : byte
    {
        Upload,
        Get,
        Delete
    }

    public class TcpPacket
    {
        private TcpPacket()
        {
            data = new List<byte[]>();
        }

        public TcpPacket(TcpPacketType requestType)
        {
            this.PacketType = requestType;
            data = new List<byte[]>();
        }

        public void Append(byte[] data)
        {
            this.data.Add(data);
        }

        public static TcpPacket FromBytes(byte[] data)
        {
            TcpPacket packet = new TcpPacket();
            packet.PacketType = (TcpPacketType)Enum.ToObject(typeof(TcpPacketType), data[0]);
            if (data.Length > 1)
            {
                int position = 1;
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
            byte[] bytes = new byte[1];
            bytes[0] = (byte)this.PacketType;
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

        public TcpPacketType PacketType
        {
            get;
            set;
        }

        private List<byte[]> data;

        public List<byte[]> Data { get { return data; } }
    }
}
