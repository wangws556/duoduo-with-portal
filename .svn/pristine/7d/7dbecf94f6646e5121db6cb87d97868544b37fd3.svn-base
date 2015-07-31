




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


namespace YoYoStudio.ManagementTool.ViewModel
{
	public partial class StampManagementViewModel
	{
	

		/// <summary>
		/// Field which backs the Images property
		/// </summary>
		private HistoryableProperty<ObservableCollection<ImageViewModel>> images = new HistoryableProperty<ObservableCollection<ImageViewModel>>(new ObservableCollection<ImageViewModel>());

		/// <summary>
		/// Gets / sets the Images value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<ImageViewModel> Images
		{
			get { return images.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<ImageViewModel>>(images, value, () => Images ); }
		}
	}
}
	