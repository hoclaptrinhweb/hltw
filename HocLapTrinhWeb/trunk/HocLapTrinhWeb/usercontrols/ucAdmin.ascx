<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAdmin.ascx.cs" Inherits="usercontrols_ucAdmin" %>
<asp:Panel ID="pUser" runat="server">
    <ul class="top_nav">
        <li><a>
            <asp:Label ID="lblFullName" runat="server" Text="Khách"></asp:Label></a>
            <ul>
                <li><a href='<%= CurrentPage.UrlRoot + "/members/changepassword.aspx" %>'>Đổi mật khẩu</a>
                </li>
                <li><a href='<%= CurrentPage.UrlRoot + "/members/login.aspx?logout=1" %>'>Đăng xuất</a>
                </li>
            </ul>
        </li>
        <li runat="server" id="liAdmin"><a target="_blank" href='<%= CurrentPage.UrlRoot + "/Admin/Default.aspx" %>'>
            Bảng điều khiển</a>
            <ul>
                <li>
                    <asp:HyperLink ID="hpEdit" Target="_blank" runat="server" Text="Edit"></asp:HyperLink></li>
            </ul>
        </li>
    </ul>
</asp:Panel>
<asp:Panel ID="pGuest" runat="server">
    <ul class="top_nav">
        <li><a id="signup" href='<%= CurrentPage.UrlRoot + "/members/register.aspx" %>'>Đăng
            ký</a></li>
        <li><a id="signin" href='<%= CurrentPage.UrlRoot + "/members/login.aspx" %>'>Đăng nhập</a></li>
    </ul>
</asp:Panel>
