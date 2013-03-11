<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" Theme="Admin"
    AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="administrator_NewsDetail"
    Title="Chi tiết tin tức | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucNewsDetail.ascx" TagName="NewsDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:NewsDetail ID="NewsDetail1" runat="server" />
</asp:Content>
