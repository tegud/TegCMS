(function () {
    var animatorFactory = (function() {
        var animators = {
            Css3: function(headers, contentContainer) {
                var headerDeferred;
                var contentDeferred;

                function transitionToNext(container, className) {
                    var animateTo = -(container.children(':first').outerWidth());

                    container
                        .addClass(className)
                        .css('left', animateTo);

                    return $.Deferred();
                }

                headers.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", function() {
                    headerDeferred.resolve();
                });
                contentContainer.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", function() {
                    contentDeferred.resolve();
                });

                return function() {
                    headerDeferred = transitionToNext(headers, 'animated-header');
                    contentDeferred = transitionToNext(contentContainer, 'animated-sidebar-section', -300);

                    return $.when(headerDeferred, contentDeferred);
                };
            }
        };

        return {
            build: function(headers, contentContainer) {
                return animators.Css3(headers, contentContainer);
            }
        };
    })();

    var panelContentFactory = (function () {
        




    })();
    
    TEGUD.BlogSideBar = function (element) {
        var blogSideTop = element.offset().top - 51;
        var options = $('.blog-sidebar-options', element);
        var marker = $('.blog-sidebar-options-marker', options);
        var headers = $('.blog-sidebar-headers', element).css('left', 0);
        var panels = $('.blog-sidebar-panel').hide();
        var contentContainer = $('.blog-sidebar-content-container', element);
        var markerHeight = marker.height();

        function resetToSingleElement(containerElement, className) {
            if (containerElement.children().size() > 1) {
                containerElement
                    .children(':first')
                    .remove();
            }
            containerElement.removeClass(className).css('left', 0);
        }
        
        function positionMarker(item) {
            var topPosition = item.position().top + ((item.height() - markerHeight) / 2) + 1;
            marker.css('top', topPosition);
        }
        
        function selectMenuItem(item) {
            item.addClass('selected').siblings().removeClass('selected');
        }
        
        function appendNextPanel(item) {
            function getPanelData(item) {
                var panel = item.data('panelId');
                var panelSelector = '#' + panel;
                var headerText = $('h3', panelSelector).text();
                var content = $('.blog-sidebar-panel-content', panelSelector).html();

                return {
                    headerText: headerText,
                    content: content
                };
            }
            
            function appendElement(elementFactory, container, contents) {
                container.append(elementFactory(contents));
            }
            
            function buildHeaderElement(headerText) {
                return $('<li />', {
                    'text': headerText
                });
            }

            function buildContentElement(content) {
                return $('<div />', {
                    'class': 'blog-sidebar-content',
                    'html': content
                });
            }
            
            var panelData = getPanelData(item);
            appendElement(buildHeaderElement, headers, panelData.headerText);
            appendElement(buildContentElement, contentContainer, panelData.content);
        }

        var fsm = new nano.Machine({
            states: {
                waiting: {
                    _onEnter: function () {
                        resetToSingleElement(headers, 'animated-header');
                        resetToSingleElement(contentContainer, 'animated-sidebar-section');
                    },
                    selectPanel: function (item) {
                        positionMarker(item);
                        selectMenuItem(item);
                        appendNextPanel(item);

                        this.transitionToState('animating');
                    }
                },
                animating: {
                    _onEnter: function () {
                        animatorFactory.build(headers, contentContainer)().then(function () {
                            fsm.handle('complete');
                        });
                    },
                    complete: function () {
                        this.transitionToState('waiting');
                    }
                }
            },
            initialState: 'waiting'
        });


        options.on('click', 'li', function () {
            fsm.handle('selectPanel', $(this));

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