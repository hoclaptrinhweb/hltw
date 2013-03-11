<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="upNewsType.aspx.cs" Inherits="Admin_upNewsType" Title="NewsType | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpNewsType.ascx" TagName="ucUpNewsType" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucUpNewsType ID="UcUpNewsType1" runat="server" />
</asp:Content>
