<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucListNews.ascx.cs" Inherits="usercontrols_ucListNews" %>
<asp:Repeater ID="rpData" runat="server">
    <ItemTemplate>
        <li itemscope itemtype="http://schema.org/Event">
            <a itemprop="url" title='<%# Eval("Title") %>' rel='<%# UrlRoot + "/Handler/tooltip.ashx?id=" + Eval("NewsID") %>'
            href='<%# UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
            <span itemprop="name">
                <%# Global.GetSubContent(Eval("Title").ToString(),40) %></span> </a>
            <meta itemprop="startDate" content='<%# ((DateTime)Eval("CreatedDate")).ToString("yyyy-MM-dd") %>'>
        </li>
    </ItemTemplate>
</asp:Repeater>
