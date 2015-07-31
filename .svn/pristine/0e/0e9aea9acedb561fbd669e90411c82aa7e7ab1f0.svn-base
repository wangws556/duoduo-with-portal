




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
	public partial class RoleViewModel
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
		/// Field which backs the Order property
		/// </summary>
		private HistoryableProperty<int> order = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the Order value
		/// </summary>
		[Browsable(false)]
		public  int Order
		{
			get { return order.GetValue(); }
			set { ChangeAndNotifyHistory<int>(order, value, () => Order ); }
		}


		/// <summary>
		/// Field which backs the RoleCommandVMs property
		/// </summary>
		private HistoryableProperty<ObservableCollection<RoleCommandViewModel>> roleCommandVMs = new HistoryableProperty<ObservableCollection<RoleCommandViewModel>>(null);

		/// <summary>
		/// Gets / sets the RoleCommandVMs value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<RoleCommandViewModel> RoleCommandVMs
		{
			get { return roleCommandVMs.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<RoleCommandViewModel>>(roleCommandVMs, value, () => RoleCommandVMs ); }
		}
	}
}
	