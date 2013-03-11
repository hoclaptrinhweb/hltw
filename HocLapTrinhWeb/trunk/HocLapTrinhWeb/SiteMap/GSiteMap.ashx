<%@ WebHandler Language="C#" Class="GSiteMap" %>

using System;
using System.Web;
using HocLapTrinhWeb.BLL;
using System.Xml;

public class GSiteMap : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "utf-8";
        GenerateXML(context.Response.OutputStream, PageIndex, PageSize);
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    #region Method

    public string UrlRoot
    {
        get
        {
            return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ((HttpContext.Current.Request.Url.Port == 80) ? "" : (":" + HttpContext.Current.Request.Url.Port)) + ((HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath));
        }
    }

    private int PageSize
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pagesize"]) == false)
            {
                try
                {
                    return int.Parse(HttpContext.Current.Request.QueryString["pagesize"]);
                }
                catch
                {
                    return 500;
                }
            }
            return 500;
        }
    }

    private int PageIndex
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pageindex"]) == false)
            {
                try
                {
                    return int.Parse(HttpContext.Current.Request.QueryString["pageindex"]);
                }
                catch
                {
                    return 1;
                }
            }
            return 1;
        }
    }

    public void GenerateXML(System.IO.Stream stream, int pageindex, int pagesize)
    {
        var settings = new XmlWriterSettings {Indent = true};

        using (var writer = XmlWriter.Create(stream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            var con = new DH.Data.SqlServer.Connection();
            con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
            var vnnNewsBll = new vnn_NewsBLL(con);
            //Lấy url tin phân trang vào sitemap
            var dt = vnnNewsBll.GetAllNewsForSiteMap("UpdatedDate,NewsTypeName,Title,NewsID", (pageindex - 1) * pagesize, pagesize, -1, 1, "", "");

            if (dt != null && dt.Count > 0)
            {
                foreach (var t in dt)
                {
                    WriteTag("0.5", "", t.UpdatedDate.ToString("yyyy-MM-dd"), UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(t.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(t.Title) + "-hltw" + t.NewsID + ".aspx", writer);
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="priority">Độ ưu tiên của url (0 -> 1). Gợi ý nhưng link menu thì đặt là 1, bình thường thi đặt là 0.5</param>
    /// <param name="freq"></param>
    /// <param name="dateUpdate"></param>
    /// <param name="navigation"></param>
    /// <param name="myWriter"></param>
    private  void WriteTag(string priority, string freq, string dateUpdate, string navigation, XmlWriter myWriter)
    {
        myWriter.WriteStartElement("url");
        myWriter.WriteStartElement("loc");
        myWriter.WriteValue(navigation);
        myWriter.WriteEndElement();

        if (dateUpdate != "")
        {
            myWriter.WriteStartElement("lastmod");
            myWriter.WriteValue(dateUpdate);
            myWriter.WriteEndElement();
        }
        if (freq != "")
        {
            myWriter.WriteStartElement("changefreq");
            myWriter.WriteValue(freq);
            myWriter.WriteEndElement();
        }
        if (priority != "")
        {
            myWriter.WriteStartElement("priority");
            myWriter.WriteValue(priority);
            myWriter.WriteEndElement();
        }
        myWriter.WriteEndElement();
    }
    #endregion

}