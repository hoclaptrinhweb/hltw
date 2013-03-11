<%@ Page Title="Kích hoạt tài khoản | Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master"
    AutoEventWireup="true" CodeFile="ActiveUser.aspx.cs" Inherits="ActiveUser" %>
<%@ Register src="usercontrols/ucActiveUser.ascx" tagname="ucActiveUser" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucActiveUser ID="ucActiveUser1" runat="server" />
</asp:Content>
