var MOBILE = (function () {
	'use strict';

	var doc = document;
	if (doc.addEventListener) {
		doc.addEventListener('DOMContentLoaded', hideURLbar, false);
		doc.documentElement.addEventListener('click', keepInWebAppMode, false);
	}

	function hideURLbar() {
		setTimeout(function () {
			window.scrollTo(0, 1);
		}, 0);
	}

	// if using apple-mobile-web-app-capable mode, then this is needed to keep the phone in 'standalone' mode when links are clicked
	function keepInWebAppMode(e) {
		var el = e.target;
		if (el.nodeName !== 'A') return;
		e.stopPropagation();
		e.preventDefault();
		location.href = el.getAttribute('href');
	}
}());
