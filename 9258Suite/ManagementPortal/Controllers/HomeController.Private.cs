using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using YoYoStudio.DataService.Client.Generated;
using YoYoStudio.ManagementPortal.Models;
using YoYoStudio.Model;
using YoYoStudio.Model.Core;
using YoYoStudio.Model.Json;
using YoYoStudio.Resource;

namespace YoYoStudio.ManagementPortal.Controllers
{
    public partial class HomeController
    {
        private TreeNodeModel GetCommandNode(List<Command> commands, List<RoleCommandView> myCommands)
        {
            //if the logined user has all commands for all application or one application
            //either way, we should show all the commands.
            bool hasAllCommand = myCommands.FirstOrDefault(rc => rc.Command_Id == BuiltIns.AllCommand.Id) != null;

            TreeNodeModel allCommandsNode = new TreeNodeModel { name = Text.AllCommand,items = new List<TreeNodeModel>()};

            if (hasAllCommand)
            {
                commands.ForEach(cmd =>
                {
                    if (cmd.CommandType == BuiltIns.BackendCommandType
                        && cmd.Application_Id == BuiltIns.AllApplication.Id
                        && !string.IsNullOrEmpty(cmd.ActionName)
                        && !BuiltIns.ExcludeCommandIds.Contains(cmd.Id))
                    {
                        allCommandsNode.items.Add(new TreeNodeModel
                        {
                            id = cmd.Id,
                            name = cmd.Name,
                            url = cmd.ActionName
                        });
                    }
                });
            }
            else
            {
                myCommands.ForEach(rc =>
                {
                    var cmd = commands.FirstOrDefault(cc => cc.Id == rc.Command_Id);
                    if (rc.CommandType == BuiltIns.BackendCommandType
                        && rc.Application_Id == BuiltIns.AllApplication.Id
                        && !string.IsNullOrEmpty(rc.ActionName)
                        && !BuiltIns.ExcludeCommandIds.Contains(rc.Command_Id))
                    {
                        allCommandsNode.items.Add(new TreeNodeModel
                        {
                            id=cmd.Id,
                            name = rc.Command_Name,
                            url = rc.ActionName
                        });

                    }
                });
            }
            return allCommandsNode;
        }

        private TreeNodeModel GetApplicationNode(List<Application> applications, List<Command> commands, List<RoleCommandView> myCommands)
        {
            bool hasAllApplication = myCommands.FirstOrDefault(rc => rc.Application_Id == BuiltIns.AllApplication.Id) != null;

            TreeNodeModel allApplicationsNode = new TreeNodeModel { name = Text.AllApplication, items = new List<TreeNodeModel>() };

            if (hasAllApplication)
            {
                applications.ForEach(a => allApplicationsNode.items.Add(new TreeNodeModel { name = a.Name, id = a.Id }));
            }
            else
            {
                myCommands.ForEach(rc =>
                {
                    if (rc.CommandType == BuiltIns.BackendCommandType)
                    {
                        var appNode = allApplicationsNode.items.FirstOrDefault(a => a.id== rc.Application_Id);
                        if (appNode == null)
                        {
                            var app = applications.FirstOrDefault(a => a.Id == rc.Application_Id);
                            if (app != null)
                            {
                                allApplicationsNode.items.Add(new TreeNodeModel { id = app.Id, name = app.Name });
                            }
                        }
                    }
                });
            }

            bool hasAllCommandForAllApplication = myCommands.FirstOrDefault(rc => rc.Application_Id == BuiltIns.AllApplication.Id && rc.Command_Id == BuiltIns.AllCommand.Id) != null;

            allApplicationsNode.items.ForEach(r =>
                {
                    r.items = new List<TreeNodeModel>();

                    bool hasAllCommandForCurrentApplication = myCommands.FirstOrDefault(rc => rc.Application_Id == r.id && rc.Command_Id == BuiltIns.AllCommand.Id) != null;
                    
                    if (hasAllCommandForAllApplication || hasAllCommandForCurrentApplication)
                    {
                        commands.ForEach(c =>
                            {
                                if (c.CommandType == BuiltIns.BackendCommandType
                                    && c.Application_Id == r.id
                                    && !string.IsNullOrEmpty(c.ActionName)
                                    && !BuiltIns.ExcludeCommandIds.Contains(c.Id))
                                {
                                    r.items.Add(new TreeNodeModel { id = c.Id, name = c.Name, url = c.ActionName });
                                }
                            });
                    }
                    else
                    {
                        myCommands.ForEach(rc =>
                            {
                                var cmd = commands.FirstOrDefault(c => c.Id == rc.Command_Id);
                                if (rc.Application_Id == r.id
                                    && cmd.CommandType == BuiltIns.BackendCommandType
                                    && cmd.Application_Id == r.id
                                    && !string.IsNullOrEmpty(cmd.ActionName)
                                    && !BuiltIns.ExcludeCommandIds.Contains(cmd.Id))
                                {
                                    r.items.Add(new TreeNodeModel { id = cmd.Id, name = cmd.Name, url = cmd.ActionName });
                                }
                            });
                    }
                    
                });

            return allApplicationsNode;
        }

        private TreeNodeModel GetPersonalNode()
        {
            TreeNodeModel personalNode = new TreeNodeModel { name = Text.PersonalManagement, items = new List<TreeNodeModel>() };
            personalNode.items.Add(new TreeNodeModel { id = BuiltIns.PersonalInfoCommand.Id, name = BuiltIns.PersonalInfoCommand.Name, url = BuiltIns.PersonalInfoCommand.ActionName });
            personalNode.items.Add(new TreeNodeModel { id = BuiltIns.PasswordCommand.Id, name = BuiltIns.PasswordCommand.Name, url = BuiltIns.PasswordCommand.ActionName });
            return personalNode;
        }

        private string GetQueryCondition()
        {
            if (Request.Form.Count > 0)
            {
                string where = Request.Form["where"];
                if (!string.IsNullOrEmpty(where))
                {
                    RuleModel rule = JsonConvert.DeserializeObject<RuleModel>(where);
                    if (rule != null)
                    {
                        return rule.GetCondition();
                    }
                }
            }
            return "";
        }
    }
}