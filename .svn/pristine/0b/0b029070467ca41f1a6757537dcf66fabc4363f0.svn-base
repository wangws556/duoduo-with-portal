
var allTopLevelRoomGroups;
var searchResult = new Array();
var currentTabIndex = 0;
var roomGroupTab;
var roomGroupTabHeader;
var roomGroupTrees = new Array();
var rankSplitters = new Array();
var roomGroupSplitters = new Array();
var roomsPanels = new Array();
var rankPanels = new Array();
var searchTexts = new Array();
var searchButtons = new Array();
var locations = new Array();
var roomGroupTreeDockPanels = new Array();
var roomGroupDockPanels = new Array();
var rankPanels = new Array();
var rankDockPanels = new Array();
var rankTabs = new Array();
var originalRankPanel;
var loginWindow;
var loginingWindow;
var userName;
var password;
var remember;
var autologin;
var loginButton;

function login() {
    //loginWindow.jqxWindow('close');
    //loginingWindow.jqxWindow('open');
    window.external.Login(userName.val(), password.val(), remember.prop('checked'),autologin.prop('checked'));
}

function cancelLogin() {
    loginingWindow.jqxWindow('close');
    loginWindow.jqxWindow('open');
}

function register() {
    window.external.Register();
}

function cancel() {
    window.external.Close();
}

function navigate(topRoomGroupId, rg, r) {
    var roomGroupTree = GetElement(topRoomGroupId, roomGroupTrees);
    var li = roomGroupTree.find("li:has(div:contains('" + rg.name + "'))").last().get(0);
    roomGroupTree.jqxTree('expandItem', li);
    roomGroupTree.jqxTree('selectItem', li);

    var roomsPanel = GetElement(topRoomGroupId, roomsPanels);
    var roomDiv = roomsPanel.find("div[roomId='" + r.id + "']").first();
    var bg = roomDiv.css('background-color');
    roomDiv.animate({ backgroundColor: 'lightgreen' }, 1000, function () {
        roomDiv.animate({ backgroundColor: bg }, 1000);
    });
}

function searchRoomGroup(topRoomGroupId, rg, val) {
    if (val) {
        if (rg.nodes && rg.nodes.length > 0) {
            for (var i = 0; i < rg.nodes.length; i++) {
                var r = rg.nodes[i];
                if (isNaN(val)) {
                    if (r.name.indexof(val) >= 0) {
                        navigate(topRoomGroupId, rg, r);
                        return true;
                    }
                }
                else {
                    var valString = val.toString();
                    var idString = r.id.toString();
                    if (idString.indexOf(valString) >= 0
                        || r.name.indexOf(valString) >= 0) {
                        navigate(topRoomGroupId, rg, r);
                        return true;
                    }
                }
            }
        }
        if (rg.items && rg.items.length > 0) {
            for (var i = 0; i < rg.items.length; i++) {
                if (searchRoomGroup(topRoomGroupId, rg.items[i], val)) {
                    return true;
                }
            }
        }
    }
    return false;
}

function searchRooms(val) {
    if (val) {
        var rg = allTopLevelRoomGroups[currentTabIndex];
        var topRoomGroupId = rg.id;
        if (searchRoomGroup(topRoomGroupId, rg, val)) {

        }
        else {
            alert('没有您要搜索的房间！');
        }
    }
}

function selectRoomGroup(rg) {
    if (rg) {
        var id = allTopLevelRoomGroups[currentTabIndex].id;
        var location = GetElement(id, locations);
        location.text(rg.url);
        var roomsPanel = GetElement(id, roomsPanels);
        roomsPanel.html('');
        if (rg.nodes) {
            var rooms = rg.nodes;
            for (var i = 0; i < rooms.length; i++) {
                var div = $("<div class='roomBlock'></div>");
                div.attr("roomId", rooms[i].id);
                div.append("<img class='roomImage' src='" + rooms[i].icon + "'/>");
                div.append("<div class='roomText'><span>房间名：" + rooms[i].name + "</span>" +
                    "<span>房间Id：" + rooms[i].id + "</span>" +
                    "<span>人数：" + rooms[i].count + "/" + rooms[i].maxcount + "</span>");
                div.hover(function () {
                    $(this).addClass("l-over");
                }, function () {
                    $(this).removeClass("l-over");
                }).click(function () {
                    window.external.EnterRoom($(this).attr("roomId"));
                });
                div.appendTo(roomsPanel);
            }
        }
    }
}

function forgetPassword() {

}

function LoginSucceed() {
    //loginingWindow.jqxWindow('close');
    loginWindow.jqxWindow('close');
    //roomGroupTab.attr('disabled', false);
}

