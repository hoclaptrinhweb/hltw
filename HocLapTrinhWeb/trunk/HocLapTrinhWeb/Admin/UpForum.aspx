<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UpForum.aspx.cs" Inherits="Admin_UpForum" Title="Quản lý Forum | hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpForum.ascx" TagName="ucUpForum" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpForum ID="UcUpForum1" runat="server" />
</asp:Content>

