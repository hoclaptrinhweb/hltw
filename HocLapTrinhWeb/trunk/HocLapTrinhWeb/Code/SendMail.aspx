<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Demo.master" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="Code_SendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 80%; margin: 0px auto;">
        <div class="container">
            <div class="box-headerdetail">
                <asp:Label ID="lblBoxHeader" runat="server" Text="Demo chương trình gửi mail"></asp:Label>
            </div>
            <div class="box">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top">
                        <td style="width: 20%">
                            <asp:Label ID="Label2" runat="server" Text="Email" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Nhập vào email" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Mật khẩu" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="Nhập vào mật khẩu" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="SMTP" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrlMail" runat="server" placeholder="host" Width="450px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text=" : " CssClass="Caption"></asp:Label>
                            <asp:TextBox ID="txtPort" runat="server" placeholder="port" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="EnableSsl" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbEnableSsl" runat="server" Checked="True" />
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Email người nhận" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailTo" runat="server" placeholder="Email người nhận" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Tiêu đề" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" placeholder="Tiêu đề" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Nội dung" CssClass="Caption"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" placeholder="Nội dung" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="trempty">
                        <td colspan="2"></td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2">
                            <asp:Button ID="btnSend" runat="server" CssClass="button" Text="Gửi" OnClick="btnSend_Click" />
                            <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                            <asp:CustomValidator ID="SaveValidate" runat="server" CssClass="FormStyle" Display="None" ErrorMessage="" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <pre></pre>
</asp:Content>

