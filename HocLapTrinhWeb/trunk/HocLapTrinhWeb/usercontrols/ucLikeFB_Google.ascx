<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLikeFB_Google.ascx.cs"
    Inherits="usercontrols_ucLikeFB_Google" %>
<div style="float: left; position: relative">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td>
                <!-- Place this tag where you want the +1 button to render -->
                <g:plusone size="medium"></g:plusone>
                <!-- Place this render call where appropriate -->

                <script type="text/javascript">
                    window.___gcfg = { lang: 'vi' };
                    (function () {
                        var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                        po.src = 'https://apis.google.com/js/plusone.js';
                        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                    })();
                </script>

            </td>
            <td style="padding-left: 10px">
                <div class="fb-like" data-send="false" href="http://facebook.com/hoclaptrinhweb" data-layout="button_count" data-width="80"
                    data-show-faces="true">
                </div>
            </td>
        </tr>
    </table>
    <style type="text/css">
        #___plusone_0
        {
            width: 60px !important;
        }
    </style>
</div>
