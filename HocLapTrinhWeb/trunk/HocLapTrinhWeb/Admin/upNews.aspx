<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="upNews.aspx.cs" Inherits="Admin_upNews" Title="News | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpNews.ascx" TagName="ucUpNews" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpNews id="UcUpNews1" runat="server">
    </uc1:ucUpNews>
</asp:Content>

