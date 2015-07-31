using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "sourceRoleVM", property = "SourceRoleVM", type = "RoleViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "targetRoleVM", property = "TargetRoleVM", type = "RoleViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "commandVM", property = "CommandVM", type = "CommandViewModel", defaultValue = "null")]
    [SnippetPropertyINPC(field = "isManagerCommand", property = "IsManagerCommand", type = "bool", defaultValue = "false")]
    public partial class RoleCommandViewModel : IdedEntityViewModel
    {
        public RoleCommandViewModel(RoleCommandView roleCommandView)
            : base(roleCommandView)
        {
        }

        public override void Initialize()
        {
            RoleCommandView rcv = GetConcretEntity<RoleCommandView>();
            sourceRoleVM.SetValue(ApplicationVM.LocalCache.AllRoleVMs.FirstOrDefault(r => r.Id == rcv.SourceRole_Id));
            targetRoleVM.SetValue(ApplicationVM.LocalCache.AllRoleVMs.FirstOrDefault(r => r.Id == rcv.TargetRole_Id));
            commandVM.SetValue(ApplicationVM.LocalCache.AllCommandVMs.FirstOrDefault(c => c.Id == rcv.Command_Id));
            base.Initialize();
        }
    }
}
