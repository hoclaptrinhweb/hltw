<%@ Page Title="Đăng ký thành viên - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="members_register" %>

<%@ Register src="../usercontrols/ucRegisterUser.ascx" tagname="ucRegisterUser" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucRegisterUser ID="ucRegisterUser1" runat="server" />
</asp:Content>

