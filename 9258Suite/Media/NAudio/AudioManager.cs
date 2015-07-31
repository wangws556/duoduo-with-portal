using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Media.NAudio
{
	public enum AudioCodec
	{
		AcmALaw,
		ALaw,
		G722,
		Gsm610,
		MicrosoftAdpcm,
		MuLaw,
		NarrowBandSpeex,
		WideBandSpeex,
		UltraWideBandSpeex,
		TrueSpeech,
		UnCompressedPcm
	}

	public enum PlayerType
	{
		WaveOut,
		DirectSound
	}

	public class AudioManager
	{
        static MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
        static MMDeviceCollection captureDevices;
        static MMDeviceCollection renderDevices;

        static AudioManager()
        {
             captureDevices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
             renderDevices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        }




		public static INetworkChatCodec GetCodec(AudioCodec codec)
		{
			switch (codec)
			{
				case AudioCodec.AcmALaw:
					return new AcmALawChatCodec();
				case AudioCodec.ALaw:
					return new ALawChatCodec();
				case  AudioCodec.G722:
					return new G722ChatCodec();
				case AudioCodec.Gsm610:
					return new Gsm610ChatCodec();
				case AudioCodec.MicrosoftAdpcm:
					return new MicrosoftAdpcmChatCodec();
				case AudioCodec.MuLaw:
					return new MuLawChatCodec();
				case AudioCodec.NarrowBandSpeex:
					return new NarrowBandSpeexCodec();
				case AudioCodec.WideBandSpeex:
					return new WideBandSpeexCodec();
				case AudioCodec.UltraWideBandSpeex:
					return new UltraWideBandSpeexCodec();
				case AudioCodec.TrueSpeech:
					return new TrueSpeechChatCodec();
				case AudioCodec.UnCompressedPcm:
					return new UncompressedPcmChatCodec();
				default:
					return null;
			}
		}

		public static IRecorder CreateRecorder(AudioCodec codec, bool loopback, int recordDeviceNumber=0)
		{
            if (loopback)
            {
                return new WasapiLoopbackRecorder(GetCodec(codec));
            }
            else
            {
                return new WaveInRecorder(GetCodec(codec), recordDeviceNumber);
            }
		}

		public static INetworkPlayer CreatePlayer(AudioCodec codec, PlayerType type)
		{
			switch (type)
			{
				case PlayerType.DirectSound:
					return new DirectSoundPlayer(GetCodec(codec));
				case PlayerType.WaveOut:
					return new WaveOutPlayer(GetCodec(codec));
				default:
					return null;
			}
		}
	}
}
