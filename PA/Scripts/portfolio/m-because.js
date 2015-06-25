// BECAUSE MOBILE PHONE

/*
* jQuery throttle / debounce - v1.1 - 3/7/2010
* http://benalman.com/projects/jquery-throttle-debounce-plugin/
* 
* Copyright (c) 2010 "Cowboy" Ben Alman
* Dual licensed under the MIT and GPL licenses.
* http://benalman.com/about/license/
*/
(function (b, c) { var $ = b.jQuery || b.Cowboy || (b.Cowboy = {}), a; $.throttle = a = function (e, f, j, i) { var h, d = 0; if (typeof f !== "boolean") { i = j; j = f; f = c } function g() { var o = this, m = +new Date() - d, n = arguments; function l() { d = +new Date(); j.apply(o, n) } function k() { h = c } if (i && !h) { l() } h && clearTimeout(h); if (i === c && m > e) { l() } else { if (f !== true) { h = setTimeout(i ? k : l, i === c ? e - m : e) } } } if ($.guid) { g.guid = j.guid = j.guid || $.guid++ } return g }; $.debounce = function (d, e, f) { return f === c ? a(d, e, false) : a(d, f, e !== false) } })(this);


/*
* Swipe 1.0
*
* Brad Birdsall, Prime
* Copyright 2011, Licensed GPL & MIT
* http://swipejs.com/
*/
window.Swipe = function (e, t) { if (!e) return null; var n = this; this.options = t || {}, this.index = this.options.startSlide || 0, this.speed = this.options.speed || 300, this.callback = this.options.callback || function () { }, this.delay = this.options.auto || 0, this.container = e, this.element = this.container.children[0], this.container.style.overflow = "hidden", this.element.style.listStyle = "none", this.element.style.margin = 0, this.setup(), this.begin(), this.element.addEventListener && (this.element.addEventListener("touchstart", this, !1), this.element.addEventListener("touchmove", this, !1), this.element.addEventListener("touchend", this, !1), this.element.addEventListener("webkitTransitionEnd", this, !1), this.element.addEventListener("msTransitionEnd", this, !1), this.element.addEventListener("oTransitionEnd", this, !1), this.element.addEventListener("transitionend", this, !1), window.addEventListener("resize", this, !1)) }, Swipe.prototype = { setup: function () { this.slides = this.element.children, this.length = this.slides.length; if (this.length < 2) return null; this.width = "getBoundingClientRect" in this.container ? this.container.getBoundingClientRect().width : this.container.offsetWidth; if (!this.width) return null; this.container.style.visibility = "hidden", this.element.style.width = this.slides.length * this.width + "px"; var e = this.slides.length; while (e--) { var t = this.slides[e]; t.style.width = this.width + "px", t.style.display = "table-cell", t.style.verticalAlign = "top" } this.slide(this.index, 0), this.container.style.visibility = "visible" }, slide: function (e, t) { var n = this.element.style; t == undefined && (t = this.speed), n.webkitTransitionDuration = n.MozTransitionDuration = n.msTransitionDuration = n.OTransitionDuration = n.transitionDuration = t + "ms", n.MozTransform = n.webkitTransform = "translate3d(" + -(e * this.width) + "px,0,0)", n.msTransform = n.OTransform = "translateX(" + -(e * this.width) + "px)", this.index = e }, getPos: function () { return this.index }, prev: function (e) { this.delay = e || 0, clearTimeout(this.interval), this.index && this.slide(this.index - 1, this.speed) }, next: function (e) { this.delay = e || 0, clearTimeout(this.interval), this.index < this.length - 1 ? this.slide(this.index + 1, this.speed) : this.slide(0, this.speed) }, begin: function () { var e = this; this.interval = this.delay ? setTimeout(function () { e.next(e.delay) }, this.delay) : 0 }, stop: function () { this.delay = 0, clearTimeout(this.interval) }, resume: function () { this.delay = this.options.auto || 0, this.begin() }, handleEvent: function (e) { switch (e.type) { case "touchstart": this.onTouchStart(e); break; case "touchmove": this.onTouchMove(e); break; case "touchend": this.onTouchEnd(e); break; case "webkitTransitionEnd": case "msTransitionEnd": case "oTransitionEnd": case "transitionend": this.transitionEnd(e); break; case "resize": this.setup() } }, transitionEnd: function (e) { this.delay && this.begin(), this.callback(e, this.index, this.slides[this.index]) }, onTouchStart: function (e) { this.start = { pageX: e.touches[0].pageX, pageY: e.touches[0].pageY, time: Number(new Date) }, this.isScrolling = undefined, this.deltaX = 0, this.element.style.MozTransitionDuration = this.element.style.webkitTransitionDuration = 0 }, onTouchMove: function (e) { if (e.touches.length > 1 || e.scale && e.scale !== 1) return; this.deltaX = e.touches[0].pageX - this.start.pageX, typeof this.isScrolling == "undefined" && (this.isScrolling = !!(this.isScrolling || Math.abs(this.deltaX) < Math.abs(e.touches[0].pageY - this.start.pageY))), this.isScrolling || (e.preventDefault(), clearTimeout(this.interval), this.deltaX = this.deltaX / (!this.index && this.deltaX > 0 || this.index == this.length - 1 && this.deltaX < 0 ? Math.abs(this.deltaX) / this.width + 1 : 1), this.element.style.MozTransform = this.element.style.webkitTransform = "translate3d(" + (this.deltaX - this.index * this.width) + "px,0,0)") }, onTouchEnd: function (e) { var t = Number(new Date) - this.start.time < 250 && Math.abs(this.deltaX) > 20 || Math.abs(this.deltaX) > this.width / 2, n = !this.index && this.deltaX > 0 || this.index == this.length - 1 && this.deltaX < 0; this.isScrolling || this.slide(this.index + (t && !n ? this.deltaX < 0 ? 1 : -1 : 0), this.speed) } };