function SwitchUser()
{
    loginWindow.jqxWindow('open');
}

function SetLoginUser(id, pwd) {
    userName.val(id);
    password.val(pwd);
}

function GetElement(id, dic) {
    for (var i = 0; i < dic.length; i++) {
        if (dic[i].id == id) {
            return dic[i].element;
        }
    }
    return null;
}

function autologinclick() {
    if (autologin.prop('checked'))
        remember.prop('checked', true);
    else
        remember.prop('checked', false);
}

function rememberclick() {
    if (autologin.prop('checked'))
        remember.prop('checked', true);
}

function InitHallWithLogin(roomGroups, userId, pwd, remberPwd, autoLogin)
{
    userName = $('#userid');
    password = $('#password');
    remember = $('#remember');
    autologin = $('#autologin');
    loginButton = $('#loginBtn');


    //loginingWindow = $('#loginingWindow').jqxWindow({
    //    theme: 'energyblue', width: 450, height: 250, zIndex: 99999, resizable: false, isModal: true, draggable: false,
    //    showCollapseButton: false, showCloseButton: false, autoOpen: false, position: { x: 350, y: 150 },
    //    showAnimationDuration: 10, closeAnimationDuration: 10
    //});
       
    allTopLevelRoomGroups = eval(roomGroups);
    originalRankPanel = $('#rankPanel');
    roomGroupTab = $('#roomGroupTab');
    roomGroupTabHeader = $('#roomGroupTabHeader');
    for (var i = 0; i < allTopLevelRoomGroups.length; i++) {
        roomGroupTabHeader.append("<li><p class='channelTabHeader'>" + allTopLevelRoomGroups[i].name + "</p></li>");
        var id = allTopLevelRoomGroups[i].id;
        var div =
            "<div id='rankSplitter" + id + "' >" +
                "<div>" +
                    "<div id='roomGroupSplitter" + id + "' >" +
                        "<div>" +
                            "<div id='roomGroupTreeDockPanel" + id + "'>" +
                                "<div dock='top' style='height:30px;'>" +
                                    "<input type='text' id='searchText" + id + "' class='searchBox' />" +
                                    "<input id='search" + id + "' class='rb1' type='button' value='搜索' onmouseover=\"this.className='rb2'\" onmouseout=\"this.className='rb1'\"></input>" +
                                "</div>" +
                                "<div><div id='roomGroupTree" + id + "' class='roomGroupTree' style='position:absolute;top:0;left:0'/></div>" +
                            "</div>" +
                        "</div>" +
                        "<div>" +
                            "<div id='roomGroupDockPanel" + id + "'>" +
                                "<div dock='top' style='height:30px;overflow-x:hidden;overflow-y:hidden;'>" +
                                    "<span id='location" + id + "' class='location'></span>" +
                                "</div>" +
                                "<div><div id='roomsPanel" + id + "' style='position:absolute;height:100%;width:100%;top:0;left:0;overflow-x:hidden;overflow-y:auto;'/></div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
                "<div><div id='rankPanel" + id + "' style='position:absolute;height:100%;width:100%;top:0;left:0;'/></div>" +
            "</div>";
        roomGroupTab.append(div);
        rankSplitters.push({ id: id, element: $("#rankSplitter" + id) });
        roomGroupTreeDockPanels.push({ id: id, element: $("#roomGroupTreeDockPanel" + id) });
        roomGroupSplitters.push({ id: id, element: $("#roomGroupSplitter" + id) });
        searchTexts.push({ id: id, element: $("#searchText" + id) });
        searchButtons.push({ id: id, element: $("#search" + id) });
        roomGroupTrees.push({ id: id, element: $("#roomGroupTree" + id) });
        roomGroupDockPanels.push({ id: id, element: $("#roomGroupDockPanel" + id) });
        locations.push({ id: id, element: $("#location" + id) });
        roomsPanels.push({ id: id, element: $("#roomsPanel" + id) });
        rankPanels.push({ id: id, element: $("#rankPanel" + id) });

        loginWindow = $('#loginWindow').jqxWindow({
            theme: 'energyblue', width: 450, height: 340, zIndex: 99999, resizable: false, isModal: true, draggable: false,
            showCollapseButton: false, showCloseButton: false, autoOpen: false, position: { x: 300, y: 200 },
            showAnimationDuration: 10, closeAnimationDuration: 10
        });
       
    }

    roomGroupTab.jqxTabs({
        theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: true, scrollable: true, scrollPosition: 'left',
        initTabContent: function (index) {
            var id = allTopLevelRoomGroups[index].id;

            var rankSplitter = GetElement(id, rankSplitters);
            rankSplitter.jqxSplitter({
                theme: 'energyblue', width: '100%', height: '100%', orientation: 'vertical', splitBarSize: 1, resizable: false,
                showSplitBar: true, panels: [{ size: '80%' }, { size: '20%', min: 230 }]
            });
            var roomGroupSplitter = GetElement(id, roomGroupSplitters);
            roomGroupSplitter.jqxSplitter({
                theme: 'energyblue', width: '100%', height: '100%', orientation: 'vertical', splitBarSize: 1, resizable: false,
                showSplitBar: true, panels: [{ size: 210, min: 210 }]
            });

            var roomGroupTreeDockPanel = GetElement(id, roomGroupDockPanels);
            roomGroupTreeDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });

            var searchText = GetElement(id, searchTexts);
            searchText.jqxInput({ theme: 'energyblue', placeHolder: "搜索房间Id或房间名" });
            var searchButton = GetElement(id, searchButtons);
            searchButton.jqxButton({ theme: 'energyblue' });
            searchButton.on('click', function () {
                var val = searchText.jqxInput('val');
                if (val) {
                    searchRooms(val)
                }
                else {
                    alert('请输入房间Id或房间名');
                }
            });

            var roomGroupTree = GetElement(id, roomGroupTrees);
            roomGroupTree.jqxTree({
                theme: 'energyblue', width: '100%', height: '100%',
                source: allTopLevelRoomGroups[index].items, toggleMode: 'click', toggleIndicatorSize: 16
            });

            var roomGroupDockPanel = GetElement(id, roomGroupDockPanels);
            roomGroupDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });

            var rankPanel = GetElement(id, rankPanels);
            rankPanel.html(originalRankPanel.html());
            var rankDockPanel = rankPanel.find('div.rankDockPanel');
            var rankTab = rankPanel.find('div.rankTab');
            rankDockPanels.push({ id: id, element: rankDockPanel });
            rankTabs.push({ id: id, element: rankTab });
            rankDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });
            rankTab.jqxTabs({ theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: true });

            var roomGroupTree = GetElement(id, roomGroupTrees);
            roomGroupTree.on('select', function (e) {
                var args = e.args;
                var item = roomGroupTree.jqxTree('getItem', args.element);
                if (item) {
                    var rg = GetRoomGroup(item.value);
                    selectRoomGroup(rg);
                }
            });

            if (index == 0) {
                //window.external.WebPageContentLoaded();
            }
        }
    });
    roomGroupTab.on('selected', function (event) {
        currentTabIndex = event.args.item;
    });
    //roomGroupTab.attr('disabled', true);

    userName.val(userId);
    password.val(pwd);
    remember.prop("checked", remberPwd);
    autologin.prop("checked", autoLogin);
    loginWindow.jqxWindow('open');

    if (autoLogin == true) {
        login(userId, pwd, remberPwd, autoLogin);
    }
}

