var giftInOutManager;
var giftInOutUsers;
var gifts;
var rooms;

function Init(gridTagId) {
    $.ajax({
        type: "GET",
        url: "Home/GetUsers",
        cache: false,
        success: function (users) {
            if (users.Success) {
                giftInOutUsers = users.Rows;
            }
            else {
                alert('失败！', users.Message);
            }
        },
        error: function (error) { alert(error); }
    })

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

    $.ajax({
        type: "GET",
        url: "Home/GetGifts",
        cache: false,
        success: function (allGifts) {
            if (allGifts.Success) {
                gifts = allGifts.Rows;
            }
            else {
                alert('失败！', allGifts.Message);
            }
        },
        error: function (error) { alert(error); }
    })

    
    InitGrid(gridTagId);
    
}

function InitGrid(gridTagId) {
    giftInOutManager = $(gridTagId).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        {
            display: '赠送人', name: 'SourceUser_Id', width: 150, type:'int',
            editor: { type: 'select', data: giftInOutUsers, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < giftInOutUsers.length; i++) {
                    if (item.SourceUser_Id == giftInOutUsers[i].Id) {
                        return giftInOutUsers[i].Name;
                    }
                }
                return '';
            }
        },
        {
            display: '接收人', name: 'TargetUser_Id', width: 150, type: 'int',
            editor: { type: 'select', data: giftInOutUsers, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < giftInOutUsers.length; i++) {
                    if (item.TargetUser_Id == giftInOutUsers[i].Id) {
                        return giftInOutUsers[i].Name;
                    }
                }
                return '';
            }
        },

        {
            display: '房间', name: 'Room_Id', width: 100, type: 'int',
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

        {
            display: '礼物', name: 'Gift_Id', width: 100, type: 'int',
            editor: { type: 'select', data: gifts, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < giftInOutUsers.length; i++) {
                    if (item.Gift_Id == gifts[i].Id) {
                        return gifts[i].Name;
                    }
                }
                return '';
            }
        },

        { display: '数量', name: 'Count', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '赠送时间', name: 'Time', width: 150, type: 'date', editor: { type: 'date' }, IsFrozen: true, isSort: true }
        ],
        url: "Home/GetGiftsInOutHistories",
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false,
        width: '95%', height: '95%',
        pageSizeOptions: [10, 20, 40, 80], pageSize: 20,
        toolbar: { items: [{ text: '自定义查询', click: search, icon: 'search2' }] }
    });
}

function search() {
    giftInOutManager.showFilter();
}