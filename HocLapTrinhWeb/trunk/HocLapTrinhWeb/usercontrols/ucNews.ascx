<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucNews.ascx.cs"
    Inherits="usercontrols_ucNews" %>
<%@ Register Src="~/usercontrols/ucTreeView.ascx" TagPrefix="uc1" TagName="ucTreeView" %>
<div class="metro" xmlns:v="http://rdf.data-vocabulary.org/#">
    <uc1:ucTreeView runat="server" ID="ucTreeView" />
    <div class="zonetab">
        <span class="<%= "button " + (Type != "xem-nhieu" ? "danger" : "")  %>"><a href='<%= CurrentPage.UrlRoot + "/" + Title + "/hltw" + NewsTypeID +".aspx" + (PageIndex == 1 ? "" : "?trang=" + PageIndex) %>'>Tin mới</a></span>
        <span class="<%= "button " + (Type == "xem-nhieu" ? "danger" : "")  %>"><a href='<%= CurrentPage.UrlRoot + "/" + Title + "/xem-nhieu/hltw" + NewsTypeID +".aspx" + (PageIndex == 1 ? "" : "?trang=" + PageIndex) %>'>Xem nhiều</a></span>
    </div>
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
                <div class="cat_article_warap">
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
                        <a class="article_read_more" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>Chi tiết <span>›</span></a>
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

<%--<script type="text/javascript">
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
--%>