<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoDetail.ascx.cs"
    Inherits="usercontrols_ucVideoDetail" %>
<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/swfobject.js" type="text/javascript"></script>
<script src="<%= CurrentPage.UrlRoot %>/Code/jwp58l/jwplayer.js" type="text/javascript"></script>
<pre></pre>
<div id="jwplayer" style="position: relative; width: 641px; height: 391px;">
</div>
<asp:HiddenField ID="hdLink" runat="server" />
<script type="text/javascript">
    jwplayer("jwplayer").setup({
        'flashplayer': "<%= CurrentPage.UrlRoot %>/Code/jwp58l/player.swf",
        'width': '641',
        'height': '391',
        'allowfullscreen': 'true',
        'allowscriptaccess': 'always',
        'wmode': 'opaque',
        'controlbar': 'over',
        'dock': 'true',
        'dock.position': 'left',
        'mute': 'false',
        'stretching': 'uniform',
        'autostart': 'false',
        'file': '<%= hdLink.Value %>',
        'logo.file': '<%= CurrentPage.UrlRoot %>/Code/jwp58l/logo.png',
        'logo.hide': 'false',
        'logo.position': 'top-right',
        'logo.link': 'http://www.hoclaptrinhweb.com',
        'abouttext': 'Hoclaptrinhweb',
        'aboutlink': 'http://www.hoclaptrinhweb.com',
        'skin': '<%= CurrentPage.UrlRoot %>/Code/jwp58l/NewTubeDark.zip'
    });
</script>
