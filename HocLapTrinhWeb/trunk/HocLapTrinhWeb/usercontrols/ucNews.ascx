<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucNews.ascx.cs"
    Inherits="usercontrols_ucNews" %>
<style type="text/css">
    .top-menu-wrap
    {
        width: 100%;
        float: left;
        margin-bottom: 10px;
        margin-top: 10px;
        border-bottom: 1px solid #E0E0E0;
        font-family: play;
        font-size: 13px;
        background: #e0e0e0;
    }
    .treeview li
    {
        float: left;
    }
    .treeview li a
    {
        font-size: 12px;
        display: inline-block;
        font-weight: bold;
        position: relative;
        padding: 5px 18px 5px 8px;
        display: inline-block;
        background: url('<%= CurrentPage.UrlRoot %>/images/chevron1.png') no-repeat right center;
    }
    .link-rss
    {
        float: right;
    }
    a.home {
        background: url('<%= CurrentPage.UrlRoot %>/images/bg-timo.png') no-repeat right center !important;
        color: white;
    }
</style>
<div class="top-menu-wrap" xmlns:v="http://rdf.data-vocabulary.org/#">
    <ul class="treeview" itemprop="breadcrumb">
        <li typeof="v:Breadcrumb"><a class="home" rel="v:url" property="v:title" href='<%= CurrentPage.UrlRoot %>'>
            Trang chủ</a></li>
        <asp:Literal ID="lrTreeView" runat="server"></asp:Literal>
    </ul>
    <%--<asp:Literal ID="lrRss" runat="server" Text="&lt;a class=&quot;link-rss&quot; rel=&quot;nofollow&quot; href=&quot;{href}&quot;&gt;RSS&lt;img class=&quot;img-rss&quot; alt=&quot;{des}&quot; src=&quot;<%= CurrentPage.UrlRoot >/Images/icon/rss.gif&quot;&gt;&lt;/a&gt;"></asp:Literal>--%>
</div>
<div style="clear: both;">
</div>
<asp:Repeater ID="rpData" runat="server">
    <ItemTemplate>
        <div class="box_outer">
            <div class="cat_article" itemscope itemtype="http://schema.org/Article">
                <h2 class="cat_article_title">
                    <a itemprop="url" rel='<%# CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + Eval("NewsID") %>'
                        href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'
                        title="<%# Eval("Title").ToString().Replace('"',' ') %>"><span itemprop="name">
                            <%# Eval("Title") %>
                        </span></a>
                </h2>
                <div class="cat_article_warap none">
                    <div class="cat_article_img">
                        <div class="cat_img">
                            <a title="<%# Eval("Title").ToString().Replace('"',' ') %>" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                                <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + CurrentPage.UrlRoot + "/images/w93-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx\" />"%>
                                <span class="ca_article_icon"></span></a>
                        </div>
                    </div>
                    <div class="cat_article_content">
                        <p itemprop="description">
                            <%# Eval("Brief") %>
                        </p>
                        <a class="article_read_more" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                            Chi tiết <span>›</span></a>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div class="clear">
</div>
<div class="paging">
    <ul class="pagination" runat="server" id="Paging">
    </ul>
</div>
<script type="text/javascript">
    jQuery(document).ready(function ($)
    {
        $('.cat_article_title a').cluetip({
            width: '400px',
            showTitle: true,
            positionBy: 'topBottom',
            topOffset: 20,
            cluezIndex: 100
        });
    });
</script>