function InitHall(roomGroups) {
    userName = $('#userid');
    password = $('#password');
    remember = $('#remember');
    autologin = $('#autologin');
    loginButton = $('#loginBtn');

    //loginingWindow = $('#loginingWindow').jqxWindow({
    //    theme: 'energyblue', width: 450, height: 250, zIndex: 99999, resizable: false, isModal: true, draggable: false,
    //    showCollapseButton: false, showCloseButton: false, autoOpen: false, position: { x: 300, y: 200 },
    //    showAnimationDuration: 10, closeAnimationDuration: 10
    //});

    loginWindow = $('#loginWindow').jqxWindow({
        theme: 'energyblue', width: 450, height: 340, zIndex: 99999, resizable: false, isModal: true, draggable: false,
        showCollapseButton: false, showCloseButton: false, autoOpen: false, position: { x: 300, y: 200 },
        showAnimationDuration: 10, closeAnimationDuration: 10
    });
    loginWindow.jqxWindow('open');
    allTopLevelRoomGroups = eval(roomGroups);
    originalRankPanel = $('#rankPanel');
    roomGroupTab = $('#roomGroupTab');
    roomGroupTabHeader = $('#roomGroupTabHeader');
    for (var i = 0; i < allTopLevelRoomGroups.length; i++) {
        roomGroupTabHeader.append("<li><p class='channelTabHeader'>" + allTopLevelRoomGroups[i].name + "</p></li>");
        var id = allTopLevelRoomGroups[i].id;
        var div =
            "<div id='rankSplitter" + id + "' >" +
                "<div>" +
                    "<div id='roomGroupSplitter" + id + "' >" +
                        "<div>" +
                            "<div id='roomGroupTreeDockPanel" + id + "'>" +
                                "<div dock='top' style='height:30px;'>" +
                                    "<input type='text' id='searchText" + id + "' class='searchBox' />" +
                                    "<input id='search" + id + "' class='rb1' type='button' value='搜索' onmouseover=\"this.className='rb2'\" onmouseout=\"this.className='rb1'\"></input>" +
                                "</div>" +
                                "<div><div id='roomGroupTree" + id + "' class='roomGroupTree' style='position:absolute;top:0;left:0'/></div>" +
                            "</div>" +
                        "</div>" +
                        "<div>" +
                            "<div id='roomGroupDockPanel" + id + "'>" +
                                "<div dock='top' style='height:30px;overflow-x:hidden;overflow-y:hidden;'>" +
                                    "<span id='location" + id + "' class='location'></span>" +
                                "</div>" +
                                "<div><div id='roomsPanel" + id + "' style='position:absolute;height:100%;width:100%;top:0;left:0;overflow-x:hidden;overflow-y:auto;'/></div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
                "<div><div id='rankPanel" + id + "' style='position:absolute;height:100%;width:100%;top:0;left:0;'/></div>" +
            "</div>";
        roomGroupTab.append(div);
        rankSplitters.push({ id: id, element: $("#rankSplitter" + id) });
        roomGroupTreeDockPanels.push({ id: id, element: $("#roomGroupTreeDockPanel" + id) });
        roomGroupSplitters.push({ id: id, element: $("#roomGroupSplitter" + id) });
        searchTexts.push({ id: id, element: $("#searchText" + id) });
        searchButtons.push({ id: id, element: $("#search" + id) });
        roomGroupTrees.push({ id: id, element: $("#roomGroupTree" + id) });
        roomGroupDockPanels.push({ id: id, element: $("#roomGroupDockPanel" + id) });
        locations.push({ id: id, element: $("#location" + id) });
        roomsPanels.push({ id: id, element: $("#roomsPanel" + id) });
        rankPanels.push({ id: id, element: $("#rankPanel" + id) });
    }

    roomGroupTab.jqxTabs({
        theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: true, scrollable: true, scrollPosition: 'left',
        initTabContent: function (index) {
            var id = allTopLevelRoomGroups[index].id;

            var rankSplitter = GetElement(id, rankSplitters);
            rankSplitter.jqxSplitter({
                theme: 'energyblue', width: '100%', height: '100%', orientation: 'vertical', splitBarSize: 1, resizable: false,
                showSplitBar: true, panels: [{ size: '80%' }, { size: '20%', min: 230 }]
            });
            var roomGroupSplitter = GetElement(id, roomGroupSplitters);
            roomGroupSplitter.jqxSplitter({
                theme: 'energyblue', width: '100%', height: '100%', orientation: 'vertical', splitBarSize: 1, resizable: false,
                showSplitBar: true, panels: [{ size: 210, min: 210 }]
            });

            var roomGroupTreeDockPanel = GetElement(id, roomGroupDockPanels);
            roomGroupTreeDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });

            var searchText = GetElement(id, searchTexts);
            searchText.jqxInput({ theme: 'energyblue', placeHolder: "搜索房间Id或房间名" });
            var searchButton = GetElement(id, searchButtons);
            searchButton.jqxButton({ theme: 'energyblue' });
            searchButton.on('click', function () {
                var val = searchText.jqxInput('val');
                if (val) {
                    searchRooms(val)
                }
                else {
                    alert('请输入房间Id或房间名');
                }
            });

            var roomGroupTree = GetElement(id, roomGroupTrees);
            roomGroupTree.jqxTree({
                theme: 'energyblue', width: '100%', height: '100%',
                source: allTopLevelRoomGroups[index].items, toggleMode: 'click', toggleIndicatorSize: 16
            });

            var roomGroupDockPanel = GetElement(id, roomGroupDockPanels);
            roomGroupDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });

            var rankPanel = GetElement(id, rankPanels);
            rankPanel.html(originalRankPanel.html());
            var rankDockPanel = rankPanel.find('div.rankDockPanel');
            var rankTab = rankPanel.find('div.rankTab');
            rankDockPanels.push({ id: id, element: rankDockPanel });
            rankTabs.push({ id: id, element: rankTab });
            rankDockPanel.jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });
            rankTab.jqxTabs({ theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: true });

            var roomGroupTree = GetElement(id, roomGroupTrees);
            roomGroupTree.on('select', function (e) {
                var args = e.args;
                var item = roomGroupTree.jqxTree('getItem', args.element);
                if (item) {
                    var rg = GetRoomGroup(item.value);
                    selectRoomGroup(rg);
                }
            });

            if (index == 0) {
                //window.external.WebPageContentLoaded();
            }
        }
    });
    roomGroupTab.on('selected', function (event) {
        currentTabIndex = event.args.item;
    });
    //roomGroupTab.attr('disabled', true);
}

