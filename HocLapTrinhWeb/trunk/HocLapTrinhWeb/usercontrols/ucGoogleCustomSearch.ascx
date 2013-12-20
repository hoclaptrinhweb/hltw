<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGoogleCustomSearch.ascx.cs"
    Inherits="usercontrols_ucGoogleCustomSearch" %>
<style type="text/css">
    input.gsc-search-button
    {
        cursor: pointer;
    }
    .gsc-control-cse
    {
        padding: 0px;
        background: none !important;
        border: none !important;
    }
    input.gsc-input
    {
        width: 300px;
    }
    .gsc-results-wrapper-nooverlay
    {
        display: inline;
        left: -216px;
        line-height: normal;
        width: 647px;
        background: white;
        position: absolute !important;
    }
    .gssb_c
    {
        margin-top:10px;
    }
    .googlesearch table, .googlesearch div
    {
        padding: 0px;
        margin: 0px;
    }
    td.gsc-twiddleRegionCell
    {
        display: none;
    }
    .gsc-results
    {
        height: 500px;
        overflow: auto;
    }
    .gs-title, .gs-title b
    {
        font-size: 13px !important;
        display: inline;
    }
    .gsc-wrapper
    {
        border-bottom: 1px solid;
        border-left: 1px solid;
        border-right: 1px solid;
    }
    .gsc-above-wrapper-area
    {
        border-left: 1px solid;
        border-right: 1px solid;
    }
    .gsc-clear-button {
        margin-left: 10px !important;
        padding-left: 5px !important;
    }
    form.gsc-search-box {
        width: 425px !important;
        background: none !important;
    }
    .gsc-control-wrapper-cse {
        background: none !important;
    }
</style>
<div class="googlesearch">
    <div class="googleresult">
        <script>
            (function () {
                var cx = '017169870419036937743:WMX1028136485';
                var gcse = document.createElement('script'); gcse.type = 'text/javascript'; gcse.async = true;
                gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
        '//www.google.com/cse/cse.js?cx=' + cx;
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(gcse, s);
            })();
        </script>
        <!-- Place this tag where you want both of the search box and the search results to render -->
        <gcse:search></gcse:search>
    </div>
</div>
