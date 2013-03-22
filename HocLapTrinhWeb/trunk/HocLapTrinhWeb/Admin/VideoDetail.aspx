<%@ Page Theme="Admin" Title="Chi tiết video | Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="VideoDetail.aspx.cs" Inherits="Admin_VideoDetail" %>

<%@ Register src="usercontrols/ucVideoDetail.ascx" tagname="ucVideoDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucVideoDetail ID="ucVideoDetail1" runat="server" />
</asp:Content>

