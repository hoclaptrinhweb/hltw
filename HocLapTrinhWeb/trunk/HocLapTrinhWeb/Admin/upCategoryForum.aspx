<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="upCategoryForum.aspx.cs" Inherits="Admin_upCategoryForum" Title="CategoryForum | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpCategoryForum.ascx" TagName="ucUpCategoryForum"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpCategoryForum ID="UcUpCategoryForum1" runat="server" />
</asp:Content>