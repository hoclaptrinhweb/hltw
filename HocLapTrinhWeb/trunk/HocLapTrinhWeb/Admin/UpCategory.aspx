<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UpCategory.aspx.cs" Inherits="Admin_UpCategory" Title="Category | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/UpCategory.ascx" TagName="UpCategory" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UpCategory ID="UpCategory1" runat="server" />
</asp:Content>

