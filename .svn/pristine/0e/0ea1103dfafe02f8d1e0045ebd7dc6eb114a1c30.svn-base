var yesOrNo = [{ val: 1, text: '是' }, { val: 0, text: '否' }];

var isSaving = false;

function checkSaving()
{
    if (isSaving)
    {
        alert('保存中...');
    }
    return isSaving;
}

function Init() {
    $().ajaxStart(function () {
        $('#framecenter').css('cursor', 'wait');
    });

    $().ajaxStop(function () {
        $('#framecenter').css('cursor', 'auto');
    });

    $("#mainLayout").ligerLayout({bottomHeight:30});
    $("#bodyLayout").ligerLayout({ space: 3, leftWidth: 150, allowLeftCollapse: true, onHeightChanged: heightChange });

    //var height = $("#bodyLayout").height();
    var height = $(".l-layout-center").height();

    accordion = $("#managementAccordion").ligerAccordion({ height: height - 24, speed: null });

    //tab = $("#framecenter").ligerTab({ height: height });

    //Initialize function list
    $.ajax({
        url: 'Home/GetFunctionTree',
        type: 'GET',
        cache: false,
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data.Success) {
                InitTree(data.Rows);
            }
            else {
                alert(data.Message);
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}

function InitTree(data) {
    $("#functionTree").ligerTree({
        textFieldName: 'name',
        idFieldName: 'id',
        iconFieldName: 'icon',
        childrenFieldName:'items',
        data: data[0].items,
        checkbox: false,
        slide: false,
        btnClickToToggleOnly: false,
        nodeWidth: 120,
        attribute: ['nodename', 'url'],
        onSelect: selectNode
    });

    $("#applicationTree").ligerTree({
        textFieldName: 'name',
        idFieldName: 'id',
        iconFieldName: 'icon',
        childrenFieldName: 'items',
        data: data[1].items,
        checkbox: false,
        slide: false,
        btnClickToToggleOnly:false,
        nodeWidth: 120,
        attribute: ['nodename', 'url'],
        onSelect: selectNode
    });

    $("#personalTree").ligerTree({
        textFieldName: 'name',
        idFieldName: 'id',
        iconFieldName: 'icon',
        childrenFieldName: 'items',
        data: data[2].items,
        checkbox: false,
        slide: false,
        btnClickToToggleOnly: false,
        nodeWidth: 120,
        attribute: ['nodename', 'url'],
        onSelect: selectNode
    });
}

function heightChange(options) {
    $("#framecenter").height += options.diff;
    if (accordion && options.middleHeight - 24 > 0) {
        accordion.setHeight(options.middleHeight - 24);
    }
}

function selectNode(node) {
    if (node && node.data && node.data.url) {
        $.ajax({
            url: "Home/IsAuthenticated",
            type: "GET",
            cache: false,
            success: function (data) {
                if (data.Success) {
                    $("#framecenter").addClass("loading");
                    $.ajax(
                        {
                            type: "GET",
                            url: "Home/" + node.data.url,
                            cache: false,
                            success: function (data) {
                                $("#framecenter").html(data);
                            },
                            error: function (error) { alert(error); }
                        });
                    $("#framecenter").removeClass("loading");
                }
                else {
                    window.location = 'Account/Login';
                }
            },
            error: function (error) {
                alert(error);
                window.location = 'Account/Login';
            }
        });
    }
}

function uploadImage(urlstring, index) {
    $.ligerDialog.open(
        {
            width: 400,
            type: 'ok',
            title: '上传图片',
            showMin: false,
            showMax: true,
            isResize: true,
            allowClose: false,
            buttons: {
                text: '关闭', onclick: function (btn, dialog, index) {
                    dialog.close();
                }
            },
            url: urlstring
        });
}

function search(manager) {
    manager.showFilter();
}

function beginEdit(manager, rowid) {
    if (manager) {
        manager.beginEdit(rowid);
    }
}

function cancelEdit(manager,rowid) {
    manager.cancelEdit(rowid);
}
function endEdit(manager,rowid) {
    manager.endEdit(rowid);
}

function deleteRow(manager,rowid) {
    if (confirm('确定删除?')) {
        manager.deleteRow(rowid);
    }
}

function deleteSelected(manager) {
    if (manager) {
        if (confirm('确定删除?')) {
            manager.deleteRange(manager.getCheckedRows());
        }
    }
}

function addNewRow(manager,action,appendToLast) {
    if (manager) {
        $.ajax({
            url: "Home/"+action,
            type: 'POST',
            cache: false,
            datatype: 'json',
            success: function (row) {
                if (appendToLast) {
                    manager.addRow(row.Model);
                    manager.loadData();
                }
                else {
                    var row = manager.getSelectedRow();
                    if (row) {
                        manager.addRow(row.Model, row, false);
                        manager.loadData();
                    }
                }
            },
            error: function (error) { alert(error); }
        });
    }
}

function saveAll(manager,action) {
    if (!checkSaving()) {
        isSaving = true;
        if (manager) {
            manager.endEdit();
            var data = manager.currentData;
            $.ajax({
                url: 'Home/'+action,
                type: 'POST',
                dataType: 'json',
                async: false,
                data: JSON.stringify(data.Rows),
                contentType: 'application/json;charset=utf-8',
                success: function (msg) {
                    if (msg.Success) {
                        manager.loadData();
                        $.ligerDialog.success("保存成功！");
                    }
                    else {
                        alert(msg.Message);
                    }
                    isSaving = false;
                },
                error: function (error) {
                    $.ligerDialog.error("保存失败！");
                    isSaving = false;
                }
            });
        }
    }
}

function isNum(value) {
    
    if(value)
    {
        if(value.search("^-?\\d+$") != 0)
            return false;
        return true;
    }
    
    return false;
}

$("#pageloading").hide();


