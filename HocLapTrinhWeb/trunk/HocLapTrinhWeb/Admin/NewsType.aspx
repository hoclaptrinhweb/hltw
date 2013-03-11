<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="NewsType.aspx.cs" Inherits="Admin_NewsType" Title="News Type | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucNewsType.ascx" TagName="ucNewsType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucNewsType ID="UcNewsType1" runat="server" />
</asp:Content>

