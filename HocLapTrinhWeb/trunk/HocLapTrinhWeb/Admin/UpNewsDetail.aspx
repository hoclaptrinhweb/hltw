<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UpNewsDetail.aspx.cs" Inherits="Admin_UpNewsDetail" Title="Thêm & Sửa bài viết | Hoclaptrinhweb.com" %>

<%@ Register Src="usercontrols/ucUpNewsDetail.ascx" TagName="ucUpNewsDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ucUpNewsDetail ID="UcUpNewsDetail1" runat="server" />
</asp:Content>

