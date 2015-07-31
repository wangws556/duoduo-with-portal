using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using YoYoStudio.Model;

namespace YoYoStudio.RoomService.Client
{
	public partial class RoomServiceClient
	{
		public static string GetServiceAddress(string ip, int port)
		{
			return "net.tcp://" + ip+ ":" + port + "/RoomService";
		}

		public RoomServiceClient(IRoomServiceCallback callback, string serviceIp, int port)
			:this(new System.ServiceModel.InstanceContext(callback),Const.RoomServiceName,
            new EndpointAddress(new Uri(GetServiceAddress(serviceIp,port)),new DnsEndpointIdentity("_9258RoomService")))
		{
		}
	}
}
