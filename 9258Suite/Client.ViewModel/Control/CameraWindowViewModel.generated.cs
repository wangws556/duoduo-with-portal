




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
	public partial class CameraWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the CapturePhoto property
		/// </summary>
		private HistoryableProperty<string> capturePhoto = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the CapturePhoto value
		/// </summary>
		[Browsable(false)]
		public  string CapturePhoto
		{
			get { return capturePhoto.GetValue(); }
			set { ChangeAndNotifyHistory<string>(capturePhoto, value, () => CapturePhoto ); }
		}


		/// <summary>
		/// Field which backs the Save property
		/// </summary>
		private HistoryableProperty<string> save = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the Save value
		/// </summary>
		[Browsable(false)]
		public  string Save
		{
			get { return save.GetValue(); }
			set { ChangeAndNotifyHistory<string>(save, value, () => Save ); }
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


		/// <summary>
		/// Field which backs the CanSave property
		/// </summary>
		private HistoryableProperty<bool> canSave = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the CanSave value
		/// </summary>
		[Browsable(false)]
		public  bool CanSave
		{
			get { return canSave.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(canSave, value, () => CanSave ); }
		}
	}
}
	