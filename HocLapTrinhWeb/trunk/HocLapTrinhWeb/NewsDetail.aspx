<%@ Page Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs"
    Inherits="NewsDetail" Title="Untitled Page" %>

<%@ Register Src="usercontrols/ucNewsDetail1.ascx" TagName="ucNewsDetail" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucNewsDetail ID="ucNewsDetail1" runat="server" />
</asp:Content>
