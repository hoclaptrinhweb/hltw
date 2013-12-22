<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoDetail.ascx.cs"
    Inherits="usercontrols_ucVideoDetail" %>
<%@ Register Src="ucLikeFB_Google.ascx" TagName="ucLikeFB_Google" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrols/ucTreeView.ascx" TagPrefix="uc1" TagName="ucTreeView" %>
<%@ Register Src="~/usercontrols/ucListVideo.ascx" TagPrefix="uc1" TagName="ucListVideo" %>


<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/swfobject.js" type="text/javascript"></script>
<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/jwplayer.js" type="text/javascript"></script>

<div class="metro" xmlns:v="http://rdf.data-vocabulary.org/#">
    <uc1:ucTreeView runat="server" ID="ucTreeView" />
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
                    'logo.file': '<%= CurrentPage.UrlRoot %>/images/icon_video.png',
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
            <span itemprop="ratingValue">100</span> out of <span itemprop="bestRating">100</span> based on <span itemprop="ratingCount">
                <asp:Literal ID="lbRatingCount" runat="server"></asp:Literal></span> user ratings
        </div>
        <div class="single_share">
            <div style="padding-bottom: 7px">
                Nếu bạn thấy bài viết hữu ích, hãy nhấn +1 và các liên kết chia sẻ để website ngày
                càng phát triển hơn. Xin cám ơn bạn!
            </div>
            <uc1:ucLikeFB_Google ID="ucLikeFB_Google1" runat="server" />
        </div>
        <div class="dots">
        </div>
        <div class="articles_nav">
            <span class="prev_article"><a id="aPrev" visible="false" runat="server" rel="prev"><span>‹</span> Video cũ hơn</a></span> <span class="next_article"><a id="aNext" visible="false"
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
        <div id="cmFacebook" runat="server" class="fb-comments" data-num-posts="5" data-width="609">
        </div>
    </div>
</div>

<div class="box_outer">
    <div class="news_box">
        <div class="news_box_heading bblue">
            <div class="nb_dots">
                <h2><a>Video mới hơn</a></h2>
            </div>
        </div>
        <div class="box-content">
            <ul class="view c-view clear">
                <uc1:ucListVideo runat="server" ID="ucListVideoNew" />
            </ul>
        </div>
    </div>
</div>

<div class="box_outer">
    <div class="news_box">
        <div class="news_box_heading bviolet">
            <div class="nb_dots">
                <h2><a>Video cũ hơn</a></h2>
            </div>
        </div>
        <div class="box-content">
            <ul class="view c-view clear">
                <uc1:ucListVideo runat="server" ID="ucListVideoOld" />
            </ul>
        </div>
    </div>
</div>
