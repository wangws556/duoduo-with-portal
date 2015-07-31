




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
	public partial class PlayMusicWindowViewModel
	{
	

		/// <summary>
		/// Field which backs the PlayMusicLabel property
		/// </summary>
		private HistoryableProperty<string> playMusicLabel = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the PlayMusicLabel value
		/// </summary>
		[Browsable(false)]
		public  string PlayMusicLabel
		{
			get { return playMusicLabel.GetValue(); }
			set { ChangeAndNotifyHistory<string>(playMusicLabel, value, () => PlayMusicLabel ); }
		}


		/// <summary>
		/// Field which backs the MusicItems property
		/// </summary>
		private HistoryableProperty<ObservableCollection<string>> musicItems = new HistoryableProperty<ObservableCollection<string>>(new ObservableCollection<string>());

		/// <summary>
		/// Gets / sets the MusicItems value
		/// </summary>
		[Browsable(false)]
		public  ObservableCollection<string> MusicItems
		{
			get { return musicItems.GetValue(); }
			set { ChangeAndNotifyHistory<ObservableCollection<string>>(musicItems, value, () => MusicItems ); }
		}


		/// <summary>
		/// Field which backs the SelectedMusic property
		/// </summary>
		private HistoryableProperty<string> selectedMusic = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the SelectedMusic value
		/// </summary>
		[Browsable(false)]
		public  string SelectedMusic
		{
			get { return selectedMusic.GetValue(); }
			set { ChangeAndNotifyHistory<string>(selectedMusic, value, () => SelectedMusic ); }
		}


		/// <summary>
		/// Field which backs the MusicRtmpUrl property
		/// </summary>
		private HistoryableProperty<string> musicRtmpUrl = new HistoryableProperty<string>(string.Empty);

		/// <summary>
		/// Gets / sets the MusicRtmpUrl value
		/// </summary>
		[Browsable(false)]
		public  string MusicRtmpUrl
		{
			get { return musicRtmpUrl.GetValue(); }
			set { ChangeAndNotifyHistory<string>(musicRtmpUrl, value, () => MusicRtmpUrl ); }
		}
	}
}
	