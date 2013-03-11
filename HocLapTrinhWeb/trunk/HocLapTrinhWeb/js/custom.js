jQuery(document).ready(function ($) {

    $('.scrollTo_top').hide();

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollTo_top').fadeIn(300);
        }
        else {
            $('.scrollTo_top').fadeOut(300);
        }
    });

    $('.scrollTo_top a').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 500);
        return false;
    });

});
var nhtml = "0";
function setCookie(cName, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var cValue = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString())+";path=/";
    document.cookie = cName + "=" + cValue;
}
function getCookie(cName) {
    var i, x, y, arrcookies = document.cookie.split(";");
    for (i = 0; i < arrcookies.length; i++) {
        x = arrcookies[i].substr(0, arrcookies[i].indexOf("="));
        y = arrcookies[i].substr(arrcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == cName) {
            return unescape(y);
        }
    }
    return undefined;
}
