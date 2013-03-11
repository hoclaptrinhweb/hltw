using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucNewsSlide : DH.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsHot(7, -1, -1, "", "", "newsid", "desc");
        rpSlide.DataSource = dt;
        rpSlide.DataBind();
        rpImages.DataSource = dt;
        rpImages.DataBind();
    }
}