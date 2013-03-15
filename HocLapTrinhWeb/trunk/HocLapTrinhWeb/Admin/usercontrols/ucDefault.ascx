<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDefault.ascx.cs" Inherits="Admin_usercontrols_ucDefault" %>
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Bảng điều khiển"></asp:Label></h1>
    </div>
    <div class="container">
        <div>
            <asp:HyperLink ID="hdPromote" class="dashboard_button button1" NavigateUrl="~/admin/News.aspx"
                runat="server">
                <asp:Label ID="lbNews" class="dashboard_button_heading" runat="server" Text="Tin tức"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="Quản lý tin tức"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdNewsType" class="dashboard_button button1" NavigateUrl="~/admin/NewsType.aspx"
                runat="server">
                <asp:Label ID="lbNewsType" class="dashboard_button_heading" runat="server" Text="Loại tin"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="Quản lý loại tin"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdTag" class="dashboard_button button1" NavigateUrl="~/admin/Tag.aspx"
                runat="server">
                <asp:Label ID="lbTag" class="dashboard_button_heading" runat="server" Text="Thẻ tag"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="Quản lý loại tin"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdCommentNews" class="dashboard_button button1" NavigateUrl="~/Admin/CommentNews.aspx"
                runat="server">
                <asp:Label ID="lbCommentNews" class="dashboard_button_heading" runat="server" Text="Bình luận"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Quản lý bình luận"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdContact" class="dashboard_button button1" NavigateUrl="~/Admin/Contact.aspx"
                runat="server">
                <asp:Label ID="lbContact" class="dashboard_button_heading" runat="server" Text="Liên hệ"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="Quản lý liên hệ"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdUser" class="dashboard_button button1" NavigateUrl="~/Admin/User.aspx"
                runat="server">
                <asp:Label ID="lbUser" class="dashboard_button_heading" runat="server" Text="Thành viên"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Quản lý thành viên"></asp:Label></asp:HyperLink>
        </div>
    </div>
</div>
