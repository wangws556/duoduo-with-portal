using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Resource;
using YoYoStudio.Common.Wpf.ViewModel;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "configurationItemVMs", property = "ConfigurationItemVMs", type = "ObservableCollection<ConfigurationItemViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "currentConfigurationItemVM", property = "CurrentConfigurationItemVM", type = "ConfigurationItemViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "message", property = "Message", type = "string", defaultValue = "string.Empty")]
    public partial class ConfigurationWindowViewModel : WindowViewModel
    {
        public ConfigurationWindowViewModel()
        {
            configurationItemVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<ConfigurationItemViewModel>());
            ConfigurationItemVMs.Add(new ConfigurationItemViewModel(ApplicationVM.ProfileVM.PersonalInfoConfigurationVM));
            ConfigurationItemVMs.Add(new ConfigurationItemViewModel(ApplicationVM.ProfileVM.AudioConfigurationVM));
            ConfigurationItemVMs.Add(new ConfigurationItemViewModel(ApplicationVM.ProfileVM.VideoConfigurationVM));
            ConfigurationItemVMs.Add(new ConfigurationItemViewModel(ApplicationVM.ProfileVM.SecurityConfigurationVM));
            currentConfigurationItemVM.SetValue(ConfigurationItemVMs[0]);
            Initialize();
        }

        public ConfigurationWindowViewModel(ConfigurationItemViewModel customizedConfig)
        {
            configurationItemVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<ConfigurationItemViewModel>());
            ConfigurationItemVMs.Add(customizedConfig);
            Initialize();
        }

        public string SaveLabel { get; private set; }
        public string ResetLabel { get; private set; }

        protected override void InitializeResource()
        {
            title = Text.Configuration;
            SaveLabel = Text.Save;
            ResetLabel = Text.Reset;
            base.InitializeResource();
        }

        public override void Initialize()
        {
            foreach (var configVM in ConfigurationItemVMs)
            {
                configVM.Initialize();
            }
            base.Initialize();
        }

        public override void Save()
        {
            if (Dirty)
            {
                try
                {
                    Message = string.Empty;
                    PhotoSelectorViewModel CurrentVM = CurrentConfigurationItemVM.ConfigurationVM as PhotoSelectorViewModel;
                    if (CurrentVM != null)
                    {
                        CurrentVM.Save();
                    }
                    foreach (var config in ConfigurationItemVMs)
                    {
                        config.Save();
                    }
                    ApplicationVM.ProfileVM.Save();
                    Message = Resource.Messages.SaveSucceeded;
                    base.Save();
                }
                catch
                {
                    Message = Resource.Messages.SaveFailed;
                }
            }
        }

        public override void Reset()
        {
            Message = string.Empty;
            PhotoSelectorViewModel CurrentVM = CurrentConfigurationItemVM.ConfigurationVM as PhotoSelectorViewModel;
            if (CurrentVM != null)
            {
                CurrentVM.Reset();
            }
            foreach (var config in ConfigurationItemVMs)
            {
                config.Reset();
            }
            base.Reset();
        }

        protected override void ReleaseManagedResource()
        {
            ApplicationVM.ConfigurationWindowVM = null;
            base.ReleaseManagedResource();
        }

		#region Commands

		public SecureCommand SaveCommand { get; set; }
		public SecureCommand ResetCommand { get; set; }

		public void SaveCommandExecute(SecureCommandArgs args)
		{
			Save();
		}

		public bool CanSaveCommandExecute(SecureCommandArgs args)
		{
            return true;
		}

        public override bool Dirty
        {
            get
            {
                bool result = ConfigurationItemVMs.FirstOrDefault(c => c.Dirty) != null;
                if (!result)
                {
                    PhotoSelectorViewModel CurrentVM = CurrentConfigurationItemVM.ConfigurationVM as PhotoSelectorViewModel;
                    if (CurrentVM != null)
                        result = CurrentVM.Dirty;
                }
                return result;
            }
            set { }
        }

		public void ResetCommandExecute(SecureCommandArgs args)
		{
			Reset();
		}

		public bool CanResetCommandExecute(SecureCommandArgs args)
		{
            return true;
		}

        protected override void InitializeCommand()
        {
            SaveCommand = new SecureCommand(SaveCommandExecute, CanSaveCommandExecute);
            ResetCommand = new SecureCommand(ResetCommandExecute, CanResetCommandExecute);
            base.InitializeCommand();
        }

		#endregion
	}
}
