var userScoreManager;
var userScoreApplications;

function Init(gridTagId) {
    $.ajax(
       {
           type: "GET",
           url: "Home/GetExchangeHistoryCommandApplications",
           cache: false,
           success: function (roles) {
               if (roles.Success) {
                   userScoreApplications = roles.Rows;
                   InitGrid(gridTagId);
               }
               else {
                   alert('失败！', roles.Message);
               }
           },
           error: function (error) { alert(error); }
       });
}

function InitGrid(gridTagId) {
    userScoreManager = $(gridTagId).ligerGrid({
        columns: [
        //{ display: 'ID', name: 'Id', width: 80, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
        { display: '操作人员ID', name: 'OptUser_Id', width: 120, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '会员ID', name: 'User_Id', width: 120, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        {
            display: '应用程序', name: 'Application_Id', width: 150, type: 'int',
            editor: { type: 'select', data: userScoreApplications, textField: 'Name', valueField: 'Id' }, IsFrozen: true,
            isSort: true, render: function (item) {
                for (var i = 0; i < userScoreApplications.length; i++) {
                    if (item.Application_Id == userScoreApplications[i].Id) {
                        return userScoreApplications[i].Name;
                    }
                }
                return '';
            }
        },
        { display: '积分', name: 'Score', width: 120, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '金币', name: 'Money', width: 120, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: true },
        { display: '时间', name: 'Time', width: 120, type: 'date', editor: { type: 'date' }, IsFrozen: true, isSort: true }
        ],
        url: "Home/GetUserScoreHistories",
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false,
        width: '95%', height: '95%',
        pageSizeOptions: [10, 20, 40, 80], pageSize: 20,
        toolbar: { items: [{ text: '自定义查询', click: search, icon: 'search2' }] }
    });
}

function search() {
    userScoreManager.showFilter();
}