<%@ Page Title="" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true"
    CodeFile="combres.aspx.cs" Inherits="combres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%= Combres.WebExtensions.CombresLink("DefaultThemeCss") %>
    <%=  Combres.WebExtensions.CombresLink("DefaultThemeJs") %>
</asp:Content>
