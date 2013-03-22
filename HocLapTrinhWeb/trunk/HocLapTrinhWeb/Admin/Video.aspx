<%@ Page Title="Quản lý video | Hoclaptrinhweb.com" Theme="Admin" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Video.aspx.cs" Inherits="Admin_Video" %>

<%@ Register src="usercontrols/ucVideo.ascx" tagname="ucVideo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucVideo ID="ucVideo1" runat="server" />
</asp:Content>

