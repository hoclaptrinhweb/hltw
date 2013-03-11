<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="AlexaDetail.aspx.cs" Inherits="Admin_AlexaDetail" Title="Chi tiết thống kê Alexa.com - Hoclaptrinhweb.com" %>
<%@ Register src="usercontrols/ucAlexaDetail.ascx" tagname="ucAlexaDetail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucAlexaDetail ID="ucAlexaDetail1" runat="server" />
</asp:Content>
