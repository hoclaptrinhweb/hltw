<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsDetail.ascx.cs"
    Inherits="usercontrols_ucNewsDetail" %>
<div class="article">
    <h1 class="article-title">
        <asp:Literal ID="lbTitle" runat="server"></asp:Literal></h1>
    <div class="article-summary">
        <p>
            <asp:Literal ID="lbBrief" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="pBody">
        <asp:Literal ID="lbContent" runat="server"></asp:Literal>
    </div>
    <ul data-role="listview" data-inset="true" data-theme="d" data-divider-theme="e"
        data-count-theme="b">
        <li data-role="list-divider">Bài viết khác</li>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <li><a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                    <img src='<%# "http://www.hoclaptrinhweb.com/images/w80-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx" %>' />
                    <h2>
                        <%# Eval("Title")%></h2>
                    <p>
                        <%# Eval("Brief") %></p>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
