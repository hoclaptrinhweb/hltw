using System;
using System.Data;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucNewsStick : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
            LoadData();
    }

    public void LoadData()
    {
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetNewsAll("NewsTypeName,NewsTypeID,Thumbnail,Title,NewsID", 6, NewsTypeID, 1, 1, "", "", "NEWID()", "");
        rpDataNewsRandom.DataSource = dt;
        rpDataNewsRandom.DataBind();

        var dts = vnnNewsBll.GetAllNewsForRepeater("NewsTypeName,Title,NewsID,RefAddress,UpdatedDate,Viewed,Thumbnail,Brief", 3, NewsTypeID, 1, "", "", "NewsID", "Desc");
        rpData.DataSource = dts;
        rpData.DataBind();
    }

    public string BindData(RepeaterItem item)
    {
        var tmp = "";
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsRow)((DataRowView)(item.DataItem)).Row;
        if (item.ItemIndex == 0)
        {
            tmp = "<li class='topnews'><a href='" + CurrentPage.UrlRoot + "/" +
                  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" +
                  XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx' title='" +
                  Eval("Title").ToString().Replace("'", "") + "'>" +
                  "<img class='newsphoto_small' src='" + CurrentPage.UrlRoot + "/images/w93-" +
                  row.Thumbnail.ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx' alt='" +
                  Eval("Title").ToString().Replace("'", "") + "' />" +
                  "<div>" +
                  "<h1>" + Eval("Title") + "</h1>" +
                  "<p class='chapeau'>" + Eval("Brief") + "</p>" +
                  "</div>" +
                  "</a></li>";
        }
        else
        {
            if (item.ItemIndex == 1)
                tmp = "<li>";
            tmp += "<h1><a href='" + CurrentPage.UrlRoot + "/" +
                  XuLyChuoi.ConvertToUnSign(Eval("NewsTypeName").ToString()) + "/" +
                  XuLyChuoi.ConvertToUnSign(Eval("Title").ToString()) + "-hltw" + Eval("NewsID") + ".aspx' title='" +
                  Eval("Title").ToString().Replace("'", "") + "'>" + Eval("Title") + "</a></h1>";
            if (item.ItemIndex == (((System.Data.DataRowView)(item.DataItem)).Row).Table.Rows.Count - 1)
                tmp += "</li>";
        }
        return tmp;
    }

    public int NewsTypeID
    {
        get
        {
            if (Request.QueryString["NewsTypeID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["NewsTypeID"]);
                }
                catch { return -1; }
            }

            if (Request.QueryString["NewsID"] == null)
                return -1;
            try
            {
                var vNewsBll = new vnn_NewsBLL(getCurrentConnection());

                var rowNews = vNewsBll.GetNewsByID("", int.Parse(Request.QueryString["NewsID"].ToString()), 1);
                if (rowNews != null)
                    return rowNews.NewsTypeID;
                return -1;
            }
            catch
            {
                return -1;
            }
        }
    }
}