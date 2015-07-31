using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using NAudio.Wave.Compression;

namespace YoYoStudio.Media.NAudio
{
    public class WaveInRecorder : Recorder
    {
        internal WaveInRecorder(INetworkChatCodec c,int devIndex = 0)
        {
			deviceNumber = devIndex;
            codec = c;
			waveIn = new WaveIn() { DeviceNumber = deviceNumber, WaveFormat = codec.RecordFormat };		
        }
    }

    public class WasapiLoopbackRecorder : Recorder
    {
        AcmStream convertionStream = null;

        internal WasapiLoopbackRecorder(INetworkChatCodec c)
        {
            deviceNumber = -1;
            codec = c;
            waveIn = new WasapiLoopbackCapture();
            convertionStream = new AcmStream(new WaveFormat(waveIn.WaveFormat.SampleRate, 16, waveIn.WaveFormat.Channels), codec.RecordFormat);
        }

        protected override byte[] ProcessRecordedData(byte[] recorded, int offset, int count)
        {
            lock (lockObject)
            {
                int sourceSamples = count / 4;
                int outSamples = sourceSamples;
                byte[] sourceBuffer = new byte[sourceSamples * 4];
                Buffer.BlockCopy(recorded, offset, sourceBuffer, 0, sourceSamples * 4);
                byte[] outBuffer = new byte[outSamples * 2];
                WaveBuffer sourceWaveBuffer = new WaveBuffer(sourceBuffer);
                WaveBuffer destWaveBuffer = new WaveBuffer(outBuffer);
                int destOffset = 0;
                for (int sample = 0; sample < sourceSamples; sample++)
                {
                    #region 32 bit float to 16bit short, same channels

                    float sample32 = sourceWaveBuffer.FloatBuffer[sample] * 1.0f;              // clip
                    if (sample32 > 1.0f)
                        sample32 = 1.0f;
                    if (sample32 < -1.0f)
                        sample32 = -1.0f;
                    destWaveBuffer.ShortBuffer[destOffset++] = (short)(sample32 * Int16.MaxValue);

                    #endregion

                    #region 32 bit float to 16bit short, combine left and right channels
                    // adjust volume
                    //float leftSample32 = sourceWaveBuffer.FloatBuffer[sample] * 1.0f;              // clip
                    //if (leftSample32 > 1.0f)
                    //    leftSample32 = 1.0f;
                    //if (leftSample32 < -1.0f)
                    //    leftSample32 = -1.0f;

                    //float rightSample32 = sourceWaveBuffer.FloatBuffer[sample + 1] * 1.0f;

                    //if (rightSample32 > 1.0f)
                    //    rightSample32 = 1.0f;
                    //if (rightSample32 < -1.0f)
                    //    rightSample32 = -1.0f;

                    //short spl = (short)(((leftSample32 * Int16.MaxValue) + (int)(rightSample32 * Int16.MaxValue)) / 2);

                    //destWaveBuffer.ShortBuffer[destOffset++] = spl;
                    #endregion
                }
                Array.Copy(destWaveBuffer.ByteBuffer, convertionStream.SourceBuffer, outSamples * 2);
                int converted = convertionStream.Convert(outSamples * 2);
                converted -= converted % 4;
                byte[] result = new byte[converted];
                Array.Copy(convertionStream.DestBuffer, result, converted);
                return base.ProcessRecordedData(result, 0, converted);
            }
        }
    }
}
