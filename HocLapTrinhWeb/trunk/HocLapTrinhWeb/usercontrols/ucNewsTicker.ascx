<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsTicker.ascx.cs"
    Inherits="usercontrols_ucNewsTicker" %>
<div class="inner">
    <div class="box_outer nt_bd">
        <div class="ticker_widget">
            <div class="news_ticker">
                <div class="right_arrow">
                </div>
                <div class="tickercontainer">
                    <div class="mask">
                        <marquee align="center" direction="left" scrollamount="4" onmouseover="this.stop()"
                            onmouseout="this.start()">
                        <ul id="ticker01" style="left:0px;" class="newsticker">
                            <asp:Repeater ID="rpNewsTicker" runat="server">
                                <ItemTemplate>
                                    <li><a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'><%# Eval("Title") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </marquee>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
