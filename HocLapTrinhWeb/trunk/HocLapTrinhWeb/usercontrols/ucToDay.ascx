<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucToDay.ascx.cs" Inherits="usercontrols_ucToDay" %>
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
    </ul>
</div>
<div style="clear: both;">
</div>
<asp:Repeater ID="rpData" runat="server">
    <ItemTemplate>
        <div class="box_outer">
            <div class="cat_article" itemscope itemtype="http://schema.org/Article">
                <h2 class="cat_article_title">
                    <a itemprop="url" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'
                        title="<%# Eval("Title").ToString().Replace('"',' ') %>"><span itemprop="name">
                            <%# Eval("Title") %>
                        </span></a>
                </h2>
                <div class="cat_article_warap">
                    <div class="cat_article_img">
                        <div class="cat_img">
                            <a title="<%# Eval("Title").ToString().Replace('"',' ') %>" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                                <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" +  Eval("Title").ToString().Replace('"',' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + CurrentPage.UrlRoot + "/images/w93-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx\" />"%>
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
