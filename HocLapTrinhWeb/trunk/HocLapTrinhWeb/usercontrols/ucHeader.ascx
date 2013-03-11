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
            <script type="text/javascript">
                var _ad360_id = 2875;
                var _ad360_w = 728;
                var _ad360_h = 90;
                var _ad360_pos = 0;
            </script>
        <script language="javascript" type="text/javascript" src="http://provider.ad360.vn/showads.min.js"></script>
        </div>
    </div>
</header>
