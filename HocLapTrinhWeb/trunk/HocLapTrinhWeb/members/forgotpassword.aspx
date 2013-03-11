<%@ Page Title="Lấy lại mật khẩu - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="members_forgotpassword" %>
<%@ Register src="../usercontrols/ucForgotPassword.ascx" tagname="ucForgotPassword" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucForgotPassword ID="ucForgotPassword1" runat="server" />
</asp:Content>

