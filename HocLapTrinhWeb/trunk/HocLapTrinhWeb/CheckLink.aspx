<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckLink.aspx.cs" Inherits="CheckLink" %>

<%@ Register Src="usercontrols/ucCheckLink.ascx" TagName="ucCheckLink" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Link | Hoclaptrinhweb.com</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:ucCheckLink ID="ucCheckLink1" runat="server" />
    </form>
</body>
</html>