function ReleaseMemory() {
    allTopLevelRoomGroups = null;
    searchResult = null;
    currentTabIndex = 0;
    roomGroupTab = null;
    roomGroupTabHeader = null;
    roomGroupTrees = null;
    rankSplitters = null;
    roomGroupSplitters = null;
    roomsPanels = null;
    rankPanels = null;
    searchTexts = null;
    searchButtons = null;
    locations = null;
    roomGroupTreeDockPanels = null;
    roomGroupDockPanels = null;
    rankPanels = null;
    rankDockPanels = null;
    rankTabs = null;
    originalRankPanel = null;
    loginWindow = null;
    loginingWindow = null;
    userName = null;
    password = null;
    remember = null;
    autologin = null;
    loginButton = null;
}

function GetRoom(roomGroup, rId) {
    if (roomGroup.nodes) {
        for (var i = 0; i < roomGroup.nodes.length; i++) {
            if (roomGroup.nodes[i].id == rId) {
                return roomGroup.nodes[i];
            }
        }
    }
    if (roomGroup.items) {
        for (var j = 0; j < roomGroup.items.length; j++) {
            var room = GetRoom(roomGroup.items[j], rId);
            if (room) {
                return room;
            }
        }
    }
    return null;
}

function GetRoomGroup(id) {
    return GetRoomGroupInArray(allTopLevelRoomGroups, id);
}

