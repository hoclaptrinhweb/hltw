<%@ Page Title="Thay đổi mật khẩu - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="members_changepassword" %>

<%@ Register src="../usercontrols/ucChangePassword.ascx" tagname="ucChangePassword" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucChangePassword ID="ucChangePassword1" runat="server" />
</asp:Content>

