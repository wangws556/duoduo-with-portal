using Snippets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YoYoStudio.Common;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model.Configuration;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "loginEntryVMs", property = "LoginEntryVMs", type = "ObservableCollection<LoginEntryViewModel>", defaultValue = "null")]
    [SnippetPropertyINPC(field = "lastLoginVM", property = "LastLoginVM", type = "LoginEntryViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "videoConfigurationVM", property = "VideoConfigurationVM", type = "VideoConfigurationViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "audioConfigurationVM", property = "AudioConfigurationVM", type = "AudioConfigurationViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "personalInfoConfigurationVM", property = "PersonalInfoConfigurationVM", type = "PersonalInfoConfigurationViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "photoSelectorVM", property = "PhotoSelectorVM", type = "PhotoSelectorViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "autoLogin", property = "AutoLogin", type = "bool", defaultValue = "false")]
    [SnippetPropertyINPC(field="securityConfigurationVM", property="SecurityConfigurationVM", type = "SecurityConfigurationViewModel", defaultValue="null")]
    public partial class ProfileViewModel : ViewModelBase
    {

        private string file =""; 
        private Profile profile;
        
        private ProfileViewModel()
        {
        }

        public override void Initialize()
        {
            file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Singleton<ApplicationViewModel>.Instance.ApplicationName, "Profile.config");
            Load();
            foreach (var login in LoginEntryVMs)
            {
                login.Initialize();
            }
            if (LastLoginVM != null)
            {
                LastLoginVM.Initialize();
            }
            VideoConfigurationVM.Initialize();
            AudioConfigurationVM.Initialize();
            PersonalInfoConfigurationVM.Initialize();
            PhotoSelectorVM.Initialize();
            SecurityConfigurationVM.Initialize();
            base.Initialize();
        }

        public override void Reset()
        {            
            AutoLogin = profile.AutoLogin;
            foreach (var login in LoginEntryVMs)
            {
                login.Reset();
            }
            if (LastLoginVM != null)
            {
                LastLoginVM.Reset();
            }
            VideoConfigurationVM.Reset();
            AudioConfigurationVM.Reset();
            base.Reset();
        }

        public override void Save()
        {
            PrepareSerialize();

            XmlSerializer serializer = new XmlSerializer(typeof(Profile));
            FileStream fs = null;
            if (File.Exists(file))
            {
                File.Delete(file);
            }
           
            if (!Directory.Exists(Path.GetDirectoryName(file)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file));
            }
            fs = File.Create(file);
           
            try
            {
                serializer.Serialize(fs, profile);
            }
            catch { }
            finally
            {
                fs.Close();
            }
			base.Save();
        }

        private void PrepareSerialize()
        {
            foreach (LoginEntryViewModel vm in LoginEntryVMs)
            {
                if (profile.LoginEntries.Find(r => r.UserId == vm.UserId) == null)
                {
                    profile.LoginEntries.Add(new LoginEntry { UserId = vm.UserId, Password = vm.Password, Remember = vm.Remember });
                }
            }

            if (LastLoginVM != null)
            {
                if (profile.LastLoginEntry == null)
                {
                    profile.LastLoginEntry = new LoginEntry { Password = LastLoginVM.Password, Remember = LastLoginVM.Remember, UserId = LastLoginVM.UserId };
                }
                else
                {
                    profile.LastLoginEntry.UserId = LastLoginVM.UserId;
                    profile.LastLoginEntry.Password = LastLoginVM.Password;
                    profile.LastLoginEntry.Remember = LastLoginVM.Remember;
                }
            }

            if (SecurityConfigurationVM != null)
            {
                SecurityConfigurationVM.AutoLogin = AutoLogin;
            }

            profile.AutoLogin = AutoLogin;
        }

        public void Load()
        {
            if (File.Exists(file))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                FileStream fs = File.OpenRead(file);
                try
                {
                    profile = serializer.Deserialize(fs) as Profile;
                }
                catch { }
                finally
                {
                    fs.Close();
                }
            }
            if (profile == null)
            {
                profile = CreateDefaultProfile();
            }

            loginEntryVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<LoginEntryViewModel>());
            foreach (var login in profile.LoginEntries)
            {
                LoginEntryVMs.Add(new LoginEntryViewModel(login));
            }
            if (profile.LastLoginEntry != null)
            {
                LastLoginVM = new LoginEntryViewModel(profile.LastLoginEntry);
            }
            videoConfigurationVM.SetValue(new VideoConfigurationViewModel(profile.VideoConfiguration));
            audioConfigurationVM.SetValue(new AudioConfigurationViewModel(profile.AudioConfiguration));
            personalInfoConfigurationVM.SetValue(new PersonalInfoConfigurationViewModel(profile.PersonalInfoConfiguration));
            photoSelectorVM.SetValue(new PhotoSelectorViewModel(profile.PhotoSelectorConfiguration));
            securityConfigurationVM.SetValue(new SecurityConfigurationViewModel(profile.SecurityConfiguration));
            SecurityConfigurationVM.AutoLogin = profile.AutoLogin;
            autoLogin.SetValue(profile.AutoLogin);
        }

        private Profile CreateDefaultProfile()
        {
            return new Profile()
            {
                AudioConfiguration = new AudioConfiguration(),
                AutoLogin = false,
                LastLoginEntry = null,
                LoginEntries = new List<LoginEntry>(),
                VideoConfiguration = new VideoConfiguration(),
                PhotoSelectorConfiguration = new PhotoSelectorConfiguration(),
                PersonalInfoConfiguration = new PersonalInfoConfiguration(),
                SecurityConfiguration = new SecurityConfiguration()
            };
        }
    }
}

