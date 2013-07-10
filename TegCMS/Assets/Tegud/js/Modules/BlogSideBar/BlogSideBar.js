(function () {
    TEGUD.BlogSideBar = function (element) {
        var blogSideTop = element.offset().top - 51;
        var options = $('.blog-sidebar-options', element);
        var marker = $('.blog-sidebar-options-marker', options);
        var headers = $('.blog-sidebar-headers', element).css('left', 0);
        var panels = $('.blog-sidebar-panel').hide();
        var contentContainer = $('.blog-sidebar-content-container', element);
        var markerHeight = marker.height();
        var headerDeferred;
        var contentDeferred;

        function resetToSingleElement(containerElement, className) {
            containerElement
                    .children(':first')
                    .remove()
                    .end()
                .removeClass(className)
                .css('left', 0);
        }

        var fsm = new nano.Machine({
            states: {
                waiting: {
                    'selectPanel': function (item) {
                        var topPosition = item.position().top + ((item.height() - markerHeight) / 2) + 1;
                        var panel = item.data('panelId');

                        marker.css('top', topPosition);
                        item.addClass('selected').siblings().removeClass('selected');

                        var panelSelector = '#' + panel;
                        var headerText = $('h3', panelSelector).text();
                        var content = $('.blog-sidebar-panel-content', panelSelector).html();

                        headerDeferred = selectPanelHeader(headerText);
                        contentDeferred = selectPanelContent(content);

                        $.when(headerDeferred, contentDeferred).then(function () {
                            fsm.handle('complete');
                        });

                        this.transitionToState('animating');
                    }
                },
                animating: {
                    'complete': function () {
                        resetToSingleElement(headers, 'animated-header');
                        resetToSingleElement(contentContainer, 'animated-sidebar-section');
                        
                        this.transitionToState('waiting');
                    }
                }
            },
            initialState: 'waiting'
        });

        function buildHeaderElement(headerText) {
            return $('<li />', {
                'text': headerText
            });
        }

        function selectPanelHeader(headerText) {
            headers
                .append(buildHeaderElement(headerText))
                .addClass('animated-header')
                .css('left', -200);

            return $.Deferred();
        }

        function selectPanelContent(content) {
            contentContainer
                .append($('<div />', {
                    'class': 'blog-sidebar-content',
                    'html': content
                }))
                .addClass('animated-sidebar-section')
                .css('left', -300);
            
            return $.Deferred();
        }

        function selectHeaderAnimationCompleted() {
            headerDeferred.resolve();
        }

        function selectPanelAnimationCompleted() {
            contentDeferred.resolve();
        }

        function selectItem(item) {
            fsm.handle('selectPanel', item);
        }

        headers.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", selectHeaderAnimationCompleted);
        contentContainer.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", selectPanelAnimationCompleted);

        options.on('click', 'a', function () {
            var item = $(this).parent();

            selectItem(item);

            return false;
        });

        TEGUD.subscribe('TEGUD.Page.Scroll', function (currentTopScroll) {
            if (currentTopScroll > blogSideTop) {
                element.addClass('fixed');
            }
            else {
                element.removeClass('fixed');
            }
        });
    };
})();