<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucHeader.ascx.cs"
    Inherits="usercontrols_ucHeader" %>
<header id="header">
    <div class="top_line">
    </div>
    <div class="inner">
        <div class="logo">
            <a href="<%= CurrentPage.UrlRoot %>">
                <img src="<%= CurrentPage.UrlRoot %>/images/logo.png" alt="Hoclaptrinhweb.com" />
            </a>
        </div>
        <div class="top_ad ads360">
            <div id="ddtopmenubar" class="mattblackmenu">
                <ul>
                    <li><a href="<%= CurrentPage.UrlRoot %>" class="blightblue"><i class="fa fa-home"></i>Trang chủ</a></li>
                    <li><a href="<%= CurrentPage.UrlRoot %>/video/" class="bred"><i class="fa fa-video-camera"></i>Video</a></li>
                    <li><a href="http://forum.hoclaptrinhweb.com" target="_blank" class="bgreen"><i class="fa fa-comments"></i>Diễn đàn</a></li>
                    <li><a href="<%= CurrentPage.UrlRoot %>/contact.aspx" class="bblue"><i class="fa fa-envelope-o"></i>Liên hệ</a></li>
                    <li><a href="http://m.hoclaptrinhweb.com" class="bviolet"><i class="fa fa-tablet"></i>Mobile</a>
                    <li><a href="www.youtube.com/user/hoclaptrinhweb" target="_blank" class="borg"><i class="fa fa-youtube"></i>Tutorial</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</header>

<div class="ads">
    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
    <!-- Top Banner 728x90 -->
    <ins class="adsbygoogle"
        style="display: inline-block; width: 728px; height: 90px"
        data-ad-client="ca-pub-5338556037145111"
        data-ad-slot="9888271788"></ins>
    <script>
        (adsbygoogle = window.adsbygoogle || []).push({});
    </script>

    <script type="text/javascript">
        var _ad360_id = 2875;
        var _ad360_w = 728;
        var _ad360_h = 90;
        var _ad360_pos = 0;
    </script>
    <script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
</div>
<style>
    .mattblackmenu ul {
        background: transparent;
        border-bottom: none;
        margin: 0;
        padding: 0;
        font: bold 12px Verdana;
        list-style-type: none;
        overflow: hidden;
        width: 100%;
    }

    .mattblackmenu li {
        display: inline;
        margin: 0;
        float: left;
    }

        .mattblackmenu li a {
            width: 98px;
            height: 98px;
            text-align: center;
            padding: 5px !important;
            border-right: 0px !important;
            margin: 0px 2px;
            font-size: 16px;
            float: left;
        }

            .mattblackmenu li a > i {
                display: block;
                font-size: 30px;
                width: 30px;
                margin: 0 auto;
                margin-bottom: 10px;
                margin-top: 18px;
            }

    .blightblue {
        background: #52b9e9 !important;
        color: #fff !important;
        border: 0px !important;
    }

    .bgreen {
        background: #43c83c !important;
        color: #fff !important;
        border: 0px !important;
    }

    .bviolet {
        background: #932ab6 !important;
        color: #fff !important;
        border: 0px !important;
    }

    .bblue {
        background: #1171a3 !important;
        color: #fff !important;
        border: 0px !important;
    }

    .bred {
        background: #fa3031 !important;
        color: #fff !important;
        border: 0px !important;
    }

    .borg {
        background: #FF7200 !important;
        color: #fff !important;
        border: 0px !important;
    }
</style>
