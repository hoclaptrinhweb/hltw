<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNavigation.ascx.cs"
    Inherits="usercontrols_ucNavigation" %>
<nav id="navigation">
    <div class="nav_wrap">
        <div class="inner">
            <ul class="nav">
                <li><a class="<%= SetClass("")  %>" href='<%= CurrentPage.UrlRoot %>'>Trang chủ</a></li>
                 <li>
                <a class="<%= SetClass("/video/")  %>" href="<%= CurrentPage.UrlRoot + "/video/" %>"> Video </a>
                </li>
                <li>
                <a href="http://forum.hoclaptrinhweb.com" target="_blank">Diễn đàn</a>
                </li>
               <li><a class="<%= SetClass("/contact.aspx")  %>" href='<%= CurrentPage.UrlRoot + "/contact.aspx" %>'>Liên hệ</a></li>
                 <li><a rel="nofollow" target="_blank" href='http://mediafire.com/hoclaptrinhweb'>Tài liệu</a></li>
                 <li><a href='http://forum.hoclaptrinhweb.com'>Diễn đàn</a></li>
                
                <li>
                <div style="margin-top:8px;">
                <div class="g-plusone" data-size="medium" data-href="http://www.hoclaptrinhweb.com"></div>
                <script type="text/javascript">
                    window.___gcfg = { lang: 'vi' };

                    (function () {
                        var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                        po.src = 'https://apis.google.com/js/plusone.js';
                        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                    })();

                    $(".inner li").hover(
                          function () {
                              if ($(this).children('a').next().length == 1)
                                  $(this).children('a').next().show();
                          },
                          function () {
                              if ($(this).children('a').next().length == 1)
                                  $(this).children('a').next().hide();
                          }
                    );
                </script>
                </div>
                </li>
            </ul>
        </div>
    </div>
</nav>
