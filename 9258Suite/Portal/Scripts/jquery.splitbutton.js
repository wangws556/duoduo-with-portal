(function ($) {
    $.fn.extend({
        splitButton: function () {
            return this.each(function () {
                var defButton = $(this).find('button:eq(0)');
                $(defButton).removeClass('ui-corner-all');
                $(defButton).css('margin-right', '0px');
                $(defButton).css('border-right', 'none');
                var menuButton = $(this).find('button:eq(1)');
                $(menuButton).removeClass('ui-corner-all');
                $(menuButton).css('margin-left', '0px');
                var menu = $(this).find('ul:first');
                $(menu).data.visible = false;
                $(menu).menu();
                $(menu).css('position', 'absolute');
                $(menu).css('display', 'none');
                $(menu).css('z-index', '1000'); //Increase this if the menu is behind things
                $(menu).css('min-width', parseInt($(menuButton).outerWidth(true) + $(defButton).outerWidth(true)) + "px");
                $(menuButton).click(function () {
                    $(menu).fadeIn('fast');
                    return false;
                });
                //close menu when clicked outside
                $(document).mouseup(function (e) {

                    if ($(menu).has(e.target).length === 0 || $(menuButton).has(e.target).length === 0) {
                        $(menu).fadeOut('fast');
                    }
                });
                //close menu when escape key pressed
                $(document).keyup(function (e) {
                    if (e.keyCode == 27) {    // esc key
                        $(menu).fadeOut('fast');
                    }
                });
            });
        }
    });
})(jQuery);