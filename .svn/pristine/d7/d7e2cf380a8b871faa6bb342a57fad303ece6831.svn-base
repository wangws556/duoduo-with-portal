




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
	public partial class ApplicationViewModel
	{
	

		/// <summary>
		/// Field which backs the HallWindowVM property
		/// </summary>
		private HistoryableProperty<HallWindowViewModel> hallWindowVM = new HistoryableProperty<HallWindowViewModel>(new HallWindowViewModel());

		/// <summary>
		/// Gets / sets the HallWindowVM value
		/// </summary>
		[Browsable(false)]
		public  HallWindowViewModel HallWindowVM
		{
			get { return hallWindowVM.GetValue(); }
			set { ChangeAndNotifyHistory<HallWindowViewModel>(hallWindowVM, value, () => HallWindowVM ); }
		}


		/// <summary>
		/// Field which backs the RoomWindowVM property
		/// </summary>
		private HistoryableProperty<RoomWindowViewModel> roomWindowVM = new HistoryableProperty<RoomWindowViewModel>(null);

		/// <summary>
		/// Gets / sets the RoomWindowVM value
		/// </summary>
		[Browsable(false)]
		public  RoomWindowViewModel RoomWindowVM
		{
			get { return roomWindowVM.GetValue(); }
			set { ChangeAndNotifyHistory<RoomWindowViewModel>(roomWindowVM, value, () => RoomWindowVM ); }
		}


		/// <summary>
		/// Field which backs the RegisterWindowVM property
		/// </summary>
		private HistoryableProperty<RegisterWindowViewModel> registerWindowVM = new HistoryableProperty<RegisterWindowViewModel>(null);

		/// <summary>
		/// Gets / sets the RegisterWindowVM value
		/// </summary>
		[Browsable(false)]
		public  RegisterWindowViewModel RegisterWindowVM
		{
			get { return registerWindowVM.GetValue(); }
			set { ChangeAndNotifyHistory<RegisterWindowViewModel>(registerWindowVM, value, () => RegisterWindowVM ); }
		}


		/// <summary>
		/// Field which backs the ConfigurationWindowVM property
		/// </summary>
		private HistoryableProperty<ConfigurationWindowViewModel> configurationWindowVM = new HistoryableProperty<ConfigurationWindowViewModel>(null);

		/// <summary>
		/// Gets / sets the ConfigurationWindowVM value
		/// </summary>
		[Browsable(false)]
		public  ConfigurationWindowViewModel ConfigurationWindowVM
		{
			get { return configurationWindowVM.GetValue(); }
			set { ChangeAndNotifyHistory<ConfigurationWindowViewModel>(configurationWindowVM, value, () => ConfigurationWindowVM ); }
		}


		/// <summary>
		/// Field which backs the LogoImagePath property
		/// </summary>
		private HistoryableProperty<string> logoImagePath = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the LogoImagePath value
		/// </summary>
		[Browsable(false)]
		public  string LogoImagePath
		{
			get { return logoImagePath.GetValue(); }
			set { ChangeAndNotifyHistory<string>(logoImagePath, value, () => LogoImagePath ); }
		}


		/// <summary>
		/// Field which backs the IsAuthenticated property
		/// </summary>
		private HistoryableProperty<bool> isAuthenticated = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the IsAuthenticated value
		/// </summary>
		[Browsable(false)]
		public  bool IsAuthenticated
		{
			get { return isAuthenticated.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(isAuthenticated, value, () => IsAuthenticated ); }
		}
	}
}
	