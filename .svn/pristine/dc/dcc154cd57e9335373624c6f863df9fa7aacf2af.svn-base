
var hallWindowVM;
var currentTheme = 'energyblue';
var roomGs;
var RoomGroupViewModel = function (roomGroup) {
    this.label = roomGroup.Name;
    this.items = ko.observableArray(new Array());
    if (roomGroup.SubRoomGroupModels && roomGroup.SubRoomGroupModels.length > 0) {
        for (var i = 0; i < roomGroup.SubRoomGroupModels.length; i++) {
            this.items.push(new RoomGroupViewModel(roomGroup.SubRoomGroupModels[i]));
        }
    }
}

function HallViewModel(rgs) {
    roomGs = eval(rgs);
    var rgroups = eval(rgs);
    var self = this;
    self.roomGroups = ko.observableArray(new Array());
    if (rgroups.length > 0) {
        for (var i = 0; i < rgroups.length; i++) {
            self.roomGroups.push(new RoomGroupViewModel(rgroups[i]));
        }
    }
}

function getJqxDataAdapter(roomGroup) {
    var roomGroupDataAdapterSource = {
        datatype: 'array',
        datafields: [
			{ name: 'label', map: 'Name' },
			{ name: 'value', map: 'Id' },
			{ name: 'id', map: 'Id' },
			{ name: 'icon', map: 'ImageUrl' },
			{ name: 'iconsize' },
			{ name: 'items' },
			{ name: 'd' },
			{ name: 'loaded' }
        ]
    };
    roomGroupDataAdapterSource.localdata = roomGroup.SubRoomGroupModels;
    var dataAdapter = new $.jqx.dataAdapter(roomGroupDataAdapterSource, {
        beforeLoadComplete: function (records, originalData) {
            var data = new Array();
            for (var i = 0; i < records.length; i++) {
                records[i].iconsize = 16;
                if (originalData[i].SubRoomGroupModels && originalData[i].SubRoomGroupModels.length > 0) {
                    //	records[i].items = getJqxDataAdapter(originalData[i]).records;
                    records[i].items = new Array();
                    records[i].items.push({ label: '', value: '', id: '', loaded: true });
                    records[i].d = originalData[i];
                    records[i].loaded = false;
                }
                else {
                    records[i].loaded = true;
                }

                data.push(records[i]);
            }
            return data;
        }
    });
    dataAdapter.dataBind();
    return dataAdapter;
}




function InitViewModel(rgs, w, h) {
    InitBase(w, h);
    hallWindowVM = new HallViewModel(rgs);
    $('#mainDockPanel').jqxDockPanel({ width: w, height: h });
    for (var i = 0; i < hallWindowVM.roomGroups().length; i++) {
        $('#topTabUl').append("<li>" + hallWindowVM.roomGroups()[i].label + "</li>");
        $('#mainDiv').append("<div id='roomGroup" + i + "Spliter' class='adjustheight'>" +
			"<div id='roomGroup" + i + "Tree' style='overflow:scroll' class='adjustheight'></div>" +
			"<div id='roomGroup" + i + "Content' class='adjustheight'></div></div>");
    }
    $('#mainDiv').jqxTabs({
        position: 'top', theme: currentTheme, width: w,
        initTabContent: function (tab) {
            var id = parseInt(tab);
            //var adapter = getJqxDataAdapter(hallWindowVM.roomGroups()[id]);
            var tree = $('#roomGroup' + id + 'Tree');
            //tree.attr('data-bind', "jqxTree: {source: hallWindowVM.roomGroups()[id], theme: '" + currentTheme + "'}");
            var spliter = $('#roomGroup' + id + 'Spliter');
            spliter.jqxSplitter({ theme: currentTheme, height: h, width: '100%', panels: [{ size: '30%' }, { size: '70%' }] });
            tree.jqxTree({ source: roomGs[id].items, theme: currentTheme });
            //tree.on('expand', function (e) {
            //	var node = tree.jqxTree('getItem', e.args.element);
            //	if (node.loaded) {
            //		return;
            //	}
            //	var ada = getJqxDataAdapter(node.item);
            //	tree.jqxTree('addTo', ada.records, $(e.args.element));
            //});
            //ko.applyBindings(hallWindowVM.roomGroups()[id]);
        }
    });
}

function resizeHall(w, h) {
    var nh = $('#mainDockPanel').height() + h;
    var nw = $('#mainDockPanel').width() + w;
    $('#mainDockPanel').width(nw);
    $('#mainDockPanel').height(nh);
    $('#mainDockPanel').jqxDockPanel('render');
    $('.adjustheight').each(function () {
        var newh = $(this).height() + h;
        $(this).height(newh);
    });
}
