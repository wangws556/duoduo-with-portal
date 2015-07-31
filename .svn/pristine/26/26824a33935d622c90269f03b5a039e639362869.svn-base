




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
	public partial class SmileManagementViewModel
	{
	

		/// <summary>
		/// Field which backs the ImageGroups property
		/// </summary>
		private HistoryableProperty<ObservableCollection<string>> imageGroups = new HistoryableProperty<ObservableCollection<string>>(new ObservableCollection<string>());

		/// <summary>
		/// Gets / sets the ImageGroups value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<string> ImageGroups
		{
			get { return imageGroups.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<string>>(imageGroups, value, () => ImageGroups ); }
		}


		/// <summary>
		/// Field which backs the SelectedImageGroup property
		/// </summary>
		private HistoryableProperty<string> selectedImageGroup = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the SelectedImageGroup value
		/// </summary>
		[Browsable(false)]
		public  string SelectedImageGroup
		{
			get { return selectedImageGroup.GetValue(); }
			set { ChangeAndNotifyHistory<string>(selectedImageGroup, value, () => SelectedImageGroup ); }
		}


		/// <summary>
		/// Field which backs the NewGroup property
		/// </summary>
		private HistoryableProperty<string> newGroup = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the NewGroup value
		/// </summary>
		[Browsable(false)]
		public  string NewGroup
		{
			get { return newGroup.GetValue(); }
			set { ChangeAndNotifyHistory<string>(newGroup, value, () => NewGroup ); }
		}
	}
}
	