<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucCopyRight.ascx.cs"
    Inherits="usercontrols_ucCopyRight" %>
<div class="bottom_bar">
    <div class="inner">
        <ul class="social_icons">
            <%--<li>, <a class="lienket" href="http://raovat.yolo.vn" title="diễn đàn rao vặt" target="_blank">
                dien dan rao vat</a> </li>--%>
            <li>
                <!-- Histats.com  (div with counter) -->
                <div id="histats_counter" style="margin-top: 16px;"></div>
                <!-- Histats.com  START  (aync)-->
                <script type="text/javascript">var _Hasync = _Hasync || [];
                    _Hasync.push(['Histats.start', '1,2403491,4,128,112,33,00011111']);
                    _Hasync.push(['Histats.fasi', '1']);
                    _Hasync.push(['Histats.track_hits', '']);
                    (function () {
                        var hs = document.createElement('script'); hs.type = 'text/javascript'; hs.async = true;
                        hs.src = ('http://s10.histats.com/js15_as.js');
                        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(hs);
                    })();</script>
                <noscript><a href="http://www.histats.com" target="_blank">
                    <img src="http://sstatic1.histats.com/0.gif?2403491&101" alt="counter easy hit" border="0"></a></noscript>
                <!-- Histats.com  END  -->
            </li>
        </ul>
        <p class="copyrights">
        </p>
    </div>
</div>
<div class="scrollTo_top">
    <a href="#" rel="nofollow" title="Scroll To Top">
        <img src="<%= CurrentPage.UrlRoot %>/images/up.png" alt="Lên trên đầu" />
    </a>
</div>
<asp:Literal ID="lbAutoAdv" runat="server"></asp:Literal>
<script type="text/javascript">
    $(window).ready(function () {
        setTimeout("adhltw()", 500);
    });
    function adhltw() {
        if ($('.top_ad iframe').contents().find('a').length > 0 && $('#right_ad360 iframe').contents().find('a').length > 0) {
            $('.ads360 iframe').contents().find('a').attr('target', '_blank');
        } else {
            setTimeout("adhltw()", 500);
        }
    }
</script>
