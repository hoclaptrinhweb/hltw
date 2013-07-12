using System;
using System.Net;

public partial class Code_ActiveMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://services.odata.org/Northwind/Northwind.svc/$metadata");
        request.Headers.Add("Authorization", "Basic Og==");
        var response = (HttpWebResponse)request.GetResponse();

    }

    protected string SetClass(params string[] defaultpage)
    {
        try
        {
            foreach (var t in defaultpage)
            {
                if (Request.Url.ToString().Contains(t.ToLower()))
                    return "active";
            }
            return "";
        }
        catch { return ""; }
    }

    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath));
        }
    }
}