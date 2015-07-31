using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snippets;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Configuration;
using YoYoStudio.Resource;
using YoYoStudio.Common;

namespace YoYoStudio.Client.ViewModel
{
    public partial class ConfigurationViewModel : TitledViewModel, IApplicationViewModel
    {
        protected Config configuration;

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
            get
            {
                return ApplicationVM.LocalCache.CurrentUserVM;
            }
        }

        public string Name
        {
            get
            {
                return configuration.Name;
            }
        }

        public ConfigurationViewModel(Config config)
        {
            configuration = config;
            title = Text.ResourceManager.GetString(configuration.Name);
        }

        public T GetConcreteConfiguration<T>() where T : Config
        {
            return configuration as T;
        }

        public SecureCommand Command { get; set; }
    }       
}
