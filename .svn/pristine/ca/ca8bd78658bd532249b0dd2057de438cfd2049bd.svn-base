using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Common.Notification;
using System.Windows.Media.Imaging;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "isCutting", property = "IsCutting", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field = "photoPath", property = "PhotoPath", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "photoSource", property = "PhotoSource", type = "ImageSource", defaultValue = "null")]
    public partial class PhotoSelectorViewModel : ConfigurationViewModel
    {
        public SecureCommand OpenLocalImageCommand{get;set;}
        public SecureCommand CameraImageCommand{get;set;}
        public SecureCommand EndScreenCaptureCommand { get; set; }
        public SecureCommand StartScreenCaptureCommand { get; set; }

        public PhotoSelectorViewModel(PhotoSelectorConfiguration config)
        :base(config)
        { }

        public override void Initialize()
        {
            //Need to initialize resource after the user has been logged in.
            if (Me != null)
            {
				photoPath.SetValue(Me.ImageVM.GetAbsoluteFile(true));
                photoSource.SetValue(Utility.CreateBitmapSourceFromFile(PhotoPath));
            }
            base.Initialize();
        }

        public string Preview { get; private set; }
        public string SmallSclae { get; private set; }
        public string LargeScale { get; private set; }
        public string LocalPhoto { get; private set; }
        public string CameraPhoto { get; private set; }
        public string PhotoRuleOne { get; private set; }
        public string PhotoRuleTwo { get; private set; }
        public string StartCaptureScreen { get; private set; }

        protected override void InitializeResource()
        {
            title = Text.CameraPhoto;
            Preview = Text.Preview;
            SmallSclae = Text.SmallScale;
            LargeScale = Text.LargeScale;
            CameraPhoto = Text.CameraPhoto;
            PhotoRuleOne = Text.PhotoRule1;
            PhotoRuleTwo = Text.PhotoRule2;
            StartCaptureScreen = Text.StartCaptureScreen;
        }

        public override void Save()
        {
            if (Dirty)
            {
                BitmapImage bImg = Utility.BitmapSourceToBitmapImage(PhotoSource as BitmapSource);
                Me.ImageVM.TheImage = Utility.BitmapImageToByteArray(bImg);
                Image img = Me.ImageVM.Tag as Image;
                if (img != null)
                {
                    img.TheImage = Me.ImageVM.TheImage;
                    ApplicationVM.ChatClient.UpdateUserHeaderImange(img);
                    Me.ImageVM.Tag = img;
                    //Tempory store the image 
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Singleton<ApplicationViewModel>.Instance.ApplicationName, "headerImage.png");
                    Utility.SaveImageToFile(path, Utility.ImageExtension.PNG, PhotoSource as BitmapSource);
                    ApplicationVM.LocalCache.CurrentUserVM.ImageVM.AbsolutePathWithoutExt = path;
                }
                base.Save();
            }
        }

        protected override void InitializeCommand()
        {
            OpenLocalImageCommand = new SecureCommand(OpenLocalImageCommandExecute, CanSelectImageCommandExecute);
            CameraImageCommand = new SecureCommand(CameraImageCommandExecute, CanSelectImageCommandExecute);
            EndScreenCaptureCommand = new SecureCommand(EndScreenCaptureCommandExecute, CanEndScreenCaptureCommandExecute);
            StartScreenCaptureCommand = new SecureCommand(StartScreenCaptureCommandExecute, CanStartScreenCaptureCommandExecute);
            base.InitializeCommand();
        }

        public override void Reset()
        {
            if (Me != null)
            {
				photoPath.SetValue(Me.ImageVM.GetAbsoluteFile(true));
                photoSource.SetValue(Utility.CreateBitmapSourceFromFile(PhotoPath));
            }
            base.Reset();
        }

       public void OpenLocalImageCommandExecute(SecureCommandArgs arg)
       {
           Messenger.Default.Send<EnumNotificationMessage<Object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.LocalPhotoSelect, this));
       }

       public bool CanSelectImageCommandExecute(SecureCommandArgs arg)
       {
           return Me != null;
       }

       public void CameraImageCommandExecute(SecureCommandArgs arg)
       {
           Messenger.Default.Send<EnumNotificationMessage<Object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.CameraPhotoSelect, this));
       }

       public void EndScreenCaptureCommandExecute(SecureCommandArgs arg)
       {
           Messenger.Default.Send<EnumNotificationMessage<object,ConfigurationWindowAction>>(new EnumNotificationMessage<object,ConfigurationWindowAction>(ConfigurationWindowAction.EndScreenCapture,this));
       }

       public void StartScreenCaptureCommandExecute(SecureCommandArgs arg)
       {
           Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.StartScreenCapture, this));
       }

       public bool CanStartScreenCaptureCommandExecute(SecureCommandArgs arg)
       {
           return PhotoPath != null;
       }

       public bool CanEndScreenCaptureCommandExecute(SecureCommandArgs arg)
       {
           return IsCutting;
       }
    }
}
