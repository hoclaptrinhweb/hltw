<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoDetail.ascx.cs"
    Inherits="usercontrols_ucVideoDetail" %>
<%@ Register Src="ucLikeFB_Google.ascx" TagName="ucLikeFB_Google" TagPrefix="uc1" %>
<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/swfobject.js" type="text/javascript"></script>
<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/jwplayer.js" type="text/javascript"></script>
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
                <asp:HyperLink ID="lbRefAddress" runat="server" Target="_blank">[lbRefAddress]</asp:HyperLink></span>
            <span itemprop="dateModified" class="meta_date">Posted date: <strong>
                <asp:Literal ID="lbCreatedDate" runat="server"></asp:Literal></strong></span>
            <span class="meta_sap">|</span> <span itemprop="review" class="meta_comments">Xem :
                <a>
                    <asp:Literal ID="lbView" runat="server"></asp:Literal></a></span>
        </div>
        <h2 class="brief" itemprop="description">
            <asp:Literal ID="lbBrief" runat="server"></asp:Literal>
        </h2>
        <div id="article_content" class="single_article_content" itemprop="articleBody">
            <div id="jwplayer" style="position: relative; width: 100%; height: 391px;">
            </div>
            <asp:HiddenField ID="hdLink" runat="server" />
            <script type="text/javascript">
                jwplayer("jwplayer").setup({
                    'flashplayer': "<%= CurrentPage.UrlRoot %>/Code/jwp58l/player.swf",
                    'width': '100%',
                    'height': '391',
                    'allowfullscreen': 'true',
                    'allowscriptaccess': 'always',
                    'wmode': 'opaque',
                    'controlbar': 'over',
                    'dock': 'true',
                    'dock.position': 'left',
                    'mute': 'false',
                    'stretching': 'uniform',
                    'autostart': 'false',
                    'file': '<%= hdLink.Value %>',
                    'logo.file': '<%= CurrentPage.UrlRoot %>/Code/jwp58l/logo.png',
                    'logo.hide': 'false',
                    'logo.position': 'top-right',
                    'logo.link': 'http://www.hoclaptrinhweb.com',
                    'abouttext': 'Hoclaptrinhweb',
                    'aboutlink': 'http://www.hoclaptrinhweb.com',
                    'skin': '<%= CurrentPage.UrlRoot %>/Code/jwp58l/NewTubeDark.zip'
                });
            </script>
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
            <span class="prev_article"><a id="aPrev" visible="false" runat="server" rel="prev"><span>
                ‹</span> Video cũ hơn</a></span> <span class="next_article"><a id="aNext" visible="false"
                    runat="server" rel="next">Video mới hơn <span>›</span></a></span>
        </div>
        <div style="margin-left: -15px;">
            <script type="text/javascript">
                var _ad360_id = 2874;
                var _ad360_w = 640;
                var _ad360_h = 90;
                var _ad360_pos = 0;
            </script>
            <script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
        </div>
    </div>
</div>
<div class="box_inner" style="margin-top: 10px; margin-bottom: 10px;">
    <div class="news_box">
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
<div class="box-content">
    <h4 class="section-title">
        Video mới hơn</h4>
    <ul class="view c-view clear">
        <asp:Repeater ID="rpDataNew" runat="server">
            <ItemTemplate>
                <li>
                    <div class="in2">
                        <div class="thumb c-thumb">
                            <div class="in2">
                                <a class="thumb-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                    <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : CurrentPage.UrlRoot + "/images/video/w180-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") + "\" />"%>
                                </a>
                                <div class="ticker">
                                    <span title="Thời lượng">01:30</span>
                                </div>
                            </div>
                        </div>
                        <div class="title">
                            <div class="in2">
                                <a class="title-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                    <%# Eval("Title") %></a>
                            </div>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div class="box-content">
    <h4 class="section-title">
        Video cũ hơn</h4>
    <ul class="view c-view clear">
        <asp:Repeater ID="rpDataOld" runat="server">
            <ItemTemplate>
                <li>
                    <div class="in2">
                        <div class="thumb c-thumb">
                            <div class="in2">
                                <a class="thumb-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                    <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : CurrentPage.UrlRoot + "/images/video/w180-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") + "\" />"%>
                                </a>
                                <div class="ticker">
                                    <span title="Thời lượng">01:30</span>
                                </div>
                            </div>
                        </div>
                        <div class="title">
                            <div class="in2">
                                <a class="title-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                    <%# Eval("Title") %></a>
                            </div>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
