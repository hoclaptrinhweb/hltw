<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ucNewsType.ascx.cs"
    Inherits="usercontrols_ucNewsType" %>
<%@ Register Src="~/usercontrols/ucListNewsType.ascx" TagPrefix="uc1" TagName="ucListNewsType" %>

<uc1:ucListNewsType runat="server" ID="ucListNewsType" />
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.more_news a').cluetip({
            width: '400px',
            showTitle: true,
            positionBy: 'topBottom',
            topOffset: 20,
            cluezIndex: 100
        });
    });
</script>
