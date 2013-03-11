<%@ Page Title="Bài viết mới hôm nay - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master" AutoEventWireup="true" CodeFile="ToDay.aspx.cs" Inherits="ToDay" %>

<%@ Register src="usercontrols/ucToDay.ascx" tagname="ucToDay" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucToDay ID="ucToDay1" runat="server" />
</asp:Content>
