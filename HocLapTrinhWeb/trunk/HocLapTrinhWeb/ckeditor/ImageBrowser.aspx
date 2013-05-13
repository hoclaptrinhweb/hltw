<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageBrowser.aspx.cs" Inherits="ImageBrowserPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Image Browser</title>
    <script src="http://www.hoclaptrinhweb.com/js/jquery.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
        form
        {
            width: 750px;
            background-color: #E3E3C7;
        }
        h1
        {
            padding: 15px;
            margin: 0px;
            padding-bottom: 0px;
            font-family: Arial;
            font-size: 14pt;
            color: #737357;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#ImageList").change(function() {
                $("#Image1").attr('src', '<%= UrlRoot %>/' + $(this).val());
            });
        });

        function  CheckImage() {
            window.top.opener.CKEDITOR.dialog.getCurrent().setValueOf('info', 'txtUrl', encodeURI($("#Image1").attr('src'))); 
            window.top.close(); 
            window.top.opener.focus();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Image Browser</h1>
        <table width="720px" cellpadding="10" cellspacing="0" border="1" style="background-color: #F1F1E3;
            margin: 15px;">
            <tr>
                <td style="width: 396px;" valign="middle" align="center">
                    <asp:Image ID="Image1" runat="server" Style="max-height: 450px; max-width: 380px;" />
                </td>
                <td style="width: 324px;" valign="top">
                    <asp:Panel ID="SearchBox" runat="server" DefaultButton="SearchButton">
                        Search:<br />
                        <asp:TextBox ID="SearchTerms" runat="server" />
                        <asp:Button ID="SearchButton" runat="server" Text="Go" OnClick="Search" UseSubmitBehavior="false" />
                        <br />
                    </asp:Panel>
                    <asp:ListBox ID="ImageList" runat="server" Style="width: 280px; height: 180px;" />
                    <asp:HiddenField ID="NewImageName" runat="server" />
                    <br />
                    <br />
                    Upload Image: (10 MB max)
                    <asp:FileUpload ID="UploadedImageFile" runat="server" />
                    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="Upload" /><br />
                    <br />
                </td>
            </tr>
        </table>
        <center>
            <asp:Button ID="OkButton" runat="server" Text="Ok" OnClientClick="CheckImage()" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClientClick="window.top.close(); window.top.opener.focus();" />
            <br />
            <br />
        </center>
    </div>
    </form>
</body>
</html>
