var giftCountArray = new Array();
giftCountArray.push({ id: 0, text: '0' });
giftCountArray.push({ id: 1, text: '1' });
giftCountArray.push({ id: 11, text: '11' });
giftCountArray.push({ id: 88, text: '88' });
giftCountArray.push({ id: 99, text: '99' });
giftCountArray.push({ id: 168, text: '168' });
giftCountArray.push({ id: 520, text: '520' });
giftCountArray.push({ id: 666, text: '666' });
giftCountArray.push({ id: 888, text: '888' });
giftCountArray.push({ id: 999, text: '999' });
giftCountArray.push({ id: 1314, text: '1314' });
giftCountArray.push({ id: 9258, text: '9258' });
giftCountArray.push({ id: 9999, text: '9999' });

var artisticDiv;
var allGiftGroups;
var allUsers = new Array();
var previewDiv;
var previewGift;
var rightPanelWidth = 0;
var toolbarWindow;
var splitter;
var privateMessageDiv;
var privateMessagePanel;
var publicMessageDiv;
var publicMessagePanel;
var hornMsgWindow;
var scoreExchangeWindow;
var giftTabDiv;
var giftCountList;
var inputMsgTxr;
var hornInputMsgTxr;
var bold;
var hornBold;
var hornMsgPanel;
var colorPanel;
var publicMsgBtns;
var privateMsgBtns;
var publicScroll;
var privateScroll;
var hornScroll;
var timerForGiftPreview;
var me;
var moneyLabel;
var scoreLabel;
var scoreToMoneyLabel;
var userList;
var toolbarUserList;
var giftReceiverList;
var fontFamilyList;
var hornFontFamilyList;
var fontSizeList;
var hornFontSizeList;
var isBold = 1;
var isHornBold = 1;
var isItalic = 1;
var isHornItalic = 1;
var isUnderline = 1;
var isHornUnderline = 1;
var pgIndexMotion = 0;
var pgIndexStamp = 0;
var tabIndexMotion = 0;
var tabIndexStamp = 0;
var obj;
var countPerPage = 100;
var motionTabDiv;
var timer;
var stampTabDiv;
var hornMsgRadio;
var offsetPublicScrollHeight = 0;
var offsetPrivateScrollHeight = 0;
var hornToolbar;
var privateMsgBtns;
var isPublicScroll = true;
var isPrivateScroll = true;
var isHornScroll = true;
var publicMessageCount = 50;
var privateMessageCount = 50;
var currentPublicMessageCount = 0;
var currentPrivateMessageCount = 0;
var messagePerSecond = 8;
var messageInterval = 125;
var contextMenu = null;
var maxImagePerMessage = 5;
var currentImagesInMessage = 0;
var userListTab;

function ReleaseMemory()
{
    artisticDiv = null;
    allGiftGroups = null;
    allUsers =null;
    previewDiv = null;
    previewGift = null;
    rightPanelWidth = null;
    toolbarWindow = null;
    splitter = null;
    privateMessageDiv = null;
    privateMessagePanel = null;
    publicMessageDiv = null;
    publicMessagePanel = null;
    hornMsgWindow = null;
    scoreExchangeWindow = null;
    giftTabDiv = null;
    giftCountList = null;
    inputMsgTxr = null;
    hornInputMsgTxr = null;
    bold = null;
    hornBold = null;
    hornMsgPanel = null;

    colorPanel = null;
    publicMsgBtns = null;
    privateMsgBtns = null;
    publicScroll = null;
    privateScroll = null;
    hornScroll = null;
    timerForGiftPreview = null;
    me = null;
    moneyLabel = null;
    scoreLabel = null;
    scoreToMoneyLabel = null;
    userList = null;
    toolbarUserList = null;
    giftReceiverList = null;
    fontFamilyList = null;
    hornFontFamilyList = null;
    fontSizeList = null;
    hornFontSizeList;
    isBold = 0;
    isHornBold = 0;
    isItalic = 0;
    isHornItalic = 0;
    isUnderline = 0;
    isHornUnderline = 0;
    pgIndexMotion = null;
    pgIndexStamp = null;
    tabIndexMotion = null;
    tabIndexStamp = null;
    obj = null;
    countPerPage = null;
    motionTabDiv = null;
    timer = null;
    stampTabDiv = null;
    hornMsgRadio = null;
    offsetPublicScrollHeight = null;
    offsetPrivateScrollHeight = null;
    hornToolbar = null;
    privateMsgBtns = null;
    giftCountArray = null;
    contextMenu = null;
    maxImagePerMessage = null;
    currentImagesInMessage = null;
    userListTab = null;
}

function KeyDown(e) {
    var e = e || event;
    var curKey = e.keyCode || e.which || e.charCode;
    switch (curKey) {
        case 13: //enter
            if (e.altKey) //Alt+enter 
                inputMsgTxr.append("\r");
            //send message
            else
                SendMessage();
            break;
        default:
            break;
    }
}

function InputTextAreaKeyDown(e) {

    var e = e || event;
    var curKey = e.keyCode || e.which || e.charCode;
    switch (curKey) {
        case 13: //enter
            e.preventDefault();
            break;
        default:
            break;
    }
};

function InitRoom(giftGroups, self, w, h, publicMsgCount, privateMsgCount, msgPerSecond) {
    InitBase();
    if (publicMsgCount > 0) {
        publicMessageCount = publicMsgCount;
    }
    if (privateMsgCount) {
        privateMessageCount = privateMsgCount;
    }
    if (msgPerSecond > 0) {
        messagePerSecond = msgPerSecond;
        messageInterval = 1000 / messagePerSecond;
    }
    document.onkeydown = KeyDown;
    inputMsgTxr = $('#InputMsgTxr');
    bold = $("#Bold");
    hornInputMsgTxr = $("#HornTextarea");
    hornBold = $("#HornBold");
    hornMsgPanel = $("#hornMsgPanel");
    colorPanel = $("#ColorPicker");
    publicMsgBtns = $("#PublicMsgDiv-Btns");
    privateMsgBtns = $("#PrivateMsgDiv-Btns");
    publicMessagePanel = document.getElementById('publicMessagePanel');
    privateMessagePanel = document.getElementById('privateMessagePanel');
    publicScroll = $("#Public-Scroll");
    privateScroll = $("#Private-Scroll");
    hornScroll = $("#Horn-Scroll");
    inputMsgTxr.keydown = InputTextAreaKeyDown;
    hornToolbar = $("#toolbarHornMsg");
    artisticDiv = $("#ArtisticDiv");

    rightPanelWidth = $('#rightPanel').width();
    $('#mainDiv').jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });
    splitter = $('#splitter').jqxSplitter({ theme: 'energyblue', width: w - rightPanelWidth, height: h - $('#topPanel').height() - $('#OutChatPanel').height() - $('#toolbar').height(), orientation: 'horizontal', panels: [{ size: '65%' }, { size: '35%' }] });
    publicMessageDiv = $('#PublicMessage-Div').jqxPanel({ theme: 'energyblue', scrollBarSize: 15, width: $('#splitter').width(), height: $('#splitter').height() * 0.65 });
    privateMessageDiv = $('#PrivateMessage-Div').jqxPanel({ theme: 'energyblue', scrollBarSize: 15, width: $('#splitter').width(), height: $('#splitter').height() * 0.35 - 5 });
    $('#rightDockPanel').jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });
    userListTab = $('#userlistTab').jqxTabs({ theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: false });
    $('#chatPanel').jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%', lastchildfill: true });

    toolbarWindow = $('#toolbarWindow').jqxWindow({
        theme: 'energyblue', width: '100%', height: 40, maxWidth: 9999, zIndex: 99999, resizable: false, draggable: false, showCollapseButton: false, showCloseButton: false, autoOpen: false, position: { x: 0, y: h - 185 }
    });

    hornMsgWindow = $("#HornMsgWindow").jqxWindow({
        height: 340, width: 410, theme: 'energyblue', position: { x: 100, y: 100 }, showCloseButton: true, autoOpen: false, resizable: false, draggable: true
    });

    scoreExchangeWindow = $('#ScoreExchangeWindow').jqxWindow({
        height: 285, width: 320, theme: 'energyblue', position: { x: 100, y: 100 }, showCloseButton: true, autoOpen: false, resizable: false, draggable: true
    });

    InitGifts(giftGroups);
    giftTabDiv.jqxTabs({ theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false, autoHeight: false });
    $('#giftPanel1 td').css('background-image', 'url(' + giftCornerBGImage + ')');
    giftCountList = $('#giftCount').jqxComboBox({
        source: giftCountArray, selectedIndex: 0, width: '100px', height: '20px', dropDownHeight: 80,
        theme: 'energyblue', valueMember: 'id', displayMember: 'text', autoDropDownHeight: false
    });

    //artisticMenu.jqxMenu({
    //    width: 450,
    //    height: 30,
    //    theme: 'energyblue',
    //    clickToOpen: true,
    //    mode:'vertical'
    //});

    InitMe(self);
    $('.jqx-tabs > div:nth-child(2)').each(function (i) { $(this).css('overflow-y', 'hidden'); });
    
    //window.external.GetHtml(publicMessageDiv.html());
}

