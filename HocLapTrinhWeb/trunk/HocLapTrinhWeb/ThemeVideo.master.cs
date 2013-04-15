using System;
using HocLapTrinhWeb.BLL;

public partial class ThemeVideo : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lrStyle.Text = Combres.WebExtensions.CombresLink("DefaultThemeCss");
        lrScript.Text = Combres.WebExtensions.CombresLink("DefaultThemeJs");
        Login();
    }

    /// <summary>
    /// Check login bang cookie
    /// </summary>
    /// <returns></returns>
    private bool Login()
    {
        try
        {
            if (Session["UserName"] != null)
                return true;
            var con = new HocLapTrinhWeb.DAL.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {
                var userBll = new ltk_UserBLL(con);
                var row = userBll.GetUserByName(Request.Cookies["UserName"].Values["UserName"]);
                if (row == null)
                    return false;
                if (!row.IsActive)
                    return false;
                if (row.Pass != Request.Cookies["UserName"].Values["Password"])
                    return false;
                Session["UserName"] = row.UserName;
                Session["FullName"] = row.FullName;
                Session["UserID"] = row.UserID.ToString();
                Session["IsAdmin"] = row.IsAdmin;
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath));
        }
    }
}
