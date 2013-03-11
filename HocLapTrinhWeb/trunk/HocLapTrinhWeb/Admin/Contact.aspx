<%@ Page Title="Quản lý liên hệ | Hoclaptrinhweb.com" Theme="Admin" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Admin_Contact" %>
<%@ Register src="usercontrols/ucContact.ascx" tagname="ucContact" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucContact ID="ucContact1" runat="server" />
</asp:Content>