function Resize(w, h) {
    if (toolbarWindow.jqxWindow('isOpen')) {
        toolbarWindow.jqxWindow({ width: w - rightPanelWidth, position: { x: 0, y: h - 185 } });
    }
    splitter.jqxSplitter({ width: w - rightPanelWidth, height: h - $('#topPanel').height() - $('#OutChatPanel').height() - $('#toolbar').height() });
    publicMessageDiv.jqxPanel({ width: splitter.width(), height: splitter.height() * 0.65 });
    privateMessageDiv.jqxPanel({ width: splitter.width(), height: splitter.height() * 0.35 - 5 });
    userListTab.jqxTabs({ width: '100%', height: h - $('#topPanel').height() - 280 });
}

function InitGifts(giftGroups) {
    allGiftGroups = eval(giftGroups);
    giftTabDiv = $('#giftTab');
    var giftHeader = $('#giftTabHeader');
    for (i = 0; i < allGiftGroups.length; i++) {
        var div = $("<div id='giftgroup" + allGiftGroups[i].Id + "' class='hScrollbar'></div>");
        for (var j = 0; j < allGiftGroups[i].Gifts.length; j++) {
            img = $('<img></img>');
            img.attr("imgPath", allGiftGroups[i].Gifts[j].GifIcon);
            img.attr("imgGroupId", allGiftGroups[i].Id);
            img.attr("imgId",allGiftGroups[i].Gifts[j].Id);
            img.attr("imgPrice", allGiftGroups[i].Gifts[j].Price);
            img.attr("imgScore", allGiftGroups[i].Gifts[j].Score);
            img.attr("src", allGiftGroups[i].Gifts[j].icon);
            img.attr("title",allGiftGroups[i].Gifts[j].Name + ":" + allGiftGroups[i].Gifts[j].Price + "金币");
            img.click(function () {
                sendGift($(this).attr("imgGroupId"), $(this).attr("imgId"), $(this).attr("imgPrice"));
            });
            img.bind({
                mouseenter:function(){
                    clearTimeout(timerForGiftPreview);
                    ShowPreviewGift($(this).attr("imgPath"), $(this).attr("imgPrice"), $(this).attr("imgScore"));
                }, mouseleave:function(){
                    timerForGiftPreview = setTimeout(function () { HidePreviewGift() }, 500);
                }
            });
            img.appendTo(div);
        }
        giftHeader.append("<li>" + allGiftGroups[i].Name + "</li>");
        giftTabDiv.append(div);
    }
}

function InitMe(user) {
    me = eval("(" + user + ")");
    moneyLabel = $('#moneyLabel');
    scoreLabel = $('#scoreLabel');
    if (me.Money) {
        moneyLabel.text(me.Money);
    }
    else {
        moneyLabel.text('0');
    }
    if (me.Score) {
        scoreLabel.text(me.Score);
    }
    else {
        scoreLabel.text('0');
    }
}

function GetUserHtml(usr, index) {
    var cls = 'odd';
    if (index % 2 == 0) {
        cls = 'even';
    }
    var html = "<div userid='" + usr.Id + "' class='" + cls + "' onmousedown='rightClickOnUser(event)'>";
    if (usr.RoleImageUrl) {
        html += "<img userid='" + usr.Id + "' src='" + usr.RoleImageUrl + "'/>";
    }
    if (usr.CameraImageUrl) {
        html += "<img userid='" + usr.Id + "' src='" + usr.CameraImageUrl + "'/>";
    }
    if (usr.icon) {
        html += "<img userid='" + usr.Id + "' src='" + usr.icon + "'/>";
    }
    html += "<span class='userListSpan' userid='" + usr.Id + "' >" + usr.NickName + "</span>" + "<span class='userListSpan'>" + usr.Id + "</span></div>";
    return html;
}

function InitUsers(users) {
    allUsers = eval(users);
    userList = $('#userList').jqxListBox({
        theme: 'energyblue', source: allUsers, displayMember: "NickName", valueMember: "Id", height: '100%', width: '100%', itemHeight: 25,
        renderer: function (index, label, value) {
            var usr = allUsers[index];
            return GetUserHtml(usr, index);
        }
    });

    updateTabHeader(userListTab, 0, '用户\\' + allUsers.length);

    userList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var index = args.index;
            var item = args.item;
            var originalEvent = args.originalEvent;
            // get item's label and value.
            var label = item.label;
            var value = item.value;

            addMicAndChatUser(label, value);
        }
    });

    giftReceiverList = $('#giftReceiver').jqxComboBox({
        source: {}, width: '100px', height: '25px', dropDownHeight: 80,
        theme: 'energyblue', valueMember: 'Id', displayMember: 'NickName', autoDropDownHeight: false
    });
    giftReceiverList.on('select',function(event){
        var args = event.args;
        if (args) {
            var item = args.item;
            toolbarUserList.jqxComboBox('selectItem', item);
    }
    });


    toolbarUserList = $('#toolBarUsers').jqxComboBox({
        source: ["所有人"], width: '100px', height: '25px', dropDownHeight: 80, selectedIndex:0,
        theme: 'energyblue', valueMember: 'Id', displayMember: 'NickName', autoDropDownHeight: false
    });
    toolbarUserList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var item = args.item;
            if(item.label != "所有人")
                giftReceiverList.jqxComboBox('selectItem', item);
        }
    });
}

