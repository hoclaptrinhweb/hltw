<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="Role.aspx.cs" Inherits="dhadmincp_Role" Title="Diendandaihoc.vn | Role" %>

<%@ Register Src="usercontrols/ucRole.ascx" TagName="ucRole" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucRole ID="UcRole1" runat="server" />
</asp:Content>
