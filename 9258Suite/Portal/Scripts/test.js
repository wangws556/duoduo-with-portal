var giftCountArray = new Array();
giftCountArray.push({ id: 1, text: '1' });
giftCountArray.push({ id: 9, text: '9' });
giftCountArray.push({ id: 99, text: '99' });
giftCountArray.push({ id: 520, text: '520' });
giftCountArray.push({ id: 999, text: '999' });
giftCountArray.push({ id: 1314, text: '1314' });
giftCountArray.push({ id: 9999, text: '9999' });

$(document).ready(function () {
    $('#mainDiv').jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%' });
    $('#splitter').jqxSplitter({ theme: 'energyblue', width: '100%', height: '100%', orientation: 'horizontal', panels: [{ size: '50%' }, { size: '50%' }] });
    $('#rightPanel').jqxDockPanel({ theme: 'energyblue', width: '100%', height: '100%' });
    $('#listTabs').jqxTabs({ theme: 'energyblue', width: '100%', height: '100%', showCloseButtons: false });
    //$('#mainDiv').bind('layout', function () { alert('l'); });
    $('#giftcount').jqxComboBox({
        source: giftCountArray, selectedIndex: 0, width: '100px', height: '25px', 
        theme: 'energyblue', valueMember: 'id', displayMember: 'text', autoDropDownHeight: true
    });
});

function test() {
    $('#mainDiv').width(800);
    $('#mainDiv').jqxDockPanel('render');
}