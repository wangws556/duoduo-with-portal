using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field="capturePhoto", property="CapturePhoto", type="string",defaultValue="string.Empty")]
    [SnippetPropertyINPC(field = "save", property = "Save", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "cameras", property = "Cameras", type = "ObservableCollection<string>", defaultValue = "new ObservableCollection<string>()")]
    [SnippetPropertyINPC(field = "canSave", property = "CanSave", type = "bool", defaultValue = "false")]
    public partial class CameraWindowViewModel:WindowViewModel
    {
        public SecureCommand StartCapturePhotoCommand { get; set; }
        public SecureCommand SaveCapturePhotoCommand { get; set; }
        public override void Initialize()
        {
            StartCapturePhotoCommand = new SecureCommand(StartCapturePhotoCommandExecute, CanStartCapturePhotoCommandExecute);
            SaveCapturePhotoCommand = new SecureCommand(SaveCapturePhotoCommandExecute, CanSaveCapturePhotoCommandExecute);
            Title = Text.CameraPhoto;
            capturePhoto.SetValue(Text.CapturePhoto);
            save.SetValue(Text.Save);
            base.Initialize();
        }

        public void StartCapturePhotoCommandExecute(SecureCommandArgs arg)
        { 
            Messenger.Default.Send<EnumNotificationMessage<Object,CameraWindowAction>>(new EnumNotificationMessage<object,CameraWindowAction>(CameraWindowAction.TakePicture,this));
            CanSave = true;
        }

        public bool CanStartCapturePhotoCommandExecute(SecureCommandArgs arg)
        {
            return Cameras.Count > 0;
        }

        public void SaveCapturePhotoCommandExecute(SecureCommandArgs arg)
        {
            Messenger.Default.Send<EnumNotificationMessage<Object, CameraWindowAction>>(new EnumNotificationMessage<object, CameraWindowAction>(CameraWindowAction.Save, this));
            CanSave = false;
        }

        public bool CanSaveCapturePhotoCommandExecute(SecureCommandArgs arg)
        {
            return CanSave;
        }
    }
}
