(function () {
    var headerTemplate = '<li>{{header}}</li>';
    var contentTemplate = '<div class="blog-sidebar-content">{{{content}}}</div>';

    TEGUD.BlogSideBar.Panels = {
        BlogHtmlPanel: function (panel) {
            var id = panel[0].id;
            var content = $('.blog-sidebar-panel-content', panel).html();
        
            panel.remove();

            function getTitle() {
                return $('h3', panel).text();
            }

            return {
                appendHeaderAndContent: function (headers, contentContainer) {
                    var viewModel = {
                        header: getTitle(),
                        content: content
                    };

                    headers.append(Mustache.render(headerTemplate, viewModel));
                    contentContainer.append(Mustache.render(contentTemplate, viewModel));
                },
                getId: function () {
                    return id;
                },
                getViewModel: function () {
                    return {
                        id: id,
                        'item-class': panel.data('itemClass'),
                        title: getTitle()
                    };
                }
            };
        },
        BlogInfoPanel: function () {
            var id = 'blog-info';
        
            function getTitle() {
                return 'Blog Info';
            }

            return {
                appendHeaderAndContent: function (headers, contentContainer) {
                    var viewModel = {
                        header: getTitle(),
                        content: '<ul class="blog-item-details"><li class="blog-item-posted-at" title="Monday, 06 May 2013 at 14:33">Monday, 06 May 2013<br/>14:33</li><li class="blog-item-author" title="by Steve Elliott">Steve Elliott</li><li class="blog-item-category" title="in Programming"><a href="/Categories/Programming">Programming</a></li><li class="blog-item-comments"><a href="/45/Finite%20State%20Machines%20in%20JavaScript#disqus_thread" data-disqus-identifier="Tegud_Blog_45">0 Comments</a></li></ul><ul class="blog-item-tags"><li><a href="/Tags/Programming">Programming</a></li><li><a href="/Tags/Javascript">Javascript</a></li></ul>'
                    };

                    headers.append(Mustache.render(headerTemplate, viewModel));
                    contentContainer.append(Mustache.render(contentTemplate, viewModel));
                },
                getId: function () {
                    return id;
                },
                getViewModel: function () {
                    return {
                        id: id,
                        'item-class': 'info selected',
                        title: getTitle()
                    };
                }
            };
        }
    };
})();