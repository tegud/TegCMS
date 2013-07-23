(function () {
    var animatorFactory = (function() {
        var animators = {
            Css3: function(headers, contentContainer) {
                var headerDeferred;
                var contentDeferred;
                var containerInnerDeferred;
                var containerInner = contentContainer.closest('.blog-sidebar-inner');

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

                headers.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", function() {
                    headerDeferred.resolve();
                });
                contentContainer.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", function () {
                    contentDeferred.resolve();
                });
                containerInner.on("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd", function () {
                    if (containerInnerDeferred) {
                        containerInnerDeferred.resolve();
                    }
                });

                return function () {
                    var allDeferreds = [];
                    var nextItemHeight = contentContainer.children(':last').height();
                    var currentHeight = contentContainer.closest('.blog-sidebar-inner').height();
                    var minHeight = 200;

                    if (nextItemHeight < minHeight) {
                        nextItemHeight = minHeight;
                    }
                    
                    headerDeferred = allDeferreds[allDeferreds.length] = transitionToNext(headers, 'animated-header');
                    contentDeferred = allDeferreds[allDeferreds.length] = transitionToNext(contentContainer, 'animated-sidebar-section', -300);

                    if(nextItemHeight !== currentHeight) {
                        containerInnerDeferred = allDeferreds[allDeferreds.length] = animateHeightChange(nextItemHeight);
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

    var BlogHtmlPanel = function (panel) {
        var id = panel[0].id;
        var content = $('.blog-sidebar-panel-content', panel).html();
        
        panel.remove();

        return {
            appendHeaderAndContent: function (headers, contentContainer) {
                var viewModel = {
                    header: $('h3', panel).text(),
                    content: content
                };

                headers.append(Mustache.render('<li>{{header}}</li>', viewModel));
                contentContainer.append(Mustache.render('<div class="blog-sidebar-content">{{{content}}}</div>', viewModel));
            },
            getId: function () {
                return id;
            },
            getViewModel: function () {
                return {
                    id: id,
                    'item-class': panel.data('itemClass')
                };
            }
        };
    };

    var BlogInfoPanel = function () {
        var id = 'blog-info';
        
        return {
            appendHeaderAndContent: function (headers, contentContainer) {
                var viewModel = {
                    header: 'Blog Info',
                    content: ''
                };

                headers.append(Mustache.render('<li>{{header}}</li>', viewModel));
                contentContainer.append(Mustache.render('<div class="blog-sidebar-content">{{{content}}}</div>'), viewModel);
            },
            getId: function () {
                return id;
            },
            getViewModel: function () {
                return {
                    id: id,
                    'item-class': 'info selected'
                };
            }
        };
    };

    var Options = function (sidebarElement, viewModel) {
        var template = '<nav class="blog-sidebar-options">' +
                '<ul>' +
                    '{{#markers}}' +
                    '<li class="{{item-class}}" data-panel-id="{{id}}"><a href=""></a></li>' +
                    '{{/markers}}' +
                '</ul>' +
            '</nav>';
        var options = $(Mustache.render(template, viewModel));
        var marker = $('<div class="blog-sidebar-options-marker"></div>');
        var markerHeight;

        options.prepend(marker);

        setTimeout(function() {
            markerHeight = marker.height();
        }, 0);

        $('.blog-sidebar-container', sidebarElement).prepend(options);

        function positionMarker(item) {
            var topPosition = item.position().top + ((item.height() - markerHeight) / 2) + 1;
            marker.css('top', topPosition);
        }

        function selectMenuItem(item) {
            item.addClass('selected').siblings().removeClass('selected');
        }
        
        options.on('click', 'li', function () {
            var item = $(this);
            
            if(item.hasClass('selected')) {
                return false;
            }

            sidebarElement.trigger('switchToPanel', item.data('panelId'));
            
            return false;
        });

        return {
            setSelected: function (id) {
                var item;
                $('li', options).each(function () {
                    var currentItem = $(this);
                    if (currentItem.data('panelId') === id) {
                        item = currentItem;
                        return false;
                    }

                    return true;
                });

                positionMarker(item);
                selectMenuItem(item);
            }
        };
    };

    var SideBarPanelCollection = function (sideBarPanelElements) {
        var sideBarPanels = {
            'blog-info': new BlogInfoPanel()
        };
        var sideBarPanelOrder = ['blog-info'];

        sideBarPanelElements.each(function () {
            var panel = new BlogHtmlPanel($(this));
            var id = panel.getId();

            sideBarPanels[id] = panel;
            sideBarPanelOrder[sideBarPanelOrder.length] = id;
        });

        return {
            buildViewModel: function() {
                var viewModel = {
                    markers: []
                };

                var panelsLength = sideBarPanelOrder.length;
                var x = 0;

                for (; x < panelsLength; x++) {
                    var panel = sideBarPanels[sideBarPanelOrder[x]];
                    viewModel.markers[x] = panel.getViewModel();
                }

                return viewModel;
            },
            getPanel: function(id) {
                return sideBarPanels[id];
            }
        };
    };
    
    TEGUD.BlogSideBar = function (sideBarElement) {
        var sideBarPanels = new SideBarPanelCollection($('.blog-sidebar-panel', sideBarElement));
        var element = $(Mustache.render('<div class="side-column-module" id="blog-sidebar">' +
                '<div class="blog-sidebar-container">' +
                    '<div class="blog-sidebar-inner">' +
                        '<ul class="blog-sidebar-headers"></ul>' +
                        '<div class="blog-sidebar-content-container"></div>' +
                    '</div>' +
                '</div>' +
            '</div>'));
        var options = new Options(element, sideBarPanels.buildViewModel());
        sideBarElement.prepend(element);

        var blogSideTop = element.offset().top - 51;
        var headers = $('.blog-sidebar-headers', element).css('left', 0);
        var contentContainer = $('.blog-sidebar-content-container', element);

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

        var fsm = new nano.Machine({
            states: {
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