$(document).ready(function () {
	var doc = document;

	// add tracking for HTML5 audio/video in Google Analytics
	addEventTracking();

	// header
	var header = doc.getElementById('header');
	header.querySelector('.menu').addEventListener('touchstart', function () { pageSlide('contents'); }, false);
	header.querySelector('.search').addEventListener('touchstart', function () { openForm('searchForm'); }, false);

	var contents = doc.getElementById('contents');
	contents.querySelector('.menu').addEventListener('touchstart', function () { pageSlide('contents', true); }, false);

	// sliders
	if (doc.querySelector('.slider')) addSwipeSlider();

	// category lists
	var categoryList = doc.getElementById('articleList');
	if (categoryList) {
		var url = 'http://becauselondon.com';
		switch (location.pathname.toLowerCase()) {
			case '/fashion':
				url += '?id=1&pg=';
				break;
			case '/culture':
				url += '?id=2&pg=';
				break;
			case '/beauty':
				url += '?id=3&pg=';
				break;
			default:
				url += '?pg=';
		}

		endlessScroll(url, '#articleList > li', categoryList);
	}

	// articles
	var articles = doc.getElementById('articles');
	if (articles) {

		articles.addEventListener('touchstart', toggleShare, false);
		articles.addEventListener('touchstart', toggleContents, false);
		articles.addEventListener('touchstart', playVideo, false);

		var url = location.href;
		url = url.indexOf('?') > -1 ? url + '&id=' : url + '?id=';
		var container = doc.getElementById('articleSlider');
		endlessScroll(url, '#articleSlider > li', container, function (el) { addSwipeSlider(el) });

	}

});



// uses CSS Transforms to slide a page in horizontally and out
function pageSlide(pageId, close) {
	var el = document.getElementById(pageId);
	if (close) el.removeAttribute('style');
	else el.setAttribute('style', 'transform: translateX(100%); -webkit-transform: translateX(100%)');
}




// slides open the search form input tag
function openForm(formId) {
	var form = document.getElementById(formId);
	var searchField = form.querySelector('input');
	if (searchField.hasAttribute('style')) {
		searchField.removeAttribute('style');
	}
	else {
		form.setAttribute('style', 'left: 50%;');
		searchField.setAttribute('style', 'transform: scaleX(1); -webkit-transform: scaleX(1);');
		searchField.addEventListener('webkitTransitionEnd', hideField, false);
	}

	var hideField = function () {
		form.setAttribute('style', 'left: -1000px;');
		searchField.removeEventListener('webkitTransitionEnd', hideField, false);
	};
}




// adds slider with swipe functionality using swipe.js
function addSwipeSlider(container) {
	var doc = document;
	if (!container) container = doc;

	var items = container.querySelectorAll('.slider');
	for (var i = 0, len = items.length; i < len; i++) {

		// dynamically create bullet list with the same number of items as the slider
		var itemList = items[i].querySelectorAll('ul > li');
		var el = items[i];
		var count = itemList.length;
		if (count > 1) {
			var bulletList = el.querySelector('ol');
			if (!bulletList) {
				bulletList = doc.createElement('ol');
				for (var j = 0; j < count; j++) {
					var li = doc.createElement('li');
					li.innerHTML = '&#8226;';
					if (j == 0) li.className = 'on';
					bulletList.appendChild(li);
				}
				el.appendChild(bulletList);
			}
		}

		// add slider
		window.mySwipe = new Swipe(
			el, {
				callback: function (e, pos) {
					var ol = e.target.parentNode.querySelector('ol');
					if (ol) {
						var bullets = ol.querySelectorAll('li');
						var i = bullets.length;
						while (i--) {
							bullets[i].className = '';
						}
						bullets[pos].className = 'on';
					}
				}
			}
		);

	}
}




