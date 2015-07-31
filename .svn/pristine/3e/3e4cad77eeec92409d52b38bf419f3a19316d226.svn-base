var applications;

var roleManager;

function Init(tag) {
    $.ajax({
        type: "GET",
        url: "Home/GetRoleCommandApplications",
        cache: false,
        success: function (apps) {
            if (apps.Success) {
                applications = apps.Rows;
                InitGrid(tag);
            }
            else {
                alert('失败！', apps.Message);
            }
        },
        error: function (error) { alert(error); }
    });
}
function InitGrid(tag) {    
    roleManager = $(tag).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 50, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '名称', name: 'Name', type: 'string', isSort: false,
            editor: { type: 'text' }
        },
        { display: '描述', name: 'Description', type: 'string', editor: { type: 'text' }, isSort: false },
        {
            display: '应用程序', name: 'Application_Id', width: 120, isSort: false,
            editor: { type: 'select', data: applications, valueField: 'Id', textField: 'Name' }, render: function (item) {
                for (var i = 0; i < applications.length; i++) {
                    if (applications[i].Id == item.Application_Id)
                        return applications[i].Name;
                }
                return '';
            }
        },
        { display: '序号', name: 'Order', type: 'int', editor: { type: 'int' }, isSort: true },
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
            display: '操作', isSort: false, width: 100, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata.ReadOnly) {
                    if (!rowdata._editing) {
                        h += " <a href='javascript:beginEdit(roleManager," + rowindex + ")'>修改</a> ";
                        h += " <a href='javascript:deleteRow(roleManager," + rowindex + ")'>删除</a> ";
                    }
                    else {
                        h += " <a href='javascript:endEdit(roleManager," + rowindex + ")'>提交</a> ";
                        h += " <a href='javascript:cancelEdit(roleManager," + rowindex + ")'>取消</a> ";
                    }
                }
                return h;
            }
        }
        ],
        url: "Home/GetRoles?includeAll=false",
        enabledEdit: true, checkbox: false, rownumbers: false, IsScroll: true, clickToEdit: false,
        width: '95%', height: '95%',
        pageSizeOptions: [20, 40,60], pageSize: 40,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(roleManager); }, icon: 'search2' },
                { text: '添加用户级别', click: function () { addNewRow(roleManager, 'NewRole'); } },
                { text: '保存修改', click: function () { saveAll(roleManager, 'SaveRoles'); } }]
        }
    });
}
function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Role/' + id + '/' + imageid, id, imageid, index);
}