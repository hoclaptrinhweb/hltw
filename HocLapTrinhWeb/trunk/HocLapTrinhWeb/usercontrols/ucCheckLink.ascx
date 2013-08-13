<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCheckLink.ascx.cs" Inherits="usercontrols_ucCheckLink" %>
<script>
    var loaded = 0;
    function close_loader() {
        loaded = 1;
        vtlai_uploader_loading.style.display = 'none';
    }
    function autoRedirect() {
        if (!loaded)
            window.location.replace('<%= ParaUrl %>');
    }
    setTimeout('autoRedirect()', 10000);
</script>
<div id="vtlai_uploader_loading">
    <img border="0" src="<%= CurrentPage.UrlRoot %>/Admin/Images/loading.gif"><br>
    Đang tải dữ liệu...
</div>
<center>
	<iframe onload="close_loader()" onerror="close_loader()" name="cwindow" style="border: 0pt none;" src="<%= ParaUrl %>" rel="nofollow" height="100%" scrolling="AUTO" width="100%">
	Bạn vừa nhấn vào một liên kết trong bài viết trên HocLapTrinhWeb.com. Liên kết này 
	&lt;i&gt;có thể&lt;/i&gt; chưa được kiểm chứng về độ an toàn và HocLapTrinhWeb.com không chịu trách nhiệm về nội dung cũng như những nguy hại tới người 
	dùng có thể gây ra bằng việc chuyển tới liên kết này.
	</iframe>
</center>

<div id="Toolbar">
    <a href="<%= CurrentPage.UrlRoot %>">Quay lại trang chủ</a> | 
	<a title="<%= ParaUrl %>" href="<%= ParaUrl %>" onclick="this.innerHTML='Đang chuyển...'">Chuyển hẳn đến trang đích</a>
</div>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-30339288-1']);
    _gaq.push(['_setDomainName', 'hoclaptrinhweb.com']);
    _gaq.push(['_setAllowLinker', true]);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>



