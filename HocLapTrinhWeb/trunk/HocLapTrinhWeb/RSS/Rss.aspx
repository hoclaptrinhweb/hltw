<%@ Page Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="Rss.aspx.cs" Inherits="RSS_Rss" Title="Rss - Hoclaptrinhweb.com" %>
<%@ Register src="../usercontrols/ucRss.ascx" tagname="ucRss" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucRss ID="ucRss1" runat="server" />
</asp:Content>

