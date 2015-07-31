using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common;
using YoYoStudio.Common.Notification;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    public partial class ConfigurationItemViewModel : ControlViewModel
    {
        public ConfigurationViewModel ConfigurationVM { get; private set; }

        public ConfigurationItemViewModel(ConfigurationViewModel configVM)
        {
            ConfigurationVM = configVM;
            Title = configVM.Title;
        }

        public SecureCommand Command { get; set; }
        public void CommandExecute(SecureCommandArgs args)
        {
            Messenger.Default.Send<EnumNotificationMessage<object, ConfigurationWindowAction>>(new EnumNotificationMessage<object, ConfigurationWindowAction>(ConfigurationWindowAction.ConfigurationStateChanged,this));
        }

        public bool CanCommandExecute(SecureCommandArgs args)
        {
            return ApplicationVM.LocalCache.CurrentUserVM != null;
        }

        public override void Save()
        {
            ConfigurationVM.Save();
            base.Save();
        }

        public override void Reset()
        {
            ConfigurationVM.Reset();
            base.Reset();
        }

        public override void Initialize()
        {
            if (ConfigurationVM != null)
            {
                ConfigurationVM.Initialize();
            }
            InitializeCommand();
            base.Initialize();
        }

        protected override void InitializeCommand()
        {
            Command = new SecureCommand(CommandExecute, CanCommandExecute);
            base.InitializeCommand();
        }

        public override bool Dirty
        {
            get
            {
                return ConfigurationVM.Dirty;
            }
            set
            {
                
            }
        }
    }
}
