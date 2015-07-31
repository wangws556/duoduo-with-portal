




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
	public partial class ImageGroupViewModel
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
		/// Field which backs the ImageVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<ImageViewModel>> imageVMs = new HistoryableProperty<ObservableCollection<ImageViewModel>>(null);

		/// <summary>
		/// Gets / sets the ImageVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<ImageViewModel> ImageVMs
		{
			get { return imageVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<ImageViewModel>>(imageVMs, value, () => ImageVMs ); }
		}
	}
}
	