function GetRoomGroupInArray(groups, id) {
    for (var i = 0; i < groups.length; i++) {
        if (groups[i].id == id) {
            return groups[i];
        }
        else if (groups[i].items) {
            var g = GetRoomGroupInArray(groups[i].items, id);
            if (g) {
                return g;
            }
        }
    }
    return null;
}

function Resize(w, h) {
}

function UpdateOnlineUserCountAsync(roomGroupsCount, roomsCount) {
    try {
        Concurrent.Thread.create(UpdateRoomGroupOnlineUserCount, roomGroupsCount);
    }
    catch (err) { }
    try {
        Concurrent.Thread.create(UpdateRoomOnlineUserCount, roomsCount);
    }
    catch (err) { }
}

function UpdateRoomGroupOnlineUserCount(roomGroupsCount) {
    if (roomGroupsCount) {
        var rgUserCounts = eval(roomGroupsCount);
        if (rgUserCounts && $.isArray(rgUserCounts) && rgUserCounts.length > 0) {
            for (var i = 0; i < rgUserCounts.length; i++) {
                try {
                    var roomGroup = GetRoomGroup(rgUserCounts[i].id);
                    var roomGroupTree = GetElement(rgUserCounts[i].rootid, roomGroupTrees);
                    var li = roomGroupTree.find("li:has(div:contains('" + roomGroup.label + "'))").last().get(0);
                    roomGroup.label = rgUserCounts[i].label;
                    roomGroup.count = rgUserCounts[i].count;
                    if (li) {
                        roomGroupTree.jqxTree('updateItem', li, { label: roomGroup.label, icon: roomGroup.icon });
                    }
                }
                catch (err) { }
            }
        }
    }
}

function UpdateRoomOnlineUserCount(roomsCount) {
    if (roomsCount) {
        var rUserCounts = eval(roomsCount);
        if (rUserCounts && $.isArray(rUserCounts) && rUserCounts.length > 0) {
            for (var i = 0; i < rUserCounts.length; i++) {
                try {
                    for (var j = 0; j < allTopLevelRoomGroups.length; j++) {
                        if (allTopLevelRoomGroups[j].id == rUserCounts[i].rootid) {
                            var room = GetRoom(allTopLevelRoomGroups[j], rUserCounts[i].id);
                            if (room) {
                                room.count = rUserCounts[i].count;
                                var roomsPanel = GetElement(rUserCounts[i].rootid, roomsPanels);
                                var roomDiv = roomsPanel.find("div[roomId='" + rUserCounts[i].id + "']").first();
                                var span = roomDiv.find('span:nth-child(3)').text("人数：" + room.count + "/" + room.maxcount);
                            }
                        }
                    }
                }
                catch (err) { }
            }
        }
    }
}
