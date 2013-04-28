<%@ Page Title="" Language="C#" MasterPageFile="~/ThemeViewState.master" AutoEventWireup="true" CodeFile="NewsPost.aspx.cs" Inherits="NewsPost" %>

<%@ Register src="usercontrols/ucNewsPost.ascx" tagname="ucNewsPost" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucNewsPost ID="ucNewsPost1" runat="server" />
</asp:Content>

