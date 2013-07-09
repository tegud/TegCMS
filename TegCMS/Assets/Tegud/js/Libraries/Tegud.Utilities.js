TEGUD.Utilities = {};

(function() {
    TEGUD.Utilities.ThrottledFunction = function(code, throttleRate) {
        var scrollThrottler;
        var throttledFunction = function() {
            if (scrollThrottler) {
                return;
            }

            code();

            if (!scrollThrottler) {
                scrollThrottler = setTimeout(function() {
                    clearTimeout(scrollThrottler);
                    scrollThrottler = false;
                }, throttleRate);
            }
        };

        return throttledFunction;
    };
})();