// slides open the credits
function toggleContents(e) {
	var el = e.target;
	if (el.className !== 'creditsButton') return;
	e.preventDefault();
	e.stopPropagation();

	var content = el.parentNode.querySelector('.content');
	if (content) {
		var css = content.style;
		if (css.display === 'block') {
			css.display = 'none';
			el.innerHTML = 'Show Credits';
		}
		else {
			css.display = 'block';
			el.innerHTML = 'Hide Credits';
		}
	}
}



// slides open the share buttons
function toggleShare(e) {
	var el = e.target;
	if (el.className != 'shareButton') return;
	e.preventDefault();
	e.stopPropagation();

	// activate share buttons
	parseShareButtons(el.parentNode);

	// add CSS Transforms to slide the item in and out
	var content = el.parentNode.querySelector('.share > ul');
	if (content) {
		if (content.hasAttribute('style')) content.removeAttribute('style');
		else content.setAttribute('style', 'transform: translateX(-100%); -webkit-transform: translateX(-100%)');
	}
}





function parseShareButtons(el) {
	// https://coderwall.com/p/dhxg5a

	// Google+
	try {
		gapi.plusone.go();
	}
	catch (e) {
		$('.gplus').text('Google+ Temporarily Unavailable');
	}

	// Twitter
	try {
		twttr.widgets.load();
	}
	catch (e) {
		$('a[href="https://twitter.com/share"]').parent().text('Twitter Temporarily Unavailable');
	}

	// Pinterest
	try {
		$.ajax({ url: 'http://assets.pinterest.com/js/pinit.js', dataType: 'script', cache: true });
	}
	catch (e) {
		$('a[href^="http://pinterest.com"]').parent().text('Pinterest Temporarily Unavailable');
	}


	// Facebook
	try {
		el ? FB.XFBML.parse(el) : FB.XFBML.parse();
		}
	catch (e) {
		$('div.fb-like').parent().text('Facebook Temporarily Unavailable');
	}

}




// adds a loading image whilst the video is loading and automatically closes the video once play has finished
function playVideo(e) {
	var el = e.target;
	if (el.className != 'play') return;
	e.preventDefault();
	e.stopPropagation();


	var container = el.parentNode;
	container.classList.add('playing');

	var video = container.querySelector('video');
	video.addEventListener('webkitendfullscreen', function () {
		var container = this.parentNode;
		container.classList.remove('playing');
	}, false);

	video.addEventListener('ended', function () {
		this.webkitExitFullscreen();
	}, false);

	video.play();

}





// tracks HTML5 video/audio usage in Google Analytics
function addEventTracking() {

	var audioFiles = document.querySelectorAll('audio');
	var el;
	for (var i = 0, len = audioFiles.length; i < len; i++) {
		el = audioFiles[i];
		el.addEventListener('play', function (e) {
			if (this.currentTime == 0) {
				_gaq.push(['_trackEvent', 'Audio', e.type, getMediaSrc(this)]);
			}
		}, false);
		el.addEventListener('ended', function (e) {
			_gaq.push(['_trackEvent', 'Audio', e.type, getMediaSrc(this)]);
		}, false);
	}

	var videoFiles = document.querySelectorAll('video');
	for (var i = 0, len = videoFiles.length; i < len; i++) {
		el = videoFiles[i];
		el.addEventListener('play', function (e) {
			if (this.currentTime == 0) {
				_gaq.push(['_trackEvent', 'Video', e.type, getMediaSrc(this)]);
			}
		}, false);
		el.addEventListener('ended', function (e) {
			_gaq.push(['_trackEvent', 'Video', e.type, getMediaSrc(this)]);
		}, false);
	}

	// gets the src attribute from an HTML5 audio/video tag
	function getMediaSrc(el) {
		if (el.hasAttribute('src')) return el.getAttribute('src');
		var source = el.querySelector('source');
		if (source) return source.getAttribute('src');
	}

}














