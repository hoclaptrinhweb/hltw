<%@ Page Title="Bài viết ngẫu nhiên | Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="RandomNews.aspx.cs" Inherits="RandomNews" %>
<%@ Register src="usercontrols/ucRandomNews.ascx" tagname="ucRandomNews" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucRandomNews ID="ucRandomNews1" runat="server" />
</asp:Content>

