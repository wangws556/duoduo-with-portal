using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Configuration;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "userId", property = "UserId", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "password", property = "Password", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "remember", property = "Remember", type = "bool", defaultValue = "false")]
    public partial class LoginEntryViewModel : ConfigurationViewModel
    {
        public LoginEntryViewModel(LoginEntry loginEntry)
            : base(loginEntry)
        {
            UserId = loginEntry.UserId;
            Password = loginEntry.Password;
            Remember = loginEntry.Remember;
        }

        public override void Save()
        {
            if (Dirty)
            {
                var entry = GetConcreteConfiguration<LoginEntry>();
                entry.UserId = UserId;
                entry.Password = Password;
                entry.Remember = Remember;
                base.Save();
            }
        }

        public override void Reset()
        {
            var entry = GetConcreteConfiguration<LoginEntry>();
            userId.SetValue(entry.UserId);
            password.SetValue(entry.Password);
            remember.SetValue(entry.Remember);
            base.Reset();
        }
    }
}
