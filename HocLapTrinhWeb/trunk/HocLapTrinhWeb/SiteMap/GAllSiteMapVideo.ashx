<%@ WebHandler Language="C#" Class="GAllSiteMapVideo" %>

using System.Web;
using System.Xml;
using HocLapTrinhWeb.BLL;

public class GAllSiteMapVideo : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        context.Response.Charset = "utf-8";
        GenerateXML(context.Response.OutputStream, PageSize);
        context.Response.End();
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

    public void GenerateXML(System.IO.Stream stream, int pagesize)
    {
        var settings = new XmlWriterSettings { Indent = true };
        using (var writer = XmlWriter.Create(stream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("sitemapindex", "http://www.google.com/schemas/sitemap/0.84");
            var con = new HocLapTrinhWeb.DAL.Connection();
            con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
            var vVideoBll = new v_VideoBLL(con);
            //Tính tổng số page để tạo số sitemap con
            var n = vVideoBll.GetAllVideoRowCount("", -1, 1, "", "", "");
            if (n > 0)
            {
                var nSumOfPage = (n - 1) / pagesize + 1;
                for (var i = 0; i < nSumOfPage; i++)
                {
                    WriteTag("", "", "", UrlRoot + "/sitemap/GSiteMapVideo.ashx?pageindex=" + (i + 1) + (PageSize != 500 ? "&pagesize=" + pagesize : ""), writer);
                }
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    private void WriteTag(string priority, string freq, string dateUpdate, string navigation, XmlWriter myWriter)
    {
        myWriter.WriteStartElement("sitemap");
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

}