<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSideBar.ascx.cs" Inherits="administrator_usercontrols_ucSideBar" %>
<div id="sidebar">
    <span class="ul-header">
        <asp:HyperLink ID="hpOption" runat="server" Text="Chức năng"></asp:HyperLink></span>
    <asp:Panel ID="pnOption" runat="server">
        <ul id="main_menu">
            <li class="<%= SetClass("admin/News.aspx","admin/NewsDetail.aspx","admin/UpdateNews.aspx","admin/NewsType.aspx")  %>">
                <asp:HyperLink ID="hpNews" class="icn_news" runat="server" NavigateUrl="~/admin/News.aspx"
                    Text="Quản lý tin tức"></asp:HyperLink>
                <ul style="display: block" class="<%= "sub " + IsShow("admin/News.aspx","admin/UpdateNews.aspx","admin/NewsType.aspx") %>">
                    <li class="<%= SetClass("admin/NewsType.aspx")  %>">
                        <asp:HyperLink ID="HyperLink2" class="icn_newstype" runat="server" NavigateUrl="~/admin/NewsType.aspx"
                            Text="Loại Tin"></asp:HyperLink></li>
                    <li class="<%= SetClass("admin/Tag.aspx")  %>">
                        <asp:HyperLink ID="HyperLink9" class="icn_tags" runat="server" NavigateUrl="~/admin/Tag.aspx"
                            Text="Tag"></asp:HyperLink></li>
                    <li class="<%= SetClass("admin/UpdateNews.aspx")  %>">
                        <asp:HyperLink ID="HyperLink10" class="icn_updatenews" runat="server" NavigateUrl="~/admin/UpdateNews.aspx"
                            Text="Lấy tin tự động"></asp:HyperLink></li>
                </ul>
            </li>
            <li class="<%= SetClass("admin/Video.aspx","admin/VideoDetail.aspx","admin/videotype.aspx")  %>">
                <asp:HyperLink ID="HyperLink1" class="icn_video" runat="server" NavigateUrl="~/admin/Video.aspx"
                    Text="Video"></asp:HyperLink>
                <ul style="display: block" class="<%= "sub " + IsShow("admin/Video.aspx","admin/VideoDetail.aspx","admin/videotype.aspx") %>">
                    <li class="<%= SetClass("admin/NewsType.aspx")  %>">
                        <asp:HyperLink ID="HyperLink3" class="icn_videotype" runat="server" NavigateUrl="~/admin/VideoType.aspx"
                            Text="Loại Video"></asp:HyperLink></li>
                </ul>
            </li>
            <li class="<%= SetClass("admin/Contact.aspx")  %>">
                <asp:HyperLink ID="hpContact" class="icn_contact" runat="server" NavigateUrl="~/admin/Contact.aspx"
                    Text="Liên hệ"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/CommentNews.aspx")  %>">
                <asp:HyperLink ID="hpCommemnt" class="icn_comment" runat="server" NavigateUrl="~/admin/CommentNews.aspx"
                    Text="Bình luận"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/User.aspx")  %>">
                <asp:HyperLink ID="hpUser" class="icn_user" runat="server" NavigateUrl="~/admin/User.aspx"
                    Text="Thành viên"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/AutoAdv.aspx")  %>">
                <asp:HyperLink ID="hpAutoAdv" class="icn_advertising" runat="server" NavigateUrl="~/admin/AutoAdv.aspx"
                    Text="Quảng cáo"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/Alexa.aspx")  %>">
                <asp:HyperLink ID="HyperLink11" class="icn_alexa" runat="server" NavigateUrl="~/admin/Alexa.aspx"
                    Text="Site Alexa"></asp:HyperLink></li>
            <li class="<%= SetClass("admin/SeoConfig.aspx")  %>">
                <asp:HyperLink ID="hpContact1" class="icn_config" runat="server" NavigateUrl="~/admin/SeoConfig.aspx"
                    Text="Site SeoConfig"></asp:HyperLink></li>
        </ul>
    </asp:Panel>
</div>
