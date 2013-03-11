<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="upUserForum.aspx.cs" Inherits="Admin_upUserForum" Title="User Forum | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUserForum.ascx" TagName="ucUserForum" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucUserForum ID="UcUserForum1" runat="server" />
</asp:Content>
