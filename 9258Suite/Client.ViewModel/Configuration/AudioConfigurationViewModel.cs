using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "soundVolume", property = "SoundVolume", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "loopbackRecording", property = "LoopbackRecording", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "microphoneVolume", property = "MicrophoneVolume", type = "int", defaultValue = "0")]
    public partial class AudioConfigurationViewModel : ConfigurationViewModel
    {
        public AudioConfigurationViewModel(AudioConfiguration config)
            : base(config)
        {
            
        }

        public override void Save()
        {
            if (Dirty)
            {
                var config = GetConcreteConfiguration<AudioConfiguration>();
                config.SoundVolume = SoundVolume;
                config.MicrophoneVolume = MicrophoneVolume;
                config.LoopbackRecording = LoopbackRecording;
                base.Save();
            }
        }

        public override void Reset()
        {
            var config = GetConcreteConfiguration<AudioConfiguration>();
            soundVolume.SetValue(config.SoundVolume);
            microphoneVolume.SetValue(config.MicrophoneVolume);
            base.Reset();
        }

        protected override void InitializeResource()
        {
            title = Text.AudioConfiguration;
            base.InitializeResource();
        }
    }
}
