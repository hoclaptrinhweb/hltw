using System;
using System.Globalization;
using HocLapTrinhWeb.BLL;
using DH.Utilities;

public partial class usercontrols_ucRssDetail : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!string.IsNullOrEmpty(Request.QueryString["NewsTypeID"]))
            GetRSS(Convert.ToInt32(Request.QueryString["NewsTypeID"]));
    }

    public int PageSize
    {
        get
        {
            try
            {
                return Request.QueryString["pagesize"] != null ? int.Parse(Request.QueryString["pagesize"]) : 50;
            }
            catch
            {
                return 50;
            }
        }
    }

    private void GetRSS(int newsType)
    {
        try
        {
            var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
            var dt = vnnNewsBll.GetAllNewsForGridView(0, PageSize, "Title,NewsID,Thumbnail,Brief,CreatedDate,Content", newsType, 1, "", "", "");
            if (dt == null) return;
            var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
            var row = vnnNewsTypeBll.GetNewsTypeByID(newsType);
            var rss = new RSS();
            var channel = new RSS.RssChannel
                {
                    Title = row.NewsTypeName + " - hoclaptrinhweb.com",
                    Link =
                        CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(row.NewsTypeName) + "/hltw" +
                        row.NewsTypeID.ToString(CultureInfo.InvariantCulture) + ".aspx",
                    Description = row.IsDescriptionNull() ? "" : row.Description
                };
            rss.AddRssChannel(channel);

            foreach (var t in dt)
            {
                try
                {
                    var item = new RSS.RssItem
                        {
                            Title = t.Title,
                            Link =
                                CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(row.NewsTypeName) + "/" +
                                XuLyChuoi.ConvertToUnSign(t.Title) + "-hltw" + t.NewsID + ".aspx"
                        };
                    if (string.IsNullOrEmpty(Request.QueryString["auto"]))
                        item.Description = @"<a href='" + item.Link + "' alt='" + t.Title.Replace('"', ' ') + "'><img  border='0' align='left' src='" + CurrentPage.UrlRoot + "/images/w149-" + (t.IsThumbnailNull() ? "" : (t.Thumbnail.ToLower().Replace(Global.ImagesNews.ToLower(), ""))) + ".ashx' alt='" + t.Title + "'/></a>" + (t.Brief.Length > 300 ? t.Brief.Substring(0, 300) : t.Brief);
                    else
                        item.Description = @"<p style='text-align: center;'><img  border='0' align='left' src='" + CurrentPage.UrlRoot + "/" + (t.IsThumbnailNull() ? "" : t.Thumbnail) + "' alt='" + t.Title + "'/></p><br/>" + t.Brief + "<br/>" + t.Content;
                    item.pubDate = t.CreatedDate.ToString("r");
                    rss.AddRssItem(item);
                }
                catch (Exception)
                {

                }
            }
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(rss.RssDocument);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
}
