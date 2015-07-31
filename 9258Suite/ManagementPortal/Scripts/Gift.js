
var giftManager;
var giftGroups;

function InitGrid(gridId) {
    giftManager = $(gridId).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 60, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '礼物名称', name: 'Name', width: 80, type: 'string', editor: { type: 'text' }, isSort: false },
        { display: '描述', name: 'Description', width: 60, type: 'string', editor: { type: 'text' }, isSort: false },
        { display: '单价', name: 'Price', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '积分', name: 'Score', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '金币', name: 'Money', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '单位', name: 'Unit', width: 60, type: 'string', editor: { type: 'string' }, isSort: false },
        { display: '跑道', name: 'RunWay', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '小喇叭', name: 'RoomBroadcast', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
        { display: '大喇叭', name: 'WorldBroadcast', width: 60, type: 'int', editor: { type: 'int' }, isSort: false },
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
              display: '礼物组', name: 'GiftGroup_Id', type: 'int', isSort: true, editor: { type: 'select', data: giftGroups, valueField: 'Id', textField: 'Name' }, render: function (item) {
                  for (var i = 0; i < giftGroups.length; i++) {
                      if (giftGroups[i].Id == item.GiftGroup_Id) {
                          return giftGroups[i].Name;
                      }
                  }
                  return '';
              }
          },
          {
              display: '操作', isSort: false, width: 100, render: function (rowdata, rowindex, value) {
                  var h = "";
                  if (!rowdata._editing) {
                      h += " <a href='javascript:beginEdit(giftManager," + rowindex + ")'>修改</a> ";
                      h += " <a href='javascript:deleteRow(giftManager," + rowindex + ")'>删除</a> ";
                  }
                  else {
                      h += " <a href='javascript:endEdit(giftManager," + rowindex + ")'>提交</a> ";
                      h += " <a href='javascript:cancelEdit(giftManager," + rowindex + ")'>取消</a> ";
                  }
                  return h;
              }
          }
        ],
        url: "Home/GetGifts",
        pageSizeOptions: [20, 40, 80], pageSize: 40, width: '95%', height: '95%',
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, checkbox: true, allowAdjustColWidth: false,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(giftManager); }, icon: 'search2' },
                { text: '添加礼物', click: function () { addNewRow(giftManager, 'NewGift'); } },
                { text: '保存修改', click: function () { saveAll(giftManager, 'SaveGifts'); } },
                { text: '删除选中纪录', click: function () { deleteSelected(giftManager);} }]
        }
    });
}

function Init(gridTagId) {
    $.ajax({
        type: "GET",
        url: "Home/GetGiftGroups",
        cache: false,
        success: function (groups) {
            if (groups.Success) {
                giftGroups = groups.Rows;
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
}


function updateImage(id, imageid, index) {
    uploadImage('Home/UploadImage/Gift/' + id + '/' + imageid, index);
}