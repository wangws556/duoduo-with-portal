var blockListManager;
var blockHistoryManager;
var blockTypes;
var applications;

function Init(blockListId, blockHistoryId) {
    $.ajax({
        url: 'Home/GetApplications?includeAll=true',
        type: 'GET',
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (apps) {
            if (apps.Success) {
                applications = apps.Rows;
                $.ajax({
                    url: "Home/GetBlockTypes",
                    type: "GET",
                    cache: false,
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            blockTypes = data.Rows;
                            InitGrid(blockListId, blockHistoryId);
                        }
                        else {
                            alert('失败！' + data.Message);
                        }
                    },
                    error: function (error) { alert(error) }
                });
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

function InitGrid(blockListId, blockHistoryId) {
    blockListManager = $(blockListId).ligerGrid({
        columns: [
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
                {
                    display: '禁封类型', name: 'BlockType_Id', width: 60, type: 'int', isSort: true, editor: { type: 'select', data: blockTypes, valueField: 'Id', textField: 'Name' }, render: function (item) {
                        for (var i = 0; i < blockTypes.length; i++) {
                            if (blockTypes[i].Id == item.BlockType_Id) {
                                return blockTypes[i].Name;
                            }
                        }
                        return '';
                    }
                },
                { display: '内容', name: 'Content', width: 100, type: 'string', isSort: false, editor: { type: 'text' } },
                {
                    display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata.ReadOnly) {
                            if (!rowdata._editing) {
                                h += " <a href='javascript:beginEdit(blockListManager," + rowindex + ")'>修改</a> ";
                                h += " <a href='javascript:deleteRow(blockListManager," + rowindex + ")'>解除</a> ";
                            }
                            else {
                                h += " <a href='javascript:endEdit(blockListManager," + rowindex + ")'>提交</a> ";
                                h += " <a href='javascript:cancelEdit(blockListManager," + rowindex + ")'>取消</a> ";
                            }
                        }
                        return h;
                    }
                }
        ],
        url: "Home/GetBlockList",
        pageSizeOptions: [20, 40], pageSize: 40, width: '95%', height: '95%',
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: false, rownumbers: false, allowAdjustColWidth: false, checkbox: false,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(blockListManager); }, icon: 'search2' },
                { text: '添加禁封', click: function () { addNewRow(blockListManager, 'NewBlockList',true); } },
                { text: '保存修改', click: function () { saveAll(blockListManager, 'SaveBlockList'); } }
            ]
        }
    });

    blockHistoryManager = $(blockHistoryId).ligerGrid({
        columns: [
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
                { display: '操作人', name: 'OptUser_Id', type: 'int', width: 80, isSort: true, editor: { type: 'text' } },
                { display: '禁封时间', name: 'Time', type: 'date', isSort: false, width: 80, editor: { type: 'text' } },
                {
                    display: '禁封类型', name: 'BlockType_Id', type: 'int', width: 100, isSort: true, editor: { type: 'select', data: blockTypes, valueField: 'Id', textField: 'Name' }, render: function (item) {
                        for (var i = 0; i < blockTypes.length; i++) {
                            if (blockTypes[i].Id == item.BlockType_Id) {
                                return blockTypes[i].Name;
                            }
                        }
                        return '';
                    }
                },
                 {
                     display: '禁封', width: 60, name: 'IsBlock', type: 'int',
                     editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
                     render: function (item) {
                         for (var i = 0; i < yesOrNo.length; i++) {
                             if (yesOrNo[i].val == item.IsBlock) {
                                 return yesOrNo[i].text;
                             }
                         }
                         return '';
                     }
                 },
                 { display: '内容', name: 'Content', width: 100, type: 'string', isSort: false, editor: { type: 'text' } }
        ],
        url: "Home/GetBlockHistory",
        pageSizeOptions: [20, 40], pageSize: 40, width: '95%', height: '95%',
        dataAction: "server",
        enabledEdit: false, clickToEdit: false, IsScroll: false, rownumbers: false, allowAdjustColWidth: false,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(blockListManager); }, icon: 'search2' }]
        }
    });
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Block/' + id + '/' + imageid, id, imageid, index);
}