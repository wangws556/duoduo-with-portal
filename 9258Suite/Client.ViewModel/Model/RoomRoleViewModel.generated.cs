




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
	public partial class RoomRoleViewModel
	{
	

		/// <summary>
		/// Field which backs the RoomId property
		/// </summary>
		private HistoryableProperty<int> roomId = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the RoomId value
		/// </summary>
		[Browsable(false)]
		public  int RoomId
		{
			get { return roomId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(roomId, value, () => RoomId ); }
		}


		/// <summary>
		/// Field which backs the UserId property
		/// </summary>
		private HistoryableProperty<int> userId = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the UserId value
		/// </summary>
		[Browsable(false)]
		public  int UserId
		{
			get { return userId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(userId, value, () => UserId ); }
		}


		/// <summary>
		/// Field which backs the RoleId property
		/// </summary>
		private HistoryableProperty<int> roleId = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the RoleId value
		/// </summary>
		[Browsable(false)]
		public  int RoleId
		{
			get { return roleId.GetValue(); }
			set { ChangeAndNotifyHistory<int>(roleId, value, () => RoleId ); }
		}
	}
}
	