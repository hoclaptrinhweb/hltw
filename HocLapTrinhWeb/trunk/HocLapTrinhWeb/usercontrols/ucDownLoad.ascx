<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDownLoad.ascx.cs" Inherits="usercontrols_ucDownLoad" %>
<script src="../js/jquery.js" type="text/javascript"></script>
<asp:HiddenField ID="hdUser" runat="server" />
<asp:HiddenField ID="hdLink" runat="server" />
<asp:HiddenField ID="hdiType" runat="server" />
<asp:HiddenField ID="hdAccess" runat="server" />
<pre></pre>
<div id='fb-root'>
</div>
<asp:Button ID="Button1" runat="server" Text="Gửi FB" OnClick="Button1_Click" />
<input type="button" value="Kiểm tra đăng nhập" onclick="CheckStatus()" />
<script type="text/javascript">
    var iType = $();
    var graphApiInitialized = false;
    var flagFirtLoad = false;
    var valueLink = document.getElementById("<%= hdLink.ClientID %>");
    var status = '';
    var accessToken = '';
    var checkconnect = false;
    var imgPro = '';

    window.fbAsyncInit = function ()
    {
        FB.init({
            appId: '312319252170353',
            status: true,
            cookie: true,
            xfbml: true,
            oauth: true
        });
        FB.getLoginStatus(function (response)
        {
            debugger;
            checkconnect = true;
            if (response.status === 'connected') {
                debugger;
                $("#<%= hdUser.ClientID %>").val(response.authResponse.userID);
                accessToken = response.authResponse.accessToken;
                status = 'connected';
                $("#<%= hdAccess.ClientID %>").val(accessToken);
            } else if (response.status === 'not_authorized')
            {
                status = 'not_authorized';
            } else
            {
                status = '';
            };
        });
        graphApiInitialized = true;
    };

    (function ()
    {
        var e = document.createElement('script');
        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
        e.async = true;
        document.getElementById('fb-root').appendChild(e);
    } ());


    function CheckPermissions(access, userid)
    {
        var accessToken = "access_token=" + access;
        var script1 = document.createElement('script');
        script1.src = "https://graph.facebook.com/me/permissions?" + accessToken + "&callback=CallbackCheckPermissions";
        document.body.appendChild(script1);
        return false;
    }

    function CallbackCheckPermissions(re)
    {
        if (re.data[0].publish_stream == 1 && re.data[0].photo_upload && re.data[0].user_photos)
        {
            $("#hlFB").attr("href", 'http://facebook.com/' + $("#<%= hdUser.ClientID %>").val());
            $("#imgFB").attr("src", 'http://graph.facebook.com/' + $("#<%= hdUser.ClientID %>").val() + '/picture');
            ShowAdd();
        }
        else
        {
            alert("Truy cập vào tài khoản Facebook của bạn đã bị từ chối. Hãy đảm bảo rằng bạn đã cho ứng dụng Facebook của chúng tôi những quyền cần thiết.");
        }
    }

    function CheckStatus()
    {
        if (graphApiInitialized == true && checkconnect == true)
        {
            if (status == 'connected')
            {
                CheckPermissions(accessToken, $("#<%= hdUser.ClientID %>").val());
            } else
            {
                if (graphApiInitialized == false)
                {
                    alert("Không thể kết nối đến App Facebook");
                } else
                {
                    FB.login(OnConfirmCallbackFunc2, { scope: 'user_photos,offline_access,publish_stream,email' });
                };
            };
        } else
        {
            alert("Không thể kết nối đến App Facebook");
        };
        return false;
    };

    function OnConfirmCallbackFunc2(response)
    {
        if (response.authResponse != null)
        {
            $("#<%= hdUser.ClientID %>").val(response.authResponse.userID);
            $("#<%= hdAccess.ClientID %>").val(response.authResponse.accessToken);
            CheckPermissions(response.authResponse.accessToken, response.authResponse.userID);
        } else
        {
            alert("Truy cập vào tài khoản Facebook của bạn đã bị từ chối. Hãy đảm bảo rằng bạn đã cho ứng dụng Facebook của chúng tôi những quyền cần thiết.");
        };
    }; ;

    function ShowAdd()
    {
        $("#imgPublic").css("background", "url(" + $("#imgShare").attr("src") + ")");
        $(".Uploadfacebook").show();
        $("#Uploadfacebook").show();
        var winwidth = document.all ? document.body.clientWidth : window.innerWidth;
        var winheight = document.all ? document.documentElement.clientHeight : window.innerHeight;
        $(".FForm_wrap").css({ left: (winwidth - $('.FForm_wrap').width() - 10) / 2 + 'px',
            top: (winheight - $('.FForm_wrap').height()) / 2 + 'px'
        });
    }

    function HideAdd()
    {
        $(".Uploadfacebook").hide();
        $("#Uploadfacebook").hide();
        $("#txtContent").val("Đây là nơi mình ở !!!!!");
    }

</script>
