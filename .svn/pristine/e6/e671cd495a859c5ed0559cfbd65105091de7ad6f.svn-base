using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Media.NAudio
{
    public interface IPlayer : IDisposable
    {
        WaveFormat Format { get; }
        INetworkChatCodec Codec { get; }
		float Volume { get; set; }
        void Play();
        void Play(string file);
        void Stop();
        void Pause();
        void Resume();        
    }

    public interface INetworkPlayer : IPlayer
    {
        void AddSample(byte[] sample, int offset, int count);
    }

    public abstract class Player : IPlayer
    {
        protected INetworkChatCodec codec;
        protected IWavePlayer wavePlayer;
        protected IWaveProvider waveProvider;

        public void Play()
        {
            wavePlayer.Play();
        }

		public float Volume
		{
			get
			{
				return wavePlayer.Volume;
			}
			set
			{
				wavePlayer.Volume = value;
			}
		}

        public void Play(string file)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            wavePlayer.Stop();
        }

        public void Dispose()
        {
            wavePlayer.Dispose();
            wavePlayer.Dispose();
        }


        public void Pause()
        {
            wavePlayer.Pause();
        }

        public void Resume()
        {
            wavePlayer.Play();
        }

        public WaveFormat Format
        {
            get { return codec.RecordFormat; }
        }

        public INetworkChatCodec Codec
        {
            get { return codec; }
        }
    }

    public abstract class NetworkPlayer :Player, INetworkPlayer
    {
		internal NetworkPlayer(INetworkChatCodec c)
		{
			codec = c;
		}

        private BufferedWaveProvider bWaveProvider;
        protected BufferedWaveProvider bufferedWaveProvider
        {
            get
            {
                if (bWaveProvider == null)
                {
                    bWaveProvider = waveProvider as BufferedWaveProvider;
                }
                return bWaveProvider;
            }
        }

        public void AddSample(byte[] sample, int offset, int count)
        {
            if (sample != null)
            {
				var decoded = codec.Decode(sample, offset, count);
				bufferedWaveProvider.AddSamples(decoded, 0, decoded.Length);
            }
        }
    }
}
