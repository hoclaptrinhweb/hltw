using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Data;
using System.Collections;

public partial class usercontrols_ucVideoIndex : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var notVideoTypeID = new ArrayList { 4 };
        var dt = vnnVideoTypeBll.GetVideoTypeByParentID("Description,VideoTypeName,VideoTypeID,PathID", -1, notVideoTypeID);
        rpVideoType.DataSource = dt;
        rpVideoType.DataBind();
    }

    protected void rpVideoType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var item = e.Item;
        if ((item.ItemType != ListItemType.Item) && (item.ItemType != ListItemType.AlternatingItem)) return;
        var rpVideo = (Repeater)item.FindControl("rpVideo");
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeRow)((DataRowView)(e.Item.DataItem)).Row;

        var vnnVideoTypeBll = new vnn_VideoTypeBLL(getCurrentConnection());
        var rchildren = vnnVideoTypeBll.GetDataAllChildrenByPathID("VideoTypeName,VideoTypeID,PathID", row.PathID);
        var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        var dt = vnnVideoBll.GetVideoAll("VideoTypeName,Brief,Viewed,VideoTypeID,Thumbnail,Title,VideoID", 6, rchildren, 1, "", "", "VideoId", "Desc");
        rpVideo.DataSource = dt;
        rpVideo.DataBind();
    }

    public string BindData(RepeaterItem item)
    {
        string tmp;
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoRow)((DataRowView)(item.DataItem)).Row;
        if (item.ItemIndex == 0)
        {
            tmp = "<div class='video_box_left'>" +
                        "<div class='recent_video_item'>" +
                            "<h4 itemprop=\"name\" class='recent_video_title'>" +
                                "<a itemprop=\"url\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("VideoID") + ".aspx'>" + row.Title + "</a></h4>" +
                            "<div class='recent_video_img'>" +
                                "<a href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("VideoID") + ".aspx'><img itemprop=\"image\" src='" + CurrentPage.UrlRoot + "/images/w93-" + row.Thumbnail.ToLower().Replace(Global.ImagesVideo.ToLower(), "") + ".ashx' alt='" + row.Title + "'  /> " +
                                    "</a>" +
                            "</div>" +
                            "<div class='recent_video_content'>" +
                                "<p class='recent_video_excpert' itemprop=\"description\">" +
                                    row.Brief +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
        }
        else
            tmp = "<a rel='" + CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + row.VideoID + "' itemprop=\"name\" itemprop=\"url\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("VideoTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("VideoID") + ".aspx' title='" + row.Title + "'>" + Global.GetSubContent(row.Title, 45) + "</a>";
        return tmp;
    }
}