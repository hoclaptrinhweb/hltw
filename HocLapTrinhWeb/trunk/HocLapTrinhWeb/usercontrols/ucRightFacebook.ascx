<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightFacebook.ascx.cs"
    Inherits="usercontrols_ucRightFacebook" %>
<%@ Register Src="ucMenuNewsType.ascx" TagName="ucMenuNewsType" TagPrefix="uc2" %>
<div id="divTag" class="box_outer">
    <div class="widget">
        <h3 class="widget_title">Chuyên mục</h3>
        <div class="wid_border">
        </div>
        <div class="">
            <uc2:ucMenuNewsType ID="ucMenuNewsType1" runat="server" />
        </div>
    </div>
</div>
<style>
    .video, .video a
    {
        position: relative;
    }

    .icon_play
    {
        display: inline-block;
        position: absolute;
        right: 70px;
        top: 55px;
        bottom: 3px;
        background: url(http://st.f1.thethao.vnexpress.net/c/v2/images/graphics/bg_icon_play.png) no-repeat top left;
        height: 26px;
        width: 26px;
    }
</style>
<div class="box_outer">
    <div class="widget">
        <div class="tabbed_widget">
            <h3 class="widget_title">Video ngẫu nhiên</h3>
            <div class="wid_border"></div>
            <div class="tabbed_container">
                <div class="tabbed_content" style="display: block;">
                    <ul class="blog_posts_widget">
                        <asp:Repeater ID="rpDataVideoViewed" runat="server">
                            <ItemTemplate>
                                <li itemscope itemtype="http://schema.org/Article" class="blog_post video"><a href='<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>'>
                                    <img itemprop="image" src="<%# (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : CurrentPage.UrlRoot + "/images/video/w100-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") %>"
                                        alt='<%# Eval("Title") %>' class="alignleft">
                                    <span class="icon_play">&nbsp;</span></a>
                                    <p>
                                        <a itemprop="url" itemprop="name" href='<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>'>
                                            <%# Eval("Title") %></a><a class="cat" href='<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/hltw"  + Eval("VideoTypeID") +  ".aspx" %>'>
                                                <%# Eval("VideoTypeName") %></a>
                                    </p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabbed_content" style="display: none;">
                </div>
            </div>
        </div>
    </div>
</div>
<div id="right_ad360" class="box_outer ads360">
    <script type="text/javascript">
        var _ad360_id = 4176;
        var _ad360_w = 300;
        var _ad360_h = 250;
        var _ad360_pos = 0;
    </script>
    <script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
</div>
<div class="box_outer" id="adnet_widget_20114">
    <div class="widget">
        <h3 class="widget_title">Liên kết hữu ích</h3>
        <div class="wid_border">
        </div>
        <div class="like_box_footer">
            <div class="tagcloud tabbed_tag">
                <a href="http://sany.vn/may-photocopy-ricoh-181/">may photocopy ricoh</a> - Sany.vn , 
                <a href="http://ahaha.vn/san-pham/thuc-pham-chuc-nang-sua-ong-chua-swanson-royal-jelly-1000mg-san-pham-chinh-hang-nk-hoa-ky/944/13">sữa ong chúa</a> tại Ahaha.vn , 
                Lap trinh <a target="_blank" href="http://maybanhang.net/phan-mem-quan-ly-ban-hang/">phan mem ban hang</a> tiet kiem va hieu qua , 
                <a href="http://www.bhiu.edu.vn/hop-tac-quoc-te/hp-tac-quc-t/doi-tac.html">Lien ket dao tao quoc te</a> , 
                Apple <a href="http://www.ngocmobile.vn/product/49/iphone-4-cu-16gb-trang-quoc-te-moi-98-99-ngocmobile-vn.html">iphone 4 cũ</a>
                Chia sẽ kinh nghiệm <a href="http://www.yeutretho.com/handmade-c300/">handmade</a> tại yeutretho.com , 
                <a href="http://insky.vn/">Thiết kế logo</a> chuyên nghiệp , 
                <a href="http://densuoiphongtam.com/">Den suoi nha tam</a> giá rẻ tại densuoiphongtam.com , 
                <a href="http://www.game24h.vn/">Game 24h</a> , 
                đọc <a href="http://thethao247.vn">Báo bóng đá</a> trên Thể thao 247 , 
                <a href="http://sany.vn/man-chieu-42/">Man chieu</a> chính hãng , 
                <a href="http://www.capbalo.com/cat/san-pham/balo-laptop/200">Balo laptop</a> giá rẻ , 
                <a href="http://viettelidc.com.vn/thue-vps">Cloud VPS</a> là gì? , 
                Tải <a href="http://www.1vs.vn/SanPham/1CQuanLyVanBan/">phần mềm quản lý văn bản</a> miễn phí , 
                <a href="http://www.vuongquocgame.com/games/Sex_And_The_City">game sex</a> , 
                <a href="http://www.thamtutu.com.vn">Văn phòng thám tử </a>VDT , 
                LINK VAO <a href="http://www.fun122.com/lm/aff/biiichfi_vi">FUN88</a> , 
                <a href="http://www.vanphongluatsu.com.vn/thue-luat-su-bao-chua-va-co-che-khi-tham-gia-bao-chua-trong-vu-an-hinh-su/">Thuê luật sư bào chữa</a> tại công ty luật Dragon , 
                công ty <a href="http://htsb.com.vn">thiet ke web</a> uy tín , 
                <a href="http://seatimes.com.vn/doanh-nhan">Doanh nhân</a> thành đạt , 
                <a href="http://linkm88.blogspot.com" target="_blank">M88</a> , 
                <a href="http://cachvaom88.net" target="_blank" title="M88">M88</a> , 
                <a href="http://hosgroup.com.vn/phan-mem/phan-mem-quan-ly-ban-hang" title="Phần mềm bán hàng">Phần mềm bán hàng</a> , 
                <a href="http://hosgroup.com.vn/phan-mem/phan-mem-quan-ly-ban-hang" title="Phần mềm quản lý bán hàng">Phần mềm quản lý bán hàng</a>
            </div>
        </div>
    </div>
</div>
