using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace YoYoStudio.Common.Wpf.ViewModel
{
    public class SecureCommandArgs
    {
        public ViewModelBase CommandOwner { get; set; }
        public ViewModelBase CommnadTarget { get; set; }
        public object Content { get; set; }
    }

    /// <summary>
    /// This class implements the WPF ICommand interface, and provide the functionality to enable/disable the command based on if the current user has certain access rights
    /// </summary>
    public class SecureCommand : ICommand
    {
        protected Func<SecureCommandArgs, bool> canExecute;
        protected Action<SecureCommandArgs> execute;
        public ViewModelBase Owner { get; set; }
        public ViewModelBase Target { get; set; }

        public SecureCommand(Action<SecureCommandArgs> execute)
            : this(execute, null)
        {
        }
        public SecureCommand(Action<SecureCommandArgs> execute, Func<SecureCommandArgs, bool> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter = null)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            SecureCommandArgs args = parameter as SecureCommandArgs;
            if (args == null)
            {
                args = new SecureCommandArgs();
                args.Content = parameter;
            }
            if (args.CommandOwner == null && Owner != null)
            {
                args.CommandOwner = Owner;
            }
            if (args.CommnadTarget == null && Target != null)
            {
                args.CommnadTarget = Target;
            }

            return this.canExecute.Invoke(args);
        }

        public virtual void Execute(object parameter = null)
        {
            if ((this.CanExecute(parameter) && (this.execute != null)))
            {
                SecureCommandArgs args = parameter as SecureCommandArgs;
                if (args == null)
                {
                    args = new SecureCommandArgs();
                    args.Content = parameter;
                }

                if (args.CommandOwner == null && Owner != null)
                {
                    args.CommandOwner = Owner;
                }
                if (args.CommnadTarget == null && Target != null)
                {
                    args.CommnadTarget = Target;
                }
                this.execute.Invoke(args);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
