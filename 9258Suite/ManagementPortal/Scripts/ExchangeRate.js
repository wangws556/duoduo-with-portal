var exchangeRateManager
var applications;
function Init(exchangeRateId) {
    $.ajax({
    	url: 'Home/GetExchangeRateCommandApplications',
        type: 'GET',
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (apps) {
            applications = apps.Rows;
            exchangeRateManager = $(exchangeRateId).ligerGrid({
                columns: [
                    //{ display: 'ID', name: 'Id', width: 60, type: 'int', isSort: false, editor: { type: 'int' }, IsFrozen: true },
                    { display: '积分兑换金币率(%)', width: 150, name: 'ScoreToMoney', type: 'int', editor: { type: 'int' } },
                    { display: '积分兑换现金率(%)', width: 150, name: 'ScoreToCache', type: 'int', editor: { type: 'int' } },
                    { display: '金币兑换现金率(%)', width: 150, name: 'MoneyToCache', type: 'int', editor: { type: 'int' } },
                    { display: '汇率有效时间', name: 'ValidTime', width: 150, type: 'date', width: 100, editor: { type: 'text' } },
                    {
                        display: '应用程序', name: 'Application_Id', width: 150, isSort: false,
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
                        display: '操作', isSort: false, width: 80, render: function (rowdata, rowindex, value) {
                            var h = "";
                            if (!rowdata.ReadOnly) {
                                if (!rowdata._editing) {
                                    h += " <a href='javascript:beginEdit(exchangeRateManager," + rowindex + ")'>修改</a> ";
                                    h += " <a href='javascript:deleteRow(exchangeRateManager," + rowindex + ")'>删除</a> ";
                                }
                                else {
                                    h += " <a href='javascript:endEdit(exchangeRateManager," + rowindex + ")'>提交</a> ";
                                    h += " <a href='javascript:cancelEdit(exchangeRateManager," + rowindex + ")'>取消</a> ";
                                }
                            }
                            return h;
                        }
                    }
                ],
                url: "Home/GetAllExchangeRate",
                pageSizeOptions: [5, 10], pageSize: 5, width: '95%',
                dataAction: "server",
                enabledEdit: true, clickToEdit: false, IsScroll: false, rownumbers: false, allowAdjustColWidth: false,
                toolbar: {
                    items: [{ text: '添加', click: function () { addNewRow(exchangeRateManager, 'NewExchangeRate',true) } },
                            { text: '保存修改', click: function () { saveAll(exchangeRateManager, 'SaveExchangeRate'); } }]
                }
            })
        },
        error: function (error) {
            alert(error);
        }
    });
   
}