<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucCopyRight.ascx.cs"
    Inherits="usercontrols_ucCopyRight" %>
<div class="bottom_bar">
    <div class="inner">
        <ul class="social_icons">
            <li class="twitter"><a target="_blank" href="https://twitter.com/hoclaptrinhweb">twitter</a></li>
            <li class="gplus"><a target="_blank" href="https://plus.google.com/s/110890830763375677721">
                google plus</a></li>
            <li class="youtube"><a target="_blank" href="http://www.youtube.com/hoclaptrinhweb">
                youtube</a></li>
            <li class="digg"><a rel="author" href="http://www.hoclaptrinhweb.com/about.html">about</a></li>
            <li>, <a class="lienket" href="http://blog.hmclip.vn" title="Chia sẻ là niềm vui"
                target="_blank">Chia sẻ là niềm vui</a> </li>
            <li>,<a class="lienket" target="_blank" href="http://www.studynet.vn" title="hoc tieng anh online">
                hoc tieng anh truc tuyen</a></li>
            <li>, <a class="lienket" href="http://infolinks.vn/quang-cao-google-adwords" title="quang cao google"
                target="_blank">quảng cáo google</a> </li>
            <li>,<a class="lienket" href="http://www.ewebvn.com" title="Học thiết kế web - Kiến thức lập trình web"
                target="_blank">Học thiết kế web</a> </li>
            <li>, <a class="lienket" href="http://raovat.yolo.vn" title="diễn đàn rao vặt" target="_blank">
                dien dan rao vat</a> </li>
            <li>,<a class="lienket" href="http://www.vietnambooking.com.vn" title="ve may bay"
                target="_blank">ve may bay</a> </li>
                <li><a class="lienket" href="http://www.goccay.vn" title="Dien Dan Hoc Tap" target="_blank">Diễn Đàn Học Tập</a></li>
        </ul>
        <p class="copyrights">
        </p>
    </div>
</div>
<div class="scrollTo_top">
    <a rel="nofollow" title="Scroll To Top">
        <img src="<%= CurrentPage.UrlRoot %>/images/up.png" alt="Scroll To Top" />
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
