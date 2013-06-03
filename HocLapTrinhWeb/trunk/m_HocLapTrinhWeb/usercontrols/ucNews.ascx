<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNews.ascx.cs" Inherits="usercontrols_ucNews" %>
<asp:Literal ID="lbProductType" runat="server"></asp:Literal>
<ul data-role="listview" data-inset="true" data-split-theme="d">
    <li data-role="list-divider">Bài viết</li>
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
<div runat="server" id="divPaging" data-role="controlgroup" data-type="horizontal"
    style="text-align: center;">
</div>
