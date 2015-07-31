var roomIncomeManager;
var rooms;

function Init(gridTagId) {
    $.ajax({
        type: "GET",
        url: "Home/GetRooms",
        cache: false,
        success: function (allRooms) {
            if (allRooms.Success) {
                rooms = allRooms.Rows;
            }
            else {
                alert('失败！', allRooms.Message);
            }
        },
        error: function (error) { alert(error); }
    })

    InitGrid(gridTagId);
}

function InitGrid(gridTagId) {
    roomIncomeManager = $(gridTagId).ligerGrid({
        columns: [
        { display: 'ID', name: 'Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '房间', name: 'Room_Id', width: 150, type: 'int',
            editor: { type: 'select', data: rooms, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < rooms.length; i++) {
                    if (item.Room_Id == rooms[i].Id) {
                        return rooms[i].Name;
                    }
                }
                return '';
            }
        },

        { display: '开始时间', name: 'FromDate', width: 150, type: 'date', editor: { type: 'date' }, IsFrozen: true, isSort: true },
        { display: '结束时间', name: 'ToDate', width: 150, type: 'date', editor: { type: 'date' }, IsFrozen: true, isSort: true }
        ],
        url: "Home/GetGiftsInOutHistories",
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false,
        width: '95%',
        pageSizeOptions: [10, 20, 40, 80], pageSize: 20,
        toolbar: { items: [{ text: '自定义查询', click: search, icon: 'search2' }] }
    });
}

function search() {
    roomIncomeManager.showFilter();
}