<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucChangePassword.ascx.cs"
    Inherits="administrator_usercontrols_ucChangePassword" %>
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="Tài khoản"></asp:Label></h1>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
                    <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" CssClass="FormStyle"
                        ErrorMessage="CustomValidator" />
                </div>
                <div class="box-header">
                    <asp:Label ID="lblBoxHeader" runat="server" Text="Thay đổi mật khẩu"></asp:Label>
                </div>
                <div class="box">
                    <table cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td>
                                <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="Tên đăng nhập"></asp:Label>
                            </td>
                            <td class="tdempty">
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="txtUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Mật khẩu cũ"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPass" Width="250px" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Vui lòng nhập mật khẩu cũ"
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtOldPass" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label7" CssClass="Caption" runat="server" Text="Mật khẩu mới"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                            </td>
                            <td>
                                <asp:TextBox ID="txtPass" Width="250px" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập mật khẩu mới"
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPass" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Nhập lại mật khẩu cũ"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassConfirm" Width="250px" TextMode="Password" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nhập lại mật khẩu cũ"
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassConfirm" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPass" ValidationGroup="vAdd"
                                    ControlToValidate="txtPassConfirm" Display="Dynamic" ErrorMessage="Mật khẩu cũ không đúng ở trên"
                                    SetFocusOnError="True"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" EnableViewState="False"
                                    Text="Cập nhật" ValidationGroup="vAdd" OnClick="BtnSaveClick" />
                                <asp:Button ID="btnCancel" CssClass="button" runat="server" UseSubmitBehavior="False"
                                    Text="Trở về" CausesValidation="False" OnClick="BtnCancelClick" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