function rightClickOnUser(e) {
    var targ;
    if (!e) {
        var e = window.event;
    }
    if (e.target) {
        targ = e.target;
    }
    else if (e.srcElement) {
        targ = e.srcElement;
    }
    var userid = targ.getAttribute('userid');
    var rightClick = isRightClick(e);
    if (rightClick && userid != me.Id) {
        if (contextMenu)
        {
            contextMenu.jqxMenu('destroy');
        }
        //get menu from C#
        var menus = eval(window.external.GetMenuData(targ.getAttribute('userid')));
        if (menus) {
            contextMenu = $('<div></div>');
            contextMenu.jqxMenu({ width: '200px', height: '300px', autoOpenPopup: false, mode: 'popup', theme: 'energyblue', source: menus });
            contextMenu.on('itemclick', function (event) {
                // get the clicked LI element.
                var element = event.args;
                var cmdid = element.getAttribute('id');
                window.external.MenuClicked(userid, cmdid);
                contextMenu.jqxMenu('destroy');
            });
            for (var i = 0; i < menus.length; i++) {
                if (menus[i].disabled == true) {
                    contextMenu.jqxMenu('disable', menus[i].id, true);
                }
            }
            contextMenu.jqxMenu('open', parseInt(event.clientX) + 5, parseInt(event.clientY) + 5);
        }
    }
    return false;
}

function InitFonts(fontFamilies, fontSizes) {
    var jsonF = eval(fontFamilies);
    var jsonS = eval(fontSizes);
    fontFamilyList = $('#FontFamily').jqxComboBox({ source: jsonF, selectedIndex: 0, width: 120, dropDownHeight: 100, autoComplete: true, autoDropDownHeight: false, placeHolder: 'Font', enableBrowserBoundsDetection: true });
    hornFontFamilyList = $('#HornFontFamily').jqxComboBox({ source: jsonF, selectedIndex: 0, width: 120, dropDownHeight: 100, autoComplete: true, autoDropDownHeight: false, placeHolder: 'Font', enableBrowserBoundsDetection: true });
    fontFamilyList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var item = fontFamilyList.jqxComboBox('getItem', args.index);
            if (item != null) {
                inputMsgTxr.css("font-family", item.label);
            }
        }
    });
    hornFontFamilyList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var item = hornFontFamilyList.jqxComboBox('getItem', args.index);
            if (item != null) {
                hornInputMsgTxr.css("font-family", item.label);
            }
        }
    });

    fontSizeList = $('#FontSize').jqxComboBox({ source: jsonS, selectedIndex: 0, width: 120, dropDownHeight: 100, autoComplete: true, autoDropDownHeight: false, placeHolder: 'Font-Size', enableBrowserBoundsDetection: true });
    hornFontSizeList = $('#HornFontSize').jqxComboBox({ source: jsonS, selectedIndex: 0, width: 120, dropDownHeight: 100, autoComplete: true, autoDropDownHeight: false, placeHolder: 'Font-Size', enableBrowserBoundsDetection: true });
    fontSizeList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var item = fontSizeList.jqxComboBox('getItem', args.index);
            if (item != null) {
                inputMsgTxr.css("font-size", item.label);
            }
        }
    });
    hornFontSizeList.on('select', function (event) {
        var args = event.args;
        if (args) {
            var item = hornFontSizeList.jqxComboBox('getItem', args.index);
            if (item != null) {
                hornInputMsgTxr.css("font-size", item.label);
            }
        }
    });
}

function sendGift(giftGroupId, giftId, price) {
    if (me.CanSendGift) {
        var u = giftReceiverList.jqxComboBox('val');
        if (u) {
            var uid = parseInt(u);
            if (uid && uid > 0) {
                if (window.external.CanReceiveGift(uid)) {
                    var c = giftCountList.jqxComboBox('val');
                    if (c) {
                        var gc = parseInt(c);
                        if (gc && gc > 0) {
                            var money = parseInt(me.Money);
                            if (money > parseInt(price) * gc) {
                                window.external.SendGift(giftGroupId, giftId, uid, gc);
                                giftCountList.jqxComboBox({ selectedIndex: 0 });
                            }
                            else {
                                alert('余额不足，请充值');
                            }
                            return;
                        }
                    }
                }
                else {
                    alert('对方没有接收礼物权限！');
                }
            }
        }
        alert('请选择礼物赠送对象和数量！');
    }
    else {
        alert('没有送礼物权限！');
    }
}

function updateTabHeader(tab,index,text)
{
    if(tab != null)
    {
        tab.jqxTabs('setTitleAt', index, text);
    }
}

function UserEntered(usr) {
    var user = $.parseJSON(usr);
    allUsers.push(user);
    var item = { value: user.Id, html: GetUserHtml(user, allUsers.length - 1) };
    userList.jqxListBox('addItem', item);
    updateTabHeader(userListTab, 0, '用户\\' + allUsers.length);
}

function UserLeft(userId) {
    for (var i = 0; i < allUsers.length; i++) {
        if (allUsers[i].Id == userId) {
            allUsers.splice(i, 1);
            userList.jqxListBox('removeItem', userId);
            updateTabHeader(userListTab, 0, '用户\\' + allUsers.length);
            return;
        }
    }
    removeMicAndChatUser(userId);
}

function addMicAndChatUser(name, id) {
    if (name && id && me.Id != id) {
        var item = toolbarUserList.jqxComboBox('getItemByValue', id);
        if (item == null) {
            toolbarUserList.jqxComboBox('insertAt', { value: id, label: name }, 0);
            toolbarUserList.jqxComboBox('selectIndex', 0);
        }
        else {
            toolbarUserList.jqxComboBox('selectItem', item);
        }
        var item2 = giftReceiverList.jqxComboBox('getItemByValue', id);
        if (item2 == null) {
            giftReceiverList.jqxComboBox('insertAt', { value: id, label: name }, 0);
            giftReceiverList.jqxComboBox('selectIndex', 0);
        }
        else {
            giftReceiverList.jqxComboBox('selectItem', item2);
        }
        
    }
}

function removeMicAndChatUser(userId) {
    giftReceiverList.jqxComboBox('removeItem', userId);
    toolbarUserList.jqxComboBox('removeItem', userId);
}

function updateMicImage(userId, imgUrl)
{
    for (var i = 0; i < allUsers.length; i++) {
        if (allUsers[i].Id == userId) {
            allUsers[i].CameraImageUrl = imgUrl;
            break;
        }
    }
    userList.jqxListBox({ Source: allUsers });
}

function ToggleBoldMsg() {
    
    if (isBold == 1) {
        bold.attr("src", "Images/Bold_Select.jpg");
        isBold = 0;
        inputMsgTxr.css({ "font-weight": "bold" })
    }
    else{
        bold.attr("src", "Images/Bold.jpg");
        isBold = 1;
        inputMsgTxr.css({ "font-weight": "normal" })
    }
}

function ToggleBoldHorn() {
    if (isHornBold == 1) {
        hornBold.attr("src", "Images/Bold_Select.jpg");
        isHornBold = 0;
        hornInputMsgTxr.css({ "font-weight": "bold" })
    }
    else{
        hornBold.attr("src", "Images/Bold.jpg");
        isHornBold = 1;
        hornInputMsgTxr.css({ "font-weight": "normal" })
    }
}

