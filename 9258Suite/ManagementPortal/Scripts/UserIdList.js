var userIdManager;
var userIdRoleManager;
var userIdRoles;
var userIdApps;
var userIdAppManager;
var agentAppManager;
var agentAppId;
var hasAll, hasAgentUserId;

function InitGrid(tagId) {
    userIdManager = $(tagId).ligerGrid({
        columns: [
        { display: '用户ID', name: 'User_Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '代理ID', name: 'Owner_Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        {
            display: '应用程序', name: 'Application_Id', width: 120, isSort: false,
            editor: { type: 'select', data: userIdApps, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < userIdApps.length; i++) {
                    if (userIdApps[i].Id == item.Application_Id) {
                        return userIdApps[i].Name;
                    }
                }
                return '';
            }

        },
        {
            display: '用户级别', name: 'Role_Id', width: 120, isSort: true, editor: { type: 'select', data: userIdRoles, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < userIdRoles.length; i++) {
                    if (item.Role_Id == userIdRoles[i].Id) {
                        return userIdRoles[i].Name;
                    }
                }
                return item.RoleId;
            }
        },
        {
            display: '已经使用', name: 'IsUsed', width: 50, type: 'int', IsFrozen: true, isSort: true, editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.IsUsed) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        }
        ],
        url: "Home/GetUserIdLists",
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, allowUnSelectRow: true, checkbox: true, frozenCheckbox: false,
        width: '95%', height: '95%',
        pageSizeOptions: [10, 20, 40, 80], pageSize: 20,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(userIdManager); }, icon: 'search2' },
                    { text: '删除选中纪录', click: function () { deleteSelected(userIdManager); } },
                    { text: '保存修改', click: function () { saveAll(userIdManager, 'SaveUserIdLists'); } }]
        }
    });
}
function Init(appTagId, roleTagId, gridTagId, agentAppTagId) {
    $.ajax({
        type: "GET",
        url: "Home/GetMyCommands",
        cache: false,
        success: function (result) {
            if (result.Success) {
                hasAll = result.hasAll;
                if (!hasAll && result.cmdIds) {
                    for (var i = 0; i < result.cmdIds.length; i++) {
                        if (result.cmdIds[i] == 16) {
                            hasAgentUserId = true;
                            break;
                        }

                    }
                }
            }
            if (hasAll || hasAgentUserId){
                $(agentAppTagId).removeAttr('disabled');
            }
            InitForm(appTagId, roleTagId, gridTagId, agentAppTagId);
        },
        error: function (error) { alert(error); }
    });
}
function InitForm(appTagId, roleTagId, gridTagId, agentAppTagId) {
    $.ajax(
       {
           type: "GET",
           url: "Home/GetUserIdCommandApplications",
           cache: false,
           success: function (apps) {
               if (apps.Success) {
                   userIdApps = apps.Rows;
                   userIdAppManager = $(appTagId).ligerComboBox({
                       textField: 'Name',
                       valueField: 'Id',
                       valueFieldID: 'selectedAppId',
                       data: userIdApps,
                       onSelected: function (app) {
                           if (app) {
                               var newRoles = new Array();
                               for (i = 0; i < userIdRoles.length; i++) {
                                   if (userIdRoles[i].Application_Id == app || userIdRoles[i].Application_Id == 1) {
                                       if (userIdRoles[i].Id != 1) {
                                           newRoles.push(userIdRoles[i]);
                                       }
                                   }
                               }
                               userIdRoleManager.setData(newRoles);
                           }
                       }
                   });
                   agentAppManager = $(agentAppTagId).ligerComboBox({
                       textField: 'Name',
                       valueField: 'Id',
                       valueFieldID: 'selectedAgentAppId',
                       data: userIdApps,
                       onSelected: function (app) {
                           agentAppId = app;
                           if (app > 0) {
                               $('#agentstartid').removeAttr('disabled', 'disabled');
                               $('#agentendid').removeAttr('disabled', 'disabled');
                               $('#ownerid').removeAttr('disabled', 'disabled');
                           }
                           else {
                               $('#agentstartid').attr('disabled', 'disabled');
                               $('#agentendid').attr('disabled', 'disabled');
                               $('#ownerid').attr('disabled', 'disabled');
                           }
                       }
                   });
                   InitRoles(roleTagId, gridTagId);
               }
               else {
                   alert('失败！' + apps.Message);
               }
           },
           error: function (error) { alert(error); }
       });
}

function InitRoles(roleTagId, gridTagId) {
    $.ajax(
       {
           type: "GET",
           url: "Home/GetRoles",
           cache: false,
           success: function (roles) {
               if (roles.Success) {
                   userIdRoles = roles.Rows;
                   if (userIdRoles) {
                       userIdRoleManager = $(roleTagId).ligerComboBox({
                           textField: 'Name',
                           valueField: 'Id',
                           valueFieldID: 'selectedRoleId',
                           onSelected: reset
                       });
                       InitGrid(gridTagId);
                   }
               }
               else {
                   alert('失败！', roles.Message);
               }
           },
           error: function (error) { alert(error); }
       });
}

function adjust(isDirect) {
    if (isDirect) {
        $('#password').removeAttr('disabled', 'disabled');
        $('#confirmpassword').removeAttr('disabled', 'disabled');
    }
    else {
        $('#password').attr('disabled', 'disabled');
        $('#confirmpassword').attr('disabled', 'disabled');
    }
}

function reset(roleid) {
    if (roleid > 1) {
        $("input[name='useridradio'][value='0']").attr('checked', 'checked');
        $("input[name='useridradio'][value='0']").removeAttr('disabled', 'disabled');
        $('#startid').removeAttr('disabled', 'disabled');
        $('#endid').removeAttr('disabled', 'disabled');
        if (roleid == 3) {
            $("input[name='useridradio'][value='1']").attr('disabled', 'disabled');
            $('#password').attr('disabled', 'disabled');
            $('#confirmpassword').attr('disabled', 'disabled');
        }
        else {
            $("input[name='useridradio'][value='1']").removeAttr('disabled');
            $('#password').removeAttr('disabled', 'disabled');
            $('#confirmpassword').removeAttr('disabled', 'disabled');
        }
    }
}

function addUserIds() {
    var start = $('#startid').val();
    var end = $('#endid').val();
    var roleid = $('#selectedRoleId').val();
    var appid = $('#selectedAppId').val();
    var password = $('#password').val();
    var confirmPassword = $('#confirmpassword').val();
    var ownerid = -1;
    var isDirect = $("input[name='useridradio']:checked").val() == 0;

    if (appid && roleid && start && end) {
        $.ajax({
            type: 'POST',
            url: 'Home/AddUserIds',
            data: { start: start, end: end, app: appid, role: roleid, owner: ownerid, password: password, isDirect: isDirect },
            success: function (data) {
                if (data.Success) {
                    alert('成功');
                    userIdManager.loadData();
                }
                else {
                    alert('失败:' + data.Message);
                }
            },
            error: function (error) { alert(error); }
        });
    }
}

function addAgentUserIds() {
    var appid = $('#selectedAgentAppId').val();    
    var start = $('#agentstartid').val();
    var end = $('#agentendid').val();
    var agent = $('#ownerid').val();
    if (appid && start && end && agent)
    {
        $.ajax({
            type: 'POST',
            url: 'Home/AssignAgentUserIds',
            data: { start: start, end: end, app: appid, agent:agent },
            success: function (data) {
                if (data.Success) {
                    alert('成功');
                    userIdManager.loadData();
                }
                else {
                    alert('失败:' + data.Message);
                }
            },
            error: function (error) { alert(error); }
        });
    }
}
