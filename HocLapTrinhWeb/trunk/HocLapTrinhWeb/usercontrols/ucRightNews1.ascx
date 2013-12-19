<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightNews1.ascx.cs"
    Inherits="usercontrols_ucRightNews1" %>

<div class="box_outer">
    <div class="widget">
        <div class="tabbed_widget">
            <ul class="tabbed_nav">
                <li val='0' class="tabbed1"><a rel="nofollow" class="current">Ngẫu nhiên</a></li>
                <li val='1' class="tabbed2"><a rel="nofollow">Xem nhiều</a></li>
            </ul>
            <div class="tabbed_container">
                <div class="tabbed_content" style="display: block;">
                    <ul class="blog_posts_widget">
                        <asp:Repeater ID="rpDataNewsRandom" runat="server">
                            <ItemTemplate>
                                <li itemscope itemtype="http://schema.org/Article" class="blog_post"><a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                                    <img itemprop="image" src="<%# CurrentPage.UrlRoot + "/images/w50-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx" %>"
                                        alt='<%# Eval("Title") %>' class="alignleft"></a>
                                    <p>
                                        <a itemprop="url" itemprop="name" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                                            <%# Eval("Title") %></a><a class="cat" href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/hltw"  + Eval("NewsTypeID") +  ".aspx" %>'>
                                                <%# Eval("NewsTypeName") %></a>
                                    </p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="tabbed_content" style="display: none;">
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $('.tabbed_nav li').click(function () {
        $('.tabbed_nav li a').removeClass();
        var i = $(this).attr('val');
        $(this).children().addClass('current');
        $('.tabbed_content').hide();
        $('.tabbed_content')[i].style.display = '';
        if ($(this).attr('class') == "tabbed2") {
            $('.tabbed_content')[i].innerHTML = "Đang tải dữ liệu !";
            $.ajax({
                type: 'POST',
                url: '<%= CurrentPage.UrlRoot %>/handler/news.ashx?NewsTypeID=<%= NewsTypeID %>',
                contentType: 'application/json; charset=utf-8',
                success: function (msg) {
                    $('.tabbed_content')[i].innerHTML = msg;
                },
                error: function () { }
            });
        }
    });
</script>
