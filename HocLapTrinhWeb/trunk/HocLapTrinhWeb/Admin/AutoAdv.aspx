<%@ Page Title="Quản lý quảng cáo - Hoclaptrinhweb.com" Theme="Admin" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="AutoAdv.aspx.cs" Inherits="Admin_AutoAdv" %>

<%@ Register src="usercontrols/ucAutoAdv.ascx" tagname="ucAutoAdv" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucAutoAdv ID="ucAutoAdv1" runat="server" />
</asp:Content>

