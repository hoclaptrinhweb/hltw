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
                <asp:Label ID="Label11" class="dashboard_button_heading" runat="server" Text="Tin tức"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="Quản lý tin tức"></asp:Label></asp:HyperLink>
        </div>
    </div>
</div>
