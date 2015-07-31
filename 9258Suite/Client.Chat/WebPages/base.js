var imageFolder;
var giftCornerBGImage;

function noerror() { return true; }

function disabled() { return false; }

function InitBase() {
    imageFolder = "Images\\";
    giftCornerBGImage = imageFolder + "giftcornerbg.jpg";
    $(document).on('contextmenu', function (e) {
        return false;
    });
    window.oncontextmenu = disabled;
    window.ondrop = disabled;
    document.oncontextmenu = disabled;
    document.ondrop = disabled;
    window.onerror = noerror;
}

function isRightClick(event) {
    var rightclick;
    if (!event) var event = window.event;
    if (event.which) rightclick = (event.which == 3);
    else if (event.button) rightclick = (event.button == 2);
    return rightclick;
}

function AlertMessage(msg) {
    alert(msg);
}

function replaceHtml(el, html) {
    var oldEl = typeof (el) === "string" ? document.getElementById(el) : el;
    /*@cc_on // Pure innerHTML is slightly faster in IE
		oldEl.innerHTML = html;
		return oldEl;
	@*/
    var newEl = oldEl.cloneNode(false);
    newEl.innerHTML = html;
    oldEl.parentNode.replaceChild(newEl, oldEl);
    /* Since we just removed the old element from the DOM, return a reference
	to the new element, which can be used to restore variable references. */
    return newEl;
};

function donothing()
{ }

