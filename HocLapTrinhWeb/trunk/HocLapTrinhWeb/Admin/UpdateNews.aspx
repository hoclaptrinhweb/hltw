<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateNews.aspx.cs" Inherits="Admin_UpdateNews" Title="Lấy tin tự động | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpdateNews.ascx" TagName="ucUpdateNews" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucUpdateNews ID="UcUpdateNews1" runat="server" />
</asp:Content>
