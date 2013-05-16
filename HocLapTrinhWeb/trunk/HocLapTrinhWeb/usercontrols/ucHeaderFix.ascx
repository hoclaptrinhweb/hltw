<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeaderFix.ascx.cs" Inherits="usercontrols_ucHeaderFix" %>
<%@ Register Src="ucAdmin.ascx" TagName="ucAdmin" TagPrefix="uc1" %>
<%@ Register Src="ucGoogleCustomSearch.ascx" TagName="ucGoogleCustomSearch" TagPrefix="uc2" %>
<div class="fixed">
    <div id="top" class="top_bar">
        <div class="inner">
            <ul class="top_nav">
                <li><a href='<%= CurrentPage.UrlRoot %>'>Hoclaptrinhweb.com</a>
                    <asp:Literal ID="lbProductType" runat="server"></asp:Literal>
                    <ul class="sub-menu" style="display: none;">
                        <li><a href="http://www.hoclaptrinhweb.com/tin-cong-nghe/hltw20.aspx">Tin công nghệ</a></li><li>
                            <a href="http://www.hoclaptrinhweb.com/bao-mat-website/hltw11.aspx">Bảo mật website</a></li><li>
                                <a href="http://www.hoclaptrinhweb.com/game/hltw35.aspx">Game</a></li><li><a href="http://www.hoclaptrinhweb.com/seo/hltw12.aspx">
                                    SEO</a></li><li><a href="http://www.hoclaptrinhweb.com/asp-net/hltw4.aspx">Asp.net</a></li><li>
                                        <a href="http://www.hoclaptrinhweb.com/html5-canvas/hltw10.aspx">HTML5 - CANVAS</a></li><li>
                                            <a href="http://www.hoclaptrinhweb.com/php/hltw6.aspx">PHP</a><ul class="sub-menu"
                                                style="display: none;">
                                                <li><a href="http://www.hoclaptrinhweb.com/can-ban/hltw7.aspx">CĂN BẢN</a></li><li><a
                                                    href="http://www.hoclaptrinhweb.com/nang-cao/hltw8.aspx">NÂNG CAO</a></li></ul>
                                        </li>
                        <li><a href="http://www.hoclaptrinhweb.com/css/hltw9.aspx">CSS</a></li><li><a href="http://www.hoclaptrinhweb.com/wordpress/hltw16.aspx">
                            wordpress</a><ul class="sub-menu" style="display: none;">
                                <li><a href="http://www.hoclaptrinhweb.com/ma-nguon-mo/hltw15.aspx">Mã nguồn mở</a><ul
                                    class="sub-menu">
                                    <li><a href="http://www.hoclaptrinhweb.com/joomla/hltw17.aspx">joomla</a></li></ul>
                                </li>
                            </ul>
                        </li>
                        <li><a href="http://www.hoclaptrinhweb.com/javascript-jquery-ajax/hltw13.aspx">Javascript
                            - Jquery - Ajax</a><ul class="sub-menu" style="display: none;">
                                <li><a href="http://www.hoclaptrinhweb.com/ajax/hltw30.aspx">Ajax</a></li><li><a
                                    href="http://www.hoclaptrinhweb.com/jquery/hltw31.aspx">Jquery</a></li><li><a href="http://www.hoclaptrinhweb.com/javascript/hltw32.aspx">
                                        Javascript</a></li></ul>
                        </li>
                        <li><a href="http://www.hoclaptrinhweb.com/lap-trinh-c-c/hltw14.aspx">Lập trình C -
                            C++</a></li><li><a href="http://www.hoclaptrinhweb.com/phat-trien-web/hltw18.aspx">Phát
                                triển web</a></li><li><a href="http://www.hoclaptrinhweb.com/do-hoa/hltw19.aspx">Đồ
                                    họa</a></li><li><a href="http://www.hoclaptrinhweb.com/thu-thuat-meo-vat/hltw21.aspx">
                                        Thủ thuật mẹo vặt</a></li><li><a href="http://www.hoclaptrinhweb.com/ngon-ngu-lap-trinh/hltw23.aspx">
                                            Ngôn ngữ lập trình</a><ul class="sub-menu" style="display: none;">
                                                <li><a href="http://www.hoclaptrinhweb.com/c/hltw24.aspx">C#</a></li><li><a href="http://www.hoclaptrinhweb.com/net-framework/hltw25.aspx">
                                                    .Net Framework </a></li>
                                                <li><a href="http://www.hoclaptrinhweb.com/ado-net/hltw26.aspx">ADO.NET</a></li><li>
                                                    <a href="http://www.hoclaptrinhweb.com/linq/hltw27.aspx">LINQ</a></li><li><a href="http://www.hoclaptrinhweb.com/wcf-web-service/hltw28.aspx">
                                                        WCF / Web Service </a></li>
                                                <li><a href="http://www.hoclaptrinhweb.com/wpf/hltw29.aspx">WPF</a></li><li><a href="http://www.hoclaptrinhweb.com/java/hltw42.aspx">
                                                    Java</a></li></ul>
                                        </li>
                        <li><a href="http://www.hoclaptrinhweb.com/phan-mem-tien-ich-hay/hltw22.aspx">Phần mềm
                            - tiện ích hay</a></li><li><a href="http://www.hoclaptrinhweb.com/html-xml/hltw33.aspx">
                                HTML - XML</a></li><li><a href="http://www.hoclaptrinhweb.com/asp-net-mvc/hltw34.aspx">
                                    Asp.net MVC</a></li><li><a href="http://www.hoclaptrinhweb.com/window-form/hltw36.aspx">
                                        WINDOW FORM</a></li><li><a href="http://www.hoclaptrinhweb.com/database/hltw37.aspx">
                                            Database</a><ul class="sub-menu">
                                                <li><a href="http://www.hoclaptrinhweb.com/sql-server/hltw38.aspx">Sql Server</a></li><li>
                                                    <a href="http://www.hoclaptrinhweb.com/oracle/hltw41.aspx">Oracle</a></li></ul>
                                        </li>
                        <li><a href="http://www.hoclaptrinhweb.com/lap-trinh-di-dong/hltw39.aspx">Lập trình
                            di động</a><ul class="sub-menu">
                                <li><a href="http://www.hoclaptrinhweb.com/android/hltw40.aspx">Android</a></li></ul>
                        </li>
                    </ul>
                </li>
                <li><a href='<%= CurrentPage.UrlRoot + "/today.aspx" %>'>Bài viết mới</a></li>
                <li>
                    <uc2:ucGoogleCustomSearch ID="ucGoogleCustomSearch1" runat="server" />
                </li>
            </ul>
            <div class="search_box">
                <uc1:ucAdmin ID="ucAdmin2" runat="server" />
            </div>
        </div>
    </div>
