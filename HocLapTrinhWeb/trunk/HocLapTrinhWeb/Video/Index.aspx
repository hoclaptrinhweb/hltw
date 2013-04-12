<%@ Page Title="Tổng hợp video về lập trình web, quản trị mạng, thông tin công nghệ" Language="C#" MasterPageFile="~/ThemeVideo.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Video_Index" %>
<%@ Register src="../usercontrols/ucVideoIndex.ascx" tagname="ucVideoIndex" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucVideoIndex ID="ucVideoIndex1" runat="server" />
</asp:Content>

