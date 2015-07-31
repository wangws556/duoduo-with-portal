var userApplications;
var userRoles;
var userManager;
var hasAll, hasUserDeposit, hasAgentDeposit, hasScoreDeposit, hasUserRoleUpDown;

function Init(gridTag) {
    $.ajax({
        type: "GET",
        url: "Home/GetMyCommands",
        cache: false,
        success: function (result) {
            if (result.Success) {
                hasAll = result.hasAll;
                if (!hasAll && result.cmdIds) {
                    for (var i = 0; i < result.cmdIds.length; i++) {
                        if (result.cmdIds[i] == 12) {
                            hasUserDeposit = true;
                        }
                        else if (result.cmdIds[i] == 13) {
                            hasAgentDeposit = true;
                        }
                        else if (result.cmdIds[i] == 14) {
                            hasScoreDeposit = true;
                        }
                        else if (result.cmdIds[i] == 15) {
                            hasUserRoleUpDown = true;
                        }
                    }
                }
            }
            $.ajax({
                type: "GET",
                url: "Home/GetUserCommandApplications",
                cache: false,
                success: function (apps) {
                    if (apps.Success) {
                        userApplications = apps.Rows;
                        InitRoles(gridTag);
                    }
                    else { 
                        alert('失败！', apps.Message);
                    }
                },
                error: function (error) { alert(error); }
            });
        },
        error: function (error) { alert(error); }
    });
}

function InitRoles(gridTagId) {
    $.ajax(
       {
           type: "GET",
           url: "Home/GetRoles",
           cache: false,
           success: function (roles) {
               if (roles.Success) {
                   userRoles = roles.Rows;
                   InitGrid(gridTagId);
               }
               else {
                   alert('失败！', roles.Message);
               }
           },
           error: function (error) { alert(error); }
       });
}

function InitGrid(tagId) {
    var disableUpDownRole = !(hasAll || hasUserRoleUpDown);
    userManager = $(tagId).ligerGrid({
        columns: [
        { display: '用户ID', name: 'User_Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '应用程序', name: 'Application_Id', width: 150, isSort: true,
            editor: { type: 'select', data: userApplications, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < userApplications.length; i++) {
                    if (userApplications[i].Id == item.Application_Id) {
                        return userApplications[i].Name;
                    }
                }
                return '';
            }
        },
        {
            display: '用户级别', name: 'Role_Id', width: 100, isSort: true,
            editor: { type: 'select', data: userRoles, valueColumnName: 'Id', displayColumnName: 'Name' }, render: function (item) {
                for (var i = 0; i < userRoles.length; i++) {
                    if (userRoles[i].Id == item.Role_Id) {
                        return userRoles[i].Name;
                    }
                }
                return '';
            }
        },
        { display: '金币', name: 'Money', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '代理金币', name: 'AgentMoney', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '积分', name: 'Score', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '操作', isSort: false, width: 250, render: function (rowdata, rowindex, value) {
                var h = "<a href='javascript:resetPassword(" + rowdata.User_Id + ")'>重置密码</a> ";
                if (hasAll || hasUserDeposit) {
                    h += "<a href='javascript:deposit(" + rowdata.Application_Id + "," + rowdata.User_Id + "," + rowindex + ")'> 充值</a> ";
                }
                if (rowdata.Role_Id == 4 && (hasAll || hasAgentDeposit)) {
                    h += "<a href='javascript:agentdeposit(" + rowdata.Application_Id + "," + rowdata.User_Id + "," + rowindex + ")'> 代理充值</a>";
                }
                if (hasAll || hasScoreDeposit) {
                    h += "<a href='javascript:scoredeposit(" + rowdata.Application_Id + "," + rowdata.User_Id + "," + rowindex + ")'> 积分充值</a>";
                }
                return h;
            }
        }

        ],
        url: "Home/GetUserInfos?includeAll=false",
        pageSizeOptions: [20, 40, 80], pageSize: 40,
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, allowUnSelectRow: true, checkbox: true, frozenCheckbox: false,
        width: '95%', height: '95%',
        detail: { onShowDetail: showDetail, height: 'auto' },
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(userManager); }, icon: 'search2' },
                    { text: '删除选中纪录', click: function () { deleteSelected(userManager); } },
                    { text: '保存修改', click: function () { saveAll(userManager, 'SaveUsers'); } },
                    { text: '修改选中用户级别', click: function () { upDownUserRole(); }, disable: disableUpDownRole }
            ]
        }
    });
}

function upDownUserRole() {
    if (userManager.getCheckedRows().length == 0)
    {
        alert('请先选择用户');
        return;
    }
    $.ligerDialog.prompt('新用户级别ID（数字）', true, function (yes, value) {
        if (yes) {
            if (!parseInt(value)) {
                alert('请输入代表用户级别的数字');
                return;
            }
            $.ajax({
                type: 'POST',
                url: 'Home/UpDownUserRoles?roleId=' + parseInt(value),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                data: JSON.stringify(userManager.getCheckedRows()),
                success: function (result) {
                    if (result.Success) {
                        alert('成功');
                    }
                    else {
                        alert('失败' + result.Message);
                    }
                },
                error: function (error) { alert(error); }
            });
        }
    });
}

