//==========================================================================================================================================
// JavaScript File for Internet Explorer - requires jQuery.js
//==========================================================================================================================================

jQuery(function () {

	var nav = new Navigator;

	//IE 6
	if (nav.majorVersion <= 6) {
		addHoverClass('.tabContainer > h2', 'hover');
		addHoverClass('.logoLinks li', 'hover');
		addHoverClass('.logoLinks li.memory', 'memoryHover');
		addHoverClass('.logoLinks li.laptopPower', 'laptopPowerHover');
		addHoverClass('.logoLinks li.ups', 'upsHover');
		addFirstChild();
		$('#menu > ul > li:nth-child(7) > ul').addClass('left');
		$('#menu > ul > li:nth-child(8) > ul').addClass('left');
		$('#menu > ul > li:nth-child(9) > ul').addClass('left');
	}

});



//******************************************************************************************************************************************
//Function:		adds a hover class - used to supply a .hover class where IE 6 does not implement the :hover pseudo-class correctly
//******************************************************************************************************************************************
function addHoverClass(cssSelector, className) {
	$(cssSelector).hover(
	  function () {
	  	$(this).addClass(className);
	  },
	  function () {
	  	$(this).removeClass(className);
	  }
	);
}


//******************************************************************************************************************************************
//Function:		dynamically adds a first-child class to certain elements
//******************************************************************************************************************************************
function addFirstChild() {
	$('ul > li:first-child').addClass('first-child');
	$('.adsHorizontal > img:first-child').addClass('first-child');
	$('.adsHorizontal > object:first-child').addClass('first-child');
	$('.adsHorizontal > a:first-child').addClass('first-child');
}





//******************************************************************************************************************************************
// Function:	toggles the selected element's visibility between 'visible' and 'hidden' - fixes bug in IE6 & IE5, where the underlying form tag is displaying through the menu dropdown box
// Example:		toggleFormTag('element1','element2','element3')
//******************************************************************************************************************************************
function toggleFormTag(selectedId) {
	if (isBrowser('IE5', 'IE6')) {
		for (var i = 0; i < arguments.length; i++) {
			var selectedId = document.getElementById(arguments[i]);
			if (selectedId) {
				current = (selectedId.style.visibility == 'hidden') ? 'visible' : 'hidden';
				selectedId.style.visibility = current;
			}
		}
	}
}
