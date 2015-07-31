var appManager, sroleManager, troleManager,cmdManager, backendApps;
var appid, sroleid,troleid, cmdType
function Init(appTagId, sroleTagId,troleTagId, cmdTagId, cType) {
    cmdType = cType;
    $.ajax({
    	url: 'Home/GetUserCommandCommandApplications',
        type: 'GET',
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data.Success) {
                backendApps = data.Rows;
                appManager = $(appTagId).ligerComboBox({
                    textField: 'Name',
                    valueField: 'Id',
                    data: backendApps,
                    width: '95%',
                    valueFieldId: 'selectedAppId',
                    onSelected: function (val, text) {
                        appid = val;
                        sroleManager.set('parms', { aid: val });
                        sroleManager.loadData();
                        troleManager.set('parms', { aid: val });
                        troleManager.loadData();
                    }
                });
            }
            else {
                alert('失败！', data.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });


    sroleManager = $(sroleTagId).ligerGrid({
        columns: [
        { display: '用户级别', name: 'Name', type: 'string', isSort: false, width: 120, editor: { type: 'text' } }
        ],
        url: "Home/GetApplicationRoles?includeAll=false",
        enabledEdit: false, checkbox: false, rownumbers: false, IsScroll: true, clickToEdit: false,
        width: '95%', height: '90%',
        pageSizeOptions: [10, 20, 40], pageSize: 40,
        onSelectRow: function (rowdata, index, row) {
            sroleid = rowdata.Id;
            if (sroleid && troleid) {
                cmdManager.set('parms', { aid: appid, srid: sroleid, trid: troleid });
                cmdManager.loadData();
            }
        }
    });
    troleManager = $(troleTagId).ligerGrid({
        columns: [
        { display: '目标用户级别', name: 'Name', type: 'string', isSort: false, width: 120, editor: { type: 'text' } }
        ],
        url: "Home/GetApplicationRoles?includeAll=false",
        enabledEdit: false, checkbox: false, rownumbers: false, IsScroll: true, clickToEdit: false,
        width: '95%', height: '90%',
        pageSizeOptions: [10, 20, 40], pageSize: 40,
        onSelectRow: function (rowdata, index, row) {
            troleid = rowdata.Id;
            if (sroleid && troleid) {
                cmdManager.set('parms', { aid: appid, srid: sroleid, trid: troleid });
                cmdManager.loadData();
            }
        }
    });

    cmdManager = $(cmdTagId).ligerGrid({
        columns: [
        { display: '权限', name: 'Name', width: 150, type: 'string', isSort: false, editor: { type: 'text' } },
        {
            display: '管理权限', name: 'IsManagerCmd', width: 80, isSort: false, type: 'int', editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.IsManagerCmd) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        },
        {
            display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                    h += " <a href='javascript:beginEdit(cmdManager," + rowindex + ")'>修改</a> ";
                }
                else {
                    h += " <a href='javascript:endEdit(cmdManager," + rowindex + ")'>提交</a> ";
                    h += " <a href='javascript:cancelEdit(cmdManager," + rowindex + ")'>取消</a> ";
                }
                return h;
            }
        }
        ],
        url: "Home/GetRoleCommands?cmdType=" + cType,
        pageSizeOptions: [10, 20, 40], pageSize: 20,
        isChecked: function (row) { return row.Enabled },
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, checkbox: true,
        toolbar: {
            items: [{ text: '保存修改', click: function () { save(cmdManager, 'SaveRoleCommands?cmdType=' + cmdType + '&aid=' + appid + '&srid=' + sroleid + '&trid='+troleid); } }]
        },
        width: '95%', height: '90%'
    });
}

function save(manager, action) {
    if (!checkSaving()) {
        isSaving = true;
        if (manager) {
            manager.endEdit();
            var data = manager.getCheckedRows();
            $.ajax({
                url: 'Home/' + action,
                type: 'POST',
                dataType: 'json',
                async: false,
                data: JSON.stringify(data),
                contentType: 'application/json;charset=utf-8',
                success: function (msg) {
                    if (msg.Success) {
                        manager.loadData();
                        $.ligerDialog.success("保存成功！");
                    }
                    else {
                        alert(msg.Message);
                    }
                    isSaving = false;
                },
                error: function (error) {
                    $.ligerDialog.error("保存失败！");
                    isSaving = false;
                }
            });
        }
    }
}