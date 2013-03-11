<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucChangePassword.ascx.cs"
    Inherits="administrator_usercontrols_ucChangePassword" %>
<div id="page-content">
    <div id="page-header">
        <h1>
            <asp:Label ID="lblPageHeader" runat="server" Text="User"></asp:Label></h1>
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
                    <asp:Label ID="lblBoxHeader" runat="server" Text="Change password"></asp:Label>
                </div>
                <div class="box">
                    <table cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td>
                                <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="Login name"></asp:Label>
                            </td>
                            <td class="tdempty">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="txtUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Old password" meta:resourcekey="Label1Resource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPass" Width="250px" TextMode="Password" runat="server" meta:resourcekey="txtOldPassResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Please input old password."
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtOldPass" SetFocusOnError="True"
                                    meta:resourcekey="rfvPassResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label7" CssClass="Caption" runat="server" Text="New password" meta:resourcekey="Label7Resource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtPass" Width="250px" TextMode="Password" runat="server" meta:resourcekey="txtPassResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input new password."
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPass" SetFocusOnError="True"
                                    meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                        </tr>
                        <tr valign="top">
                            <td>
                                <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Confirm new password"
                                    meta:resourcekey="Label2Resource1"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="Require">
                                &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassConfirm" Width="250px" TextMode="Password" runat="server"
                                    meta:resourcekey="txtPassConfirmResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please confirm new password."
                                    Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassConfirm" SetFocusOnError="True"
                                    meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPass" ValidationGroup="vAdd"
                                    ControlToValidate="txtPassConfirm" Display="Dynamic" ErrorMessage="Confirm new password wrong"
                                    SetFocusOnError="True" meta:resourcekey="cvPassResource1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr class="trEmpty">
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" EnableViewState="False"
                                    Text="Save" ValidationGroup="vAdd" OnClick="BtnSaveClick" meta:resourcekey="btnSaveResource1" />
                                <asp:Button ID="btnCancel" CssClass="button" runat="server" UseSubmitBehavior="False"
                                    Text="Back" CausesValidation="False" OnClick="BtnCancelClick" meta:resourcekey="btnCancelResource1" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
