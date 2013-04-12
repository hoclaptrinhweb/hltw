<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucNewsType.ascx.cs"
    Inherits="usercontrols_ucNewsType" %>
<asp:Repeater ID="rpNewsType" runat="server" OnItemDataBound="rpNewsType_ItemDataBound">
    <ItemTemplate>
        <div class="box_outer">
            <div class="news_box">
                <div class="news_box_heading">
                    <div class="nb_dots">
                        <h2>
                            <a title='<%# Eval("Description") %>' href='<%# CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw" + Eval("NewsTypeID") + ".aspx" %>'>
                                <%# Eval("newstypename") %></a></h2>
                    </div>
                </div>
                <ul class="newsitem">
                    <asp:Repeater ID="rpNews" runat="server">
                        <ItemTemplate>
                            <li itemscope itemtype="http://schema.org/Article" class='<%# ((RepeaterItem)Container) .ItemIndex != 0 ? "more_news" : "full" %>'>
                                <%# BindData((RepeaterItem)Container)%>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.more_news a').cluetip({
            width: '400px',
            showTitle: true,
            positionBy: 'topBottom',
            topOffset: 20,
            cluezIndex: 100
        });
    });
</script>
