var appManager;
var tagId;

function InitGrid(tag) {
    tagId = "#" + tag;
    appManager = $(tagId).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 50, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '应用程序名称', name: 'Name', type: 'string', isSort: false,
            editor: { type: 'text' }
        },
        {
            display: '主页', name: 'HomeAddress', type: 'string', isSort: false,
            editor: { type: 'text' }
        },
        { display: '描述', name: 'Description', type: 'string', editor: { type: 'text' }, isSort: false },
        {
            display: '图片', name: 'Image_Id', width: 80, isSort: false, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata.ReadOnly) {
                    if (!rowdata._editing) {
                        if (rowdata.Image_Id > 0) {
                            h += " <a href='javascript:updateImage(" + rowdata.Id + ',' + rowdata.Image_Id + ',' + rowindex + ")'>查看/修改</a> ";
                        }
                    }
                }
                return h;
            }
        },
        {
            display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata.ReadOnly) {
                    if (!rowdata._editing) {
                        h += " <a href='javascript:beginEdit(appManager," + rowindex + ")'>修改</a> ";
                        h += " <a href='javascript:deleteRow(appManager," + rowindex + ")'>删除</a> ";
                    }
                    else {
                        h += " <a href='javascript:endEdit(appManager," + rowindex + ")'>提交</a> ";
                        h += " <a href='javascript:cancelEdit(appManager," + rowindex + ")'>取消</a> ";
                    }
                }
                return h;
            }
        }
        ],
        url: "Home/GetApplications?includeAll=false",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false,
        width: '95%',height:'95%',
        pageSizeOptions: [5, 10], pageSize: 5,
        toolbar: {
            items: [{ text: '添加应用程序', click: function () { addNewRow(appManager, 'NewApplication',true); } },
                { text: '保存修改', click: function () { saveAll(appManager, 'SaveApplications');} }]
        }
    });
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Application/' + id + '/' + imageid, id, imageid, index);
}
