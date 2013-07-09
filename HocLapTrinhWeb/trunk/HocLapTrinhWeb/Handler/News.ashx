<%@ WebHandler Language="C#" Class="News" %>

using System.Web;
using HocLapTrinhWeb.BLL;
using HocLapTrinhWeb.DAL;

public class News : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var con = new Connection();
        con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
        var vnnNewsBll = new vnn_NewsBLL(con);
        var dt = vnnNewsBll.GetNewsAll("NewsTypeName,NewsTypeID,Thumbnail,Title,NewsID", 5, NewsTypeID, 1, "", "", "viewed", "desc");

        var tmpHtml = "<ul class='blog_posts_widget'>";
        if (dt != null)
        {
            for (var i = 0; i < dt.Count; i++)
            {
                tmpHtml += "<li itemscope itemtype='http://schema.org/Article' class='blog_post'>" +
                                "<a href='{linknews}'>" +
                                    "<img itemprop='image' src='{linkimage}' alt='{imgalt}' class='alignleft'>" +
                                 "</a>" +
                                 "<p>" +
                                     "<a itemprop='url' itemprop='name' href='{linknews}'>{titlenews}</a>" +
                                     "<a class='cat' href='{linkcat}'>{titlecat}</a>" +
                                "</p>" +
                               "</li>";
                tmpHtml = tmpHtml.Replace("{linknews}", UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[i].NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[i].Title) + "-hltw" + dt[i].NewsID.ToString() + ".aspx");
                tmpHtml = tmpHtml.Replace("{linkimage}", UrlRoot + "/images/w50-" + dt[i].Thumbnail.ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx");
                tmpHtml = tmpHtml.Replace("{titlenews}", dt[i].Title);
                tmpHtml = tmpHtml.Replace("{titlecat}", dt[i].NewsTypeName);
                tmpHtml = tmpHtml.Replace("{imgalt}", dt[i].Title.Replace("'",""));
                tmpHtml = tmpHtml.Replace("{linkcat}", UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[i].NewsTypeName) + "/hltw" + dt[i].NewsTypeID.ToString() + ".aspx");

            }
        }
        else
            tmpHtml += "Dữ liệu không tồn tại";
        tmpHtml += "</ul>";
        context.Response.ContentType = "text/html";
        context.Response.Write(tmpHtml);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public int NewsTypeID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString["NewsTypeID"] != null)
            {
                try
                {
                    return int.Parse(HttpContext.Current.Request.QueryString["NewsTypeID"]);
                }
                catch { return -1; }
            }
            return -1;
        }
    }

    public string UrlRoot
    {
        get
        {
            return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ((HttpContext.Current.Request.Url.Port == 80) ? "" : (":" + HttpContext.Current.Request.Url.Port)) + ((HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath));
        }
    }

}