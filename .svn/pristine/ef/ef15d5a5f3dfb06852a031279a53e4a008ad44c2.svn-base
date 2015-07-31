$.fn.myTextEditor = function (options) {
    // extend the option with the default ones
    var settings = $.extend({
        left: 1,
        top: 1,
        right: 1,
        height:100,
        fonts: ["Arial", "Comic Sans MS", "Courier New", "Monotype Corsiva", "Tahoma", "Times"]
    }, options);
    return this.each(function () {
        var $this = $(this).hide();
        var containerHeight = (settings.height + 35) + 'px';
        // create a container div on the fly
        var containerDiv = $("<div/>", {
            css: {
                position:'absolute',
                left: settings.left+'px',
                top: settings.top+'px',
                right: settings.right+'px',
                height: containerHeight,
                border: "1px solid black"
            }
        });        
        $this.after(containerDiv);
        var editor = $("<iframe/>", {
            frameborder: "0",
            css: {                
                height: settings.height+'px',
                width:'100%'
            }
        }).appendTo(containerDiv).get(0);
        
        // opening and closing the editor is a workaround to solve issue in Firefox
        editor.contentWindow.document.open();
        editor.contentWindow.document.close();
        editor.contentWindow.document.designMode = "on";
        var buttonPane = $("<div/>", {
            "class": "editor-btns",
            css: {
                left: settings.left,
                top: settings.top,
                right: settings.right,
                height: "25px"
            }
        }).prependTo(containerDiv);
        var btnBold = $("<a/>", {
            href: "#",
            text: "B",
            data: {
                commandName: "bold"
            },
            click: execCommand
        }).appendTo(buttonPane);
        var btnItalic = $("<a/>", {
            href: "#",
            text: "I",
            data: {
                commandName: "italic"
            },
            click: execCommand
        }).appendTo(buttonPane);
        var btnUnderline = $("<a/>", {
            href: "#",
            text: "U",
            data: {
                commandName: "underline"
            },
            click: execCommand
        }).appendTo(buttonPane);
        var selectFont = $("<select/>", {
            data: {
                commandName: "FontName"
            },
            change: execCommand
        }).appendTo(buttonPane);
        $.each(settings.fonts, function (i, v) {
            $("<option/>", {
                value: v,
                text: v
            }).appendTo(selectFont);

        });
        function execCommand(e) {
            $(this).toggleClass("selected");
            var contentWindow = editor.contentWindow;
            contentWindow.focus();
            contentWindow.document.execCommand($(this).data("commandName"), false, this.value || "");
            contentWindow.focus();
            return false;
        }
    });
};
