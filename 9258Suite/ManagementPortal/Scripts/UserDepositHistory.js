var userDepositManager;
var userDepositApplications;

function Init(gridTagId) {
    $.ajax(
       {
           type: "GET",
           url: "Home/GetUserDepositCommandApplications",
           cache: false,
           success: function (apps) {
               if (apps.Success) {
                   userDepositApplications = apps.Rows;
                   InitGrid(gridTagId);
               }
               else {
                   alert('失败！', apps.Message);
               }
           },
           error: function (error) { alert(error); }
       });
}


function InitGrid(gridTagId) {
    userDepositManager = $(gridTagId).ligerGrid({
        columns: [
        { display: 'ID', name: 'Id', width: 100, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '操作人员ID', name: 'OptUser_Id', width: 150, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '会员ID', name: 'User_Id', width: 150, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        {
            display: '应用程序', name: 'Application_Id', width: 150, type: 'int',
            editor: { type: 'select', data: userDepositApplications, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < userDepositApplications.length; i++) {
                    if (item.Application_Id == userDepositApplications[i].Id) {
                        return userDepositApplications[i].Name;
                    }                    
                }
                return '';
            }
        },
        {
            display: '代理充值', width: 100, name: 'IsAgent', type: 'int',
            editor: { type: 'select', data: yesOrNo, valueField: 'val', textField: 'text' },
            render: function (item) {
                for (var i = 0; i < yesOrNo.length; i++) {
                    if (yesOrNo[i].val == item.IsAgent) {
                        return yesOrNo[i].text;
                    }
                }
                return '';
            }
        },
        { display: '充值金额', name: 'Money', width: 150, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '充值时间', name: 'Time', width: 150, type: 'date', editor: { type: 'date' }, IsFrozen: true, isSort: true }
        ],
        url: "Home/GetUserDepositHistories",
        dataAction: "server",
        enabledEdit: false, clickToEdit: false, IsScroll: true, rownumbers: false,
        width: '95%', height: '95%',
        pageSizeOptions: [10, 20, 40, 80], pageSize: 20,
        toolbar: { items: [{ text: '自定义查询', click: search, icon: 'search2' }] }
    });
}

function search() {
    userDepositManager.showFilter();
}