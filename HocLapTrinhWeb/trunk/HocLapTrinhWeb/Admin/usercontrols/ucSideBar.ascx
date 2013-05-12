<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSideBar.ascx.cs" Inherits="administrator_usercontrols_ucSideBar" %>
<div id="sidebar">
    <span class="ul-header">
        <asp:HyperLink ID="hpOption" runat="server" Text="Chức năng"></asp:HyperLink></span>
    <asp:Panel ID="pnOption" runat="server">
        <ul id="main_menu">
            <li class="<%= SetClass("admin/View.aspx?action=new","Admin/View.aspx?action=newsdetail","Admin/View.aspx?action=updatenews","Admin/View.aspx?action=newtype")  %>">
                <asp:HyperLink ID="hpNews" class="icn_news" runat="server" NavigateUrl="~/admin/View.aspx?action=new"
                    Text="Quản lý tin tức"></asp:HyperLink>
                <ul style="display: block" class="<%= "sub " + IsShow("admin/View.aspx?action=new","Admin/View.aspx?action=updatenews","Admin/View.aspx?action=newtype") %>">
                    <li class="<%= SetClass("Admin/View.aspx?action=newtype")  %>">
                        <asp:HyperLink ID="HyperLink2" class="icn_newstype" runat="server" NavigateUrl="~/Admin/View.aspx?action=newtype"
                            Text="Loại Tin"></asp:HyperLink></li>
                    <li class="<%= SetClass("Admin/View.aspx?action=tag")  %>">
                        <asp:HyperLink ID="HyperLink9" class="icn_tags" runat="server" NavigateUrl="~/Admin/View.aspx?action=tag"
                            Text="Tag"></asp:HyperLink></li>
                    <li class="<%= SetClass("Admin/View.aspx?action=updatenews")  %>">
                        <asp:HyperLink ID="HyperLink10" class="icn_updatenews" runat="server" NavigateUrl="~/Admin/View.aspx?action=updatenews"
                            Text="Lấy tin tự động"></asp:HyperLink></li>
                </ul>
            </li>
            <li class="<%= SetClass("Admin/View.aspx?action=video","Admin/View.aspx?action=videodetail","Admin/View.aspx?action=videotype")  %>">
                <asp:HyperLink ID="HyperLink1" class="icn_video" runat="server" NavigateUrl="~/Admin/View.aspx?action=video"
                    Text="Video"></asp:HyperLink>
                <ul style="display: block" class="<%= "sub " + IsShow("Admin/View.aspx?action=video","Admin/View.aspx?action=videodetail","Admin/View.aspx?action=videotype") %>">
                    <li class="<%= SetClass("Admin/View.aspx?action=videotype")  %>">
                        <asp:HyperLink ID="HyperLink3" class="icn_videotype" runat="server" NavigateUrl="~/Admin/View.aspx?action=videotype"
                            Text="Loại Video"></asp:HyperLink></li>
                </ul>
            </li>
            <li class="<%= SetClass("Admin/View.aspx?action=contact")  %>">
                <asp:HyperLink ID="hpContact" class="icn_contact" runat="server" NavigateUrl="~/Admin/View.aspx?action=contact"
                    Text="Liên hệ"></asp:HyperLink></li>
            <li class="<%= SetClass("Admin/View.aspx?action=commentnew")  %>">
                <asp:HyperLink ID="hpCommemnt" class="icn_comment" runat="server" NavigateUrl="~/Admin/View.aspx?action=commentnew"
                    Text="Bình luận"></asp:HyperLink></li>
            <li class="<%= SetClass("Admin/View.aspx?action=user")  %>">
                <asp:HyperLink ID="hpUser" class="icn_user" runat="server" NavigateUrl="~/Admin/View.aspx?action=user"
                    Text="Thành viên"></asp:HyperLink></li>
            <li class="<%= SetClass("Admin/View.aspx?action=autoadv")  %>">
                <asp:HyperLink ID="hpAutoAdv" class="icn_advertising" runat="server" NavigateUrl="~/Admin/View.aspx?action=autoadv"
                    Text="Quảng cáo"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/View.aspx?action=alexa")  %>">
                <asp:HyperLink ID="HyperLink11" class="icn_alexa" runat="server" NavigateUrl="~/admin/View.aspx?action=alexa"
                    Text="Site Alexa"></asp:HyperLink></li>
            <li class="<%= SetClass("Admin/View.aspx?action=seoconfig")  %>">
                <asp:HyperLink ID="hpContact1" class="icn_config" runat="server" NavigateUrl="~/Admin/View.aspx?action=seoconfig"
                    Text="Site SeoConfig"></asp:HyperLink></li>
        </ul>
    </asp:Panel>
</div>
