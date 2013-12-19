<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTreeView.ascx.cs" Inherits="usercontrols_ucTreeView" %>
<style type="text/css">
    .zonetab
    {
        position: absolute;
        top: 0;
        right: 0;
    }

    .metro
    {
        float: left;
        margin-bottom: 10px;
        width: 100%;
        position: relative;
    }

        .metro .breadcrumbs
        {
            margin: 0.2em;
        }

            .metro .breadcrumbs ul
            {
                margin: 0;
                padding: 0;
                list-style: none;
                overflow: hidden;
                width: 100%;
            }

                .metro .breadcrumbs ul li
                {
                    float: left;
                    margin: 0 .2em 0 1em;
                }

                    .metro .breadcrumbs ul li:first-child
                    {
                        margin-left: 0 !important;
                    }

                        .metro .breadcrumbs ul li:first-child a:before
                        {
                            content: normal;
                        }

                    .metro .breadcrumbs ul li:last-child
                    {
                        background: none;
                    }

                        .metro .breadcrumbs ul li:last-child a
                        {
                            color: #1d1d1d;
                        }

                        .metro .breadcrumbs ul li:last-child:after,
                        .metro .breadcrumbs ul li:last-child:before
                        {
                            content: normal;
                        }

                .metro .breadcrumbs ul a
                {
                    background: #d9d9d9;
                    padding: .3em 1em;
                    float: left;
                    text-decoration: none;
                    color: #2e92cf;
                    position: relative;
                }

                    .metro .breadcrumbs ul a:hover
                    {
                        background: #1ba1e2;
                        color: #ffffff;
                    }

                        .metro .breadcrumbs ul a:hover:before
                        {
                            border-color: #1ba1e2 #1ba1e2 #1ba1e2 transparent;
                        }

                        .metro .breadcrumbs ul a:hover:after
                        {
                            border-left-color: #1ba1e2;
                        }

                    .metro .breadcrumbs ul a:before
                    {
                        content: "";
                        position: absolute;
                        top: 50%;
                        margin-top: -1.5em;
                        border-width: 1.5em 0 1.5em 1em;
                        border-style: solid;
                        border-color: #ddd #ddd #ddd transparent;
                        left: -1em;
                        margin-left: 1px;
                    }

                    .metro .breadcrumbs ul a:after
                    {
                        content: "";
                        position: absolute;
                        top: 50%;
                        margin-top: -1.5em;
                        border-top: 1.5em solid transparent;
                        border-bottom: 1.5em solid transparent;
                        border-left: 1em solid #ddd;
                        right: -1em;
                        margin-right: 1px;
                    }

                .metro .breadcrumbs ul .active,
                .metro .breadcrumbs ul .active:hover
                {
                    background: none;
                }

                    .metro .breadcrumbs ul .active a,
                    .metro .breadcrumbs ul .active:hover a
                    {
                        color: #1d1d1d;
                    }

                    .metro .breadcrumbs ul .active:hover a
                    {
                        color: #ffffff;
                    }

                    .metro .breadcrumbs ul .active:after,
                    .metro .breadcrumbs ul .active:before
                    {
                        content: normal;
                    }

    .button
    {
        padding: 4px 12px;
        text-align: center;
        vertical-align: middle !important;
        background-color: #d9d9d9;
        border: 1px transparent solid;
        color: #222222;
        border-radius: 0;
        cursor: pointer;
        display: inline-block;
        outline: none;
        font-family: 'Segoe UI Light_', 'Open Sans Light', Verdana, Arial, Helvetica, sans-serif;
        font-size: 14px;
        line-height: 16px;
    }

        .button.danger
        {
            background-color: #008287 !important;
            color: #ffffff !important;
        }

        .button a
        {
            color: white;
        }
</style>
<nav class="breadcrumbs">
    <ul itemprop="breadcrumb">
        <li typeof="v:Breadcrumb"><a rel="v:url" property="v:title" href='<%= CurrentPage.UrlRoot %>'><i class="fa fa-home"></i>Trang chủ</a></li>
        <asp:Literal ID="lrTreeView" runat="server"></asp:Literal>
    </ul>
</nav>
