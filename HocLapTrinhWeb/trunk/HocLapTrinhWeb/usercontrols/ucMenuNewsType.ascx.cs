using System;
using System.Web.Caching;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucMenuNewsType : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        LoadData();
    }

    public void LoadData()
    {
        if (!Request.Url.ToString().ToLower().Contains("/video"))
        {
            var dt = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)Cache["dataMenu"];
            if (dt == null)
            {
                var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
                SqlCacheDependency dependency = new SqlCacheDependency("HocLapTrinhWeb.com", "tbl_NewsType");
                Cache.Insert("dataMenu", vnnNewsTypeBll.GetDataByParentID("NewsTypeName,NewsTypeID,TotalNews,ImageURL", -1), dependency);
            }
            rpNewsType.DataSource = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)Cache["dataMenu"];
            rpNewsType.DataBind();
        }
        else
        {
            var dt = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable)Cache["dataVideoMenu"];
            if (dt == null)
            {
                var vnnVideo = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
                SqlCacheDependency dependency = new SqlCacheDependency("HocLapTrinhWeb.com", "tbl_videotype");
                Cache.Insert("dataVideoMenu", vnnVideo.GetDataByParentID("videoTypeName,videoTypeID", -1), dependency);
            }
            Repeater1.DataSource = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable)Cache["dataVideoMenu"];
            Repeater1.DataBind();
        }
    }

}