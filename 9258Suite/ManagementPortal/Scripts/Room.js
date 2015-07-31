
var roomManager;
var roomGroups;
var startIdTxtManager;
var endIdTxtManager;
var agentsManager;
var roomMgrManager;
var roomViceManager;
var allUsers;

function Init(agentsTagId, startTagId, endTagId, roomMgrTagId, roomViceTagId, gridTagId) {

    $.ajax({
        type: "GET",
        url: "Home/GetUsers",
        cache: false,
        success: function (users) {
            if (users.Success) {
                allUsers = users.Rows;
            }
            else {
                alert('失败！', users.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });


    $.ajax({
        type: "GET",
        url: "Home/GetAgents",
        cache: false,
        success: function (allAgents) {
            if (allAgents.Success) {
                agentsManager = $(agentsTagId).ligerComboBox({
                    data: allAgents.Rows,
                    isMultiSelect: false,
                    textField: 'Id',
                    valueField: 'Id'
                });
            }
            else {
                alert('失败！', allAgents.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });

    startIdTxtManager = $(startTagId).ligerTextBox({
        label: '起始房间号码',
        digits: true
    });

    endIdTxtManager = $(endTagId).ligerTextBox({
        label: '结束房间号码',
        digits: true
    });

    $.ajax({
        type: "GET",
        url: "Home/GetRoomGroups",
        cache: false,
        success: function (groups) {
            if (groups.Success) {
                roomGroups = groups.Rows;
                InitGrid(gridTagId);
            }
            else {
                alert('失败！', groups.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });

    roomViceManager = $(roomViceTagId).ligerTextBox({
        label: '副室长',
        disabled: true,
        digits: true
    });

    roomMgrManager = $(roomMgrTagId).ligerTextBox({
        label: '房间管理员',
        disabled: true,
        digits: true
    });

}

function InitGrid(gridId) {
    roomManager = $(gridTagId).ligerGrid({
        columns: [
         { display: '房间Id', name: 'Id', width: 60, editor: { type: 'int' }, type: 'int', isSort: false },
        { display: '名称', name: 'Name', width: 80, type: 'string', editor: { type: 'text' }, isSort: false },
        { display: '房主', name: 'HostUser_Id', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '代理', name: 'AgentUser_Id', width: 60, type: 'int', editor: { type: 'int' }, isSort: false, IsFrozen: true },
        { display: '最大用户数', name: 'MaxUserCount', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },

        { display: '公麦数', name: 'PublicMicCount', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '私麦数', name: 'PrivateMicCount', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '密麦数', name: 'SecretMicCount', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
        
     
          { display: '公聊时间', name: 'PublicMicTime', width: 70, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '密码', name: 'Password', width: 70, type: 'string', editor: { type: 'text' }, isSort: false },
          { display: '服务器IP', name: 'ServiceIp', width: 80, type: 'string', editor: { type: 'text' }, isSort: false },
        {
            display: '房间组', name: 'RoomGroup_Id', type: 'int', width: 80, editor: { type: 'select', data: roomGroups, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < roomGroups.length; i++) {
                    if (item.RoomGroup_Id == roomGroups[i].Id) {
                        return roomGroups[i].Name;
                    }
                }
                return '';
            }
        },
        {
            display: '公聊', width: 30, name: 'PublicChatEnabled', type: 'int',
            editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.PublicChatEnabled) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        },
         {
             display: '启用', width: 30, name: 'Enabled', type: 'int',
             editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
             render: function (item) {
                 for (var i = 0; i < yesOrNo.length; i++) {
                     if (yesOrNo[i].val == item.Enabled) {
                         return yesOrNo[i].text;
                     }
                 }
                 return '';
             }
         },
        {
            display: '礼物', width: 40, name: 'GiftEnabled', type: 'int',
            editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.GiftEnabled) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        },

        {
            display: '私聊', width: 30, name: 'PrivateChatEnabled', type: 'int',
            editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.PrivateChatEnabled) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        },
         {
             display: '隐藏', width: 40, name: 'Hide', type: 'int',
             editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
             render: function (item) {
                 for (var i = 0; i < yesOrNo.length; i++) {
                     if (yesOrNo[i].val == item.Hide) {
                         return yesOrNo[i].text;
                     }
                 }
                 return '';
             }
         },
           {
               display: '礼物', width: 40, name: 'GiftEnabled', type: 'int',
               editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
               render: function (item) {
                   for (var i = 0; i < yesOrNo.length; i++) {
                       if (yesOrNo[i].val == item.GiftEnabled) {
                           return yesOrNo[i].text;
                       }
                   }
                   return '';
               }
           },
        {
            display: '图片', name: 'Image_Id', width: 100, isSort: false, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                    if (rowdata.Image_Id > 0) {
                        h += " <a href='javascript:updateImage(" + rowdata.Id + ',' + rowdata.Image_Id + ',' + rowindex + ")'>查看/修改</a> ";
                    }
                }
                return h;
            }
        },
        
        {
            display: '操作', isSort: false, width: 100, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                    h += " <a href='javascript:beginEdit(roomManager," + rowindex + ")'>修改</a> ";
                    h += " <a href='javascript:deleteRow(roomManager," + rowindex + ")'>删除</a> ";
                }
                else {
                    h += " <a href='javascript:endEdit(roomManager," + rowindex + ")'>提交</a> ";
                    h += " <a href='javascript:cancelEdit(roomManager," + rowindex + ")'>取消</a> ";
                }
                return h;
            }
        }
        ],
        url: "Home/GetRooms",
        pageSizeOptions: [10, 20], pageSize: 10,
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, allowUnSelectRow: true, checkbox: true, frozenCheckbox: false,
        width: '98%', height: '80%',
        onSelectRow: function (rowdata, index, row) {
            if (roomManager.selected.length == 1) {
                roomMgrManager.setEnabled();
                roomViceManager.setEnabled();
                roomMgrManager.setValue(0);
                roomViceManager.setValue(0);
                var roomRoleList = rowdata.RoomRoleList;
                if (roomRoleList) {
                    for (var i = 0; i < roomRoleList.length; i++) {
                        if (roomRoleList[i].Role_Id == 1001) //房间管理员
                        {
                            roomMgrManager.setValue(roomRoleList[i].User_Id);

                        }

                        if (roomRoleList[i].Role_Id == 1002) //房间管理员
                        {
                            roomViceManager.setValue(roomRoleList[i].User_Id);
                        }
                    }
                }
            }
            else {
                roomMgrManager.setValue(0);
                roomViceManager.setValue(0);
                roomMgrManager.setDisabled();
                roomViceManager.setDisabled();
            }
        },
        //detailToEdit: true,
        //detail: { onShowDetail: showDetail, height: 80 },
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(roomManager); }, icon: 'search2' },
                { text: '添加房间', click: function () { addNewRow(roomManager, 'NewRoom',false); } },
                { text: '保存修改', click: function () { saveAll(roomManager, 'SaveRooms'); } },
                { text: '删除选中纪录', click: function () { deleteSelected(roomManager); } }]
        }
    });


}

