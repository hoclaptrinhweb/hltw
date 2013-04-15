<%@ WebHandler Language="C#" Class="ToolTip" %>

using System.Web;
using HocLapTrinhWeb.BLL;
using HocLapTrinhWeb.DAL;

public class ToolTip : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var newsId = -1;
        if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
        {
            try
            {
                newsId = int.Parse(context.Request.QueryString["id"]);
            }
            catch
            {
                newsId = -1;
            }
        }
        var con = new Connection();
        con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
        var vnnNewsBll = new vnn_NewsBLL(con);
        var row = vnnNewsBll.GetNewsAndNewsTypeByID("newsId,Thumbnail,Brief", newsId, 1);

        string tmpHtml;
        if (row != null)
        {
            tmpHtml = "<img class=\"img\" src=\"" + UrlRoot + "/images/w100-" + row.Thumbnail.ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx\" />";
            tmpHtml += "<p>" + row.Brief + "</p>";
        }
        else
            tmpHtml = "Dữ liệu không tồn tại";
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

    public string UrlRoot
    {
        get
        {
            return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ((HttpContext.Current.Request.Url.Port == 80) ? "" : (":" + HttpContext.Current.Request.Url.Port)) + ((HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath));
        }
    }

}