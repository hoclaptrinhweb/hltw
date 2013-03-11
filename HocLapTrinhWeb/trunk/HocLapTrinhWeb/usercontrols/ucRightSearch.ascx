<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucRightSearch.ascx.cs"
    Inherits="usercontrols_ucRightSearch" %>
<%--<div class="box_outer">
    <div class="widget">
        <div class="mom_social_counter">
            <div class="sc_rss sc_item">
                <div class="social_box">
                    <a href="http://www.hoclaptrinhweb.com/rss/rss.aspx">
                        <img alt="rss" src="<%= CurrentPage.UrlRoot %>/images/widgets/sc_rss.png" />
                    </a>
                </div>
            </div>
            <div class="sc_twitter sc_item">
                <div class="social_box">
                    <a href="https://twitter.com/hoclaptrinhweb" target="_blank">
                        <img alt="twitter" src="<%= CurrentPage.UrlRoot %>/images/widgets/sc_twitter.png" />
                    </a>
                </div>
            </div>
            <div class="sc_facebook sc_item">
                <div class="social_box">
                    <a href="http://facebook.com/hoclaptrinhweb" target="_blank">
                        <img alt="facebook" src="<%= CurrentPage.UrlRoot %>/images/widgets/sc_facebook.png" />
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>--%>
<div class="newsletter">
    <img alt="rss" src="<%= CurrentPage.UrlRoot %>/images/rss_icon.png" class="rs_icon" />
    <input type="text" onblur="if(this.value=='')this.value='Your Email';" onfocus="if(this.value=='Your Email')this.value='';"
        value="Your Email" name="email" class="nsf">
    <input type="hidden" value="en_US" name="loc">
    <input type="button" value="Subscribe" class="nsb">
</div>
