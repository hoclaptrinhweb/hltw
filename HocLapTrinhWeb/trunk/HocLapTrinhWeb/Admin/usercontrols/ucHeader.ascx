<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeader.ascx.cs" Inherits="administrator_usercontrols_WebUserControl" %>
<div id="header">
    <div class="header-top tr">
        <asp:Label ID="lblHello" runat="server" Text="Chào"></asp:Label>
        <strong>
            <asp:Label ID="lblFullName" runat="server" Text="Khách"></asp:Label></strong>
        <asp:LinkButton ID="btnChangePass" CausesValidation="false" runat="server" Text=" | Đổi mật khẩu"
            PostBackUrl="~/Admin/View.aspx?action=changepassword"></asp:LinkButton>
        |
        <asp:LinkButton ID="btnLogout" runat="server" Text="Đăng xuất" CausesValidation="false"
            OnClick="BtnLogoutClick"></asp:LinkButton>
    </div>
    <div class="header-middle">
        <ul id="nav" class="fr ">
            <li class="settings"><a class="settings">Cấu hình hệ thống</a>
                <ul>
                    <li>
                        <asp:HyperLink ID="hpGoogleanalytics" Target="_blank" NavigateUrl="https://www.google.com/analytics/web/"
                            runat="server" Text="Google Analytics" />
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="http://access.pavietnam.vn/"
                            runat="server" Text="Config DNS" />
                    </li>
                </ul>
            </li>
            <li class="dashboard">
                <asp:HyperLink ID="hpDashboard" class="dashboard" NavigateUrl="~/Admin/View.aspx"
                    runat="server" Text="Bảng điều khiển" /></li>
        </ul>
        <img id="logo" src="img/logo.png" alt="Admin Daihoc" />
        <br class="cl" />
    </div>
</div>
