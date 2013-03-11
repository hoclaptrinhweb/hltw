<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLogin.ascx.cs" Inherits="administrator_usercontrols_ucLogin" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdateProgress ID="upProgcess" DisplayAfter="10" runat="server">
    <ProgressTemplate>
        <div style="position: absolute; width: 100%; height: 100%; z-index: 200; background-color: #ffffff;
            opacity: 0.65; filter: alpha(opacity=65)">
            <table style="width: 100%; height: 100%;" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" valign="middle">
                        <img id="Img1" alt="Loading" src="img/loading.gif" /></td>
                </tr>
            </table>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<div id="login-wrapper">
    <div class="box-header login">
        <asp:HyperLink ID="hpHeaderLogin" runat="server" NavigateUrl="~/default.aspx" Text="Đăng Nhập - HocLapTrinhWeb.com"></asp:HyperLink></div>
    <div class="box" style="text-align: center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="margin: 0 auto;">
                    <tr>
                        <td colspan="3">
                            <img src="img/login.png" width="100%" alt="Login icon" />
                        </td>
                    </tr>
                    <tr class="trEmpty">
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                            <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" ErrorMessage="CustomValidator" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" CssClass="Caption" Text="T&#234;n"></asp:Label>
                        </td>
                        <td class="tdEmpty">
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" Style="padding: 5px; width: 190px;" runat="server"
                                ToolTip="T&#234;n quản trị vi&#234;n"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Nhập t&#234;n đăng nhập"
                                Display="Dynamic" ControlToValidate="txtUserName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="trEmpty">
                    </tr>
                    <tr class="trEmpty">
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPass" runat="server" CssClass="Caption" Text="Mật khẩu"></asp:Label>
                        </td>
                        <td class="tdEmpty">
                        </td>
                        <td>
                            <asp:TextBox ID="txtPass" runat="server" Style="padding: 5px; width: 190px;" TextMode="Password"
                                ToolTip="Mật khẩu đăng nhập"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nhập mật khẩu"
                                Display="Dynamic" ControlToValidate="txtPass" SetFocusOnError="True" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="trEmpty">
                    </tr>
                    <tr class="trEmpty">
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <asp:ImageButton ID="btnLogin" ImageUrl="~/admin/img/btn-login.jpg" runat="server"
                                OnClick="BtnLoginClick" AlternateText="Đăng nhập" Height="50px" Style="padding: 0px"
                                ToolTip="Đăng nhập hệ thống" Width="50px" meta:resourcekey="btnLoginResource1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

<script type="text/javascript">
        
        var prm = Sys.WebForms.PageRequestManager.getInstance(); 
        prm.add_beginRequest(BeginRequestHandler);   
        
        function BeginRequestHandler(sender, args)//begin ajax
        { 
           var upProgcess=  document.getElementById('<%= upProgcess.ClientID%>');
           upProgcess.style.position='fixed';
           upProgcess.style.width='100%';
           upProgcess.style.height='100%';
           upProgcess.style.top=0;
           upProgcess.style.left=0;
        }
        
</script>

