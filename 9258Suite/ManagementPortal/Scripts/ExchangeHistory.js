var statusArray;
var exchangeMgr;
var settlementMgr;
var availableScoreMgr;
var availableMoneyMgr;
var scoreToCacheMgr;
var moneyToCacheMgr;
var validTimeMgr;
var scoreMgr;
var moneyMgr;
var appMgr;
var applications;

function Init(gridId, appId,settlementId, scoreToCacheId, moneyToCacheId, validTimeId, availableScoreId,
            availableMoneyId, scoreId, moneyId) {
   
    scoreToCacheMgr = $(scoreToCacheId).ligerTextBox({
        label: '积分兑换现金率（%）',
        labelWidth: 130,
        disabled: true,
    })
    moneyToCacheMgr = $(moneyToCacheId).ligerTextBox({
        label: '金币兑换现金率（%）',
        labelWidth: 130,
        disabled: true,
    })
    validTimeMgr = $(validTimeId).ligerTextBox({
        label: '汇率有效时间',
        labelWidth: 130,
        disabled: true,
    })

    availableScoreMgr = $(availableScoreId).ligerTextBox({
        label: '可用积分',
        labelWidth:130,
        disabled: true,
    })
    availableMoneyMgr = $(availableMoneyId).ligerTextBox({
        label: '可用金币',
        labelWidth: 130,
        disabled: true,
    })

    scoreMgr = $(scoreId).ligerTextBox({
        label: '积分',
        labelWidth: 130,
        digits: true
    })
    moneyMgr = $(moneyId).ligerTextBox({
        label: '金币',
        labelWidth: 130,
        digits: true
    })

    $.ajax({
        type: "GET",
        url: "Home/GetApplications",
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data.Success) {
                statusArray = data.Status;
                applications = data.Rows;
                appMgr = $(appId).ligerComboBox({
                    data: data.Rows,
                    valueField: "Id",
                    textField: "Name",
                    isMultiSelect: false,
                    onSelected: function (newvalue) {
                        if (newvalue) {
                            SetScoreInfo(newvalue);
                            SetExchangeRate(newvalue);
                        }

                    }
                });
            }
            else {
                alert('失败！' + data.Message);
            }
        },
        error: function (error) { alert(error); }
    })

        exchangeMgr = $(gridId).ligerGrid({
            columns: [
            { display: '操作人', name: 'OptUser_Id', width: 60, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
            { display: '兑换人', name: 'User_Id', width: 60, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
                { display: '申请时间', name: 'ApplyTime', width: 150, type: 'date', width: 100, editor: { type: 'date' } },
                { display: '结算时间', name: 'SettlementTime', width: 150, type: 'date', width: 100, editor: { type: 'date' } },

            { display: '积分', name: 'Score', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
            { display: '金币', name: 'Money', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
            { display: '现金', name: 'Cache', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
			{
			    display: '状态', name: 'Status', width: 100, type: 'int', editor: { type: 'select', data: statusArray, valueField: 'Id', textField: 'Name' },
				render: function (item) {
					if (statusArray) {
					    for (var i = 0; i < statusArray.length; i++) {
					        if (statusArray[i].Id == item.Status) {
					            return statusArray[i].Name;
					        }
					    }
					}
					return '';
				}
			}
            ],
            url: "Home/GetExchangeHistories?exchangeCache=true",
            pageSizeOptions: [5, 10], pageSize: 5,
            dataAction: "server",
            enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, allowUnSelectRow: true, checkbox: true, frozenCheckbox: false,
            width: '98%', height: '70%',
            toolbar: {
                items: [{ text: '取消', click: function () { CancelExchange(); } },
                    { text: '确认收款', click: function () { ConfirmExchangeCache(); } }]
            }
        });

            settlementMgr = $(settlementId).ligerGrid({
                columns: [
                { display: '操作人', name: 'OptUser_Id', width: 60, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
                { display: '兑换人', name: 'User_Id', width: 60, type: 'int', editor: { type: 'int' }, IsFrozen: true, isSort: false },
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
                  { display: '申请时间', name: 'ApplyTime', width: 150, type: 'date',  width: 100, editor: { type: 'date' } },
                  { display: '结算时间', name: 'SettlementTime', width: 150, type: 'date',  width: 100, editor: { type: 'date' } },

                { display: '积分', name: 'Score', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
                { display: '金币', name: 'Money', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
                { display: '现金', name: 'Cache', width: 40, type: 'int', editor: { type: 'int' }, isSort: false },
				{
				    display: '状态', name: 'Status', width: 100, type: 'int', editor: { type: 'select', data: statusArray, valueField: 'Id', textField: 'Name' },
					render: function (item) {
					    if (statusArray) {
					        for (var i = 0; i < statusArray.length; i++) {
					            if (statusArray[i].Id == item.Status) {
					                return statusArray[i].Name;
					            }
					        }
					    }
						return '';
					}
				}
                ],
                url: "Home/GetExchangeHistoryForSettlement",
                pageSizeOptions: [5, 10], pageSize: 5,
                dataAction: "server",
                enabledEdit: true, clickToEdit: false, IsScroll: true, rownumbers: false, allowUnSelectRow: true, checkbox: true, frozenCheckbox: false,
                width: '98%', height: '50%',
                toolbar: {
                    items: [
                        { text: '结算', click: function () { settlement(); } }]
                }
            });
        }
   


function SetScoreInfo(appId) {
    $.ajax({
        type: "GET",
        url: "Home/GetCurrentUserInfo?appId="+appId,
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (appInfo) {
            if (appInfo && appInfo.Success && appInfo.Model != null && appInfo.Message == '') {
                availableScoreMgr.setValue(appInfo.Model.Score);
                availableMoneyMgr.setValue(appInfo.Model.Money);
            }
        },
        error: function (error) {
            alert(error);
        }
    })
}

function SetExchangeRate(appId) {
    $.ajax({
        type: "GET",
        url: "Home/GetAllExchangeRate?latest=true&appId=" + appId,
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
        if (data.Success && data.Model != null)
        {
            scoreToCacheMgr.setValue(data.Model.ScoreToCache);
            moneyToCacheMgr.setValue(data.Model.MoneyToCache);
            validTimeMgr.setValue(data.Model.ValidTime);
            exchangeMgr.set('parms', { aid: appMgr.getValue() });
            exchangeMgr.loadData();
        }
    },
    error: function (error)
    {
        alert(error);
    }
})
}

function Exchange()
{
    var score = parseInt(scoreMgr.getValue());
    var money = parseInt(moneyMgr.getValue());
    var aScore = parseInt(availableScoreMgr.getValue());
    var aMoney = parseInt(availableMoneyMgr.getValue());
    var scoreToCache = 0;
    var moneyToCache = 0;
    if (appMgr.selectedText == null) {
        $.ligerDialog.warn('请先选择应用程序！')
        return;
    }

    if (scoreToCacheMgr.getValue() == '' || moneyToCacheMgr.getValue() == '' || validTimeMgr.getValue() == '')
    {
        $.ligerDialog.warn('没有可用汇率，暂时不能兑换，请稍后再试！')
        return;
    }
    if (score > aScore || money > aMoney) {
        $.ligerDialog.warn('可用积分或者金币数量不足！')
        return;
    }

    if (score == '0' && money == '0') {
        $.ligerDialog.warn('请输入积分或者金币数量！')
        return;
    }

    else {
        scoreToCache = parseInt(scoreToCacheMgr.getValue()) * score / 100;
        moneyToCache = parseInt(scoreToCacheMgr.getValue()) * money / 100;
        $.ajax({
            type: 'POST',
            url: 'Home/NewExchangeHistory',
            data: { score: score, money: money, cache: scoreToCache + moneyToCache,appId:appMgr.getValue() },
            success: function (data) {
                if (data.Success) {
                    exchangeMgr.loadData();
                    settlementMgr.loadData();
                    scoreMgr.setValue(0);
                    moneyMgr.setValue(0);
                    SetScoreInfo(appMgr.getValue());
                    $.ligerDialog.success("申请成功，等待处理。");
                }
                else {
                    alert('失败！' + data.Message);
                }
            },
            error: function (error) {
                alert(error);
            }
        })
    }
}

function CancelExchange() {
    var rows = exchangeMgr.getCheckedRows();
    if (rows.length == 0) {
        $.ligerDialog.warn('请选择要取消的兑换申请记录！')
        return;
        if (confirm('确定取消?')) {
            $.ajax({
                type: 'POST',
                url: 'Home/CancelExchange',
                dataType: 'json',
                async: false,
                data: JSON.stringify(rows),
                contentType: 'application/json;charset=utf-8',
                success: function (data) {
                    if (data.Success) {
                        exchangeMgr.deleteRange(rows);
                        SetScoreInfo(appMgr.getValue());
                        settlementMgr.loadData();
                        $.ligerDialog.success("成功取消兑换。");
                    }
                    else {
                        alert(data.Message);
                    }
                },
                error: function (error) {
                    alert(error);
                }
            })
        }
    }
}

function settlement() {
    var rows = settlementMgr.getCheckedRows();
    if (rows.length == 0) {
        $.ligerDialog.warn('请选择要结算的记录！')
        return;
    }
    if (confirm('确定要结算选择的兑换吗？'))
    {
        $.ajax({
            type: 'POST',
            url: 'Home/SettleExchangeCache',
            dataType: 'json',
            async: false,
            data: JSON.stringify(rows),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    settlementMgr.loadData();
                    exchangeMgr.loadData();
                    $.ligerDialog.success("结算成功。");
                }
                else {
                    alert('失败！' + data.Message);
                }
            },
            error: function (error) {
                alert(error);
            }
        })
    }
}

function ConfirmExchangeCache() {
    var rows = exchangeMgr.getCheckedRows();
    if (rows.length == 0) {
        $.ligerDialog.warn('请选择要确认收款的记录！')
        return;
    }
    if (confirm('确定已经收款了吗？')) {
        $.ajax({
            type: 'POST',
            url: 'Home/ConfirmExchangeCache',
            dataType: 'json',
            async: false,
            data: JSON.stringify(rows),
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                if (data.Success) {
                    exchangeMgr.loadData();
                    settlementMgr.loadData();
                    $.ligerDialog.success("结算成功。");
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (error) {
                alert(error);
            }
        })
    }
}