function showDetail(row, detailPanel, callback) {
    var grid = document.createElement('div');
    $(detailPanel).append(grid);
    $.ajax({
        type: 'GET',
        cache: false,
        url: 'Home/GetUser?id=' + row.User_Id,
        error: function (error) { alert(error); },
        success: function (user) {
            if (user.Success) {
                $(grid).css('margin', 10).ligerGrid({
                    columns:
                        [
                                { display: '昵称', name: 'NickName', type: 'string' },
                                { display: '年龄', name: 'Age', type: 'int' },
                                {
                                    display: '性别', name: 'Gender', type: 'int', editor: { type: 'text' }, render: function (rowdata, rowindex, value) {
                                        if (rowdata.Gender == 1) {
                                            return '男';
                                        }
                                        return '女';
                                    }
                                },
                                { display: '邮件', name: 'Email', type: 'string' },
                                { display: '国家', name: 'Country', type: 'string' },
                                { display: '城市', name: 'City', type: 'string' },
                                { display: '上次登录时间', name: 'LastLoginTime', type: 'date' },
                        ],
                    isScroll: true, showToggleColBtn: false, width: '98%',
                    data: user, showTitle: false, usePager: false,
                    columnWidth: 100, onAfterShowData: callback, frozen: true
                });
            }
            else {
                alert('失败！' + user.Message);
            }
        }
    });

}


function resetPassword(userId) {
    if (userId) {
        $.ajax(
            {
                type: 'POST',
                url: 'Home/ResetPassword/' + userId,
                error: function (error) { alert(error); },
                success: function (response) {
                    if (response.Success) {
                        alert('新密码：' + response.Password);
                    }
                    else {
                        alert('重置密码失败'+response.Message);
                    }
                }
            });
    }
}


function deposit(appId, userId, idx) {
    $.ligerDialog.prompt('充值金额', true, function (yes, value) {
        if (yes) {
            if (parseInt(value) == NaN) {
                alert('请输入数字');
                return;
            }
            $.ajax(
        {
            type: 'POST',
            url: 'Home/Deposit/' + value,
            data: { aid: appId, uid: userId },
            error: function (error) { alert(error); },
            success: function (response) {
                if (response.Success) {
                    alert('充值成功');
                    if (userManager.currentData.Rows.length > idx) {
                        var r = userManager.currentData.Rows[idx];
                        var rDom = userManager.getRowObj(idx);
                        if (r) {
                            var newMoney = r.Money + parseInt(value);
                            userManager.updateCell('Money', newMoney, rDom);
                        }
                    }

                }
                else {
                    alert('充值失败' + response.Message);
                }
            }
        });
        }
    });

}

function agentdeposit(appId, userId, idx) {
    $.ligerDialog.prompt('充值金额', true, function (yes, value) {
        if (yes) {
            if (parseInt(value) == NaN) {
                alert('请输入数字');
                return;
            }
            $.ajax(
        {
            type: 'POST',
            url: 'Home/AgentDeposit/' + value,
            data: { aid: appId, uid: userId },
            error: function (error) { alert(error); },
            success: function (response) {
                if (response.success) {
                    alert('充值成功');
                    if (userManager.currentData.Rows.length > idx) {
                        var r = userManager.currentData.Rows[idx];
                        var rDom = userManager.getRowObj(idx);
                        if (r) {
                            var newMoney = r.AgentMoney + parseInt(value);
                            userManager.updateCell('AgentMoney', newMoney, rDom);
                        }
                        return;
                    }
                    alert('充值失败');
                }
            }
        });
        }
    });
}

function scoredeposit(appId, userId, idx) {
    $.ligerDialog.prompt('充值积分', true, function (yes, value) {
        if (yes) {
            if (parseInt(value) == NaN) {
                alert('请输入数字');
                return;
            }
            $.ajax(
        {
            type: 'POST',
            url: 'Home/ScoreDeposit/' + value,
            data: { aid: appId, uid: userId },
            error: function (error) { alert(error); },
            success: function (response) {
                if (response.Success) {
                    alert('充值成功');
                    if (userManager.currentData.Rows.length > idx) {
                        var r = userManager.currentData.Rows[idx];
                        var rDom = userManager.getRowObj(idx);
                        if (r) {
                            var newMoney = r.Score + parseInt(value);
                            userManager.updateCell('Score', newMoney, rDom);
                        }

                    }
                    else {
                        alert('充值失败'+response.Message);
                    }
                }
            }
        });
        }
    });
}