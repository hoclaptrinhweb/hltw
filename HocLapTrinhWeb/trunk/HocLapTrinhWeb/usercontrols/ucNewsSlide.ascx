<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNewsSlide.ascx.cs" Inherits="usercontrols_ucNewsSlide" %>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        // main slider
        $('.slider').cycle({
            fx: 'fade',
            speed: 300,
            timeout: 5000,
            pause: 1,
            cleartype: true,
            cleartypeNoBg: true,
            pager: 'ul.slider_nav',
            after: featureAfter,
            before: onbefore,
            pagerAnchorBuilder: function (idx, slide) {
                return 'ul.slider_nav li:eq(' + (idx) + ')';
            }
        });

        $('ul.slider_nav li').hover(function () {
            $('.slider').cycle('pause');

        }, function () {
            $('.slider').cycle('resume');

        });

        function featureAfter() {
            $('.slider_items .slider_caption').stop().animate({ opacity: 1, bottom: 0 }, { queue: false, duration: 300 });
            $('.feature_video_icon, .feature_slide_icon, .feature_article_icon').stop().animate({ top: 0 }, { queue: true, duration: 300 });
        }

        function onbefore() {
            $('.slider_items .slider_caption').stop().animate({ opacity: 1, bottom: '-120px' }, { queue: false, duration: 300 });
            $('.feature_video_icon, .feature_slide_icon, .feature_article_icon').animate({ top: '-40px' }, { queue: true, duration: 300 });
        }
        jQuery('.slider_nav li:not(.activeSlide) a').click(
                            function () {
                                jQuery('.slider_nav li a').css('opacity', 0.7);
                                jQuery(this).css('opacity', 1);
                            }
                    );

        jQuery('.slider_nav li:not(.activeSlide) a').hover(
                            function () {
                                jQuery(this).stop(true, true).animate({ opacity: 1 }, 300);
                            }, function () {
                                jQuery(this).stop(true, true).animate({ opacity: 0.7 }, 300);
                            }
                    );

    });

</script>
<div class="box_outer" id="feature_outer">
    <div class="Feature_news">
        <div class="slider_wrap">
            <div class="slider_items">
                <div class="slider">
                    <asp:Repeater ID="rpSlide" runat="server">
                        <ItemTemplate>
                            <div class="slider_item">
                                <div style="position: relative; overflow: hidden;">
                                    <a href="<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>">
                                        <img src="<%# CurrentPage.UrlRoot + "/images/" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx?w=615" %>"
                                            alt="<%# Eval("Title") %>" />
                                        <span class='feature_article_icon'></span></a>
                                    <div class="slider_caption">
                                        <h2>
                                            <a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                                                <%# Eval("Title") %></a></h2>
                                        <p>
                                            <%# Eval("Brief") %>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <ul class="slider_nav">
                <asp:Repeater ID="rpImages" runat="server">
                    <ItemTemplate>
                        <li><a href='<%# CurrentPage.UrlRoot + "/" +  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("NewsID") +  ".aspx" %>'>
                            <img src="<%# CurrentPage.UrlRoot + "/images/" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx?w=76&h=59" %>"
                                alt="<%# Eval("Title") %>" alt="<%# Eval("Title") %>" title="<%# Eval("Title") %>" />
                        </a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clear">
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    jQuery(document).ready(function($) {
        var vids = $(".lates_video_item");
        for(var i = 0; i < vids.length; i+=4) {
          vids.slice(i, i+4).wrapAll('<div class="four_items"></div>');
        }

       $('.scroll_items').cycle({
                fx: 'scrollHorz',
                easing: 'swing',
                speed: 300,
                timeout:0,
                pause: 1,
                cleartype: true,
                cleartypeNoBg: true,
                pager: '.navi .navi_links',
        });
    }); 

</script>
