(function() {
    var body = $('body');

    $(window).on('scroll', new TEGUD.Utilities.ThrottledFunction(function() {
        var currentTopScroll = body.scrollTop();

        TEGUD.publish('TEGUD.Page.Scroll', [currentTopScroll]);
    }, 5));
})();