<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTagCount.ascx.cs" Inherits="usercontrols_ucTagCount" %>
<div class="keyword">
    <asp:Repeater ID="rpTagCount" runat="server">
        <ItemTemplate>
            <a class='<%# GetTagClass(int.Parse(Eval("tagcount").ToString())) %>' href="<%# CurrentPage.UrlRoot + "/tag/" + Eval("TagName").ToString().Replace(" ","-") + ".aspx" %>">
                <%# Eval("TagName") %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
<asp:HiddenField ID="hdNewsCount" runat="server" />