function ToggleItalicMsg() {
    
    if (isItalic == 1) {
        $("#Italic").attr("src", "Images/Italic_Select.jpg");
        isItalic = 0;
        inputMsgTxr.css({ "font-style": "italic" });
    }
    else{
        $("#Italic").attr("src", "Images/Italic.jpg");
        isItalic = 1;
        inputMsgTxr.css({ "font-style": "normal" });
    }
}

function ToggleItalicHorn() {
    
    if (isHornItalic == 1) {
        $("#HornItalic").attr("src", "Images/Italic_Select.jpg");
        isHornItalic = 0;
        hornInputMsgTxr.css({ "font-style": "italic" });
    }
    else{
        $("#HornItalic").attr("src", "Images/Italic.jpg");
        isHornItalic = 1;
        hornInputMsgTxr.css({ "font-style": "normal" });
    }
}

function ToggleUnderlineMsg() {
    var msgArea = $(".InputMsgTxr");
    if (isUnderline == 1) {
        $("#Underline").attr("src", "Images/Underline_Select.jpg");
        isUnderline = 0;
        inputMsgTxr.css({ "text-decoration": "underline" });
    }
    else{
        $("#Underline").attr("src", "Images/Underline.jpg");
        isUnderline = 1;
        inputMsgTxr.css({ "text-decoration": "none" });
    }
}

function ToggleUnderlineHorn() {
    var msgArea = $("#HornTextarea");
    
    if (isHornUnderline == 1) {
        $("#HornUnderline").attr("src", "Images/Underline_Select.jpg");
        isHornUnderline = 0;
        hornInputMsgTxr.css({ "text-decoration": "underline" });
    }
    else{
        $("#HornUnderline").attr("src", "Images/Underline.jpg");
        isHornUnderline = 1;
        hornInputMsgTxr.css({ "text-decoration": "none" });
    }
}

function ToggleFont() {
    if (toolbarWindow.jqxWindow('isOpen')) {
        toolbarWindow.jqxWindow('close');
    }
    else {
        try
        {
            var w = $('#mainDiv').width();
            var h = $('#mainDiv').height();
            toolbarWindow.jqxWindow('open');
            toolbarWindow.jqxWindow({ width: w - rightPanelWidth, position: { x: 0, y: h - 185 } });
        }
        catch(err){}
    }
}

function ShowPrivateChatMessage(message, isSerialized) {
    var msg = isSerialized ? eval(message) : message;
    var oldHtml = privateMessagePanel.innerHTML;
    currentPrivateMessageCount++;
    if (currentPrivateMessageCount == publicMessageCount) {
        oldHtml = '';
        currentPrivateMessageCount = 0;
    }
    else if (!oldHtml) {
        oldHtml = '';
    }
    privateMessagePanel.innerHTML = oldHtml + msg + '<br />';
    if (isPrivateScroll) {
        privateMessagePanel.scrollTop = privateMessagePanel.scrollHeight;
    }
}

function ShowPublicChatMessage(message, isSerialized) {
    var msg = isSerialized ? eval(message) : message;
    var oldHtml = publicMessagePanel.innerHTML;
    currentPublicMessageCount++;
    if (currentPublicMessageCount == publicMessageCount) {
        oldHtml = '';
        currentPublicMessageCount = 0;
    }
    else if (!oldHtml) {
        oldHtml = '';
    }
    publicMessagePanel.innerHTML = oldHtml + msg + '<br />';
    if (isPublicScroll) {
        publicMessagePanel.scrollTop = publicMessagePanel.scrollHeight;
    }
}

function ShowHornMessage(message) {
    hornMsgPanel.append(eval(message));
    if (isHornScroll)
    {
        hornMsgPanel.scrollTop(hornMsgPanel.prop('scrollHeight'));
    }
}

function getCursorPosition(textarea) {
    var rangeData = { text: "", start: 0, end: 0 };
    textarea.focus();
    if (textarea.setSelectionRange) { // W3C
        rangeData.start = textarea.selectionStart;
        rangeData.end = textarea.selectionEnd;
        rangeData.text = (rangeData.start != rangeData.end) ? textarea.value.substring(rangeData.start, rangeData.end) : "";
    } else if (document.selection) { // IE
        var i,
            oS = document.selection.createRange(),
            // Don't: oR = textarea.createTextRange()
            oR = document.body.createTextRange();
        oR.moveToElementText(textarea);

        rangeData.text = oS.text;
        rangeData.bookmark = oS.getBookmark();

        // object.moveStart(sUnit [, iCount])
        // Return Value: Integer that returns the number of units moved.
        for (i = 0; oR.compareEndPoints('StartToStart', oS) < 0 && oS.moveStart("character", -1) !== 0; i++) {
            // Why? You can alert(textarea.value.length)
            if (textarea.value.charAt(i) == '\n') {
                i++;
            }
        }
        rangeData.start = i;
        rangeData.end = rangeData.text.length + rangeData.start;
    }

    return rangeData;
}

function SelectImage(path) {
    //var msgArea = $(".InputMsgTxr");
    //var cursorInfo = getCursorPosition(msgArea);
    if (currentImagesInMessage >= maxImagePerMessage)
    {
        alert("已经超过了最多表情数量。");
        return;
    }
    currentImagesInMessage = currentImagesInMessage + 1;
    var img = document.createElement("img");
    img.setAttribute("width", 30);
    img.setAttribute("height", 30);
    img.setAttribute("src", path);
    document.getElementById('InputMsgTxr').appendChild(img);
}

function InitStamp(stamps) {
    var stamps = eval("(" + stamps + ")");
    if (stamps != null) {
        stampTabDiv = $("#StampDiv");
        var ul = $('#stampTabHeader');
        for (var k = 0; k < stamps.length; k++) {
            var maxPage = Math.ceil(stamps[k].ImageVMs.length / countPerPage);
            var outerDiv = $('<div></div>');
            outerDiv.attr("id", "StampContent" + k);
            outerDiv.attr("class", "StampContent");
            for (var j = 0; j < maxPage; j++) {
                var pageDiv = $('<div></div>');
                pageDiv.attr("id", "StampPageDiv" + k + j);
                pageDiv.attr("class", "PageDiv");
                for (var i = j * countPerPage; i < j * countPerPage + countPerPage && i < stamps[k].ImageVMs.length; i++) {
                    var div = $('<div></div>');
                    div.attr("imgPath", stamps[k].ImageVMs[i].StaticImageFile);
                    div.append("<img src='" + stamps[k].ImageVMs[i].StaticImageFile + "'/>");
                    div.click(function () {
                        SelectImage($(this).attr("imgPath"));
                    });
                    div.appendTo(pageDiv);
                }
                pageDiv.appendTo(outerDiv);
            }
            var pages = $('<div></div>');
            pages.attr("id", "pages" + k);
            pages.attr("class", "pages");
            var pageStr = "<a> 共 " + maxPage + " 页 </a>";
            if (maxPage > 1) {
                var prePageId = "PrePageStamp" + k;
                var nextPageId = "NextPageStamp" + k;
                pageStr += " <a id = '" + prePageId + "' style={position:absolute,right:60px} href=\"javascript:getPrePageStamp();\">上一页</a>"
                pageStr += "<a id = '" + nextPageId + "' style={position:absolute,right:10px} href=\"javascript:getNextPageStamp();\">下一页</a>"
            }
            ul.append('<li>' + stamps[k].Name + '</li>');
            pages.append(pageStr);
            pages.appendTo(outerDiv);
            outerDiv.appendTo(stampTabDiv);
            selectedPageByNoStamp(k, 0);
        }
        stampTabDiv.jqxTabs({ theme: 'energyblue', width: 560, height: 300, showCloseButtons: false, scrollable: true, autoHeight: false });

        stampTabDiv.bind('selected', function (event) {
            tabIndexStamp = event.args.item;
            pgIndexStamp = 0;
        });

        tabIndexStamp = 0;
        pgIndexStamp = 0;
    }
}

