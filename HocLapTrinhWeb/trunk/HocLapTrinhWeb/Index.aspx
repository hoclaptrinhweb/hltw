<%@ Page Language="C#" MasterPageFile="~/ThemeNews.master" AutoEventWireup="true" CodeFile="Index.aspx.cs"
    Inherits="Index" Title="hoclaptrinhweb.com học lập trình web online, học lập trình web miễn phí, học
        lập trình web cơ bản - nâng cao" %>
<%@ Register Src="usercontrols/ucNewsType.ascx" TagName="ucNewsType" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box_outer">
    </div>
    <uc2:ucNewsType ID="ucNewsType1" runat="server" />
</asp:Content>
