<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuNewsType.ascx.cs" Inherits="usercontrols_ucMenuNewsType" %>
<style>
	.cat-list {
		float: left;
		width: 125px;
		margin: 1px;
		border-bottom: 1px solid #dedede;
		padding: 5px;
		font-size: 1.1em;
	}

    .float-right
    {
        float: right;
    }

    .float-left
    {
        float: left;
    }
    .cat-list img {
        margin-bottom: -2px;
        margin-right: 5px;
		width:23px;
    }
    .cat-list a
    {
        color: #484b40;
        text-decoration: none;
        font-size: 0.9em;
        font-weight: bold;
    }
</style>
<asp:Repeater ID="rpNewsType" runat="server">
    <ItemTemplate>
        <div class='<%# ((RepeaterItem)Container).ItemIndex % 2 == 0 ? "cat-list float-right" : "cat-list float-left" %>'>
            <img width="16" src="<%# Eval("ImageURL") %>" alt="<%# Eval("NewsTypeName") %>">
            <a href='<%# CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw" + Eval("NewsTypeID") + ".aspx" %>'><%# Global.GetSubContent(Eval("NewsTypeName").ToString(),15) %></a>
        </div>
    </ItemTemplate>
</asp:Repeater>
