<%@ Page Title="Đăng nhập vào hệ thống - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="members_login" %>

<%@ Register src="../usercontrols/ucLoginUser.ascx" tagname="ucLoginUser" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucLoginUser ID="ucLoginUser1" runat="server" />
</asp:Content>

