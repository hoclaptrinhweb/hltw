<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContact.ascx.cs" Inherits="usercontrols_ucContact" %>
<%@ Register Assembly="NatsNet.Web.UI.Controls" Namespace="NatsNet.Web.UI.Controls"
    TagPrefix="cc1" %>
<style>
    .send
    {
        display: inline-block;
        float: left;
        border: black;
        background-color: black;
        font-family: Arial;
        font-size: 1.1em;
        line-height: 130%;
        text-decoration: none;
        font-weight: normal;
        color: white;
        cursor: pointer;
        -webkit-border-radius: 2px;
        -moz-border-radius: 2px;
        border-radius: 2px;
        margin-top: 0;
        margin-right: 0.583em;
        margin-bottom: 30px;
        margin-left: 0;
        padding-top: 5px;
        padding-right: 10px;
        padding-bottom: 5px;
        padding-left: 7px;
    }
</style>
<div class="box_outer">
    <div class="cat_article">
        <h1 class="cat_article_title">
            Gửi liên hệ
        </h1>
        <div id="article_content">
            <div style="padding-left: 20px">
                <asp:ValidationSummary ID="valError1" ValidationGroup="vSave" runat="server" EnableClientScript="False">
                </asp:ValidationSummary>
                <asp:CustomValidator ID="SaveValidate1" ValidationGroup="vSave" runat="server" Display="None"
                    ErrorMessage="CustomValidator" /><div class="Spacer">
                    </div>
            </div>
            <table class="tb">
                <tr>
                    <td width="150px">
                        <asp:Label ID="lblContactName" CssClass="Caption" runat="server" Text="Tên người gửi"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvLinkWebName" runat="server" ControlToValidate="txtUsername"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" size="50" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="Caption" Text="Email"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                ValidationGroup="vAdd" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Không phải Email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">(*)</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" size="50" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="Caption" Text="Điện thoại"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" size="50" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Địa chỉ"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" size="50" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="Caption" Text="Tiêu đề"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" size="50" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" CssClass="Caption" Text="Nội dung"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContent"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" Height="200px" TextMode="MultiLine" size="50"
                            Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="Caption" Text="Nhập mã"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImgVerify"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <table class="tb" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtImgVerify" runat="server" CssClass="itext" Width="150px"></asp:TextBox>
                                </td>
                                <td class="tdempty">
                                </td>
                                <td>
                                    <cc1:ImageVerifier ID="ImageVerifier1" runat="server" Height="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="trEmpty">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSend" runat="server" class="send" Text="Gửi" ValidationGroup="vAdd"
                            OnClick="btnSend_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
