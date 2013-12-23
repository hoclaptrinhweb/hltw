using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucListNewsType : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        // Nếu không gọi databind ở đây
        // Sẽ không lấy được chuỗi kết nối hiện tại
        // Do hàm Page_Load chạy xong hàm bind dữ liệu
        rpNewsType.DataBind();
    }

    protected void rpNewsType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var item = e.Item;
        if ((item.ItemType != ListItemType.Item) && (item.ItemType != ListItemType.AlternatingItem)) return;
        var rpNews = (Repeater)item.FindControl("rpNews");
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRow)((DataRowView)(e.Item.DataItem)).Row;

        var vnnNewsTypeBll = new vnn_NewsTypeBLL(getCurrentConnection());
        var rchildren = vnnNewsTypeBll.GetDataAllChildrenByPathID("NewsTypeName,NewsTypeID,PathID", row.PathID);
        var vnnNewsBll = new NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetNewsAll("NewsTypeName,Brief,Viewed,NewsTypeID,Thumbnail,Title,NewsID", 10, rchildren, 1, "", "", "NewsId", "Desc");
        rpNews.DataSource = dt;
        rpNews.DataBind();
    }

    public string BindData(RepeaterItem item)
    {
        string tmp;
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeRow)((DataRowView)(item.DataItem)).Row;
        if (item.ItemIndex == 0)
        {
            tmp = "<div class='news_box_left'>" +
                        "<div class='recent_news_item'>" +
                            "<h4 itemprop=\"name\" class='recent_news_title'>" +
                                "<a itemprop=\"url\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx'>" + row.Title + "</a></h4>" +
                            "<div class='recent_news_img'>" +
                                "<a href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx'><img itemprop=\"image\" src='" + CurrentPage.UrlRoot + "/images/w93-" + row.Thumbnail.ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx' alt='" + row.Title + "'  /> " +
                                    "</a>" +
                            "</div>" +
                            "<div class='recent_news_content'>" +
                                "<p class='recent_news_excpert' itemprop=\"description\">" +
                                    row.Brief +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
        }
        else
            tmp = "<a rel='" + CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + row.NewsID + "' itemprop=\"name\" itemprop=\"url\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx' title='" + row.Title + "'>" + Global.GetSubContent(row.Title, 45) + "</a>";
        return tmp;
    }

    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath));
        }
    }

}