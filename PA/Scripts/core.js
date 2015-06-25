'use strict';


var CORE = (function () {
	'use strict';

	return {
		//wank: function() { alert('wank')},
		addEvent: function (selector, targetSelector, eventName, callback) {

			if (arguments.length === 3) {
				CORE.addEvent(arguments[0], null, arguments[1], arguments[2]);
				return;
			}

			var doc = document;
			if (doc.addEventListener && doc.querySelector && callback && typeof (callback) === 'function') {
				var el = doc.querySelector(selector);
				if (el) {
					if (targetSelector) {
						el.addEventListener(eventName, function (e) {
							var elm = e.target;
							if (CORE.matchSelector(elm, targetSelector)) {
								var boundCallback = callback.bind(this);
								boundCallback(e);
							}
						}, false);
					}
					else {
						el.addEventListener(eventName, callback, false);
					}
				}
			}
		},

		addEvents: function (selector, targetSelector, eventName, callback) {

			if (arguments.length === 3) {
				CORE.addEvent(arguments[0], null, arguments[1], arguments[2]);
				return;
			}

			var doc = document;
			if (doc.addEventListener && doc.querySelectorAll && callback && typeof (callback) === 'function') {
				var els = doc.querySelectorAll(selector);
				for (var i = 0, len = els.length; i < len; i++) {
					if (targetSelector) {
						els[i].addEventListener(eventName, function (e) {
							var el = e.target;
							if (CORE.matchSelector(el, targetSelector)) {
								var boundCallback = callback.bind(this);
								boundCallback(e);
							}
						}, false);
					}
					else {
						els[i].addEventListener(eventName, callback, false);
					}
				}
			}
		},

		matchSelector: function (el, targetSelector) {

			if (el.matches) {
				return el.matches(targetSelector);
			}
			else if (el.webkitMatchesSelector) {
				return el.webkitMatchesSelector(targetSelector);
			}
			else if (el.mozMatchesSelector) {
				return el.mozMatchesSelector(targetSelector);
			}
			else if (el.msMatchesSelector) {
				return el.msMatchesSelector(targetSelector);
			}
			else if (el.oMatchesSelector) {
				return el.oMatchesSelector(targetSelector);
			}

			return false;
		}
	};
})();
