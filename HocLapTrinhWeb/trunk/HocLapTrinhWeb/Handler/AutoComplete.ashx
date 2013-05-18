<%@ WebHandler Language="C#" Class="AutoComplete" %>

using System.Web;
using HocLapTrinhWeb.BLL;
using HocLapTrinhWeb.DAL;

public class AutoComplete : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        var con = new Connection();
        var strhtml = "";
        if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
        {
            var vUserBll = new v_UserBLL(con);
            var dt = vUserBll.SearchUserByName(10, KeyWord);
            if (dt != null && dt.Count > 0)
            {
                foreach (var t in dt)
                {
                    strhtml += " { \"id\": \"" + t.UserID + "\", \"label\": \"" + t.UserName + "\", \"value\": \"" + t.UserName + "\" },";
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}