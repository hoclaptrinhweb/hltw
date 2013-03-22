<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDefault.ascx.cs" Inherits="Admin_usercontrols_ucDefault" %>
<div id="page-content">
    <div id="page-header" style="display:none">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Bảng điều khiển"></asp:Label></h1>
    </div>
    <div class="container">
        <div>
            <asp:HyperLink ID="hdPromote" class="dashboard_button news" NavigateUrl="~/admin/News.aspx"
                runat="server">
                <asp:Label ID="lbNews" class="dashboard_button_heading" runat="server" Text="Tin tức"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="Quản lý tin tức"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdNewsType" class="dashboard_button newstype" NavigateUrl="~/admin/NewsType.aspx"
                runat="server">
                <asp:Label ID="lbNewsType" class="dashboard_button_heading" runat="server" Text="Loại tin"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="Quản lý loại tin"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdTag" class="dashboard_button tags" NavigateUrl="~/admin/Tag.aspx"
                runat="server">
                <asp:Label ID="lbTag" class="dashboard_button_heading" runat="server" Text="Thẻ tag"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="Quản lý loại tin"></asp:Label></asp:HyperLink>
        </div>
         <div>
            <asp:HyperLink ID="HyperLink4" class="dashboard_button video" NavigateUrl="~/Admin/video.aspx"
                runat="server">
                <asp:Label ID="Label13" class="dashboard_button_heading" runat="server" Text="Video"></asp:Label>
                <asp:Label ID="Label14" runat="server" Text="Quản lý video"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdCommentNews" class="dashboard_button comment" NavigateUrl="~/Admin/CommentNews.aspx"
                runat="server">
                <asp:Label ID="lbCommentNews" class="dashboard_button_heading" runat="server" Text="Bình luận"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Quản lý bình luận"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdContact" class="dashboard_button contact" NavigateUrl="~/Admin/Contact.aspx"
                runat="server">
                <asp:Label ID="lbContact" class="dashboard_button_heading" runat="server" Text="Liên hệ"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="Quản lý liên hệ"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="hdUser" class="dashboard_button user" NavigateUrl="~/Admin/User.aspx"
                runat="server">
                <asp:Label ID="lbUser" class="dashboard_button_heading" runat="server" Text="Thành viên"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Quản lý thành viên"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="HyperLink1" class="dashboard_button advertising" NavigateUrl="~/Admin/AutoAdv.aspx"
                runat="server">
                <asp:Label ID="Label1" class="dashboard_button_heading" runat="server" Text="Quảng cáo"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text="Quản lý quảng cáo"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="HyperLink2" class="dashboard_button alexa" NavigateUrl="~/Admin/Alexa.aspx"
                runat="server">
                <asp:Label ID="Label8" class="dashboard_button_heading" runat="server" Text="Hạng Alexa"></asp:Label>
                <asp:Label ID="Label9" runat="server" Text="Quản lý hạng alexa"></asp:Label></asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="HyperLink3" class="dashboard_button config" NavigateUrl="~/Admin/SeoConfig.aspx"
                runat="server">
                <asp:Label ID="Label10" class="dashboard_button_heading" runat="server" Text="Site Config"></asp:Label>
                <asp:Label ID="Label11" runat="server" Text="Cấu hình hệ thống"></asp:Label></asp:HyperLink>
        </div>
    </div>
</div>
