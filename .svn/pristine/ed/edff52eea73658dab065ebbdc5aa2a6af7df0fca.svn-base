using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snippets;
using YoYoStudio.Resource;
using YoYoStudio.Common.Notification;
using YoYoStudio.SocketService.Music;
using System.ComponentModel;
using YoYoStudio.Common;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field="playMusicLabel",property="PlayMusicLabel",type="string",defaultValue="string.Empty")]
    [SnippetPropertyINPC(field = "musicItems", property = "MusicItems", type = "ObservableCollection<string>", defaultValue = "new ObservableCollection<string>()")]
    [SnippetPropertyINPC(field = "selectedMusic", property = "SelectedMusic", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "musicRtmpUrl", property = "MusicRtmpUrl", type = "string", defaultValue = "string.Empty")]
    public partial class PlayMusicWindowViewModel:WindowViewModel
    {
        public RoomWindowViewModel RoomWindowVM { get { return Singleton<ApplicationViewModel>.Instance.RoomWindowVM; } }

        public delegate void InitMusics(object obj, EventArgs arg);
        public event InitMusics MusicsReadyEvent;
        public PlayMusicWindowViewModel():base()
        {
            MusicRtmpUrl = "rtmp://" + System.Configuration.ConfigurationManager.AppSettings["MusicServiceIp"] + "/oflaDemo";
        }

        protected void OnMusicsReady(EventArgs args)
        {
            if (MusicsReadyEvent != null)
                MusicsReadyEvent(this, args);
        }

        protected override void InitializeResource()
        {
            BusyMessage = Text.LoadingMusic;
            PlayMusicLabel = Text.PlayMusicLabel;
            Title = PlayMusicLabel;
            PlayMusicLabel = Text.PlayMusicLabel;
            base.InitializeResource();
        }
    }
}
