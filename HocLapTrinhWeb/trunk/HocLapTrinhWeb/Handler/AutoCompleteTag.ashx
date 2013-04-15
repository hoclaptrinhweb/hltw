<%@ WebHandler Language="C#" Class="AutoCompleteTag" %>
using System.Web;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.BLL;

public class AutoCompleteTag : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        var con = new Connection();
        var strhtml = "";
        if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
        {
            var _v_TagBll = new v_TagBLL(con);
            var dt = _v_TagBll.SearchTagByName(KeyWord);
            if (dt != null && dt.Count > 0)
            {
                foreach (var t in dt)
                {
                    strhtml += " { \"id\": \"" + t.TagID + "\", \"label\": \"" + t.TagName + "\", \"value\": \"" + t.TagName + "\" },";
                }
                strhtml = "[" + strhtml.Substring(0, strhtml.Length - 1) + "]";
            }
        }
        context.Response.Write(strhtml);
    }

    private string KeyWord
    {
        get
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["term"]) == false ? HttpContext.Current.Request.QueryString["term"] : "";
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}