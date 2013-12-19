<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVideoType.ascx.cs" Inherits="usercontrols_ucVideoType" %>
<%@ Register Src="~/usercontrols/ucTreeView.ascx" TagPrefix="uc1" TagName="ucTreeView" %>

<div class="metro" xmlns:v="http://rdf.data-vocabulary.org/#">
    <uc1:ucTreeView runat="server" ID="ucTreeView" />
</div>

<div style="clear: both;">
</div>

<div class="box-content">
    <ul class="view c-view clear">
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <li>
                    <div class="in2">
                        <div class="thumb c-thumb">
                            <div class="in2">
                                <a class="thumb-link" href="<%# CurrentPage.UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                                    title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                                    <%# Eval("Thumbnail").ToString() == "" ? "" : "<img alt=\"" + Eval("Title").ToString().Replace('"', ' ') + "\" itemprop=\"image\" class=\"left\" src=\"" + (Eval("Thumbnail").ToString().Contains("http://") ? Eval("Thumbnail").ToString() : CurrentPage.UrlRoot + "/images/video/w180-" + Eval("Thumbnail").ToString().ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx") + "\" />"%>
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
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<div class="clear">
</div>

<div class="paging">
    <ul class="pagination" runat="server" id="Paging">
    </ul>
</div>
