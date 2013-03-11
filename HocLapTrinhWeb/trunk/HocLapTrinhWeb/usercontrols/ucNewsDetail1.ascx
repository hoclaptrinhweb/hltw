<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucNewsDetail1.ascx.cs"
    Inherits="usercontrols_ucNewsDetail1" %>
<%@ Register Src="ucLikeFB_Google.ascx" TagName="ucLikeFB_Google" TagPrefix="uc1" %>
<style type="text/css">
    .top-menu-wrap
    {
        width: 100%;
        float: left;
        padding-bottom: 5px;
        margin-bottom: 10px;
        border-bottom: 1px solid #E0E0E0;
        font-family: play;
        font-size: 13px;
    }
    .treeview li
    {
        float: left;
        margin-left: 10px;
    }
    .link-rss
    {
        float: right;
    }
</style>
<div class="top-menu-wrap" xmlns:v="http://rdf.data-vocabulary.org/#">
    <ul class="treeview" itemprop="breadcrumb">
        <li typeof="v:Breadcrumb"><a rel="v:url" property="v:title" href='<%= CurrentPage.UrlRoot %>'>
            Home</a></li>
        <asp:Literal ID="lrTreeView" runat="server"></asp:Literal>
    </ul>
    <asp:Literal ID="lrRss" runat="server" Text="&lt;a class=&quot;link-rss&quot; rel=&quot;nofollow&quot; href=&quot;{href}&quot;&gt;RSS&lt;img class=&quot;img-rss&quot; alt=&quot;{des}&quot; src=&quot;/Images/icon/rss.gif&quot;&gt;&lt;/a&gt;"></asp:Literal>
</div>
<div style="clear: both;">
</div>
<div class="box_outer">
    <div class="cat_article" itemscope itemtype="http://schema.org/Article">
        <h1 class="cat_article_title" itemprop="name">
            <a>
                <asp:Literal ID="lbTitle" runat="server"></asp:Literal></a>
        </h1>
        <div class="article_meta">
            <span class="meta_author">Nguồn: 
                <asp:HyperLink ID="lbRefAddress" runat="server" Target="_blank">[lbRefAddress]</asp:HyperLink></span> <span itemprop="dateModified"
                    class="meta_date">Posted date: <strong>
                        <asp:Literal ID="lbCreatedDate" runat="server"></asp:Literal></strong></span>
            <span class="meta_sap">|</span> <span itemprop="review" class="meta_comments">Xem :
                <a>
                    <asp:Literal ID="lbView" runat="server"></asp:Literal></a></span>
        </div>
        <h2 class="brief" itemprop="description">
            <asp:Literal ID="lbBrief" runat="server"></asp:Literal>
        </h2>
        <div id="article_content" class="single_article_content" itemprop="articleBody">
            <asp:Literal ID="lbContent" runat="server"></asp:Literal>
            <div class="clear">
            </div>
        </div>
        <div itemprop="keywords" class="keyword">
            <asp:Literal ID="lbKeyword" runat="server"></asp:Literal>
        </div>
        <div class="ratings" itemprop="aggregateRating" itemscope itemtype="http://schema.org/AggregateRating">
            <span itemprop="ratingValue">100</span> out of <span itemprop="bestRating">100</span>
            based on <span itemprop="ratingCount">
                <asp:Literal ID="lbRatingCount" runat="server"></asp:Literal></span> user ratings
        </div>
        <div class="single_share">
            <div style="padding-bottom: 7px">
                Nếu bạn thấy bài viết hữu ích, hãy nhấn +1 và các liên kết chia sẻ để website ngày
                càng phát triển hơn. Xin cám ơn bạn!</div>
            <uc1:ucLikeFB_Google ID="ucLikeFB_Google1" runat="server" />
        </div>
        <div class="dots">
        </div>
        <div class="articles_nav">
            <span class="prev_article"><a id="aPrev" runat="server" rel="prev"><span>‹</span> Tin cũ hơn</a></span> 
            <span class="next_article"><a id="aNext" runat="server" rel="next">Tin mới hơn <span>›</span></a></span>
        </div>
        <!--Ad360.vn-ad-2874-640-90-start -->
<script type="text/javascript">
    var _ad360_id = 2874;
    var _ad360_w = 640;
    var _ad360_h = 90;
    var _ad360_pos = 0;
