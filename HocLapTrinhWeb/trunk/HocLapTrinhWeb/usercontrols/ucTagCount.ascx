<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTagCount.ascx.cs" Inherits="usercontrols_ucTagCount" %>
<div class="keyword">
    <asp:Repeater ID="rpTagCount" runat="server">
        <ItemTemplate>
            <a href="">
                <%# Eval("TagName") %></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
