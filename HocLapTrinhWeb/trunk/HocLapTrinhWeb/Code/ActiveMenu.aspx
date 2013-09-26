<%@ Page Title="Demo ActiveMenu - Học lập trình web" Language="C#" MasterPageFile="~/Code/Demo.master"
    AutoEventWireup="true" CodeFile="ActiveMenu.aspx.cs" Inherits="Code_ActiveMenu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%= Combres.WebExtensions.CombresLink("DefaultThemeCss") %>
    <nav id="navigation">
    <div class="nav_wrap">
        <div class="inner">
    <ul class="nav">
        <li><a class="<%= SetClass("active=1")  %>" href="<%= UrlRoot + "/code/activemenu.aspx?active=1" %>">Menu 1</a></li>
        <li><a class="<%= SetClass("active=2")  %>" href="<%= UrlRoot + "/code/activemenu.aspx?active=2" %>">Menu 2</a></li>
        <li><a class="<%= SetClass("active=3")  %>" href='<%= UrlRoot + "/code/activemenu.aspx?active=3" %>'>Menu 3</a></li>
        <li><a class="<%= SetClass("active=4")  %>" href='<%= UrlRoot + "/code/activemenu.aspx?active=4" %>'>Menu 4</a></li>
    </ul>
        </div>
    </div>
    </nav>
</asp:Content>
