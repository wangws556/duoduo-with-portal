var blockTypeManager

function Init(blockTypeId) {
    blockTypeManager = $(blockTypeId).ligerGrid({
        columns: [
            //{ display: 'ID', name: 'Id', width: 150, type: 'int', isSort: false, editor: { type: 'int' },IsFrozen:true },
            { display: '名称', name: 'Name', width: 150, type: 'string', isSort: false, editor: { type: 'text' } },
            { display: '描述', name: 'Description', width: 150, type: 'string', isSort: false, editor: { type: 'text' } },
            { display: '禁封期限(天)', name: 'Days', width: 150, type: 'int', isSort: false, editor: { type: 'int' } },
            {
                display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata.ReadOnly) {
                        if (!rowdata._editing) {
                            h += " <a href='javascript:beginEdit(blockTypeManager," + rowindex + ")'>修改</a> ";
                            h += " <a href='javascript:deleteRow(blockTypeManager," + rowindex + ")'>删除</a> ";
                        }
                        else {
                            h += " <a href='javascript:endEdit(blockTypeManager," + rowindex + ")'>提交</a> ";
                            h += " <a href='javascript:cancelEdit(blockTypeManager," + rowindex + ")'>取消</a> ";
                        }
                    }
                    return h;
                }
            }
        ],
        url: "Home/GetBlockTypes",
        pageSizeOptions: [5, 10], pageSize: 5, width: '95%',
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: false, rownumbers: false, allowAdjustColWidth: false,
        toolbar: {
            items: [{ text: '添加禁封类型', click: function () { addNewRow(blockTypeManager, 'NewBlockType',true) }},
                    { text: '保存修改', click: function () { saveAll(blockTypeManager, 'SaveBlockType'); } },
                    { text: '删除选中纪录', click: function () { deleteSelected(blockTypeManager); } }]
        }
    })
     
}