function showDetail(row, detailPanel, callback) {
    var grid = document.createElement('div');
    $(detailPanel).append(grid);
    $.ajax({
        type: "GET",
        cache: "false",
        url: "Home/GetRoom?roomId=" + row.Id,
        success: function (room) {
            $(grid).css('margin', 10).ligerGrid({
                columns: [
                    { display: 'ID', name: 'Id', width: 60, type: 'int', editor: { type: 'int' }, frozen: true, isSort: false },
                   
                ],
                isScroll: true,
                showToggleColBtn: false,
                width: '98%', height: '95%',
                data: room,
                showTitle: false,
                usePage: false,
                onAfterShowData: callback,
                frozen: false

            });
        },
        error: function (error)
        {
            alert(error);
        }
    });
}

function assignRooms() {
    var startId = $.trim(startIdTxtManager.getValue());
    var endId = $.trim(endIdTxtManager.getValue());
    var agentId = agentsManager.selectedValue;
   
    if (startId != '0' && endId != '0') {
        //ensure the startId and endId are valid
        //var startIdExist = false;
        //var endIdExist = false;
        //for (var i = 0; i < roomManager.data.length; i++) {
        //    if (roomManager.data[i].Id == startId)
        //        startIdExist = true;
        //    if (roomManager.data[i].Id == endId)
        //        endIdExist = true;
        //}
        //if (startIdExist == false)
        //{
        //    alert("开始房间不存在");
        //    return;
        //}
        //if (endIdExist == false)
        //{
        //    alert("结束房间不存在");
        //    return;
        //}
        if (agentsManager.selectedValue && agentsManager.selectedValue != "") {
            $.ajax({
                type: "POST",
                url: "Home/AssignRooms",
                data: { startId: startId, endId: endId, agentId: agentId },
                success: function (data) {
                    if (data.Success) {
                        alert('成功！');
                        roomManager.loadData();
                    }
                    else {
                        alert('失败：' + data.Message);
                    }
                },
                error: function (error) { alert(error); }
            })
        }
        else {
            agentsManager.element.focus();
            alert("必须选择代理！")
        }
    }
    else {
        alert("请设置起始，结束房间号码。");
    }
}

function setRoomRole() {
    if (roomManager.selected.length == 1) {
        if (roomMgrManager.getValue() != '0' && roomViceManager.getValue() != '0') {
            $.ajax({
                type: "POST",
                url: "Home/SetRoomRole",
                data: { roomId: roomManager.selected[0].Id, roomAdminId: $.trim(roomMgrManager.getValue()), roomDirId: $.trim(roomViceManager.getValue()) },
                success: function (data) {
                    if (data.success) {
                        alert('成功！');
                        roomManager.loadData();
                    }
                    else {
                        alert('失败：' + data.message);
                    }
                },
                error: function (error) { alert(error); }
            })
        }

        else {
            alert("请设置房间管理员，副室长号码。");
        }

    }
    else {
        alert("请选择一个房间。");
    }


}

function deleteRoomRole() {
    if (roomManager.selected.length == 1) {
        if (roomMgrManager.getValue() != '0' && roomViceManager.getValue() != '0') {
            $.ajax({
                type: "POST",
                url: "Home/DeleteRoomRole",
                data: { roomId: roomManager.selected[0].Id, roomAdminId: $.trim(roomMgrManager.getValue()), roomDirId: $.trim(roomViceManager.getValue()) },
                success: function (data) {
                    if (data.Success) {
                        alert('成功！');
                        roomMgrManager.value = "";
                        roomViceManager.value = "";
                        roomManager.loadData();
                    }
                    else {
                        alert('失败：' + data.Message);
                    }
                },
                error: function (error) { alert(error); }
            })
        }
        else {
            alert("还没有为此房间设置房间管理员与副室长号码。");
        }
    }
    else {
        alert("请选择一个房间。");
    }
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Room/' + id + '/' + imageid, id, imageid, index);
}