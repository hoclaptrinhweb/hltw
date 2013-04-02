<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLoginUser.ascx.cs" Inherits="usercontrols_ucLoginUser" %>
<style>
    .single_article_content table span
    {
        color: #555555;
        font: bold 13px/1.48em tahoma;
    }
    .single_article_content table input
    {
        -moz-border-bottom-colors: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #CFCFCF #DDDDDD #DDDDDD;
        border-image: none;
        border-radius: 2px 2px 2px 2px;
        border-style: solid;
        border-width: 1px;
        box-shadow: none;
        color: #666666;
        font-size: 14px;
        margin-bottom: 0;
        padding: 5px 7px;
        width: 292px;
    }
    .single_article_content table input:focus
    {
        -moz-border-bottom-colors: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #F2BA49 #F2C05C #F2C05C;
        border-image: none;
        border-style: solid;
        border-width: 1px;
        box-shadow: 0 0 3px rgba(0, 0, 0, 0.1) inset, 0 1px 2px rgba(0, 0, 0, 0.05) inset, 0 1px #FEFEFE;
    }
    .OKBtn
    {
        cursor: pointer;
        border: 1px solid !important;
        float: left;
        margin: 20px 20px;
        display: inline-block;
        color: #444;
        padding: 5px 25px;
        font: bold 16px arial;
        letter-spacing: -.01em;
        user-select: none;
        -moz-user-select: none;
        -webkit-user-select: none;
        -o-user-select: none;
        border-color: #ddd #ddd #c6c6c6 !important;
        border-radius: 4px;
        -moz-border-radius: 4px;
        -webkit-border-radius: 4px;
        background: none #f2f2f2;
        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#f6f6f6, endColorStr=#f0efef);
        background: -webkit-gradient(linear, 0% 0%, 0 30%, from(#f6f6f6), to(#f0efef)) #f2f2f2;
        background: -webkit-linear-gradient(top , #f6f6f6, #f0efef) #f2f2f2;
        background: -moz-linear-gradient(top , #f6f6f6, #f0efef) #f2f2f2;
        background: -ms-linear-gradient(top , #f6f6f6, #f0efef) #f2f2f2;
        background: -o-linear-gradient(top, #f6f6f6, #f0efef) #f2f2f2; /* Opera 11.10+ */
        box-shadow: 1px 0 0 0 rgba(255, 255, 255, 0.3) inset, 0 1px 0 0 rgba(255, 255, 255, 0.4) inset, 0 -1px 0 0 rgba(255, 255, 255, 0.2) inset, -1px 0 0 0 rgba(255, 255, 255, 0.3) inset;
        -moz-box-shadow: 1px 0 0 0 rgba(255, 255, 255, 0.3) inset, 0 1px 0 0 rgba(255, 255, 255, 0.4) inset, 0 -1px 0 0 rgba(255, 255, 255, 0.2) inset, -1px 0 0 0 rgba(255, 255, 255, 0.3) inset;
        -webkit-box-shadow: 1px 0 0 0 rgba(255, 255, 255, 0.3) inset, 0 1px 0 0 rgba(255, 255, 255, 0.4) inset, 0 -1px 0 0 rgba(255, 255, 255, 0.2) inset, -1px 0 0 0 rgba(255, 255, 255, 0.3) inset;
    }
    .OKBtn:hover
    {
        border: 1px solid #b9b9b9 !important;
        box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fff inset;
        -moz-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fff inset;
        -webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fff inset;
    }
    .OKBtn:active
    {
        padding: 6px 25px 4px;
        border: 1px solid #b9b9b9 !important;
        background: none #d3d7dc;
        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#efeff0, endColorStr=#e2e4e5);
        background: #d3d7dc -webkit-gradient(linear, 0% 0%, 0 30%, from(#efeff0), to(#e2e4e5));
        background: -webkit-linear-gradient(top , #efeff0, #e2e4e5) #d3d7dc;
        background: -moz-linear-gradient(top , #efeff0, #e2e4e5) #d3d7dc;
        background: -ms-linear-gradient(top , #efeff0, #e2e4e5) #d3d7dc;
        background: -o-linear-gradient(top , #efeff0, #e2e4e5) #d3d7dc;
        box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
        -moz-box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
        -webkit-box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
    }
    /*color*/
    .OKBtn.red
    {
        color: #fff;
        text-shadow: 0 1px #670b0e;
        border-color: #eb5e5e #dd5858 #c64444 !important;
        background: none #e15356;
        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#ef605d, endColorStr=#db4d52);
        background: -webkit-gradient(linear, 0% 0%, 0 30%, from(#ef605d), to(#db4d52)) #e15356;
        background: -webkit-linear-gradient(top , #ef605d, #db4d52) #e15356;
        background: -moz-linear-gradient(top , #ef605d, #db4d52) #e15356;
        background: -ms-linear-gradient(top , #ef605d, #db4d52) #e15356;
        background: -o-linear-gradient(top, #ef605d, #db4d52) #e15356; /* Opera 11.10+ */ /*box-shadow: 0 -15px 15px #db4d52 inset, 0 0 0 #ef605d inset, 0 0 0 #ef605d inset;
	-moz-box-shadow: 0 -15px 15px #db4d52 inset, 0 0 0 #ef605d inset, 0 0 0 #ef605d inset;
	-webkit-box-shadow: 0 -15px 15px #db4d52 inset, 0 0 0 #ef605d inset, 0 0 0 #ef605d inset;*/
    }
    .OKBtn.red:hover
    {
        border-color: #c64444 !important;
        background: none #ed6c6d;
        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#f57572, endColorStr=#e76569);
        background: -webkit-gradient(linear, 0% 0%, 0 30%, from(#f57572), to(#e76569)) #ed6c6d;
        background: -webkit-linear-gradient(top , #f57572, #e76569) #ed6c6d;
        background: -moz-linear-gradient(top , #f57572, #e76569) #ed6c6d;
        background: -ms-linear-gradient(top , #f57572, #e76569) #ed6c6d;
        background: -o-linear-gradient(top, #f57572, #e76569) #ed6c6d; /* Opera 11.10+ */
        box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fed8d7 inset;
        -moz-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fed8d7 inset;
        -webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, 0.3), 0 0 3px #fed8d7 inset;
    }
    .OKBtn.red:active
    {
        border-color: #a23939 !important;
        box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
        -moz-box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
        -webkit-box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3) inset;
        background: none #d3d7dc;
        filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#e25b59, endColorStr=#ce4b50);
        background: #d3d7dc -webkit-gradient(linear, 0% 0%, 0 30%, from(#e25b59), to(#ce4b50));
        background: -webkit-linear-gradient(top , #e25b59, #ce4b50) #d3d7dc;
        background: -moz-linear-gradient(top , #e25b59, #ce4b50) #d3d7dc;
        background: -ms-linear-gradient(top , #e25b59, #ce4b50) #d3d7dc;
        background: -o-linear-gradient(top , #e25b59, #ce4b50) #d3d7dc;
    }
    table td
    {
        padding-top: 5px;
    }
</style>
<div class="box_outer">
    <div class="cat_article">
        <h1 class="cat_article_title">
            <a>Đăng nhập vào hệ thống - Hoclaptrinhweb.com</a>
        </h1>
        <div class="article_meta">
            <%--<span class="meta_author">Posted by: <a>Hoclaptrinhweb</a></span>--%>
        </div>
        <div class="brief">
            <asp:ValidationSummary ID="valError" runat="server" EnableClientScript="False" />
            <asp:CustomValidator ID="SaveValidate" runat="server" Display="None" ErrorMessage="CustomValidator" />
        </div>
        <div id="article_content" class="single_article_content">
            <table class="tb">
                <tr>
                    <td>
                        <span class="Caption">Tên đăng nhập</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="Caption">Mật khẩu</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btnSend" CssClass="submitButton OKBtn red" runat="server" Text="Đăng nhập" OnClick="btnSend_Click" />
        </div>
    </div>
</div>
