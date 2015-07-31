




/// <copyright>
/// Copyright ©  2012 Unisys Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using YoYoStudio.Common.ObjectHistory;
using YoYoStudio.Common.Extensions;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Chat;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YoYoStudio.Model.Media;

namespace YoYoStudio.Client.ViewModel
{
	public partial class RoomViewModel
	{
	

		/// <summary>
		/// Field which backs the RoomGroupVM property
		/// </summary>
		private HistoryableProperty<RoomGroupViewModel> roomGroupVM = new HistoryableProperty<RoomGroupViewModel>(null);

		/// <summary>
		/// Gets / sets the RoomGroupVM value
		/// </summary>
		[Browsable(false)]
		public  RoomGroupViewModel RoomGroupVM
		{
			get { return roomGroupVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoomGroupViewModel>(roomGroupVM, value, () => RoomGroupVM ); }
		}


		/// <summary>
		/// Field which backs the Name property
		/// </summary>
		private HistoryableProperty<string> name = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Name value
		/// </summary>
		[Browsable(false)]
		public  string Name
		{
			get { return name.GetValue(); }
			set { ChangeAndNotifyHistory<string>(name, value, () => Name ); }
		}


		/// <summary>
		/// Field which backs the Enabled property
		/// </summary>
		private HistoryableProperty<bool> enabled = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Enabled value
		/// </summary>
		[Browsable(false)]
		public  bool Enabled
		{
			get { return enabled.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(enabled, value, () => Enabled ); }
		}


		/// <summary>
		/// Field which backs the ServiceIp property
		/// </summary>
		private HistoryableProperty<string> serviceIp = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the ServiceIp value
		/// </summary>
		[Browsable(false)]
		public  string ServiceIp
		{
			get { return serviceIp.GetValue(); }
			set { ChangeAndNotifyHistory<string>(serviceIp, value, () => ServiceIp ); }
		}


		/// <summary>
		/// Field which backs the RtmpUrl property
		/// </summary>
		private HistoryableProperty<string> rtmpUrl = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the RtmpUrl value
		/// </summary>
		[Browsable(false)]
		public  string RtmpUrl
		{
			get { return rtmpUrl.GetValue(); }
			set { ChangeAndNotifyHistory<string>(rtmpUrl, value, () => RtmpUrl ); }
		}


		/// <summary>
		/// Field which backs the ReserveRoom property
		/// </summary>
		private HistoryableProperty<string> reserveRoom = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the ReserveRoom value
		/// </summary>
		[Browsable(false)]
		public  string ReserveRoom
		{
			get { return reserveRoom.GetValue(); }
			set { ChangeAndNotifyHistory<string>(reserveRoom, value, () => ReserveRoom ); }
		}


		/// <summary>
		/// Field which backs the RecommendRoom property
		/// </summary>
		private HistoryableProperty<string> recommendRoom = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the RecommendRoom value
		/// </summary>
		[Browsable(false)]
		public  string RecommendRoom
		{
			get { return recommendRoom.GetValue(); }
			set { ChangeAndNotifyHistory<string>(recommendRoom, value, () => RecommendRoom ); }
		}


		/// <summary>
		/// Field which backs the RoomHeader property
		/// </summary>
		private HistoryableProperty<string> roomHeader = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the RoomHeader value
		/// </summary>
		[Browsable(false)]
		public  string RoomHeader
		{
			get { return roomHeader.GetValue(); }
			set { ChangeAndNotifyHistory<string>(roomHeader, value, () => RoomHeader ); }
		}
	}
}
	