<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UpPost.aspx.cs" Inherits="Admin_UpPost" Title="Post | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpPost.ascx" TagName="ucUpPost" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpPost ID="UcUpPost1" runat="server" />
</asp:Content>