/*************************************************************************************************************************************
Adds endless scrolling to the page: the next page is loaded and added to the current page as the user scrolls down the current page.
* url - is the URL of the page that will be loaded
* section - specifies the actual HTML from the HTML that is loaded that will be added to the existing page
* container - is the element that is being scrolled - must be either the document body or an element with CSS overflow: scroll added
* callback (optional) - allows JavaScript to be added to the loaded HTML
**************************************************************************************************************************************/
var pageNo = 2;
var isLocked = false;
function endlessScroll(url, section, container, callback) {
	var doc = document;
	var isWindow = !(container.scrollHeight > container.clientHeight); // works out whether or not the container is scrollable - if it is not, then it defaults to the document object
	var screenHeight = isWindow ? doc.documentElement.clientHeight : container.clientHeight;
	var pageHeight = isWindow ? doc.documentElement.scrollHeight : container.scrollHeight;

	var timeout;

	// validate
	if (!url || !section) return; // required parameters
	if (!doc.querySelector) return;
	if (!doc.addEventListener) return;

	if (isWindow) window.addEventListener('scroll', onScroll, false);
	else container.addEventListener('scroll', onScroll, false);

	function onScroll(e) {

		// the locking ensures only 1 timer is queued up at a time
		if (isLocked) return;
		isLocked = true;

		// add a timer to reduce the amount of polling to increase efficiency
		setTimeout(function () {
			var el = e.target;
			var scrollPosition = isWindow ? window.pageYOffset : el.scrollTop;

			isLocked = false;

			// if the user scrolls to less than 6 screen heights from the bottom
			if (scrollPosition > (pageHeight - (screenHeight * 6))) {

				// disable scrolling event until loading of content is finished
				isLocked = true;

				// create a new request
				var request = new XMLHttpRequest();

				request.open('GET', url + pageNo);

				// define the event listener to wait for the asynchronous response back from the server
				request.onreadystatechange = function () {

					// if the request is complete and was successful
					if (request.readyState === 4 && request.status === 200) {

						// get the type of the response
						var type = request.getResponseHeader('Content-Type');

						// load the HTML content into a temporary element container - this is because DocumentFragment does not support innerHTML
						var tempContainer = doc.createElement('div');
						tempContainer.innerHTML = request.responseText;

						// append the requested section of the HTML	into the Document Fragment, and then append to document
						var docFrag = doc.createDocumentFragment();
						var requestedHtml = tempContainer.querySelectorAll(section);
						if (requestedHtml) {
							for (var i = 0, len = requestedHtml.length; i < len; i++) {
								docFrag.appendChild(requestedHtml[i]);
							}
						}

						// use the callback function to add JavaScript and perform any processing on the HTML DOM
						if (callback) {
							callback(docFrag);
						}

						container.appendChild(docFrag);

						// re-enable scrolling event
						pageNo++;
						pageHeight = container.scrollHeight;

						isLocked = false;
					}
					
				};

				request.send(null); // send the request now
				
			}
		}, 800);
	}
}


















function displayPopup(event, popupId) {

	// disable the link
	if (event) {
		event.preventDefault ? event.preventDefault() : event.returnValue = false;
	}

	// create the background element, if one does not already exist
	if (!document.getElementById('overlay')) {
		var overlay = document.createElement('div');
		overlay.setAttribute('id', 'overlay');
		document.body.appendChild(overlay);
		$('#overlay').hide();
	}

	// move the 'overlay' element next to the popup item so that it has the same z-index value
	$('#overlay').insertAfter($('#' + popupId));
	$('#overlay').fadeIn('slow');
	$('#' + popupId).fadeIn('slow');

	// adjust to make sure popup is centred in the page
	var popupWidth = ($('#' + popupId).outerWidth() / 2) + 'px';
	var popupHeight = ($('#' + popupId).outerHeight() / 2) + 'px';
	$('#' + popupId).css('margin-left', '-' + popupWidth).css('margin-top', '-' + popupHeight);

	//close all emails
	$(".popupClose").click(function () {
		$('#' + popupId).fadeOut('slow');
		$("#overlay").fadeOut('slow');
	});

}



function submitNewsletterEmail(el, event) {

	var emailId = 'emailNewsletter';
	var fromName = $('#' + emailId).find('#fromName').val();
	var fromEmail = $('#' + emailId).find('#fromEmail').val();
	var age = $('#' + emailId).find('#age').val();
	var gender = $('#' + emailId).find('input[name="gender"]:checked').val();
	var address1 = $('#' + emailId).find('#address1').val();
	var address2 = $('#' + emailId).find('#address2').val();
	var town = $('#' + emailId).find('#town').val();
	var postCode = $('#' + emailId).find('#postCode').val();
	var subscribe = $('#' + emailId).find('input[name="subscribe"]:checked').val();

	$.ajax({
		type: "post",
		//contentType: "application/json; charset=utf-8",
		url: "/umbraco/webservices/bm-email.asmx/NewsletterRequest",
		data: { fromName: fromName, fromEmail: fromEmail, age: age, gender: gender, address1: address1, address2: address2, town: town, postCode: postCode, subscribe: subscribe },
		success: function () {
			// close popup
			$('#' + emailId).fadeOut('slow');
			$("#overlay").fadeOut('slow');
		}
	});

	// disable the link
	event.preventDefault ? event.preventDefault() : event.returnValue = false;

}





