using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucMenuNewsType : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsTypeBll.GetDataByParentID("NewsTypeName,NewsTypeID,TotalNews,ImageURL", -1);
        rpNewsType.DataSource = dt;
        rpNewsType.DataBind();
    }

}