function InitFaceTab(imgGroup, done) {
    var imageGroup = eval("(" + imgGroup + ")");
    if (imageGroup) {
        if (!motionTabDiv) {
            motionTabDiv = $("#MotionDiv");
        }
        var ul = $('#imageTabHeader');
        var maxPage = Math.ceil(imageGroup.ImageVMs.length / countPerPage);
        var outerDiv = $('<div></div>');
        outerDiv.attr("id", "MotionContent" + imageGroup.Id);
        outerDiv.attr("class", "MotionContent");
        for (var j = 0; j < maxPage; j++) {
            var pageDiv = $('<div></div>');
            pageDiv.attr("id", "PageDiv" + imageGroup.Id + j);
            pageDiv.attr("class", "PageDiv");
            for (var i = j * countPerPage; i < j * countPerPage + countPerPage && i < imageGroup.ImageVMs.length; i++) {
                var div = $('<div></div>');
                div.attr('path', imageGroup.ImageVMs[i].DynamicImageFile);
                div.append("<img style='{width:28px,height:28px}' src='" + imageGroup.ImageVMs[i].StaticImageFile + "'/>");
                div.click(function () {
                    SelectImage($(this).attr("path"));
                });
                div.bind({
                    mouseenter: function () {
                        clearTimeout(timer);
                    ShowPreview(this,$(this).attr("path"));
                    }, mouseleave: function () {
                        timer = setTimeout(function () { HidePreview() }, 500);
                }});
                div.appendTo(pageDiv);
            }
            pageDiv.appendTo(outerDiv);
        }
        var pages = $('<div></div>');
        pages.attr("id", "pages" + imageGroup.Id);
        pages.attr("class", "pages");
        var pageStr = "<a id='AddMotion" + imageGroup.Id + "' href=\"javascript:AddMotionImages();\">添加表情</a> <a> 共 " + maxPage + " 页 </a>";
        if (maxPage > 1) {
            var prePageId = "PrePageMotion" + imageGroup.Id;
            var nextPageId = "NextPageMotion" + imageGroup.Id;
            pageStr += " <a id = '" + prePageId;
            pageStr += "' style={position:absolute,right:60px} href=\"javascript:getPrePageMotion();\">上一页</a>"
            pageStr += "<a id = '" + nextPageId + "' style={position:absolute,right:10px} href=\"javascript:getNextPageMotion();\">下一页</a>"
        }
        ul.append("<li>" + imageGroup.Name + "</li>");
        pages.append(pageStr);
        pages.appendTo(outerDiv);
        outerDiv.appendTo(motionTabDiv);
        selectedPageByNoMotion(imageGroup.Id, 0);

        if (done) {
            motionTabDiv.jqxTabs({ theme: 'energyblue', width: 560, height: 300, showCloseButtons: false, scrollable: true, autoHeight: false });

            motionTabDiv.bind('selected', function (event) {
                tabIndexMotion = event.args.item;
                pgIndexMotion = 0;
            });

            tabIndexMotion = 0;
            pgIndexMotion = 0;
        }
    }
}

function ShowPreview(elem,imgPath){
    var left;
    if(!previewDiv)
        previewDiv = $('#PreviewImageDiv');
    previewDiv.html('');
    positionX = pageX(elem);
    if (positionX < 584/2)
        left = 546;
    else
        left = 70;
    previewDiv.css({'left': left });
    var div = $('<div></div>');
    div.append("<img src='" + imgPath + "'/>");
    div.appendTo(previewDiv);
    previewDiv.fadeIn(0.1, "linear");
}

function HidePreview() {
    if (!previewDiv)
        previewDiv = $('#PreviewImageDiv');
    
    previewDiv.fadeOut(0.1, "linear");
}

function ShowPreviewGift(path,price,score)
{
    if (!previewGift)
        previewGift = $('#PreviewGiftDiv');
    previewGift.html('');
    previewGift.append("<img style='margin-right:10px;' src='" + path + "'/>");

    //var innerDiv = $('<div></div>');

    previewGift.append("<p style='font-size:14px;text-align: left; width:100px;position:absolute;left:100px;top:10px;'>金币：  " + price + " </p>");
    previewGift.append("<p style='font-size:14px;text-align: left; width:100px;position:absolute;left:100px;top:40px;'>积分： " + score + " </p>");
    //innerDiv.appendTo(previewGift);
    previewGift.fadeIn(0.1, "linear");
}

function HidePreviewGift()
{
    if (!previewGift)
        previewGift = $('#PreviewGiftDiv');
   previewGift.fadeOut(0.1, "linear");
}

function AddMotionImages() {
    //var newImages = window.external.AddMotionImages();
}

function getPrePageStamp() {
    pgIndexStamp = parseInt(pgIndexStamp) - 1;
    selectedPageByNoStamp(tabIndexStamp, pgIndexStamp);
}

function getNextPageStamp() {
    pgIndexStamp = parseInt(pgIndexStamp) + 1;
    selectedPageByNoStamp(tabIndexStamp, pgIndexStamp);
}

function getNextPageMotion() {
    pgIndexMotion = parseInt(pgIndexMotion) + 1;
    selectedPageByNoMotion(tabIndexMotion, pgIndexMotion);
}

function getPrePageMotion() {
    pgIndexMotion = parseInt(pgIndexMotion) - 1;
    selectedPageByNoMotion(tabIndexMotion, pgIndexMotion);
}

function selectedPageByNoStamp(tabIndex, pageNo) {
    var obj = document.getElementById("StampContent" + tabIndex);
    var divcountlength = obj.childNodes.length;
    for (var no = 0; no < divcountlength; no++) {
        if (no == pageNo) {
            obj.childNodes[no].style.display = "block";
            if (divcountlength > 2) {
                if (no == 0) {
                    $("#PrePageStamp" + tabIndex).removeAttr('href');
                    $("#NextPageStamp" + tabIndex).attr('href', 'javascript:getNextPageStamp()');
                }
                else if (no == divcountlength - 2) {
                    $("#PrePageStamp" + tabIndex).attr('href', 'javascript:getPrePageStamp()');
                    $("#NextPageStamp" + tabIndex).removeAttr('href');
                }
                else {
                    $("#PrePageStamp" + tabIndex).attr('href', 'javascript:getPrePageStamp()');
                    $("#NextPageStamp" + tabIndex).attr('href', 'javascript:getNextPageStamp()');
                }
            }
        }
        else if (no != divcountlength - 1) { //exclue pages div
            obj.childNodes[no].style.display = "none";
        }
    }
    pgIndexStamp = pageNo;
}

