<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucListVideo.ascx.cs" Inherits="usercontrols_ucListVideo" %>
<asp:Repeater ID="rpData" runat="server">
    <ItemTemplate>
        <li>
            <div class="in2">
                <div class="thumb c-thumb">
                    <div class="in2">
                        <a class="thumb-link" href="<%# UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
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
                        <a class="title-link" href="<%# UrlRoot + "/video/" +  XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/"  + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw"  + Eval("VideoID") +  ".aspx" %>"
                            title="<%# Eval("Title").ToString().Replace('"',' ') %>">
                            <%# Eval("Title") %></a>
                    </div>
                </div>
            </div>
    </ItemTemplate>
</asp:Repeater>