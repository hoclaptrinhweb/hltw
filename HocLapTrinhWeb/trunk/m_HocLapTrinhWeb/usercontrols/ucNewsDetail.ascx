<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsDetail.ascx.cs"
    Inherits="usercontrols_ucNewsDetail" %>
<div class="article">
    <h1 class="article-title">
        <asp:Literal ID="lbTitle" runat="server"></asp:Literal></h1>
    <div class="article-summary">
        <p>
            <asp:Literal ID="lbBrief" runat="server"></asp:Literal>
        </p>
    </div>
    <div class="pBody">
        <asp:Literal ID="lbContent" runat="server"></asp:Literal>
    </div>
</div>
