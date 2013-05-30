<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="NewsDetail" %>

<%@ Register src="usercontrols/ucNewsDetail.ascx" tagname="ucNewsDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucNewsDetail ID="ucNewsDetail1" runat="server" />
</asp:Content>

