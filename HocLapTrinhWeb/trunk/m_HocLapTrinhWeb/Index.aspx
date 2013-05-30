<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Src="usercontrols/ucIndex.ascx" TagName="ucIndex" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucIndex ID="ucIndex1" runat="server" />
</asp:Content>