</script>
<script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
<!-- Ad360.vn-ad-2874-640-90-ends -->

        <asp:Panel ID="pGuest" runat="server">
            <div class="single_share">
                <div id="_commentNotiBox">
                    <p class="notification attention">
                        Nếu là khách, bạn phải <a style="color: Blue;" href="<%= CurrentPage.UrlRoot %>/members/register.aspx">
                            đăng ký</a> tài khoản và kích hoạt tài khoản để bình luận được hiển thị ở đây.
                        <br />
                        Thông tin kích hoạt gửi đến mail của bạn. <a rel="nofollow" title="Đóng" class="close">
                        </a>
                    </p>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pUser" runat="server">
            <div class="single_share">
                <div class="mingid-comment">
                    <h2 class="mingid-head rs">
                        <span class="total_comment"></span>Bình luận</h2>
                    <div class="clear">
                    </div>
                    <div class="mingid-inputbox">
                        <div class="mingid-rowForm">
                            <table width="100%">
                                <tr>
                                    <td style="vertical-align: top;">
                                        Nội dung
                                    </td>
                                    <td>
                                        <textarea cols="5" rows="4" class="mingid-inputTxt" id="cmContent"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nhập mã
                                    </td>
                                    <td>
                                        <input type="text" maxlength="50" value="" class="mingid-inputTxt" style="width: 50%"
                                            id="cmCaptcha" />
                                        <img class="imgCaptcha" src='<%= CurrentPage.UrlRoot + "/handler/captcha.ashx" %>'
                                            alt="captcha hoclaptrinhweb.com" />
                                        <a rel="nofollow" onclick="NewCaptcha();">Mã mới</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: right;">
                                        <span id="Span3"></span><span class="mingid-btnBlue" id="Span4" onclick="Send()"><span>
                                            Bình luận</span></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    function Send() {
                        $("#result").text('');
                        $.ajax({
                            type: "POST",
                            url: "<%= CurrentPage.UrlRoot %>/webservice/commentnews.asmx/PostDataUser",
                            data: "{'content':'" + $('#cmContent').val() + "','newsid' : " + $("#<%= hdNewsID.ClientID %>").val() + ",'keyname' : '" + $('#cmCaptcha').val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d == "Gửi bình luận thành công !") {
                                    window.location.reload();
                                    NewCaptcha();
                                }
                                else {
                                    $("#result").text(msg.d);
                                }
                            },
                            error: function () { $("#result").text('Có lỗi xảy ra') }
                        });
                        return false;
                    };
                    function NewCaptcha() {
                        $('.imgCaptcha').attr('src', '<%= CurrentPage.UrlRoot %>/handler/captcha.ashx?guid=' + Math.random())
                    };
                </script>
            </div>
        </asp:Panel>
        <div class="_commentList">
            <asp:HiddenField ID="hdNewsID" runat="server" />
            <asp:Repeater ID="rpComment" runat="server">
                <ItemTemplate>
                    <div class="comment-item" itemprop="review" itemscope itemtype="http://schema.org/Review">
                        <div class="avatar">
                            <span><span><a>
                                <img alt='<%# Eval("username") %>' border="0" height="50" width="50" src="http://www.hoclaptrinhweb.com/upload/user/user.jpg"></a>
                            </span></span>
                        </div>
                        <p>
                            <span class="nonchange"><span><a itemprop="author">
                                <%# Eval("username") %></a></span></span> <span itemprop="datePublished" class="comment-time">
                                    <%# Eval("CreatedDate") %></span> <span itemprop="reviewBody" class="comment-content _contentComment">
                                        <%# Eval("Content") %>
                                    </span><span class="dialog"></span>
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="fb-root">
        </div>
        <script type="text/javascript">            
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=312319252170353";
                fjs.parentNode.insertBefore(js, fjs);
            } (document, 'script', 'facebook-jssdk'));</script>
        <div id="cmFacebook" runat="server" class="fb-comments" data-num-posts="5" data-width="609">
        </div>
    </div>
</div>
<div style="float: left; width: 49%;">
    <h4>
        Tin mới hơn</h4>
    <ul class="newmore">
        <asp:Repeater ID="rpDataNew" runat="server">
            <ItemTemplate>
                <li itemscope itemtype="http://schema.org/Event"><a itemprop="url" title='<%# Eval("Title") %>'
                    rel='<%# CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + Eval("NewsID") %>'
                    href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                    <span itemprop="name">
                        <%# Eval("Title") %></span> </a><a itemprop="location" class="none">
                            <%# Eval("NewsTypeName") %></a>
                    <meta itemprop="startDate" content='<%# ((DateTime)Eval("CreatedDate")).ToString("yyyy-MM-dd") %>'>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div style="float: right; width: 49%;">
    <h4>
        Tin cũ hơn</h4>
    <ul class="newmore">
        <asp:Repeater ID="rpDataOld" runat="server">
            <ItemTemplate>
                <li itemscope itemtype="http://schema.org/Event"><a itemprop="url" title='<%# Eval("Title") %>'
                    rel='<%# CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + Eval("NewsID") %>'
                    href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                    <span itemprop="name">
                        <%# Eval("Title") %></span> </a><a itemprop="location" class="none">
                            <%# Eval("NewsTypeName") %></a>
                    <meta itemprop="startDate" content='<%# ((DateTime)Eval("CreatedDate")).ToString("yyyy-MM-dd") %>'>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.newmore a').cluetip({
            width: '400px',
            showTitle: true,
            positionBy: 'topBottom',
            topOffset: 20,
            cluezIndex: 100
        });
    });
</script>
