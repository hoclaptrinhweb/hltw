<%@ Page Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="User.aspx.cs" Inherits="administrator_User" Title="Quản lý thành viên | Hoclaptrinhweb.com" Theme="Admin" %>

<%@ Register Src="usercontrols/ucUser.ascx" TagName="ucUser" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucUser ID="UcUser1" runat="server" />
</asp:Content>
