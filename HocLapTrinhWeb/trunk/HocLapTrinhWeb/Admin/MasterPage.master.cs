using System;
using System.Configuration;
using HocLapTrinhWeb.BLL;

public partial class administrator_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            if (Request.Cookies["UserName"] != null)
            {
                if (!Login())
                    Response.Redirect("~/admin/Login.aspx?nextpage=" + Server.UrlEncode(Request.Url.PathAndQuery));
                return;
            }
            Response.Redirect("~/admin/Login.aspx?nextpage=" + Server.UrlEncode(Request.Url.PathAndQuery));
        }
    }


    /// <summary>
    /// Check login bang cookie
    /// </summary>
    /// <returns></returns>
    private bool Login()
    {
        try
        {
            var iconnect = new DH.Data.SqlServer.Connection();
            if (!iconnect.CreateConnection(ConfigurationManager.ConnectionStrings["cs_sqlserver"].ToString(), ConfigurationManager.AppSettings["Key"], ConfigurationManager.AppSettings["ValidKey"]))
                return false;

            var userBll = new ltk_UserBLL(iconnect);
            var row = userBll.GetUserByName(Request.Cookies["UserName"].Values["UserName"]);
            if (row == null)
            {
                return false;
            }
            if (!row.IsActive)
            {
                return false;
            }
            if (!row.IsAdmin)
            {
                return false;
            }
            if (row.Pass != Request.Cookies["UserName"].Values["Password"])
            {
                return false;
            }
            Session["UserName"] = row.UserName;
            Session["FullName"] = row.FullName;
            Session["UserID"] = row.UserID.ToString();
            Session["IsAdmin"] = row.IsAdmin;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
