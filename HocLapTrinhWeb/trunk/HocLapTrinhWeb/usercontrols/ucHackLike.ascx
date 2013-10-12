<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHackLike.ascx.cs" Inherits="usercontrols_ucHackLike" %>

<div id='fb-root'>
</div>
<div id="icontainer">
    <iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Ffacebook.com%2Fhoclaptrinhweb&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;font=tahoma&amp;colorscheme=light&amp;height=80"
        scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 50px; height: 23px; position: absolute; left: -30px;"
        allowtransparency="true" id="fbframe"
        name="fbframe"></iframe>
</div>
<%= Combres.WebExtensions.CombresLink("DefaultThemeFB")  %>