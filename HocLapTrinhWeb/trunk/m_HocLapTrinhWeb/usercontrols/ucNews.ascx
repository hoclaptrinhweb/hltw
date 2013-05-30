<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNews.ascx.cs" Inherits="usercontrols_ucNews" %>
<ul data-role="listview" data-inset="true" data-split-theme="b">
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <li>
                <a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                    <img src='<%# "http://www.hoclaptrinhweb.com/images/w80-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx" %>' />
                <h2>
                    <%# Eval("Title")%></h2>
                <p>
                    <%# Eval("Brief") %></p>
            </a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
