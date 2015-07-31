using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace YoYoStudio.Common.Wcf
{
	public class WcfService
	{
		protected RemoteEndpointMessageProperty GetClientRemoteEndPoint()
		{
			return OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
		}
	}
}
