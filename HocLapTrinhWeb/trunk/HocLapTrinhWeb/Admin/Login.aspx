<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Theme="Admin"
    Title="Đăng nhập | hoclaptrinhweb.com" Inherits="administrator_Login" %>

<%@ Register Src="usercontrols/ucLogin.ascx" TagName="ucLogin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HocLapTrinhWeb.com-Login</title>
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" type="image/png" href="../images/favicon.png" />

    <script type="text/javascript" src="../js/script.js"></script>

    <script type="text/javascript" src="../js/jquery-1.2.6.min.js"></script>

    <link rel="shortcut icon" href="../images/favicon.jpg" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server" class="login">
        <div id="autoheight">
            <uc1:ucLogin ID="UcLogin1" runat="server" />
        </div>
    </form>
</body>
</html>

<script>AutoHeight();</script>

