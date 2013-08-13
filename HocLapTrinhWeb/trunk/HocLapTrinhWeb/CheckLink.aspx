<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckLink.aspx.cs" Inherits="CheckLink" %>

<%@ Register Src="usercontrols/ucCheckLink.ascx" TagName="ucCheckLink" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>Check Link | Hoclaptrinhweb.com</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="language" content="english, en, Vietnamese,vn" />
    <link rel="shortcut icon" href="http://www.hoclaptrinhweb.com/favicon.png" />
    <meta name="ROBOTS" content="NOINDEX,FOLLOW" />
    <style type="text/css">
        #vtlai_uploader_loading
        {
            background: #000;
            background: rgba(0, 0, 0, 0.65);
            filter: alpha(opacity = 65);
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-background-clip: padding;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            font-family: Tahoma;
            font-size: 12px;
            width: 200px;
            left: 50%;
            top: 50%;
            margin-left: -100px;
            margin-top: -33px;
            color: #FFF;
            text-align: center;
            padding: 10px;
            position: fixed;
        }

        #Toolbar
        {
            color: #000000;
            text-decoration: none;
            position: fixed;
            width: 100%;
            z-index: 0;
            left: 0;
            bottom: 0;
            border-top: 1px solid #C0C0C0;
            background-color: #F3F3F3;
            font-family: tahoma;
            font-size: 10pt;
            font-weight: bold;
            text-align: right;
            box-sizing: border-box;
        }

            #Toolbar A
            {
                font-family: tahoma;
                font-size: 10pt;
                font-weight: bold;
                color: #000000;
                text-decoration: none;
                margin: 3px 5px;
                display: inline-block;
            }

                #Toolbar A:hover
                {
                    font-family: tahoma;
                    font-size: 10pt;
                    font-weight: bold;
                    color: #0000FF;
                    text-decoration: underline;
                }
    </style>
</head>
<body style="margin: 0px;">
    <uc1:ucCheckLink ID="ucCheckLink1" runat="server" />
</body>
</html>
