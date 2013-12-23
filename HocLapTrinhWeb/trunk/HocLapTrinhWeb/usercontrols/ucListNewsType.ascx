<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucListNewsType.ascx.cs" Inherits="usercontrols_ucListNewsType" %>
<asp:Repeater ID="rpNewsType" runat="server" OnItemDataBound="rpNewsType_ItemDataBound">
    <ItemTemplate>
        <div class="box_outer">
            <div class="news_box">
                <div class="news_box_heading bblue">
                    <div class="nb_dots">
                        <h2>
                            <img src="<%# Eval("ImageURL").ToString() == "" ? "http://www.hoclaptrinhweb.com/App_Themes/Admin/img/icons/32x32/newstype.png" : Eval("ImageURL") %>" alt="<%# Eval("NewsTypeName") %>">
                            <a title='<%# Eval("Description") %>' href='<%# UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw" + Eval("NewsTypeID") + ".aspx" %>'>
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
