<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<%@ Register src="usercontrols/ucNews.ascx" tagname="ucNews" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <uc1:ucNews ID="ucNews1" runat="server" />
    
</asp:Content>

