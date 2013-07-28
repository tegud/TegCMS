(function () {
    TEGUD.BlogSideBar.SideBarPanelCollection = function (sideBarPanelElements) {
        var sideBarPanels = {
            'blog-info': new TEGUD.BlogSideBar.Panels.BlogInfoPanel()
        };
        var sideBarPanelOrder = ['blog-info'];

        sideBarPanelElements.each(function () {
            var panel = new TEGUD.BlogSideBar.Panels.BlogHtmlPanel($(this));
            var id = panel.getId();

            sideBarPanels[id] = panel;
            sideBarPanelOrder[sideBarPanelOrder.length] = id;
        });

        return {
            buildViewModel: function () {
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
            getPanel: function (id) {
                return sideBarPanels[id];
            }
        };
    };
})();