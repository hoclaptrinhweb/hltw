<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightFacebook.ascx.cs"
    Inherits="usercontrols_ucRightFacebook" %>
<%@ Register Src="ucMenuNewsType.ascx" TagName="ucMenuNewsType" TagPrefix="uc2" %>
<uc2:ucMenuNewsType ID="ucMenuNewsType1" runat="server" />
<style>
    .video, .video a {
        position: relative;
    }

    .icon_play {
        display: inline-block;
        position: absolute;
        right: 35px;
        top: 25px;
        bottom: 3px;
        background: url(http://st.f1.thethao.vnexpress.net/c/v2/images/graphics/bg_icon_play.png) no-repeat top left;
        height: 26px;
        width: 26px;
    }
</style>
<div class="box_outer">
    <div class="widget">
        <div class="tabbed_widget">
            <h3 class="widget_title bred">Video ngẫu nhiên</h3>
            <div class="wid_border"></div>
            <div class="tabbed_container">
                <div class="tabbed_content" style="display: block;">
                    <ul class="blog_posts_widget">
                        <asp:Repeater ID="rpDataVideoViewed" runat="server">
                            <ItemTemplate>
                                <li itemscope itemtype="http://schema.org/Article" class="blog_post video"><a href='<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>'>
                                    <img itemprop="image" src="<%# (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : CurrentPage.UrlRoot + "/images/video/w90-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") %>"
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
    <script type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
</div>
<div class="box_outer" id="adnet_widget_20114">
    <div class="widget">
        <h3 class="widget_title">Liên kết hữu ích</h3>
        <div class="wid_border">
        </div>
        <div class="like_box_footer">
            <div class="tagcloud tabbed_tag">
                Thiết bị <a href="http://d-olympic.vn/product/san-pham-khac/nha-thi-dau">nhà thi đấu</a> , 
                Tin tức <a href="http://xuatkhaulaodongvietnhat.com/tuyen-dung/51-thong-tin-tuyen-dung-xuat-khau-lao-dong-nhat-ban.html">tuyển dụng xuất khẩu lao động nhật bản</a> , 
                LINK VAO <a href="http://www.fun122.com/lm/aff/biiichfi_vi">FUN88</a> , 
                <a href="http://www.vuongquocgame.com/games/Sex_And_The_City">game sex</a> , 
                <a href="http://www.game24h.vn/">Game 24h</a> , 
                đọc <a href="http://thethao247.vn">Báo bóng đá</a> trên Thể thao 247 , 
                
                <%-- 17 tháng 10 năm 2014 --%>
                <a href="http://www.dangky88.com/">Link m88</a> , 
                <a href="http://www.cadotrenmang.info/">m88.com</a> , 
                <a href="http://cachvaom88.net" target="_blank" title="M88">M88</a>
            </div>
        </div>
    </div>
</div>
