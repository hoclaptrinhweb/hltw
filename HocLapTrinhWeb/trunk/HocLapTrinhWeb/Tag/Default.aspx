<%@ Page Title="" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tag_Default" %>
<%@ Register src="../usercontrols/ucSearchTag.ascx" tagname="ucSearchTag" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucSearchTag ID="ucSearchTag1" runat="server" />
</asp:Content>

