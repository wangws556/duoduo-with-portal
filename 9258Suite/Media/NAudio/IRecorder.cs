using NAudio.Mixer;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NAudio.Utils;
using System.IO;

namespace YoYoStudio.Media.NAudio
{
    public interface IRecorder
    {
        void Start(string file="");
        void Stop();
        WaveFormat Format { get; }
        INetworkChatCodec Codec { get; }
        bool SaveToFile { get; }
        int DeviceNumber { get; }
        event Action<List<byte[]>> SampleAvailableEvent;
		int NotifySize { get; }
    }

    public abstract class Recorder : IRecorder
    {
        //private FileStream fileStream = null;
        //private StreamWriter streamWriter = null;
        protected IWaveIn waveIn;
        protected WaveFileWriter fileWriter = null;
        protected bool saveToFile = false;
        protected INetworkChatCodec codec;
        protected int deviceNumber;
        protected object lockObject = new object();
		protected int notifySize = 1380;
        protected int recordedSize = 0;
        protected List<byte[]> buffer = new List<byte[]>();

		public int NotifySize { get { return notifySize; } }

        public event Action<List<byte[]>> SampleAvailableEvent;

        public void Start()
        {
            
        }

        void RecordingStoppedHandler(object sender, StoppedEventArgs e)
        {
            if (saveToFile)
            {
                fileWriter.Flush();
                fileWriter.Close();
                fileWriter = null;
            }
            //if (streamWriter != null)
            //{
            //    streamWriter.Dispose();
            //    streamWriter = null;
            //}
            //if (fileStream != null)
            //{
            //    fileStream.Dispose();
            //    fileStream = null;
            //}
            waveIn.DataAvailable -= DataAvailableHandler;
            waveIn.RecordingStopped -= RecordingStoppedHandler;
        }

        public virtual void Start(string file = "")
        {
            if (!string.IsNullOrEmpty(file))
            {
                fileWriter = new WaveFileWriter(file, codec.RecordFormat);
                saveToFile = true;
            }
            //if (fileStream == null)
            //{
            //    fileStream = new FileStream(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\AudioLog.txt", FileMode.Create);
            //}
            //if (streamWriter == null)
            //{
            //    streamWriter = new StreamWriter(fileStream);
            //}
            waveIn.RecordingStopped += RecordingStoppedHandler;
            waveIn.DataAvailable += DataAvailableHandler;
            waveIn.StartRecording();
        }

		protected virtual byte[] ProcessRecordedData(byte[] recorded, int offset, int count)
		{
			lock (lockObject)
			{
				if (saveToFile && fileWriter != null)
				{
					fileWriter.Write(recorded, offset, count);
				}
				return codec.Encode(recorded, offset, count);
			}
		}

        private void DataAvailableHandler(object sender, WaveInEventArgs e)
        {
            lock (lockObject)
            {
                if (e != null && e.Buffer != null && e.BytesRecorded > 0)
                {
                    //streamWriter.WriteLine("Raw data: " + e.BytesRecorded);
					var processedData = ProcessRecordedData(e.Buffer, 0, e.BytesRecorded);
                    //streamWriter.WriteLine("Encoded data: " + processedData.Count());
                    if (SampleAvailableEvent != null)
                    {
                        if (recordedSize + processedData.Length > notifySize)
                        {
                            RaiseSampleAvailableEvent(buffer);
                            buffer = new List<byte[]>();
                            recordedSize = 0;
                        }
                        buffer.Add(processedData);
                        recordedSize += processedData.Length;
                    }
                }
            }
        }

        protected void RaiseSampleAvailableEvent(List<byte[]> datas)
        {
			lock (lockObject)
			{
                if (SampleAvailableEvent != null)
                {
                    SampleAvailableEvent(datas);
                }
            }
        }

        public void Stop()
        {
            waveIn.StopRecording();
        }

        public WaveFormat Format
        {
            get { return codec.RecordFormat; }
        }

        public INetworkChatCodec Codec
        {
            get { return codec; }
        }

        public bool SaveToFile
        {
            get { return saveToFile; }
        }

        public int DeviceNumber
        {
            get { return deviceNumber; }
        }

        private void TryGetVolumeControl()
        {
            //int waveInDeviceNumber = WaveIn.DeviceNumber;
            //if (Environment.OSVersion.Version.Major >= 6) // Vista and over
            //{
            //    var mixerLine = WaveIn.GetMixerLine();
            //    //new MixerLine((IntPtr)waveInDeviceNumber, 0, MixerFlags.WaveIn);
            //    foreach (var control in mixerLine.Controls)
            //    {
            //        if (control.ControlType == MixerControlType.Volume)
            //        {
            //            this.volumeControl = control as UnsignedMixerControl;
            //            MicrophoneLevel = desiredVolume;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    var mixer = new Mixer(waveInDeviceNumber);
            //    foreach (var destination in mixer.Destinations
            //        .Where(d => d.ComponentType == MixerLineComponentType.DestinationWaveIn))
            //    {
            //        foreach (var source in destination.Sources
            //            .Where(source => source.ComponentType == MixerLineComponentType.SourceMicrophone))
            //        {
            //            foreach (var control in source.Controls
            //                .Where(control => control.ControlType == MixerControlType.Volume))
            //            {
            //                volumeControl = control as UnsignedMixerControl;
            //                MicrophoneLevel = desiredVolume;
            //                break;
            //            }
            //        }
            //    }
            //}

        }
    }
}
