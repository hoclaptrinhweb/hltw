<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" Title="Bảng điều khiển | HocLapTrinhWeb.com" %>

<%@ Register Src="usercontrols/ucDefault.ascx" TagName="ucDefault" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucDefault ID="UcDefault1" runat="server" />
</asp:Content>

