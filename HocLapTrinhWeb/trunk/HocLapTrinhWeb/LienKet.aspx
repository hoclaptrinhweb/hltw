<%@ Page Title="Trao đổi liên kết - Hoclaptrinhweb.com" Language="C#" MasterPageFile="~/Theme.master"
    AutoEventWireup="true" CodeFile="LienKet.aspx.cs" Inherits="LienKet" %>
<%@ Register src="usercontrols/ucLienKet.ascx" tagname="ucLienKet" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucLienKet ID="ucLienKet1" runat="server" />
</asp:Content>
