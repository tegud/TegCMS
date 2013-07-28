(function () {
    TEGUD.BlogSideBar.Options = function (sidebarElement, viewModel) {
        var template = '<nav class="blog-sidebar-options">' +
                '<ul>' +
                    '{{#markers}}' +
                    '<li class="{{item-class}}" data-panel-id="{{id}}"><div class="blog-options-tooltip">{{title}}</div><a href=""></a></li>' +
                    '{{/markers}}' +
                '</ul>' +
            '</nav>';
        var options = $(Mustache.render(template, viewModel));
        var marker = $('<div class="blog-sidebar-options-marker"></div>');
        var markerHeight;

        options.prepend(marker);

        setTimeout(function () {
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

            if (item.hasClass('selected')) {
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
})();