<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoType.ascx.cs" Inherits="usercontrols_ucVideoType" %>
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
        <li typeof="v:Breadcrumb"><a rel="v:url" property="v:title" href='<%= CurrentPage.UrlRoot + "/video/" %>'>
            Home</a></li>
        <asp:Literal ID="lrTreeView" runat="server"></asp:Literal>
    </ul>
    <%--<asp:Literal ID="lrRss" runat="server" Text="&lt;a class=&quot;link-rss&quot; rel=&quot;nofollow&quot; href=&quot;{href}&quot;&gt;RSS&lt;img class=&quot;img-rss&quot; alt=&quot;{des}&quot; src=&quot;<%= CurrentPage.UrlRoot >/Images/icon/rss.gif&quot;&gt;&lt;/a&gt;"></asp:Literal>--%>
</div>
<div style="clear: both;">
</div>
<div class="box-content">
<ul class="view c-view clear">
    <asp:Repeater ID="rpData" runat="server">
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
<div class="clear">
</div>
<div class="paging">
    <ul class="pagination" runat="server" id="Paging">
    </ul>
</div>
