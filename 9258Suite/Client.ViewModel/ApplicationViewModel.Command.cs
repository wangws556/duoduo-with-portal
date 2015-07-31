using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    public partial class ApplicationViewModel
    {
        public void ExecuteCommand(CommandViewModel commandVM, SecureCommandArgs args)
        {
            var act = GetCommandDelegate(commandVM);
            if (act != null)
            {
                act(args);
            }
        }

        public Action<SecureCommandArgs> GetCommandDelegate(CommandViewModel cmd)
        {
            return null;
        }

    }
}
