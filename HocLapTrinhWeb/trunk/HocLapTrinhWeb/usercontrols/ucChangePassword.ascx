<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucChangePassword.ascx.cs" Inherits="usercontrols_ucChangePassword" %>
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
            <a>Đổi mật khẩu - Hoclaptrinhweb.com</a>
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
                        <asp:Label ID="lblUserName" CssClass="Caption" runat="server" 
                            Text="Mật khẩu cũ"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassOld" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Phải nhập"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassOld" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label1" CssClass="Caption" runat="server" Text="Mật khẩu mới"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassNew" Width="300px" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Phải nhập mật khẩu"
                            Display="Dynamic" ValidationGroup="vAdd" ControlToValidate="txtPassNew" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label2" CssClass="Caption" runat="server" 
                            Text="Nhập lại mật khẩu mới"></asp:Label>
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
                        <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPassNew" ValidationGroup="vAdd"
                            ControlToValidate="txtPassConfirm" Display="Dynamic" ErrorMessage="Mật khẩu không giống nhau"
                            SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnChangePass" CssClass="send" runat="server" Text="Lưu" 
                            ValidationGroup="vAdd" onclick="btnChangePass_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div class="single_share">
        </div>
    </div>
</div>