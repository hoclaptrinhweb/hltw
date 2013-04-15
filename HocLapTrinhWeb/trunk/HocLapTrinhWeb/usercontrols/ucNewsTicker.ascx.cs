using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucNewsTicker : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
            LoadData();
    }

    public void LoadData()
    {
        var vnnNewsBll = new NewsBLL(CurrentPage.getCurrentConnection());
        rpNewsTicker.DataSource = vnnNewsBll.GetNewsAll("NewsTypeName,Title,NewsID", 10, -1, 1, "", "", "NEWID()", "");
        rpNewsTicker.DataBind();
    }
}