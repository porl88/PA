//var CORE = window.CORE || {};



var isLoaded = false;
(function () {
	var doc = document;
	if (addEventListener) {
		doc.addEventListener('DOMContentLoaded', function () {
			if (!isLoaded) {
				isLoaded = true;
				stripOutHtml.init();
			};
		}, false);
	}
})();

var stripOutHtml = (function () {
	var doc = document;
	var isReady = false;
	return {
		init: function () {
			CORE.addEvent('.html', '.htmlSelect', 'click', stripOutHtml.toggleAll);
		}
		, toggleAll: function (e) {
			var el = e.target;
			var classes = el.classList;
			classes.toggle('uncheck');
			var container = el.parentElement;
			if (classes.contains('uncheck')) {
				el.textContent = 'Clear All';
				stripOutHtml.selectAll(container);
			}
			else {
				el.textContent = 'Select All';
				stripOutHtml.removeAll(container);
			}
		}
		, selectAll: function (container) {
			var checkboxes = container.querySelectorAll('input[type="checkbox"]');
			for (var i = 0, len = checkboxes.length; i < len; i++) {
				checkboxes[i].checked = true;
			}
		}
		, removeAll: function (container) {
			var checkboxes = container.querySelectorAll('input[type="checkbox"]');
			for (var i = 0, len = checkboxes.length; i < len; i++) {
				checkboxes[i].checked = false;
			}
		}
	};
})();

//var whenReady = (function () { // This function returns the whenReady() function

//	var funcs = []; // The functions to run when we get an event
//	var ready = false; // Switches to true when the handler is triggered

//	// The event handler invoked when the document becomes ready
//	function handler(e) {

//		// If we've already run once, just return
//		if (ready) return;

//		// If this was a readystatechange event where the state changed to something other than "complete", then we're not ready yet
//		if (e.type === "readystatechange" && document.readyState !== "complete") return;

//		// Run all registered functions. Note that we look up funcs.length each time, in case calling one of these functions causes more functions to be registered.
//		for (var i = 0; i < funcs.length; i++) funcs[i].call(document);

//		// Now set the ready flag to true and forget the functions
//		ready = true;
//		funcs = null;

//	}
//	// Register the handler for any event we might receive
//	if (document.addEventListener) {
//		document.addEventListener("DOMContentLoaded", handler, false);
//		document.addEventListener("readystatechange", handler, false);
//		window.addEventListener("load", handler, false);
//	}

//	// Return the whenReady function
//	return function whenReady(f) {
//		if (ready) f.call(document); // If already ready, just run it
//		else funcs.push(f); // Otherwise, queue it for later.
//	}

//}());

//whenReady(function () {
//	if (window.innerWidth <= 500) {
//		toggleClassOnClick('#header > nav', 'open');
//	}
//});



