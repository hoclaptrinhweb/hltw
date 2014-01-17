using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucMenuNewsType : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var dt = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)Cache["dataMenu"];
        if (dt == null)
        {
            var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
            Cache.Insert("dataMenu", vnnNewsTypeBll.GetDataByParentID("NewsTypeName,NewsTypeID,TotalNews,ImageURL", -1), null, DateTime.Now.AddDays(10), System.Web.Caching.Cache.NoSlidingExpiration);
            dt = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)Cache["dataMenu"];
        }
        rpNewsType.DataSource = dt;
        rpNewsType.DataBind();
    }

}