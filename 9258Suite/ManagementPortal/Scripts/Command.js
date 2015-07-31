var cmdManager, applications;
var cmdTypes = [{ val: 1, text: '后台权限' }, { val: 2, text: '前台权限' }, { val: 4, text: '用户权限' }];

function InitGrid(tagId) {
    $.ajax({
    	url: 'Home/GetCommandApplications',
        type: 'GET',
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (apps) {
            if (apps.Success) {
                applications = apps.Rows;
                Init(tagId);
            }
            else {
                alert('失败！' + apps.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function Init(tagId) {
    cmdManager = $(tagId).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 50, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '权限名称', name: 'Name', type: 'string', isSort: false, editor: { type: 'text' } },
        { display: '描述', name: 'Description', type: 'string', editor: { type: 'text' }, isSort: false },
        {
            display: '图片', name: 'Image_Id', width: 80, isSort: false, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                    h += " <a href='javascript:updateImage(" + rowdata.Id + ',' + rowdata.Image_Id + ',' + rowindex + ")'>查看/修改</a> ";
                }
                return h;
            }
        },
        {
            display: '权限类型', width: 100, name: 'CommandType', type: 'int',
            editor: { type: 'select', data: cmdTypes, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < cmdTypes.length; i++)
                {
                    if (cmdTypes[i].val == item.CommandType)
                    {
                        return cmdTypes[i].text;
                    }
                }                
                return '';
            }
        },
        {
            display: '应用程序', name: 'Application_Id', width: 120, isSort: false,
            editor: { type: 'select', data: applications, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < applications.length; i++) {
                    if (applications[i].Id == item.Application_Id) {
                        return applications[i].Name;
                    }
                }
                return '';
            }

        },
        { display: '金币', width: 100, name: 'Money', type: 'int',editor: { type: 'int' } },
        {
            display: '操作', isSort: false, width: 100, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata.ReadOnly) {
                    if (!rowdata._editing) {
                        h += "<a href='javascript:beginEdit(cmdManager," + rowindex + ")'>修改</a> ";
                        h += "<a href='javascript:deleteRow(cmdManager," + rowindex + ")'>删除</a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(cmdManager," + rowindex + ")'>提交</a> ";
                        h += "<a href='javascript:cancelEdit(cmdManager," + rowindex + ")'>取消</a> ";
                    }
                }
                return h;
            }
        }
        ],
        url: "Home/GetCommands?cmdType=-1&includeAll=false",
        pageSizeOptions: [10, 20,40], pageSize: 20,
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, checkbox: false,
        width: '95%', height: '95%',
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(cmdManager); }, icon: 'search2' },
                { text: '添加权限', click: function () { addNewRow(cmdManager, 'NewCommand',true); } },
                { text: '保存修改', click: function () { saveAll(cmdManager, 'SaveCommands'); } }]
        }
    });
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Command/' + id + '/' + imageid, id, imageid, index);
}