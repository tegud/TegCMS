(function() {
    TEGUD.Menu = function(element) {
        var body = $('body');
        var menuTop = element.offset().top;
        var fixedMenu = $('<div class="fixed-menu hidden"></div>')
            .append(element.clone().removeAttr('id'))
            .appendTo(body);
        var jumpToTopElement = $('.jump-to-top', fixedMenu);

        TEGUD.subscribe('TEGUD.Page.Scroll', function(currentTopScroll) {
            if (currentTopScroll > menuTop) {
                fixedMenu.removeClass('hidden');
                setTimeout(function() {
                    jumpToTopElement.addClass('visible');
                }, 0);
            } else {
                fixedMenu.addClass('hidden');
                jumpToTopElement.removeClass('visible');
            }
        });
    };
})();