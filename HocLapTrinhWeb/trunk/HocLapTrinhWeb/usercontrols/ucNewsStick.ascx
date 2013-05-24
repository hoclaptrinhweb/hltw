<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsStick.ascx.cs" Inherits="usercontrols_ucNewsStick" %>
<div class="typical_news">
    <div id="featured_news">
        <ul>
            <asp:Repeater ID="rpData" runat="server">
                <ItemTemplate>
                   <%# BindData((RepeaterItem)Container)%>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <ul class="typicalevents">
        <asp:Repeater ID="rpDataNewsRandom" runat="server">
            <ItemTemplate>
                <li itemscope itemtype="http://schema.org/Article"><a title='<%# Eval("Title").ToString().Replace("'","") %>'
                    href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw"  + Eval("NewsTypeID") +  ".aspx" %>'>
                    <img class="newsphoto_small" itemprop="image" src="<%# CurrentPage.UrlRoot + "/images/w80-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx" %>"
                        alt='<%# Eval("Title") %>' />
                    <h2>
                        <%# Eval("Title") %></h2>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clr">
    </div>
</div>
<style type="text/css">
    #featured_news
    {
        float: left;
        width: 380px;
        overflow: hidden;
        padding: 10px 5px 5px 10px;
    }
    #featured_news li.topnews
    {
        margin-top: 0;
        border-top: 0;
        line-height: 18pt;
        overflow: visible;
        padding: 0;
        background: none;
    }
    #featured_news li.topnews a img
    {
        width: 100%;
        float: none;
    }
    #featured_news li.topnews a h1
    {
        font-size: 12pt;
        color: #333;
        text-shadow: 1px 1px 0 #fff;
        margin: 8px 0 0;
        line-height: 16pt;
        max-height: none;
        padding: 0;
        background: none;
        font-weight: bold;
        white-space: normal;
        text-overflow: visible;
    }
    #featured_news li.topnews a p.chapeau
    {
        line-height: 12pt;
        margin: 0px 0 10px;
        padding: 5px 0 0 0;
        color: #555;
        max-height: 36pt;
        overflow: hidden;
    }
    #featured_news li
    {
        float: left;
        width: 100%;
        overflow: hidden;
        padding: 9px 0;
        background: #f0f0f0 url(http://static2.news.zing.vn/v3/images/bg_tinmoi.png) no-repeat left top;
    }
    #featured_news li h1
    {
        margin: 0px 0 5px 26px;
        overflow: hidden;
        background: url(http://static2.news.zing.vn/v3/images/icon_bullet.png) no-repeat -5px 0px;
        padding-left: 15px;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
        font-weight: normal;
    }
    #featured_news li h1 a
    {
        font-size: 10pt;
        color: #333;
    }
    
    
    .typicalevents
    {
        display: block;
        float: left;
        width: 245px;
        padding: 10px 5px 5px 5px;
    }
    .typicalevents li
    {
        border-bottom: 1px solid #eee;
        margin-bottom: 8px;
        display: block;
        width: 100%;
        overflow: hidden;
        line-height: 15pt;
    }
    .typicalevents li a
    {
        font-weight: bold;
        text-decoration: none !important;
        display: block;
    }
    .typicalevents li a h2
    {
        height: 58px;
        overflow: hidden;
        padding: 0px;
        font-weight: normal;
        font-size: 12px;
        line-height: normal;
    }
    .typicalevents li a:hover h2
    {
        text-decoration: underline;
    }
    .typicalevents li .newsphoto_small
    {
        width: 80px;
        height: auto;
        float: left;
        margin-right: 10px;
    }
    .typical_news
    {
        float: left;
        background: white;
        margin-bottom: 10px;
    }
</style>
