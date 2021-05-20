window.bladezorFunctions = {
	scrollTo: function (container, element) {
		container.scrollTo(element.offsetLeft, element.offsetTop);
	},
	setUrl: function (url) {
		history.pushState(null, '', url);
	}
}

window.patternflyBlazorFunctions.registerDocumentEventListeners();