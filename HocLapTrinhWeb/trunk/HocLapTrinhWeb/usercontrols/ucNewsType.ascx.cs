using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Data;
using System.Collections;
using System.Web.Caching;

public partial class usercontrols_ucNewsType : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack)
            return;
        SetBase();
        LoadData();
    }

    public void LoadData()
    {
        //Quản lý Cache ở phần admin
        if (Cache["dataNewsType"] == null)
        {
            var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
            var notNewsTypeID = new ArrayList { 14, 22, 23, 34, 36 };
            var dt = vnnNewsTypeBll.GetNewsTypeByParentID("Description,NewsTypeName,NewsTypeID,PathID,ImageURL", -1, notNewsTypeID);
            SqlCacheDependency dependency = new SqlCacheDependency("HocLapTrinhWeb.com", "tbl_NewsType");
            Cache.Insert("dataNewsType", dt, dependency);
        }
        var rpNewsType = (Repeater)ucListNewsType.FindControl("rpNewsType");
        if (rpNewsType != null)
            rpNewsType.DataSource = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)Cache["dataNewsType"];
    }

    public void SetBase()
    {
        var lrBase = (Literal)Page.Master.FindControl("lrBase");
        if (lrBase != null)
            lrBase.Text = "<base href='" + CurrentPage.UrlRoot + "/'></base>";
    }

}