




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
	public partial class RoomGroupViewModel
	{
	

		/// <summary>
		/// Field which backs the RoomVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<RoomViewModel>> roomVMs = new HistoryableProperty<ObservableCollection<RoomViewModel>>(null);

		/// <summary>
		/// Gets / sets the RoomVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<RoomViewModel> RoomVMs
		{
			get { return roomVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<RoomViewModel>>(roomVMs, value, () => RoomVMs ); }
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
		/// Field which backs the SubRoomGroupVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<RoomGroupViewModel>> subRoomGroupVMs = new HistoryableProperty<ObservableCollection<RoomGroupViewModel>>(null);

		/// <summary>
		/// Gets / sets the SubRoomGroupVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<RoomGroupViewModel> SubRoomGroupVMs
		{
			get { return subRoomGroupVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<RoomGroupViewModel>>(subRoomGroupVMs, value, () => SubRoomGroupVMs ); }
		}


		/// <summary>
		/// Field which backs the ParentRoomGroupVM property
		/// </summary>
		private HistoryableProperty<RoomGroupViewModel> parentRoomGroupVM = new HistoryableProperty<RoomGroupViewModel>(null);

		/// <summary>
		/// Gets / sets the ParentRoomGroupVM value
		/// </summary>
		[Browsable(false)]
		public  RoomGroupViewModel ParentRoomGroupVM
		{
			get { return parentRoomGroupVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoomGroupViewModel>(parentRoomGroupVM, value, () => ParentRoomGroupVM ); }
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
	}
}
	