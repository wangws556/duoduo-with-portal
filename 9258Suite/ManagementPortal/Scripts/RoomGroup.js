
var roomGroupManager;
var parentRoomGroups = new Array();
function InitPage(gridTagId) {
    $.ajax({
        url: 'Home/GetRoomGroups',
        type: 'GET',
        cache: false,
        success: function (groups) {
            if (groups.Success) {
                parentRoomGroups = groups.Rows;
                parentRoomGroups.unshift(groups.EmptyGroup);
                roomGroupManager = $(gridTagId).ligerGrid({
                    columns: [

                    { display: '房建组Id', name: 'Id', width: 60, editor: { type: 'int' }, type: 'int', isSort: false },
                    { display: '房间组名称', name: 'Name', width: 100, type: 'string', editor: { type: 'text' }, isSort: true },
                    { display: '描述', name: 'Description', width: 150, type: 'string', editor: { type: 'text' }, isSort: false },
                    {
                        display: '上级房间组', name: 'ParentGroup_Id', type: 'int', width: 120, isSort: true,
                        editor: { type: 'select', data: parentRoomGroups, valueField: 'Id', textField: 'Name' }, render: function (item) {
                            for (var i = 0; i < parentRoomGroups.length; i++) {
                                if (parentRoomGroups[i].Id == item.ParentGroup_Id)
                                    return parentRoomGroups[i].Name;
                            }
                            return ' ';
                        }
                    },
                    //{
                    //    display: '房间', width: 80, render: function (rowdata, rowindex, value) {
                    //        var h = '';
                    //        //if (!rowdata.ParentGroup_Id || rowdata.ParentGroup_Id == '') {
                    //        //    h += " <p>浏览</p> ";
                    //        //}
                    //        //else {
                    //            h += " <a href='javascript:showRooms(" + rowdata.Id + ")'>浏览</a> ";
                    //        //}
                    //        return h;
                    //    }
                    //},
                    {
                        display: '启用', width: 50, name: 'Enabled',type:'int', 
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
                          display: '图片', name: 'Image_Id', width: 80, isSort: false, render: function (rowdata, rowindex, value) {
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
                          display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                              var h = "";
                              if (!rowdata._editing) {
                                  h += " <a href='javascript:beginEdit(roomGroupManager," + rowindex + ")'>修改</a> ";
                                  h += " <a href='javascript:deleteRow(" + rowindex + ")'>删除</a> ";
                              }
                              else {
                                  h += " <a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
                                  h += " <a href='javascript:cancelEdit(roomGroupManager," + rowindex + ")'>取消</a> ";
                              }
                              return h;
                          }
                      }
                    ],
                    url: "Home/GetRoomGroups",
                    pageSizeOptions: [20, 40, 80], pageSize: 40, width: '95%', height: '95%',
                    dataAction: "server",
                    enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, checkbox: true, allowAdjustColWidth: false,
                    toolbar: {
                        items: [{ text: '自定义查询', click: function () { search(roomGroupManager); }, icon: 'search2' },
                                { text: '添加房间组', click: function () { add(); } },
                                { text: '保存修改', click: function () { saveAll(roomGroupManager, 'SaveRoomGroups'); } },
                                { text: '删除选中纪录', click: function () { deleteSelectedRows(); } }]
                    }
                });
            }
            else {
                alert('失败！', groups.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });

}

function showRooms(groupId) {
    $.ajax({
        url: "Home/GetRooms",
        type: 'GET',
        cache: false,
        datatype: 'json',
        success: function (row) {
            if (allRooms.Success) {
                rooms = allRooms.Rows;
            }
            else {
                alert('失败！', row.Message);
            }
        },
        error: function (error) { alert(error); }
    });
}


function endEdit(rowIndex) {
    roomGroupManager.endEdit(rowIndex);
    parentRoomGroups[rowIndex + 1].Name = roomGroupManager.data.Rows[rowIndex].Name;
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/RoomGroup/' + id + '/' + imageid, id, imageid, index);
}

function deleteRow(index) {
    if (roomGroupManager) {
        if (confirm('确定删除?')) {
            parentRoomGroups.splice(index + 1, index + 1);
            roomGroupManager.deleteRow(index);
        }
    }
}

function deleteSelectedRows() {
    if (roomGroupManager) {
        if (confirm('确定删除?')) {
            var rowData = roomGroupManager.getCheckedRows();
            var deleteIndex = new Array();
            $.each(rowData, function (key, val) {
                $.each(parentRoomGroups, function (gKey, gVal) {
                    if (gVal.Id == val.Id) {
                        deleteIndex.push(gKey);
                    }
                })
            })

            var offset = 1;

            $.each(deleteIndex, function (key, val) {
                parentRoomGroups.splice(val - offset, 1);
                offset = offset + 1;
            })

            roomGroupManager.deleteRange(rowData);
        }
    }
}

function add() {
    if (roomGroupManager) {
        $.ajax({
            url: "Home/NewRoomGroup",
            type: 'POST',
            cache: false,
            datatype: 'json',
            success: function (row) {
                if (row.Success) {
                    roomGroupManager.addRow(row.Model,roomGroupManager.getSelectedRow(),false);
                    parentRoomGroups.push(row.Model);
                }
                else {
                    alert('失败！', row.Message);
                }
            },
            error: function (error) { alert(error); }
        });
    }
}