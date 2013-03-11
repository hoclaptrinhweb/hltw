<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucindex.ascx.cs"
    Inherits="usercontrols_ucindex" %>
<%@ Register Src="ucRightLink.ascx" TagName="ucRightLink" TagPrefix="uc1" %>
<%@ Register Src="ucRightFacebook.ascx" TagName="ucRightFacebook" TagPrefix="uc2" %>
<%@ Register Src="ucGoogleCustomSearch.ascx" TagName="ucGoogleCustomSearch" TagPrefix="uc3" %>
<uc3:ucGoogleCustomSearch ID="ucGoogleCustomSearch1" runat="server" />
<div id="container">
    <div id="col_left">
        <asp:Repeater ID="rpNewType" runat="server" OnItemDataBound="rpNewType_ItemDataBound">
            <ItemTemplate>
                <div class="top-menu-wrap">
                    <ul class="treeview">
                        <li><a href='<%# CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw" + Eval("NewsTypeID") + ".aspx" %>'>
                            <asp:Literal ID="lrTitle" runat="server" Text='<%# Eval("newstypename") %>'></asp:Literal>
                        </a></li>
                    </ul>
                </div>
                <ul class="ulnews">
                    <asp:Repeater ID="rpData" runat="server">
                        <ItemTemplate>
                            <li class='<%# ((RepeaterItem)Container).ItemIndex != 0 ? "right" : "left" %>'>
                                <div class="newsitem" itemscope itemtype="http://schema.org/Article">
                                    <a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'
                                        title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                        <%# ((RepeaterItem)Container).ItemIndex != 0 ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + CurrentPage.UrlRoot + "/images/" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx\" />"%>
                                    </a><a itemprop="url" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'
                                        title="<%# Eval("Title") %>">
                                        <h2 itemprop="name" class="title">
                                            <%# Eval("Title") %>
                                        </h2>
                                    </a>
                                    <p itemprop="description">
                                        <%# ((RepeaterItem)Container).ItemIndex != 0 ? "" : Eval("Brief") %>
                                    </p>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <br class="clearfix" />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="col_right">
        <uc1:ucRightLink ID="ucRightLink1" runat="server" />
        <uc2:ucRightFacebook ID="ucRightFacebook1" runat="server" />
    </div>
</div>
