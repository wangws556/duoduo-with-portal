using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Media.NAudio
{
    public class WaveOutPlayer : NetworkPlayer
    {
        public WaveOutPlayer(INetworkChatCodec c):base(c)
        {
            waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            wavePlayer = new WaveOut();
            wavePlayer.Init(waveProvider);
        }
    }

	public class DirectSoundPlayer : NetworkPlayer
	{
		public DirectSoundPlayer(INetworkChatCodec c)
			: base(c)
		{
			waveProvider = new BufferedWaveProvider(codec.RecordFormat);
			wavePlayer = new DirectSoundOut();
			wavePlayer.Init(waveProvider);
		}
	}
}
