<%@ Page Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="administrator_ChangePassword" Title="hadohitech.com | Quản trị viên" Theme="Admin" %>

<%@ Register Src="usercontrols/ucChangePassword.ascx" TagName="ucChangePassword" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucChangePassword ID="UcChangePassword1" runat="server" />
</asp:Content>