function selectedPageByNoMotion(tabIndex, pageNo) {
    var obj = document.getElementById("MotionContent" + tabIndex);
    var divcountlength = obj.childNodes.length;
    for (var no = 0; no < divcountlength; no++) {
        if (no == pageNo) {
            obj.childNodes[no].style.display = "block";
            if (divcountlength > 2) {
                if (no == 0) {
                    $("#PrePageMotion" + tabIndex).removeAttr('href');
                    $("#NextPageMotion" + tabIndex).attr('href', 'javascript:getNextPageMotion()');
                }
                else if (no == divcountlength - 2) {
                    $("#PrePageMotion" + tabIndex).attr('href', 'javascript:getPrePageMotion()');
                    $("#NextPageMotion" + tabIndex).removeAttr('href');
                }
                else {
                    $("#PrePageMotion" + tabIndex).attr('href', 'javascript:getPrePageMotion()');
                    $("#NextPageMotion" + tabIndex).attr('href', 'javascript:getNextPageMotion()');
                }
            }
        }
        else if (no != divcountlength - 1) { //exclue pages div
            obj.childNodes[no].style.display = "none";
        }
    }
    pgIndexMotion = pageNo;
}

function ShowStamp(event) {
    if (stampTabDiv.is(':hidden')) {
        stampTabDiv.fadeIn("fast");
    }
    else if (stampTabDiv.is(':visible')) {
        stampTabDiv.fadeOut("fast");
    }
    //hide motion div on casual click on the page
    $(document).click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != "StampDiv" && tar.id != "Stamp" && tar.className != "PrePage" && tar.className != "NextPage"
            && tar.id != "stampTabHeader" && tar.className != "PageDiv") {
            stampTabDiv.fadeOut("fast");
            stampTabDiv.css({ 'vivibility': 'hidden' });
        }
    });
    stampTabDiv.click(function (event) {
        event.stopPropagation();
    });
}

function ShowArtistic(event) {
    if (artisticDiv.is(':hidden')) {
        artisticDiv.fadeIn("fast");
    }
    else if (artisticDiv.is(':visible')) {
        artisticDiv.fadeOut("fast");
    }
    //hide motion div on casual click on the page
    $(document).click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'ArtisticFont') {
            artisticDiv.fadeOut("fast");
            artisticDiv.css({ 'vivibility': 'hidden' });
        }
    });
    artisticDiv.click(function (event) {
        event.stopPropagation();
    });
}

function ShowFace(event) {
    if (motionTabDiv.is(':hidden')) {
        motionTabDiv.fadeIn("fast");
    }
    else if (motionTabDiv.is(':visible')) {
        motionTabDiv.fadeOut("fast");
    }
    //hide motion div on casual click on the page
    $(document).click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != "MotionDiv" && tar.id != "Motion" && tar.className != "PrePage" && tar.className != "NextPage"
            && tar.id != "imageTabHeader" && tar.className != "PageDiv" && tar.id != "MotionContent") {
            motionTabDiv.fadeOut("fast");
            motionTabDiv.css({ 'vivibility': 'hidden' });
        }
    });
    motionTabDiv.click(function (event) {
        event.stopPropagation();
    });
}

function ShowColor(event) {
    if (colorPanel.is(':hidden')) {
        colorPanel.fadeIn("fast");
    }
    else if (colorPanel.is(':visible')) {
        colorPanel.fadeOut("fast");
    }
    $(document).click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != "ColorPicker" && tar.id != "Color" && tar.className != "Color") {
            colorPanel.fadeOut("fast");
            $("#ColorPicker").css({ 'vivibility': 'hidden' });
        }
    });

    $('.colors').click(function () {
        $('#Color').css({ 'background-color': $(this).css('background-color') });
        inputMsgTxr.css({ "color": $(this).css('background-color') })
    });
}

function pageX(elem) {
    return elem.offsetParent ? (elem.offsetLeft + pageX(elem.offsetParent)) : elem.offsetLeft;
}

function pageY(elem) {
    return elem.offsetParent ? (elem.offsetTop + pageY(elem.offsetParent)) : elem.offsetTop;

}

function HexColorFromName(str) {
    var dc = {
        aliceblue: '#F0F8FF', antiquewhite: '#FAEBD7', aqua: '#00FFFF',
        aquamarine: '#7FFFD4', azure: '#F0FFFF', beige: '#F5F5DC',
        bisque: '#FFE4C4', black: '#000000', blanchedalmond: '#FFEBCD',
        blue: '#0000FF', blueviolet: '#8A2BE2', brown: '#A52A2A',
        burlywood: '#DEB887', cadetblue: '#5F9EA0', chartreuse: '#7FFF00',
        chocolate: '#D2691E', coral: '#FF7F50', cornflowerblue: '#6495ED',
        cornsilk: '#FFF8DC', crimson: '#DC143C', cyan: '#00FFFF',
        darkblue: '#00008B', darkcyan: '#008B8B', darkgoldenrod: '#B8860B',
        darkgray: '#A9A9A9', darkgreen: '#006400', darkkhaki: '#BDB76B',
        darkmagenta: '#8B008B', darkolivegreen: '#556B2F', darkorange: '#FF8C00',
        darkorchid: '#9932CC', darkred: '#8B0000', darksalmon: '#E9967A',
        darkseagreen: '#8FBC8F', darkslateblue: '#483D8B', darkslategray: '#2F4F4F',
        darkturquoise: '#00CED1', darkviolet: '#9400D3', deepskyblue: '#00BFFF',
        dimgray: '#696969', dodgerblue: '#1E90FF', firebrick: '#B22222',
        floralwhite: '#FFFAF0', forestgreen: '#228B22', fuchsia: '#FF00FF',
        gainsboro: '#DCDCDC', ghostwhite: '#F8F8FF', gold: '#FFD700',
        goldenrod: '#DAA520', gray: '#808080', green: '#008000',
        greenyellow: '#ADFF2F', honeydew: '#F0FFF0', hotpink: '#FF69B4',
        indianred: '#CD5C5C', indigo: '#4B0082', ivory: '#FFFFF0',
        khaki: '#F0E68C', lavender: '#E6E6FA', lavenderblush: '#FFF0F5',
        lawngreen: '#7CFC00', lemonchiffon: '#FFFACD', lightblue: '#ADD8E6',
        lightcoral: '#F08080', lightcyan: '#E0FFFF', lightgoldenrodyellow: '#FAFAD2',
        lightgreen: '#90EE90', lightgrey: '#D3D3D3', lightpink: '#FFB6C1',
        lightsalmon: '#FFA07A', lightseagreen: '#20B2AA', lightskyblue: '#87CEFA',
        lightslategray: '#778899', lightsteelblue: '#B0C4DE', lightyellow: '#FFFFE0',
        lime: '#00FF00', limegreen: '#32CD32', linen: '#FAF0E6',
        magenta: '#FF00FF', maroon: '#800000', mediumauqamarine: '#66CDAA',
        mediumblue: '#0000CD', mediumorchid: '#BA55D3', mediumpurple: '#9370D8',
        mediumseagreen: '#3CB371', mediumslateblue: '#7B68EE', mediumspringgreen: '#00FA9A',
        mediumturquoise: '#48D1CC', mediumvioletred: '#C71585', midnightblue: '#191970',
        mintcream: '#F5FFFA', mistyrose: '#FFE4E1', moccasin: '#FFE4B5',
        navajowhite: '#FFDEAD', navy: '#000080', oldlace: '#FDF5E6',
        olive: '#808000', olivedrab: '#688E23', orange: '#FFA500',
        orangered: '#FF4500', orchid: '#DA70D6', palegoldenrod: '#EEE8AA',
        palegreen: '#98FB98', paleturquoise: '#AFEEEE', palevioletred: '#D87093',
        papayawhip: '#FFEFD5', peachpuff: '#FFDAB9', peru: '#CD853F',
        pink: '#FFC0CB', plum: '#DDA0DD', powderblue: '#B0E0E6',
        purple: '#800080', red: '#FF0000', rosybrown: '#BC8F8F',
        royalblue: '#4169E1', saddlebrown: '#8B4513', salmon: '#FA8072',
        sandybrown: '#F4A460', seagreen: '#2E8B57', seashell: '#FFF5EE',
        sienna: '#A0522D', silver: '#C0C0C0', skyblue: '#87CEEB',
        slateblue: '#6A5ACD', slategray: '#708090', snow: '#FFFAFA',
        springgreen: '#00FF7F', steelblue: '#4682B4', tan: '#D2B48C',
        teal: '#008080', thistle: '#D8BFD8', tomato: '#FF6347',
        turquoise: '#40E0D0', violet: '#EE82EE', wheat: '#F5DEB3',
        white: '#FFFFFF', whitesmoke: '#F5F5F5', yellow: '#FFFF00',
        green: '#9ACD32'
    };

    return (dc[str]) ? dc[str] : '';
}

