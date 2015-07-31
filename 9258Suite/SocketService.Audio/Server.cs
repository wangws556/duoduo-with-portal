using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoYoStudio.Common;
using YoYoStudio.Common.Net;
using YoYoStudio.RoomService.Client;
using YoYoStudio.SocketService.Udp;

namespace YoYoStudio.SocketService.Audio
{
    public partial class Server : UdpServerBase
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RoomServiceCallback roomCallback = new RoomServiceCallback();
        private RoomServiceClient roomClient;
        private SafeDictionary<int, SafeDictionary<int, IPEndPoint>> clientCache = new SafeDictionary<int, SafeDictionary<int, IPEndPoint>>();

        private int roomServicePort;

        public Server()
        {
            roomServicePort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RoomServicePort"]);
            roomClient = new RoomServiceClient(roomCallback,ip,roomServicePort);
            roomClient.AudioServiceLogin(ip,port);
            roomClient.AudioServiceLogOff();
        }

        private void PerformBroadcasting(UdpPacket packet, byte[] packageData)
        {
            if (clientCache.ContainsKey(packet.UserId))
            {
                var receivers = clientCache[packet.PId];
                if (receivers.Count > 0)
                {
                    Parallel.ForEach(receivers, receiver =>
                    {
                        if (receiver.Key != packet.UserId)
                        {
                            AsyncBeginSend(new UdpPacketBuffer(packageData, receiver.Value));
                        }
                    });
                }
            }
        }

        private void Login(UdpPacket packet, IPEndPoint clientEP)
        {
            if (!clientCache.ContainsKey(packet.PId))
            {
                clientCache.Add(packet.PId, new SafeDictionary<int, IPEndPoint>());
            }
            clientCache[packet.PId][packet.UserId] = clientEP;
        }

        private void Logoff(UdpPacket packet)
        {
            if (clientCache.ContainsKey(packet.PId))
            {
				if (clientCache[packet.PId].ContainsKey(packet.UserId))
				{
					clientCache[packet.PId].Remove(packet.UserId);
				}
            }
        }

        protected override void PacketReceived(UdpPacketBuffer buffer)
        {
            EndPoint client = buffer.RemoteEndPoint;
            byte[] receivedData = buffer.Data.Take(buffer.DataLength).ToArray();
            UdpPacket packet = UdpPacket.FromBytes(receivedData);
            switch (packet.PacketType)
            {
                case UdpPacketType.Login:
					Console.WriteLine("User Login -- " + packet.UserId);
                    Login(packet, buffer.RemoteEndPoint);
                    packet.PacketType = UdpPacketType.LoginSucceed;
                    var bytes = packet.ToBytes();
                    udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					Console.WriteLine("User Login Succeed-- " + packet.UserId);
                    break;
                case UdpPacketType.Data:
                    PerformBroadcasting(packet, receivedData);
                    break;
                case UdpPacketType.Logoff:
					Console.WriteLine("User Logoff -- " + packet.UserId);
                    Logoff(packet);
                    packet.PacketType = UdpPacketType.LogoffSuccedd;
                    bytes = packet.ToBytes();
					udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					udpSocket.Send(bytes, bytes.Length, buffer.RemoteEndPoint);
					Console.WriteLine("User Logoff Succeed -- " + packet.UserId);
                    break;
                default:
                    break;
            }
        }

        public override void Stop()
        {
            base.Stop();
            roomClient.AudioServiceLogOff();
            roomClient.Close();
        }
    }
}
