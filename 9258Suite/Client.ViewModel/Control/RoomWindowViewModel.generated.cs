




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
	public partial class RoomWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the SelectedUserVM property
		/// </summary>
		private HistoryableProperty<UserViewModel> selectedUserVM = new HistoryableProperty<UserViewModel>(null);

		/// <summary>
		/// Gets / sets the SelectedUserVM value
		/// </summary>
		[Browsable(false)]
		public  UserViewModel SelectedUserVM
		{
			get { return selectedUserVM.GetValue(); }
			set { ChangeAndNotifyHistory<UserViewModel>(selectedUserVM, value, () => SelectedUserVM ); }
		}


		/// <summary>
		/// Field which backs the FontFamilies property
		/// </summary>
		private HistoryableProperty<ObservableCollection<string>> fontFamilies = new HistoryableProperty<ObservableCollection<string>>(null);

		/// <summary>
		/// Gets / sets the FontFamilies value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<string> FontFamilies
		{
			get { return fontFamilies.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<string>>(fontFamilies, value, () => FontFamilies ); }
		}


		/// <summary>
		/// Field which backs the FontSizes property
		/// </summary>
		private HistoryableProperty<ObservableCollection<int>> fontSizes = new HistoryableProperty<ObservableCollection<int>>(null);

		/// <summary>
		/// Gets / sets the FontSizes value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<int> FontSizes
		{
			get { return fontSizes.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<int>>(fontSizes, value, () => FontSizes ); }
		}


		/// <summary>
		/// Field which backs the UserVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<UserViewModel>> userVMs = new HistoryableProperty<ObservableCollection<UserViewModel>>(null);

		/// <summary>
		/// Gets / sets the UserVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<UserViewModel> UserVMs
		{
			get { return userVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<UserViewModel>>(userVMs, value, () => UserVMs ); }
		}


		/// <summary>
		/// Field which backs the PrivateMicUserVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<UserViewModel>> privateMicUserVMs = new HistoryableProperty<ObservableCollection<UserViewModel>>(null);

		/// <summary>
		/// Gets / sets the PrivateMicUserVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<UserViewModel> PrivateMicUserVMs
		{
			get { return privateMicUserVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<UserViewModel>>(privateMicUserVMs, value, () => PrivateMicUserVMs ); }
		}


		/// <summary>
		/// Field which backs the SecretMicUserVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<UserViewModel>> secretMicUserVMs = new HistoryableProperty<ObservableCollection<UserViewModel>>(null);

		/// <summary>
		/// Gets / sets the SecretMicUserVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<UserViewModel> SecretMicUserVMs
		{
			get { return secretMicUserVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<UserViewModel>>(secretMicUserVMs, value, () => SecretMicUserVMs ); }
		}


		/// <summary>
		/// Field which backs the GiftGroupVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<GiftGroupViewModel>> giftGroupVMs = new HistoryableProperty<ObservableCollection<GiftGroupViewModel>>(null);

		/// <summary>
		/// Gets / sets the GiftGroupVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<GiftGroupViewModel> GiftGroupVMs
		{
			get { return giftGroupVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<GiftGroupViewModel>>(giftGroupVMs, value, () => GiftGroupVMs ); }
		}


		/// <summary>
		/// Field which backs the QueueMicUserVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<UserViewModel>> queueMicUserVMs = new HistoryableProperty<ObservableCollection<UserViewModel>>(null);

		/// <summary>
		/// Gets / sets the QueueMicUserVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<UserViewModel> QueueMicUserVMs
		{
			get { return queueMicUserVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<UserViewModel>>(queueMicUserVMs, value, () => QueueMicUserVMs ); }
		}


		/// <summary>
		/// Field which backs the MotionImageVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<ImageViewModel>> motionImageVMs = new HistoryableProperty<ObservableCollection<ImageViewModel>>(null);

		/// <summary>
		/// Gets / sets the MotionImageVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<ImageViewModel> MotionImageVMs
		{
			get { return motionImageVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<ImageViewModel>>(motionImageVMs, value, () => MotionImageVMs ); }
		}


		/// <summary>
		/// Field which backs the StampImageVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<ImageViewModel>> stampImageVMs = new HistoryableProperty<ObservableCollection<ImageViewModel>>(null);

		/// <summary>
		/// Gets / sets the StampImageVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<ImageViewModel> StampImageVMs
		{
			get { return stampImageVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<ImageViewModel>>(stampImageVMs, value, () => StampImageVMs ); }
		}


		/// <summary>
		/// Field which backs the RoomVM property
		/// </summary>
		private HistoryableProperty<RoomViewModel> roomVM = new HistoryableProperty<RoomViewModel>(null);

		/// <summary>
		/// Gets / sets the RoomVM value
		/// </summary>
		[Browsable(false)]
		public  RoomViewModel RoomVM
		{
			get { return roomVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoomViewModel>(roomVM, value, () => RoomVM ); }
		}


		/// <summary>
		/// Field which backs the VideoWidth property
		/// </summary>
		private HistoryableProperty<double> videoWidth = new HistoryableProperty<double>(0);

		/// <summary>
		/// Gets / sets the VideoWidth value
		/// </summary>
		[Browsable(false)]
		public  double VideoWidth
		{
			get { return videoWidth.GetValue(); }
			set { ChangeAndNotifyHistory<double>(videoWidth, value, () => VideoWidth ); }
		}


		/// <summary>
		/// Field which backs the VideoHeight property
		/// </summary>
		private HistoryableProperty<double> videoHeight = new HistoryableProperty<double>(0);

		/// <summary>
		/// Gets / sets the VideoHeight value
		/// </summary>
		[Browsable(false)]
		public  double VideoHeight
		{
			get { return videoHeight.GetValue(); }
			set { ChangeAndNotifyHistory<double>(videoHeight, value, () => VideoHeight ); }
		}
	}
}
	