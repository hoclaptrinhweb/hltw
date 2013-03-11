<%@ Page Title="CommnentNews | Hoclaptrinhweb.com" Theme="Admin" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="CommentNews.aspx.cs" Inherits="Admin_CommentNews" %>

<%@ Register src="usercontrols/ucCommentNews.ascx" tagname="ucCommentNews" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucCommentNews ID="ucCommentNews1" runat="server" />
</asp:Content>

