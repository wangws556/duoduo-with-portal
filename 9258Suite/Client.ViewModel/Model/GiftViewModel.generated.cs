




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
	public partial class GiftViewModel
	{
	

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
		/// Field which backs the Price property
		/// </summary>
		private HistoryableProperty<int> price = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Price value
		/// </summary>
		[Browsable(false)]
		public  int Price
		{
			get { return price.GetValue(); }
			set { ChangeAndNotifyHistory<int>(price, value, () => Price ); }
		}


		/// <summary>
		/// Field which backs the Score property
		/// </summary>
		private HistoryableProperty<int> score = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Score value
		/// </summary>
		[Browsable(false)]
		public  int Score
		{
			get { return score.GetValue(); }
			set { ChangeAndNotifyHistory<int>(score, value, () => Score ); }
		}


		/// <summary>
		/// Field which backs the Money property
		/// </summary>
		private HistoryableProperty<int> money = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Money value
		/// </summary>
		[Browsable(false)]
		public  int Money
		{
			get { return money.GetValue(); }
			set { ChangeAndNotifyHistory<int>(money, value, () => Money ); }
		}


		/// <summary>
		/// Field which backs the Unit property
		/// </summary>
		private HistoryableProperty<string> unit = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Unit value
		/// </summary>
		[Browsable(false)]
		public  string Unit
		{
			get { return unit.GetValue(); }
			set { ChangeAndNotifyHistory<string>(unit, value, () => Unit ); }
		}


		/// <summary>
		/// Field which backs the Description property
		/// </summary>
		private HistoryableProperty<string> description = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Description value
		/// </summary>
		[Browsable(false)]
		public  string Description
		{
			get { return description.GetValue(); }
			set { ChangeAndNotifyHistory<string>(description, value, () => Description ); }
		}


		/// <summary>
		/// Field which backs the GiftGroupVM property
		/// </summary>
		private HistoryableProperty<GiftGroupViewModel> giftGroupVM = new HistoryableProperty<GiftGroupViewModel>(null);

		/// <summary>
		/// Gets / sets the GiftGroupVM value
		/// </summary>
		[Browsable(false)]
		public  GiftGroupViewModel GiftGroupVM
		{
			get { return giftGroupVM.GetValue(); }
			set { ChangeAndNotifyHistory<GiftGroupViewModel>(giftGroupVM, value, () => GiftGroupVM ); }
		}


		/// <summary>
		/// Field which backs the RunWay property
		/// </summary>
		private HistoryableProperty<int> runWay = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the RunWay value
		/// </summary>
		[Browsable(false)]
		public  int RunWay
		{
			get { return runWay.GetValue(); }
			set { ChangeAndNotifyHistory<int>(runWay, value, () => RunWay ); }
		}


		/// <summary>
		/// Field which backs the RoomBroadCast property
		/// </summary>
		private HistoryableProperty<int> roomBroadCast = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the RoomBroadCast value
		/// </summary>
		[Browsable(false)]
		public  int RoomBroadCast
		{
			get { return roomBroadCast.GetValue(); }
			set { ChangeAndNotifyHistory<int>(roomBroadCast, value, () => RoomBroadCast ); }
		}


		/// <summary>
		/// Field which backs the WorldBroadCast property
		/// </summary>
		private HistoryableProperty<int> worldBroadCast = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the WorldBroadCast value
		/// </summary>
		[Browsable(false)]
		public  int WorldBroadCast
		{
			get { return worldBroadCast.GetValue(); }
			set { ChangeAndNotifyHistory<int>(worldBroadCast, value, () => WorldBroadCast ); }
		}
	}
}
	