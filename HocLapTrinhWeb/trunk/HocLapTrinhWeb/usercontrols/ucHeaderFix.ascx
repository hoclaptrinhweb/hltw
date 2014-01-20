<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeaderFix.ascx.cs" Inherits="usercontrols_ucHeaderFix" %>
<%@ Register Src="ucAdmin.ascx" TagName="ucAdmin" TagPrefix="uc1" %>
<%@ Register Src="ucGoogleCustomSearch.ascx" TagName="ucGoogleCustomSearch" TagPrefix="uc2" %>
<div class="fixed">
    <div id="top" class="top_bar">
        <div class="inner">
            <ul class="top_nav">
                <li><a href='<%= CurrentPage.UrlRoot %>'>Hoclaptrinhweb.com</a>
                    <asp:Literal ID="lbProductType" runat="server"></asp:Literal>
                </li>
                <li><a href='<%= CurrentPage.UrlRoot + "/today.aspx" %>'>Bài viết mới</a></li>
                <li>
                    <uc2:ucGoogleCustomSearch ID="ucGoogleCustomSearch1" runat="server" />
                </li>
            </ul>
            <div class="search_box">
                <uc1:ucAdmin ID="ucAdmin2" runat="server" />
            </div>
        </div>
    </div>
</div>
