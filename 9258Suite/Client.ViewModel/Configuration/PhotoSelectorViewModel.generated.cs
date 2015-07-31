




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
	public partial class PhotoSelectorViewModel
	{
	

		/// <summary>
		/// Field which backs the IsCutting property
		/// </summary>
		private HistoryableProperty<bool> isCutting = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the IsCutting value
		/// </summary>
		[Browsable(false)]
		public  bool IsCutting
		{
			get { return isCutting.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(isCutting, value, () => IsCutting ); }
		}


		/// <summary>
		/// Field which backs the PhotoPath property
		/// </summary>
		private HistoryableProperty<string> photoPath = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the PhotoPath value
		/// </summary>
		[Browsable(false)]
		public  string PhotoPath
		{
			get { return photoPath.GetValue(); }
			set { ChangeAndNotifyHistory<string>(photoPath, value, () => PhotoPath ); }
		}


		/// <summary>
		/// Field which backs the PhotoSource property
		/// </summary>
		private HistoryableProperty<ImageSource> photoSource = new HistoryableProperty<ImageSource>(null);

		/// <summary>
		/// Gets / sets the PhotoSource value
		/// </summary>
		[Browsable(false)]
		public  ImageSource PhotoSource
		{
			get { return photoSource.GetValue(); }
			set { ChangeAndNotifyHistory<ImageSource>(photoSource, value, () => PhotoSource ); }
		}
	}
}
	