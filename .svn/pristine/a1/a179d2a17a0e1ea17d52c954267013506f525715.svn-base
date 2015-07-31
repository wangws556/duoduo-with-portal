




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
	public partial class AudioConfigurationViewModel
	{
	

		/// <summary>
		/// Field which backs the SoundVolume property
		/// </summary>
		private HistoryableProperty<int> soundVolume = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the SoundVolume value
		/// </summary>
		[Browsable(false)]
		public  int SoundVolume
		{
			get { return soundVolume.GetValue(); }
			set { ChangeAndNotifyHistory<int>(soundVolume, value, () => SoundVolume ); }
		}


		/// <summary>
		/// Field which backs the LoopbackRecording property
		/// </summary>
		private HistoryableProperty<bool> loopbackRecording = new HistoryableProperty<bool>(false);

		/// <summary>
		/// Gets / sets the LoopbackRecording value
		/// </summary>
		[Browsable(false)]
		public  bool LoopbackRecording
		{
			get { return loopbackRecording.GetValue(); }
			set { ChangeAndNotifyHistory<bool>(loopbackRecording, value, () => LoopbackRecording ); }
		}


		/// <summary>
		/// Field which backs the MicrophoneVolume property
		/// </summary>
		private HistoryableProperty<int> microphoneVolume = new HistoryableProperty<int>(0);

		/// <summary>
		/// Gets / sets the MicrophoneVolume value
		/// </summary>
		[Browsable(false)]
		public  int MicrophoneVolume
		{
			get { return microphoneVolume.GetValue(); }
			set { ChangeAndNotifyHistory<int>(microphoneVolume, value, () => MicrophoneVolume ); }
		}
	}
}
	