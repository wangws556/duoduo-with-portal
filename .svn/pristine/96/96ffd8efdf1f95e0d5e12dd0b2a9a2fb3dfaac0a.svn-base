using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Common.Wpf.ViewModel;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;

namespace YoYoStudio.Client.ViewModel
{    
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "description", property = "Description", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "money", property = "Money", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "commandType", property = "CommandType", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "disable", property="Disable", type="bool", defaultValue="false")]
    public partial class CommandViewModel : ImagedEntityViewModel
    {
        private Action<SecureCommandArgs> execute;

        private Func<SecureCommandArgs, bool> canExecute;

        public SecureCommand SecureCommand { get; set; }

        public CommandViewModel(Command cmd)
            : this(cmd, null)
        {
        }

        public CommandViewModel(Command cmd,Action<SecureCommandArgs> execute):this(cmd,null,execute)
        {
            
        }

        public CommandViewModel(Command cmd, RoomWindowViewModel owner, Action<SecureCommandArgs> execute)
            : this(cmd, owner, execute, null)
        {
        }

        public CommandViewModel(Command cmd, RoomWindowViewModel owner, Action<SecureCommandArgs> execute, Func<SecureCommandArgs, bool> canExecute)
            : base(cmd)
        {
            if (cmd != null)
            {
                name.SetValue(cmd.Name);
                description.SetValue(cmd.Description);
                money.SetValue(cmd.Money.HasValue ? cmd.Money.Value : 0);
                commandType.SetValue(cmd.CommandType);
                SecureCommand = new SecureCommand(SecureCommandExecute, CanSecureCommandExecute) { Owner = owner };
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        private void SecureCommandExecute(SecureCommandArgs args)
        {
            if (CanSecureCommandExecute(args))
            {
                if (execute == null)
                {
                    ApplicationVM.ExecuteCommand(this, args);
                }
                else
                {
                    execute(args);
                }
            }
        }

        private bool CanSecureCommandExecute(SecureCommandArgs args)
        {
            if(canExecute == null || canExecute.Invoke(args))
            {
                RoomWindowViewModel roomWindowVM = args.CommandOwner as RoomWindowViewModel;

                if (Me != null && roomWindowVM != null)
                {
                    int targetRoleId = roomWindowVM.SelectedUserVM == null ? BuiltIns.AllRole.Id : roomWindowVM.SelectedUserVM.RoleVM.Id;
                    
                    switch (CommandType)
                    {
                        case BuiltIns.FrontendCommandType:
                        case BuiltIns.UserCommandType:
                            if (roomWindowVM.SelectedUserVM != null)
                            {
                                return Me.HasCommand(roomWindowVM.RoomVM.Id, Id, targetRoleId);
                            }
                            break;
                        case BuiltIns.BackendCommandType:
                            return false;
                        case BuiltIns.NormalCommandType:
                            return true;
                    }
                }
            }
            return false;
        }

        public override object ToJson()
        {
			return new MenuModel
            {
                id = Id,
                label = Name,
				img = ImageVM.StaticImageFile
            };
        }
    }
}
