<%@ Page Title="" Language="C#" MasterPageFile="~/ThemeVideo.master" AutoEventWireup="true" CodeFile="VideoType.aspx.cs" Inherits="Video_VideoType" %>

<%@ Register src="../usercontrols/ucVideoType.ascx" tagname="ucVideoType" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucVideoType ID="ucVideoType1" runat="server" />
</asp:Content>

