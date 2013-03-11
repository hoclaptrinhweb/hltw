<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" Theme="Admin"
    AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="administrator_News"
    Title="Tin tức | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucNews.ascx" TagName="ucNews" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucNews ID="UcNews1" runat="server" />
</asp:Content>
