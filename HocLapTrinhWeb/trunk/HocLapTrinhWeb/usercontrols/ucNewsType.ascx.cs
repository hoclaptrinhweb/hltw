using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Data;
using System.Collections;

public partial class usercontrols_ucNewsType : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack)
            return;
        SetBase();
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var notNewsTypeID = new ArrayList { 14, 22, 23, 34, 36 };
        var dt = vnnNewsTypeBll.GetNewsTypeByParentID("Description,NewsTypeName,NewsTypeID,PathID", -1, notNewsTypeID);
        rpNewsType.DataSource = dt;
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

    public string BindAds(RepeaterItem item)
    {
        var tmp = "";

        if (item.ItemIndex % 3 == 0 && item.ItemIndex == 0)
        {
            tmp = "<div id=\"adVatgia_block_0" + item.ItemIndex + "\"></div>";
            tmp += "<script type=\"text/javascript\">" +
                        "adblock.adblock0" + item.ItemIndex + " = {" +
                            "'divIdShow': 'adVatgia_block_0" + item.ItemIndex + "'," +
                            "'numAd': 3," +
                            "'typeShow': 1," +
                            "'style': { 'width': 651 }" +
                        "};" +
                    "</script>";
        }
        return tmp;
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
                //"<div class='nb_meta' >" +
                //    "<span class='news_date'></span><span class='news_comments_count'><a itemprop=\"review\">(" + row.Viewed + ") xem</a></span>" +
                //"</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
        }
        else
            tmp = "<a rel='" + CurrentPage.UrlRoot + "/Handler/tooltip.ashx?id=" + row.NewsID + "' itemprop=\"name\" itemprop=\"url\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" + XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx' title='" + row.Title + "'>" + Global.GetSubContent(row.Title, 45) + "</a>";
        return tmp;
    }

    public void SetBase()
    {
        var lrBase = (Literal)Page.Master.FindControl("lrBase");
        if (lrBase != null)
            lrBase.Text = "<base href='" + CurrentPage.UrlRoot + "/'></base>";
    }

}