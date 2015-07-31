using Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;

namespace YoYoStudio.Client.ViewModel
{
    [SnippetPropertyINPC(field = "name", property = "Name", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "description", property = "Description", type = "string", defaultValue = "string.Empty")]
    [SnippetPropertyINPC(field = "order", property = "Order", type = "int", defaultValue = "0")]
    [SnippetPropertyINPC(field = "roleCommandVMs", property = "RoleCommandVMs", type = "ObservableCollection<RoleCommandViewModel>", defaultValue = "null")]
    public partial class RoleViewModel : ImagedEntityViewModel
    {
        public RoleViewModel(Role role)
            : base(role)
        {
            name.SetValue(role.Name);
            description.SetValue(role.Description);
            order.SetValue(role.Order);
        }

        public override void Initialize()
        {
            var rcs = ApplicationVM.LocalCache.AllRoleCommandVMs.Where(rc => rc.SourceRoleVM == this || rc.SourceRoleVM.Id == BuiltIns.AllRole.Id);
            if (rcs != null)
            {
                roleCommandVMs.SetValue(new System.Collections.ObjectModel.ObservableCollection<RoleCommandViewModel>(rcs));
            }
            base.Initialize();
        }
    }
    
}