function getMessageStyle() {
    var underline;
    var bold;
    var italic;
    var color = '#000000';

    if (isUnderline == 0) {
        underline = 'underline';
    }
    else {
        underline = 'none';
    }
    if (isItalic == 0) {
        italic = 'italic';
    }
    else {
        italic = 'normal';
    }
    if (isBold == 0) {
        bold = 'bold';
    }
    else {
        bold = 'normal';
    }


    var fontF = fontFamilyList.jqxComboBox('val');
    var fontS = fontSizeList.jqxComboBox('val');

    if (fontF == '') {
        fontF = 'Arial';
    }
    if (fontS == '') {
        fontS = 14;
    }

    var style = 'font-family:' + fontF + ';color:'
        + HexColorFromName($('#Color').css('background-color')) + ';font-size:' + fontS
    + ';text-decoration:' + underline + ';font-style:' + italic + ';font-weight:' + bold + ";";

    return style;
}

function SendArtisticMessage(path,imgCount) {
    var isPrivate = $('#checks').attr("checked") == "checked";

    var toUser = toolbarUserList.jqxComboBox('val');
    if (toUser == "所有人" && isPrivate == true) {
        alert("不能给所有人发悄悄话。");
        return;
    }
    var pClassName;
    if (isPrivate) {
        pClassName = 'OuterPrivateMsg';
    }
    else {
        pClassName = 'OuterPublicMsg';
    }

    var toUserId = -1;
    var toUserName = "所有人";

    if (toUser != toUserName) {
        toUser = toolbarUserList.jqxComboBox('getSelectedItem');
        toUserId = toUser.value;
        toUserName = toUser.label;
    }

    var messageLabel = '';
    var receiver = getUser(toUserId);
    if (toUserId != -1 && receiver != null) {
        messageLabel = "<img src='" + me.RoleImageUrl + "'>" + me.NickName + "</img> 对 " +
                                        "<img src='" + receiver.RoleImageUrl + "'>" + receiver.NickName + "</img> 说 ：";
    }
    else {
        messageLabel = "<img src='" + me.RoleImageUrl + "'>" + me.NickName + "</img> 对 " + toUserName + "说 ： ";
    }
    var message = "<p class = '" + pClassName + "' style='word-break: break-all'><span class='ChatMessageLabel'>" + messageLabel + "</span>" + createArtisticImg(path, imgCount) + "</p>";
    isPrivate ? ShowPrivateChatMessage(message, false) : ShowPublicChatMessage(message, false);
    window.external.SendChatMessage(message, toUserId, isPrivate);
}

function createArtisticImg(path,imgCount)
{
    var result="";
    for (var i = 0; i < imgCount; i++)
    {
        result += "<img src='" + path + "' />";
    }
    return result;
}

function SendMessage() {
    var content = inputMsgTxr.html();
    if (content == '') {
        alert("消息不能为空。");
        return;
    }
    var isPrivate = $('#checks').attr("checked") == "checked";

    var toUser = toolbarUserList.jqxComboBox('val');
    if (toUser == "所有人" && isPrivate == true)
    {
        alert("不能给所有人发悄悄话。");
        return;
    }

    var pClassName;
    if (isPrivate) {
        pClassName = 'OuterPrivateMsg';
    }
    else {
        pClassName = 'OuterPublicMsg';
    }
   
    var toUserId = -1;
    var toUserName = "所有人";

    if (toUser != toUserName) {
        toUser = toolbarUserList.jqxComboBox('getSelectedItem');
        toUserId = toUser.value;
        toUserName = toUser.label;
    }

    var messageLabel = ''; 
    var receiver = getUser(toUserId);
    if (toUserId != -1 && receiver != null) {
        messageLabel = "<img src='" + me.RoleImageUrl + "'>" + me.NickName + "</img> 对 " +
                                        "<img src='" + receiver.RoleImageUrl + "'>" + receiver.NickName +"</img> 说 ：";
    }
    else {
        messageLabel = "<img src='" + me.RoleImageUrl + "'>" + me.NickName + "</img> 对 " +toUserName + "说 ： ";
    }
    var style = getMessageStyle();
    var message = "<p class = '" + pClassName + "' style='word-break: break-all'><span class='ChatMessageLabel'>" + messageLabel + "</span>" +
        "<span style = '" + style + "'>" + content + "</span></p>";
    isPrivate ? ShowPrivateChatMessage(message, false) : ShowPublicChatMessage(message,false);
    inputMsgTxr.html('');
    currentImagesInMessage = 0;
    window.external.SendChatMessage(message, toUserId, isPrivate);
}

function getUser(Id)
{
    for (var i = 0; i < allUsers.length; i++)
    {
        if (allUsers[i].Id == parseInt(Id)) {
            return allUsers[i];
        }
    }
    return null;
}

function ShowPrivateBtns() {
    var position = privateMessageDiv.jqxPanel('getVScrollPosition');
    privateMsgBtns.css({ "top": position });
    privateMsgBtns.show();
}

function HidePrivateBtns() {
    privateMsgBtns.hide();
}

function ShowPublicBtns() {
    var position = publicMessageDiv.jqxPanel('getVScrollPosition');
    publicMsgBtns.css({ "top": position });
    publicMsgBtns.show();
}

function HidePublicBtns() {
    publicMsgBtns.hide();
}

function HornScroll_Click() {
    if (hornScroll.attr("checked") == "checked") 
    {
        isHornScroll = true;
    }
    else
    {
        isHornScroll = false;
    }
}

function PublicScroll_Click() {
    if (publicScroll.attr("checked") == "checked") {
        isPublicScroll = true;
    }
    else {
        isPublicScroll = false;
    }
}

function PrivateScroll_Click() {
    if (privateScroll.attr("checked") == "checked") {
        isPrivateScroll = true;
    }
    else {
        isPrivateScroll = false;
    }
}

