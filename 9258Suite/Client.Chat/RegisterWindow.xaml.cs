using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoYoStudio.Client.ViewModel;
using YoYoStudio.Common.Wpf;
using YoYoStudio.Resource;

namespace YoYoStudio.Client.Chat
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow
    {
        private RegisterWindowViewModel registerWindowVM = null;
        public RegisterWindow(RegisterWindowViewModel vm):base(vm)
        {
            registerWindowVM = vm;
            MinHeight = ActualHeight;
            InitializeComponent();       
        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, RegisterWindowAction> message)
        {
            throw new NotImplementedException();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(AccountTxt))
            {
                MessageBox.Show(Messages.AccountFormatWrong, Text.Error, MessageBoxButton.OK);
                return;
            }
            if (String.IsNullOrEmpty(AccountTxt.Text.Trim())
                || String.IsNullOrEmpty(PasswordTxt.Password.Trim())
                || String.IsNullOrEmpty(RepeatPwdTxt.Password.Trim()))
            {
                MessageBox.Show(string.Format(Messages.CannotBeEmpty, Text.AccountNameLabel + Text.PasswordLabel), Text.Error, MessageBoxButton.OK);
                return;
            }

            if (String.Compare(PasswordTxt.Password.Trim(), RepeatPwdTxt.Password.Trim()) != 0)
            {
                MessageBox.Show(Messages.PasswordMismatch, Text.Error, MessageBoxButton.OK);
                return;
            }

            bool agree = agreeCheckbox.IsChecked.HasValue? agreeCheckbox.IsChecked.Value:false;

            registerWindowVM.Register(AccountTxt.Text.Trim(), PasswordTxt.Password.Trim(), SexCombo.SelectedIndex);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink source = sender as Hyperlink;
            if (source != null)
            {
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
            }

        }                
    }
    
}
