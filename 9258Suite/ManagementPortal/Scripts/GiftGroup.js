
var giftGroupManager

function InitPage(gridTagId) {
    giftGroupManager = $(gridTagId).ligerGrid({
        columns: [          
        //{ display: 'ID', name: 'Id', width: 30, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '礼物组名称', name: 'Name', width: 100, type: 'string', editor: { type: 'text' }, isSort: false },
        { display: '描述', name: 'Description', width: 120, type: 'string', editor: { type: 'text' }, isSort: false },
          {
              display: '图片', name: 'Image_Id', width: 60, isSort: false, render: function (rowdata, rowindex, value) {
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
              display: '操作', isSort: false, width: 60, render: function (rowdata, rowindex, value) {
                  var h = "";                  
                  if (!rowdata._editing) {
                      h += " <a href='javascript:beginEdit(giftGroupManager," + rowindex + ")'>修改</a> ";
                      h += " <a href='javascript:deleteRow(giftGroupManager," + rowindex + ")'>删除</a> ";
                  }
                  else {
                      h += " <a href='javascript:endEdit(giftGroupManager," + rowindex + ")'>提交</a> ";
                      h += " <a href='javascript:cancelEdit(giftGroupManager," + rowindex + ")'>取消</a> ";
                  }
                  return h;
              }
          }
        ],
        url: "Home/GetGiftGroups",
        pageSizeOptions: [20, 40, 80], pageSize: 40, width: '95%', height: '95%',
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, checkbox: true, allowAdjustColWidth: false,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(giftGroupManager); }, icon: 'search2' },
                { text: '添加礼物组', click: function () { addNewRow(giftGroupManager, 'NewGiftGroup'); } },
                { text: '保存修改', click: function () { saveAll(giftGroupManager, 'SaveGiftGroups'); } },
                { text: '删除选中纪录', click: function () { deleteSelected(giftGroupManager);} }]
        }
    });
       
}

function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/GiftGroup/' + id + '/' + imageid, id, imageid, index);
}