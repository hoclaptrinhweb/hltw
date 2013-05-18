<%@ Page Title="Demo ActiveMenu - Học lập trình web" Language="C#" MasterPageFile="~/Code/Demo.master"
    AutoEventWireup="true" CodeFile="ActiveMenu.aspx.cs" Inherits="Code_ActiveMenu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <nav id="navigation">
    <div class="nav_wrap">
        <div class="inner">
            <ul class="nav">
                 <li>
                <a class="<%= SetClass("active=1")  %>" href="<%= UrlRoot + "/code/activemenu.aspx?active=1" %>"> Menu 1 </a>
                </li>
                <li>
                <a class="<%= SetClass("active=2")  %>" href="<%= UrlRoot + "/code/activemenu.aspx?active=2" %>">Menu 2</a>
                </li>
               <li><a class="<%= SetClass("active=3")  %>" href='<%= UrlRoot + "/code/activemenu.aspx?active=3" %>'>Menu 3</a></li>
                 <li><a class="<%= SetClass("active=4")  %>" href='<%= UrlRoot + "/code/activemenu.aspx?active=4"%>'>Menu 4</a></li>
                
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
                </script>
                </div>
                </li>
            </ul>
        </div>
    </div>
</nav>
</asp:Content>
