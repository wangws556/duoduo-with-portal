var allMusics;
var musicManager;

function Init(gridTagId) {
    musicManager = $(gridTagId).ligerGrid({
        columns:[
            { display: "名称", name: 'Name', width: 300, type: 'string'},
            { display: "大小", name: 'Size', width: 200, type: 'string' },
            { display: "最后修改时间", name: 'LastModified', width: 200, type: 'date' }
        ],
        url: "Home/GetMusics",
        pageSizeOptions: [20, 40], pageSize: 40, width: '95%', height: '95%',
        dataAction: "server",
        enabledEdit: true, clickToEdit: false, IsScroll: false, rownumbers: false, allowAdjustColWidth: false, checkbox: false,
        toolbar: {
            items: [{ text: '自定义查询', click: function () { search(musicManager); }, icon: 'search2' },
                { text: '上传', click: function () { uploadMusic("/UploadMusics", 1); } },
                { text: '删除选中纪录', click: function () { deleteSelected(musicManager); } },
            ]
        }
    })
}

function uploadMusic(urlstring, index) {
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