using Snippets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.ObjectHistory;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "mirror", property = "Mirror", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "cameras", property = "Cameras", type = "ObservableCollection<string>", defaultValue = "new ObservableCollection<string>()")]
    public partial class VideoConfigurationViewModel : ConfigurationViewModel
    {
        public VideoConfigurationViewModel(VideoConfiguration config)
            : base(config)
        {
        }

        public override void Save()
        {
            var config = GetConcreteConfiguration<VideoConfiguration>();
            config.CameraIndex = CameraIndex;
            config.Mirror = Mirror;
            base.Save();
        }

        public override void Reset()
        {
            var config = GetConcreteConfiguration<VideoConfiguration>();
            cameraIndex.SetValue(config.CameraIndex);
            mirror.SetValue(config.Mirror);
            base.Reset();
        }

        public string MirrorLabel { get; private set; }
        public string RefreshLabel { get; private set; }
        public string VideoQualityLabel { get; private set; }

        protected override void InitializeResource()
        {
            title = Text.VideoConfiguration;
            MirrorLabel = Text.MirrorLabel;
            RefreshLabel = Text.RefreshLabel;
            VideoQualityLabel = Text.VideoQualityLabel;
            base.InitializeResource();
        }

        public SecureCommand RefreshCommand { get; set; }

        private void RefreshCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.VideoRefresh));
        }

        /// <summary>
        /// Field which backs the CameraIndex property
        /// </summary>
        private HistoryableProperty<int> cameraIndex = new HistoryableProperty<int>(0);

        /// <summary>
        /// Gets / sets the CameraIndex value
        /// </summary>
        [Browsable(false)]
        public int CameraIndex
        {
            get { return cameraIndex.GetValue(); }
            set
            {
                ChangeAndNotifyHistory<int>(cameraIndex, value, () => CameraIndex);
                Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.CameraIndexChanged, CameraIndex));
            }
        }
    }
}
