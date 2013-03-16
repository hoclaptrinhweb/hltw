<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightFacebook.ascx.cs"
    Inherits="usercontrols_ucRightFacebook" %>
<%@ Register Src="ucTagCount.ascx" TagName="ucTagCount" TagPrefix="uc1" %>
<div class="box_outer">
    <div class="widget">
        <h3 class="widget_title">
            Từ khoá nổi bật</h3>
        <div class="wid_border">
        </div>
        <uc1:ucTagCount ID="ucTagCount1" runat="server" />
    </div>
</div>
<div class="box_outer">
    <div class="widget">
        <h3 class="widget_title">
            Facebook</h3>
        <div class="wid_border">
        </div>
        <div class="like_box_footer">
            <iframe src="//www.facebook.com/plugins/likebox.php?href=http%3A%2F%2Fwww.facebook.com%2Fhoclaptrinhweb&amp;width=283&amp;height=290&amp;colorscheme=light&amp;show_faces=true&amp;border_color=red&amp;stream=false&amp;header=true&amp;appId=367413556628956"
                scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 283px;
                height: 290px;" allowtransparency="true"></iframe>
        </div>
    </div>
</div>
<div id="right_ad360" class="box_outer ads360">
    <script type="text/javascript">
        var _ad360_id = 3099;
        var _ad360_w = 300;
        var _ad360_h = 600;
        var _ad360_pos = 0;
    </script>
    <script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
</div>
<div class="box_outer">
    <div class="widget">
        <h3 class="widget_title">
            Liên kết hữu ích</h3>
        <div class="wid_border">
        </div>
        <div class="like_box_footer">
            <div class="tagcloud tabbed_tag">
                <a href="http://hosgroup.com.vn/phan-mem/phan-mem-quan-ly-ban-hang" title="Phần mềm quản lý bán hàng phổ biến nhất">
                    phan mem quan ly ban hang</a>
                <br />
                <a href="http://www.phanmembanhang.com" title="Phần mềm bán hàng tối ưu nhất">phan mem
                    ban hang</a><br />
                <a href="http://hosgroup.com.vn/phan-mem/phan-mem-quan-ly-nhan-su" title="Phần mềm quản lý nhân sự chuyên nghiệp LotusPro">
                    phan mem quan ly nhan su</a>
                <br />
                Lap trinh <a href="http://maybanhang.net/phan-mem-quan-ly-ban-hang/">phan mem ban hang</a>
                tiet kiem va hieu qua
                <br />
                Tin tức mới nhất về <a href="http://www.tinmoi.vn/samsung-galaxy-s4-e218.html">Samsung
                    Galaxy S4</a>
            </div>
        </div>
    </div>
</div>
<div>
    <div id="adnet_widget_20114" style="display: none;">
    </div>
    <script type="text/javascript">        var is_load_adnet_lib = is_load_adnet_lib || 1; if (is_load_adnet_lib == 1) { is_load_adnet_lib = 2; if (typeof (jQuery) == 'undefined') { document.write(unescape("%3Cscript src='http://s0.adnet.vn/jquery.min.js' type='text/javascript'%3E%3C/script%3E")); } document.write(unescape("%3Cscript src='http://s0.adnet.vn/js/adnet34.js' type='text/javascript'%3E%3C/script%3E")); }</script>
    <script type="text/javascript" src="http://widget.adnet.vn/js/js.php?widget_id=20114"></script>
</div>
<div class="box_outer">
    <embed onmousedown="AdsvnRenderClick(912)" src="<%= CurrentPage.UrlRoot %>/images/ads/vietbando336x140.swf"
        flashvars="stringURL=http%3A%2F%2Fwww.vietbando.com" width="300px" pluginspage="http://www.macromedia.com/go/getflashplayer"
        type="application/x-shockwave-flash" allowscriptaccess="always" wmode="transparent"
        quality="high">
</div>
