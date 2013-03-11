<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLoginUser.ascx.cs" Inherits="usercontrols_ucLoginUser" %>
<div class="box_outer">
    <div class="cat_article">
        <h1 class="cat_article_title">
            <a>Đăng nhập vào hệ thống - Hoclaptrinhweb.com</a>
        </h1>
        <div class="article_meta">
            <%--<span class="meta_author">Posted by: <a>Hoclaptrinhweb</a></span>--%>
        </div>
        <div class="brief">
            <asp:ValidationSummary ID="valError" runat="server" 
                EnableClientScript="False" />
            <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" 
                ErrorMessage="CustomValidator" />
        </div>
        <div id="article_content" class="single_article_content">
            <table class="tb">
                <tr>
                    <td>
                        <span class="Caption">Tên đăng nhập</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="Caption">Mật khẩu</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSend" runat="server" Text="Đăng nhập" 
                            onclick="btnSend_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="single_share">
        </div>
    </div>
</div>
