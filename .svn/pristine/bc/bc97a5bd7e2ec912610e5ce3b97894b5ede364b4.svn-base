




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
	public partial class VideoConfigurationViewModel
	{
	

		/// <summary>
		/// Field which backs the Mirror property
		/// </summary>
		private HistoryableProperty<bool> mirror = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the Mirror value
		/// </summary>
		[Browsable(false)]
		public  bool Mirror
		{
			get { return mirror.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(mirror, value, () => Mirror ); }
		}


		/// <summary>
		/// Field which backs the Cameras property
		/// </summary>
		private HistoryableProperty<ObservableCollection<string>> cameras = new HistoryableProperty<ObservableCollection<string>>(new ObservableCollection<string>());

		/// <summary>
		/// Gets / sets the Cameras value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<string> Cameras
		{
			get { return cameras.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<string>>(cameras, value, () => Cameras ); }
		}
	}
}
	