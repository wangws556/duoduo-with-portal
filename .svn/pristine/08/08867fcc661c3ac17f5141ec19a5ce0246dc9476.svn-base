using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YoYoStudio.Common.Wpf.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    public class SecureCommandViewModel : TitledViewModel
    {        
        public SecureCommand Command { get;private set; }

        public SecureCommandViewModel(Action<SecureCommandArgs> execute):this(execute,null)
        {
        }

        public SecureCommandViewModel(Action<SecureCommandArgs> execute, Func<SecureCommandArgs, bool> canExecute)
        {
            Command = new SecureCommand(execute, canExecute);
        }

    }
}
