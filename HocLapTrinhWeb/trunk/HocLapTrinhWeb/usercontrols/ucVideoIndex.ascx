<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoIndex.ascx.cs"
    Inherits="usercontrols_ucVideoIndex" %>
<asp:Repeater ID="rpVideoType" runat="server" OnItemDataBound="rpVideoType_ItemDataBound">
    <ItemTemplate>
        <div class="box_outer">
            <div class="news_box">
                <div class="news_box_heading bviolet">
                    <div class="nb_dots">
                        <h2><a title='<%# Eval("Description") %>' href='<%# CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/hltw" + Eval("VideoTypeID") + ".aspx" %>'><%# Eval("videotypename") %></a></h2>
                    </div>
                </div>
                <div class="box-content">
                    <ul class="view c-view clear">
                        <asp:Repeater ID="rpVideo" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="in2">
                                        <div class="thumb c-thumb">
                                            <div class="in2">
                                                <a class="thumb-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                                    <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : "http://www.hoclaptrinhweb.com" + "/images/video/w180-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") + "\" />"%>
                                                    <span class="icon_plays">&nbsp;</span>
                                                </a>
                                                <div class="ticker">
                                                    <span title="Thời lượng">01:30</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="title">
                                            <div class="in2">
                                                <a class="title-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                                    <%# Eval("Title") %></a>
                                            </div>
                                        </div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>

    </ItemTemplate>
</asp:Repeater>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('.more_video a').cluetip({
            width: '400px',
            showTitle: true,
            positionBy: 'topBottom',
            topOffset: 20,
            cluezIndex: 100
        });
    });
</script>