</div>
<style>
    #container
    {
        overflow: hidden;
        width: 10px;
        height: 12px;
        position: absolute;
        filter: alpha(opacity=0);
        -moz-opacity: 0.0;
        -khtml-opacity: 0.0;
        opacity: 0.0;
    }
</style>
<div id='fb-root'>
</div>
<div id="icontainer">
    <iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Ffacebook.com%2Fhoclaptrinhweb&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;font=tahoma&amp;colorscheme=light&amp;height=80"
        scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 50px;
        height: 23px;" allowtransparency="true" id="fbframe" name="fbframe"></iframe>
</div>
<script type="text/javascript">
    var graphApiInitialized = false;
    var status = '';
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
            if (response.status === 'connected')
            {
                status = 'connected';
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
        e.src = 'http://connect.facebook.net/en_US/all.js';
        e.async = true;
        document.getElementById('fb-root').appendChild(e);
    } ());

    var interval;
    $(function ()
    {
        interval = setInterval("updateActiveElement();", 50);
    });

    function updateActiveElement()
    {
        if ($(document.activeElement).attr('id') == "fbframe")
        {
            clearInterval(interval);
            iflag = 1;
        }
    };

    var iflag = 0;
    var icontainer = document.getElementById('icontainer');
    var standardbody = (document.compatMode == "CSS1Compat") ? document.documentElement : document.body
    function mouseFollower(e)
    {
        if (window.event)
        {
            icontainer.style.top = (window.event.y - 5) + standardbody.scrollTop + 'px';
            icontainer.style.left = (window.event.x - 5) + standardbody.scrollLeft + 'px';
        }
        else
        {
            icontainer.style.top = (e.pageY - 5) + 'px';
            icontainer.style.left = (e.pageX - 5) + 'px';
        }
    };

    document.onmousemove = function (e)
    {
        if (iflag == 0 && graphApiInitialized == true && status != '')
        {
            mouseFollower(e);
        }
    };
</script>
