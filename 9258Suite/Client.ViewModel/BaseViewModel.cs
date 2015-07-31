using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using Snippets;
using YoYoStudio.Common;
using System.Runtime.InteropServices;
using System.IO;
using YoYoStudio.Model.Json;
using System.Web.Script.Serialization;
using YoYoStudio.Common.Wpf;
using YoYoStudio.Model.Core;
using YoYoStudio.Common.Notification;

namespace YoYoStudio.Client.ViewModel
{

    public interface IApplicationViewModel
    {
        ApplicationViewModel ApplicationVM { get; }
        UserViewModel Me { get; }
        bool IsAuthenticated{get;}
    }

    [ComVisible(true)]
    public class ControlViewModel : TitledViewModel, IApplicationViewModel
    {
        public string FlexPath
        {
            get
            {
                return ApplicationVM.FlexFile;
            }
        }

        public string MusicFlexPath
        {
            get
            {
                return ApplicationVM.MusicFlexFile;
            }
        }

        public bool IsAuthenticated
        {
            get { return ApplicationVM.IsAuthenticated; }
        }

        private ApplicationViewModel applicationVM = null;

        public ApplicationViewModel ApplicationVM
        {
            get
            {
                if (applicationVM == null)
                {
                    applicationVM = Singleton<ApplicationViewModel>.Instance;
                }
                return applicationVM;
            }
        }

        public UserViewModel Me
        {
            get { return ApplicationVM.LocalCache.CurrentUserVM; }
        }
    }

    [ComVisible(true)]
    [SnippetPropertyINPC(type = "bool", field = "busy", property = "Busy", defaultValue = "false")]
	[SnippetPropertyINPC(type = "WebPageViewModel", field = "webPageVM", property = "WebPageVM", defaultValue = "new WebPageViewModel()")]
    [SnippetPropertyINPC(field = "welcomeMessage", property = "WelcomeMessage", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "busyMessage", property = "BusyMessage", type = "string", defaultValue = "string.Empty")]
    public partial class WindowViewModel : ControlViewModel
    {
		public WindowViewModel()
		{
		}

        public void Load(WebPageViewModel pageVM)
        {
            WebPageVM = pageVM;
            WebPageVM.Source = Path.Combine(ApplicationVM.Root, pageVM.Source);
        }

		protected override void ReleaseUnManagedResource()
		{
			WebPageVM.Dispose();
			base.ReleaseUnManagedResource();
		}

        public virtual void WebPageLoadCompletedHandler()
        {
        }

        public virtual void LoadAsync()
        {

        }
    }
	
    [ComVisible(true)]
    [Serializable]
    public abstract class EntityViewModel : ViewModelBase, IApplicationViewModel
	{
        private ApplicationViewModel applicationVM = null;

        public ApplicationViewModel ApplicationVM
        {
            get
            {
                if (applicationVM == null)
                {
                    applicationVM = Singleton<ApplicationViewModel>.Instance;
                }
                return applicationVM;
            }
        }

        public bool IsAuthenticated
        {
            get { return ApplicationVM.IsAuthenticated; }
        }

        public string FlexPath
        {
            get
            {
                return ApplicationVM.FlexFile;
            }
        }

        public UserViewModel Me
        {
            get { return ApplicationVM.LocalCache.CurrentUserVM; }
        }

		private ModelEntity entity;

		public EntityViewModel(ModelEntity entity)
		{
			this.entity = entity;
		}

		public T GetConcretEntity<T>() where T : ModelEntity
		{
			return entity as T;
		}

        public virtual object ToJson()
        {
            return null;
        }

        private object jsonData = null;

        public virtual string GetJson(bool reGenerate = false) 
        {
            if (jsonData == null || reGenerate)
            {
                jsonData = ToJson();
            }
            if (jsonData != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(jsonData);
            }
            return string.Empty;
        }
	}
    [ComVisible(true)]
    [Serializable]
	public class IdedEntityViewModel : EntityViewModel
	{
		public int Id { get;private set; }
		public IdedEntityViewModel(IdedEntity entity):base(entity)
		{
            if (entity != null)
            {
                Id = entity.Id;
            }
		}
	}

    [ComVisible(true)]
    [Serializable]
	public class ImagedEntityViewModel : IdedEntityViewModel
	{
		public ImageViewModel ImageVM { get; private set; }

        public ImagedEntityViewModel(ImagedEntity entity)
			: base(entity)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
            ImagedEntity entity = GetConcretEntity<ImagedEntity>();
            if (entity != null)
            {
                if (entity.Image_Id.HasValue && entity.Image_Id.Value > 0)
                {
                    int typeId = entity.GetImageType().Id;
                    int imageValue = entity.Image_Id.Value;
                    if (ApplicationVM.LocalCache.AllImages.ContainsKey(typeId))
                    {
                        if (ApplicationVM.LocalCache.AllImages[typeId].ContainsKey(imageValue))
                            ImageVM = ApplicationVM.LocalCache.AllImages[typeId][imageValue];
                    }
					if (ImageVM == null || !File.Exists(ImageVM.StaticImageFile))
                    {
                        var img = ApplicationVM.ChatClient.GetImage(imageValue);
                        if (img != null)
                        {
                            ImageVM = ApplicationVM.AddImage(img);
							string dir = Path.GetDirectoryName(ImageVM.StaticImageFile);
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
							File.WriteAllBytes(ImageVM.StaticImageFile, img.TheImage);
                        }
                    }
                }
            }
		}
	}
}
