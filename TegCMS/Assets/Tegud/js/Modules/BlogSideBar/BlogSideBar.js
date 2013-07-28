(function () {
    var animatorFactory = (function() {
        var animators = {
            Css3: function(headers, contentContainer) {
                var headerDeferred;
                var contentDeferred;
                var containerInnerDeferred;
                var containerInner = contentContainer.closest('.blog-sidebar-inner');
                var transitionEndEvents = "webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd";

                function transitionToNext(container, className) {
                    var animateTo = -(container.children(':first').outerWidth());

                    container
                        .addClass(className)
                        .css('left', animateTo);

                    return $.Deferred();
                }

                function animateHeightChange(newHeight) {
                    containerInner
                        .addClass('animate-height')
                        .css('height', newHeight);
                    
                    return $.Deferred();
                }

                headers.on(transitionEndEvents, function() {
                    headerDeferred.resolve();
                });
                contentContainer.on(transitionEndEvents, function () {
                    contentDeferred.resolve();
                });
                containerInner.on(transitionEndEvents, function () {
                    if (containerInnerDeferred) {
                        containerInnerDeferred.resolve();
                    }
                });

                return function (newContainerHeight) {
                    var allDeferreds = [];
                    var currentHeight = contentContainer.closest('.blog-sidebar-inner').height();
                    
                    headerDeferred = allDeferreds[allDeferreds.length] = transitionToNext(headers, 'animated-header');
                    contentDeferred = allDeferreds[allDeferreds.length] = transitionToNext(contentContainer, 'animated-sidebar-section', -300);

                    if (newContainerHeight && newContainerHeight !== currentHeight) {
                        containerInnerDeferred = allDeferreds[allDeferreds.length] = animateHeightChange(newContainerHeight);
                    }

                    return $.when.apply(this, allDeferreds);
                };
            }
        };

        return {
            build: function(headers, contentContainer) {
                return animators.Css3(headers, contentContainer);
            }
        };
    })();
    
    TEGUD.BlogSideBar = function (sideBarElement) {
        var element = $(Mustache.render('<div class="side-column-module" id="blog-sidebar">' +
                '<div class="blog-sidebar-container">' +
                    '<div class="blog-sidebar-inner">' +
                        '<ul class="blog-sidebar-headers"></ul>' +
                        '<div class="blog-sidebar-content-container"></div>' +
                    '</div>' +
                '</div>' +
            '</div>'));
        var sideBarPanels;
        var options;
        var blogSideTop;
        var headers;
        var contentContainer;

        function resetToSingleElement(containerElement, className) {
            if (containerElement.children().size() > 1) {
                containerElement
                    .children(':first')
                    .remove();
            }
            containerElement.removeClass(className).css('left', 0);
        }
        
        function appendNextPanel(panelId) {
            var panel = sideBarPanels.getPanel(panelId);
            
            panel.appendHeaderAndContent(headers, contentContainer);
            options.setSelected(panelId);
        }
        
        function calculateContainerHeight() {
            return contentContainer.children(':last').height() + 25;
        }

        var fsm = new nano.Machine({
            states: {
                initiating: {
                    _onEnter: function () {
                        var containerInner;
                        var panelId = 'blog-info';
                        var panel;
                        
                        sideBarPanels = new TEGUD.BlogSideBar.SideBarPanelCollection($('.blog-sidebar-panel', sideBarElement));
                        options = new TEGUD.BlogSideBar.Options(element, sideBarPanels.buildViewModel());
                        
                        sideBarElement.prepend(element);
                        
                        blogSideTop = element.offset().top - 51;
                        headers = $('.blog-sidebar-headers', element).css('left', 0);
                        contentContainer = $('.blog-sidebar-content-container', element);
                        containerInner = contentContainer.closest('.blog-sidebar-inner');

                        panel = sideBarPanels.getPanel(panelId);
                        panel.appendHeaderAndContent(headers, contentContainer);
                        options.setSelected(panelId);
                        
                        containerInner
                            .removeClass('animate-height')
                            .css('height', calculateContainerHeight());
                        
                        this.transitionToState('waiting');
                    }
                },
                waiting: {
                    _onEnter: function () {
                        resetToSingleElement(headers, 'animated-header');
                        resetToSingleElement(contentContainer, 'animated-sidebar-section');
                    },
                    selectPanel: function (item) {
                        appendNextPanel(item);

                        this.transitionToState('animating');
                    }
                },
                animating: {
                    _onEnter: function () {
                        animatorFactory.build(headers, contentContainer)(calculateContainerHeight()).then(function () {
                            fsm.handle('complete');
                        });
                    },
                    complete: function () {
                        this.transitionToState('waiting');
                    }
                }
            },
            initialState: 'initiating'
        });
        
        element.on('switchToPanel', function (event, id) {
            fsm.handle('selectPanel', id);
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