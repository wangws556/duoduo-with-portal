using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Resource;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common;
using System.ComponentModel;
using YoYoStudio.SocketService.Music;

namespace YoYoStudio.Client.ViewModel
{
    public partial class ManageMusicWindowViewModel:WindowViewModel
    {
        public ManageMusicWindowViewModel()
            : base()
        { }

        public string UploadMusicLabel { get; set; }
        public string DeleteMusicLabel { get; set; }
        private List<string> songs { get; set; }
        private BackgroundWorker worker = new BackgroundWorker();
        public System.Collections.ObjectModel.ObservableCollection<MusicDataBinding> Musics = new System.Collections.ObjectModel.ObservableCollection<MusicDataBinding>();

        protected override void InitializeResource()
        {
            Title = Text.MusicLabel;
            UploadMusicLabel = Text.UploadMusicLabel;
            DeleteMusicLabel = Text.DeleteSelectedMusic;
            base.InitializeResource();
        }

        public void LoadMusicsFromServer()
        {
            worker.DoWork += (s, e) =>
            {
                songs = LoadMusics();
            };
            worker.RunWorkerCompleted += (s, e) =>
            {
                songs.ForEach((r) =>
                {
                    Musics.Add(new MusicDataBinding { Name = r, IsSelected = false });
                }
                );

                Messenger.Default.Send<EnumNotificationMessage<object, ManageMusicWindowAction>>(
               new EnumNotificationMessage<object, ManageMusicWindowAction>(ManageMusicWindowAction.LoadMusicComplete));
            };
            worker.RunWorkerAsync();
        }

        private List<string> LoadMusics()
        {
            TcpAsynchronousClient client = new TcpAsynchronousClient();
            return client.GetMusics();
        }

        protected override void InitializeCommand()
        {
            base.InitializeCommand();
            UploadMusicCommand = new SecureCommand(UploadMusicCommandExecute, CanUploadMusicCommandExecute);
            DeleteMusicCommand = new SecureCommand(DeleteMusicCommandExecute, CanDeleteMusicCommandExecute);
        }

        public SecureCommand UploadMusicCommand { get; set; }
        private void UploadMusicCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, ManageMusicWindowAction>>(new EnumNotificationMessage<object, ManageMusicWindowAction>(ManageMusicWindowAction.UploadMusic));
        }
        private bool CanUploadMusicCommandExecute(SecureCommandArgs args)
        {
            return !worker.IsBusy;
        }

        public bool UploadMusic(string path)
        {
            YoYoStudio.SocketService.Music.TcpAsynchronousClient tcpClient = new YoYoStudio.SocketService.Music.TcpAsynchronousClient(path);
            bool result = tcpClient.UploadFile();
            if (result)
            {
                MusicDataBinding bind = new MusicDataBinding { Name = System.IO.Path.GetFileName(path), IsSelected = false };
                Musics.Add(bind);
            }
            return result;
        }

        public SecureCommand DeleteMusicCommand { get; set; }
        private void DeleteMusicCommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, ManageMusicWindowAction>>(new EnumNotificationMessage<object, ManageMusicWindowAction>(ManageMusicWindowAction.DeleteMusic));
        }
        private bool CanDeleteMusicCommandExecute(SecureCommandArgs args)
        {
            return Musics.Where(r => r.IsSelected == true).ToList().Count > 0;
        }
        public void DeleteMusic()
        {
            YoYoStudio.SocketService.Music.TcpAsynchronousClient tcpClient = new YoYoStudio.SocketService.Music.TcpAsynchronousClient();
            List<string> toDelete = new List<string>();
            List<MusicDataBinding> toRemove = new List<MusicDataBinding>();
            foreach (var r in Musics)
            {
                if (r.IsSelected == true)
                {
                    toDelete.Add(r.Name);
                    toRemove.Add(r);
                }
            }
            tcpClient.DeleteFile(toDelete);
            toRemove.ForEach(
                r => {
                    Musics.Remove(r);
                }
                );
        }
    }

    public class MusicDataBinding:INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChange("Name");
            }
        }

        private bool isSelected;
        public bool IsSelected 
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChange("IsSelected");
            }
        }

        private void OnPropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
