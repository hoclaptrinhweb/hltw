<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UpUser.aspx.cs" Inherits="Admin_UpUser" Title="Tạo tài khoản | hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpUser.ascx" TagName="ucUpUser" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpUser ID="UcUpUser1" runat="server" />
</asp:Content>

