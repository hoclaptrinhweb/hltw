using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucCheckLink : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        //if (ParaUrl == "") return;
        //var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        //var rNew = vnnNewsBll.GetNewsByRefAddress(ParaUrl, 1);
        //if (rNew != null)
        //    Response.Redirect(CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(rNew.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNew.Title) + "-hltw" + rNew.NewsID + ".aspx");
        //else
        //    Response.Redirect(ParaUrl);
    }

    public string ParaUrl
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["url"]) ? "" : Request.QueryString["url"];
        }
    }

}
