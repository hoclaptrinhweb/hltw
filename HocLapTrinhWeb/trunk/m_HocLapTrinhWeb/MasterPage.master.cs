using System;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lrScript.Text = "<script src='" + UrlRoot + "/js/jquery.js' type='text/javascript'></script>";
        lrStyle.Text = "<link href='" + UrlRoot + "/jquery.mobile.min.css' rel='stylesheet' type='text/css' />";

        lrScriptMobile.Text = "<script src='" + UrlRoot + "/js/jquery.mobile.min.js' type='text/javascript'></script>";
    }

    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath));
        }
    }
}
