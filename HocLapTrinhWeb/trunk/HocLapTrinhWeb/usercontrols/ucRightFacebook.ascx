<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightFacebook.ascx.cs"
    Inherits="usercontrols_ucRightFacebook" %>
<%@ Register Src="ucTagCount.ascx" TagName="ucTagCount" TagPrefix="uc1" %>
<%@ Register src="ucMenuNewsType.ascx" tagname="ucMenuNewsType" tagprefix="uc2" %>
<div class="box_outer">
    <div class="widget">
        <h3 class="widget_title">Từ khoá nổi bật</h3>
        <div class="wid_border">
        </div>
        <uc1:ucTagCount ID="ucTagCount1" runat="server" />
    </div>
</div>
<div id="divTag" class="box_outer">
    <div class="widget">
        <h3 class="widget_title">
            Chuyên mục</h3>
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
        position:relative;
    }
    .icon_play {
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
                <a target="_blank" href="http://thugianviet.info" title="Phim online, phim chất lương caot">Phim online, phim chất lương cao</a>
                <br />
                Tin tức mới nhất về <a target="_blank" href="http://www.tinmoi.vn/samsung-galaxy-s4-e218.html">Samsung Galaxy S4</a>
                <br />
                Lap trinh <a target="_blank" href="http://maybanhang.net/phan-mem-quan-ly-ban-hang/">phan mem ban hang</a> tiet kiem va hieu qua
                <br />
                LINK VAO <a href="http://www.cacuoc1.com">M88</a> NHÀ CÁI M88
                <br />
                <a href="http://www.bhiu.edu.vn/hop-tac-quoc-te/hp-tac-quc-t/doi-tac.html">Lien ket dao tao quoc te</a>
                <br />
                Apple <a href="http://www.ngocmobile.vn/product/49/iphone-4-cu-16gb-trang-quoc-te-moi-98-99-ngocmobile-vn.html">iphone 4 cũ</a>
                <br />
                Chia sẽ kinh nghiệm <a href="http://www.yeutretho.com/handmade-c300/">handmade</a> tại yeutretho.com
				<br />
				<a href="http://duhocvietnhat.com/du-hoc-nhat-ban/">Du hoc Nhat Ban</a> giá rẻ tại JellyFish Education
				<br/>
				<a href="http://dulich-singapore.vn">Du lich malaysia</a>
				<br/>
				<a href="http://bizzone.vn">Phần mềm quản lý nhân sự</a>
				<br/>
				<a href="http://superwatch.vn/cid-92/dong-ho-thoi-trang.html">Dong ho thoi trang</a> tại Superwatch.vn
				<br/>
				Tải <a href="http://kenhgame.vn/838/tai-ucweb.html">ucweb</a> cho điện thoại
				<br/>
				<a href="http://insky.vn/">Thiết kế logo</a> chuyên nghiệp
				<br/>
				Phukiendientu.vn chuyên <a href="http://phukiendientu.vn/sanpham/day-cap-day-chuyen-doi/cap-tin-hieu-vga-dvi">cap VGA</a> chính hãng, giá rẻ
            </div>
        </div>
    </div>
</div>
