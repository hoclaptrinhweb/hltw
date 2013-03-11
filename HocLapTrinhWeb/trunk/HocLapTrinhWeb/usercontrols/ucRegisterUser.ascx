<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRegisterUser.ascx.cs"
    Inherits="usercontrols_ucRegisterUser" %>
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
            <a>Đăng ký thành viên - Hoclaptrinhweb.com</a>
        </h1>
        <div class="article_meta">
            <%--<span class="meta_author">Posted by: <a>Hoclaptrinhweb</a></span>--%>
        </div>
        <div class="brief">
            <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
            <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" ErrorMessage="CustomValidator" />
        </div>
        <div id="article_content" class="single_article_content">
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td width="150">
                        <asp:Label ID="lblUserName" CssClass="Caption" runat="server" Text="Tên đăng nhập"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Phải nhập tên"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtUserName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Mật khẩu"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPass" Width="300px" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Phải nhập mật khẩu"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPass" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label2" CssClass="Caption" runat="server" Text="Nhập lại mật khẩu"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassConfirm" Width="300px" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rqvConfirmPass" runat="server" ErrorMessage="Phải nhập lại mật khẩu"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassConfirm" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPass" ValidationGroup="vAdd"
                            ControlToValidate="txtPassConfirm" Display="Dynamic" ErrorMessage="Mật khẩu không giống nhau"
                            SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="Caption" Text="Email"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" Width="300px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Phải nhập Email"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtEmail" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ValidationGroup="vAdd" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Không phải Email"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label4" CssClass="Caption" runat="server" Text="Tên đầy đủ"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFullName" Width="300px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                      </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvUserName0" runat="server" ErrorMessage="Phải nhập tên"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtFullName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="Caption" Text="Điện thoại"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" Width="300px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="Caption" Text="Website"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWebsite" Width="300px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label6" CssClass="Caption" runat="server" Text="Địa chỉ"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" Width="300px" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" CssClass="Caption" Text="Nhập mã"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImgVerify"
                            Display="Dynamic" ErrorMessage="Phải nhập" SetFocusOnError="True" ValidationGroup="vAdd">(*)</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <table class="tb" width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtImgVerify" runat="server" CssClass="itext" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <cc1:ImageVerifier ID="ImageVerifier1" runat="server" Height="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnRegister" CssClass="send" runat="server" Text="Đăng ký" 
                            ValidationGroup="vAdd" onclick="BtnRegisterClick" />
                        <asp:Label ID="lbResult" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="single_share">
        </div>
    </div>
</div>
