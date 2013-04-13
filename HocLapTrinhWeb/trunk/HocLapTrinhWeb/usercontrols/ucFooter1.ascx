<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucFooter1.ascx.cs"
    Inherits="usercontrols_ucFooter1" %>
<footer id="footer">
<div class="footer_wrap">
    <div class="inner">
        <div>
            <ul class="menu">
				<li class="menu_title"><h3>Hoclaptrinhweb.com</h3></li>
				<li><a href="<%= CurrentPage.UrlRoot %>">Trang chủ</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/upnews.aspx">Đăng tin</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/contact.aspx">Liên hệ</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/lienket.aspx">Liên kết</a></li>
                <li><a href="<%= CurrentPage.UrlRoot %>/rss/rss.aspx">RSS</a></li>
				<li><a href="http://mediafire.com/hoclaptrinhweb" target="_blank">Tài liệu</a></li>
			</ul>
            <ul class="menu">
				<li class="menu_title"><h3>Chuyên mục</h3></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/phan-mem-tien-ich-hay/hltw22.aspx">Phần mềm - tiện ích hay</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/thu-thuat-meo-vat/hltw21.aspx">Thủ thuật mẹo vặt</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/tin-cong-nghe/hltw20.aspx">Tin công nghệ</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/game/hltw35.aspx">Game</a></li>
                <li><a href="<%= CurrentPage.UrlRoot %>/video/">Video</a></li>
			</ul>
            <ul class="menu">
				<li class="menu_title"><h3>Chuyên mục</h3></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/lap-trinh-di-dong/hltw39.aspx">Lập trình di động</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/bao-mat-website/hltw11.aspx">Bảo mật website</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/phat-trien-web/hltw18.aspx">Phát triển web</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/do-hoa/hltw19.aspx">Đồ họa</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/seo/hltw12.aspx">SEO</a></li>
			</ul>
            <ul class="menu">
				<li class="menu_title"><h3>Chuyên mục</h3></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/javascript-jquery-ajax/hltw13.aspx">Javascript - Jquery - Ajax</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/html5-canvas/hltw10.aspx">HTML5 - CANVAS</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/html-xml/hltw33.aspx">Html - xml</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/asp-net/hltw4.aspx">Asp.net</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/php/hltw6.aspx">Php</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/css/hltw9.aspx">Css</a></li>
			</ul>
            <ul class="menu">
				<li class="menu_title"><h3>Chuyên mục</h3></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/wcf-web-service/hltw28.aspx">WCF / Web Service</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/sql-server/hltw38.aspx">Sql Server</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/wordpress/hltw16.aspx">Wordpress</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/ado-net/hltw26.aspx">ADO.NET</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/joomla/hltw17.aspx">Joomla</a></li>
				<li><a href="<%= CurrentPage.UrlRoot %>/linq/hltw27.aspx">LINQ</a></li>
			</ul>
            <ul class="menu">
                <li class="menu_title"><h3>Liên kết & hợp tác</h3></li>
				<li><a href="http://www.facebook.com/groups/hoclaptrinhweb/" target="_blank">Group Facebook</a></li>
                <li><a href="http://www.facebook.com/hoclaptrinhweb" target="_blank">Page Facebook</a></li>
				<li><a href="http://www.deptainha.com" target="_blank">Đẹp tại nhà</a></li>
				<li><a href="http://apkviet.com/" target="_blank">APK Việt</a></li>
				<li><a href="http://www.vietbando.com" target="_blank">Bản đồ</a></li>
			</ul>
        </div>
    </div>
</div>
<asp:Literal ID="lrIframe" runat="server"></asp:Literal>
</footer>