function PrivateClear_Click() {
    privateMessageDiv.jqxPanel('remove', $('.OuterPrivateMsg'));
    offsetPrivateScrollHeight = privateMessageDiv.jqxPanel('getScrollHeight');
}

function PublicClear_Click() {
    publicMessageDiv.jqxPanel('remove', $('.OuterPublicMsg'));
    offsetPublicScrollHeight = publicMessageDiv.jqxPanel('getScrollHeight');
}

function EditHornMsg() {
    hornMsgWindow.jqxWindow('open');
}

function scoreExchange() {
    if (parseInt(scoreToMoneyLabel.text()) == 0 )
    {
        alert("暂时不能兑换积分，请稍后再试或者联系系统管理员。")
        return;
    }
    if(scoreLabel && parseInt(scoreLabel.text()) == 0)
    {
        alert("没有积分可供兑换！");
        return;
    }
    if (scoreLabel) {
        $("#availableScore").text(scoreLabel.text());
    }
    $("#toExchangeScore").val('0');
    $("#getMoney").text('0');
    
    scoreExchangeWindow.jqxWindow('open');
}

function ShowToolbar() {
    hornToolbar.css({ 'top': hornMsgPanel.prop('scrollTop') });
    hornToolbar.show();
}

function HideToolbar() {
    hornToolbar.hide();
}

function hornMsgCancel() {
    hornInputMsgTxr.html('');
    $('input[type="radio"][name="radio"]').attr("checked", false);
    hornMsgWindow.jqxWindow('close');
}

function exchangeCancel() {

    scoreExchangeWindow.jqxWindow('close');
}

function getHornStyle() {
    var underline;
    var bold;
    var italic;
    if (isHornUnderline == 0)
        underline = 'underline';
    else
        underline = 'none';
    if (isHornItalic == 0)
        italic = 'italic';
    else
        italic = 'normal';
    if (isHornBold == 0)
        bold = 'bold'
    else
        bold = 'normal'

    
    var fontF = hornFontFamilyList.jqxComboBox('val');
    var fontS = hornFontSizeList.jqxComboBox('val');
    if (fontF == '')
        fontF = 'Arial';
    if (fontS == '')
        fontS = 14;

    var style = 'font-family:' + fontF + ';font-size:' + fontS
    + ';text-decoration:' + underline + ';font-style:' + italic + ';font-weight:' + bold
    + ';margin:0;';

    return style;
}

function scoreReady() {
    $('#getMoney').text(parseInt($('#toExchangeScore').val()) * 1000* parseInt(scoreToMoneyLabel.text()) / 100);
}

function exchangeOk() {
    var toExchange = parseInt($('#toExchangeScore').val()) * 1000;
    var availableScore = parseInt($('#availableScore').text());
    var getMoney = parseInt($('#getMoney').text());
    if (toExchange)
    {
        if (toExchange > availableScore) {
            alert("积分不够！");
            return;
        }
        else {
            if (window.external.ScoreExchange(me.Id, toExchange, getMoney)) {
                $('#availableScore').text(availableScore - toExchange);
                scoreLabel.text(availableScore - toExchange);
                moneyLabel.text(parseInt(moneyLabel.text()) + getMoney);
                me.Score = me.Score - toExchange;
                me.Money = me.Money + getMoney;
                alert("兑换成功！");
            }
            else {
                alert("兑换失败,请稍候再试。");
            }
        }
    }
    else
    {
        alert("输入的兑换积分格式不正确，必须是正整数！");
    }
}

function hornMsgOk() {
    var list = $('input:radio[name="radio"]:checked').val();
    if (list == null) {
        alert("请选择喇叭类型。");
        return;
    }
    else {
        if (hornInputMsgTxr.html() == '') {
            alert("喇叭信息内容不能为空。");
            return;
        }
        else {
            var style = getHornStyle();
            var content = hornInputMsgTxr.html();
            var message = '<p><span style = "' + style + '">' + content + '</span></p>'
            if (list == 'HornMsg') {
                message = me.NickName + " 发小喇叭：" + message;
                window.external.SendHornMsg(message);
            }
            else if (list == 'HallHornMsg') {
                message = me.NickName + " 发大喇叭：" + message;
                window.external.SendHallHornMsg(message);
            }
            else if (list == 'GlobalHornMsg') {
                message = me.NickName + " 发世界喇叭：" + message;
                window.external.SendGlobalHornMsg(message);
            }
        }
    }
    hornInputMsgTxr.html('');
    $('input[name=radio][name="radio"]').attr("checked", false);
    hornMsgWindow.jqxWindow('close');
}

function InitMoneyForHorn(hornMoney, hallHornMoney, globalHornMoney) {
    $("#hornMsgMoney").text(hornMoney);
    $("#hallHornMsgMoney").text(hallHornMoney);
    $("#globalHornMsgMoney").text(globalHornMoney);
}

function InitExchangeRate(scoreToMoneyRate) {
    scoreToMoneyLabel = $("#exchangeRate");
    scoreToMoneyLabel.text(scoreToMoneyRate);
}

function HornMsgClick() {

}

function HallHornMsgClick() {
    var list = $('input:radio[name="radio"]:checked').val();
    if (list == 'HallHornMsg') {
        if (!me.CanSendHallHornMsg) {
            alert("对不起，你没有发大喇叭的权限！");
            $('input[name=radio][value="HallHornMsg"]').attr("checked", false);
        }
    }
}

function GlobalMsgClick() {
    var list = $('input:radio[name="radio"]:checked').val();
    if (list == 'GlobalHornMsg') {
        if (!me.CanSendGlobalHornMsg) {
            alert("对不起，你没有发世界喇叭的权限！");
            $('input[name=radio][value="GlobalHornMsg"]').attr("checked", false);
        }
    }
}

function GiftSent(msg, count, unit) {
    try {
        Concurrent.Thread.create(giftSentAsync,msg,count,unit);
    }
    catch (err) { }
}

function giftSentAsync(m, c, u) {
    var currentGift = 1;
    var giftMsg = '';
    while (currentGift <= c) {
        giftMsg = "<code>" + m + currentGift + u + "</span></code><br />";
        currentGift++;
        ShowPublicChatMessage(giftMsg, false);
        Concurrent.Thread.sleep(messageInterval);
    }
}

function ScrollMessage(msg) {
    $('#roomScroll').html(msg);
}

function Good()
{
    SendArtisticMessage("Images/good.gif",5);
}


function Stick() {
    SendArtisticMessage("Images/stick.gif",5);
}


function Perfect() {
    SendArtisticMessage("Images/perfect.gif",5);
}

function VeryPerfect() {
    SendArtisticMessage("Images/veryperfect.gif",3);
}

function Cool() {
    SendArtisticMessage("Images/cool.gif",5);
}

function GoGo() {
    SendArtisticMessage("Images/go.gif",3);
}

function Power() {
    SendArtisticMessage("Images/power.gif",5);
}

function Fans() {
    SendArtisticMessage("Images/fans.gif",1);
}

function GoodSinger() {
    SendArtisticMessage("Images/goodsinger.gif",5);
}

function Welcome() {
    SendArtisticMessage("Images/Welcome.gif",2);
}

function Love() {
    SendArtisticMessage("Images/love.gif",5);
}

function High() {
    SendArtisticMessage("Images/high.gif",3);
}

function Kiss() {
    SendArtisticMessage("Images/kiss.gif",5);
}
