using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Data;
using System.Collections;

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
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var notNewsTypeID = new ArrayList { 14, 22, 23, 34, 36 };
        var dt = vnnNewsTypeBll.GetNewsTypeByParentID("Description,NewsTypeName,NewsTypeID,PathID,ImageURL", -1, notNewsTypeID);
        var rpNewsType = (Repeater)ucListNewsType.FindControl("rpNewsType");
        if (rpNewsType != null)
            rpNewsType.DataSource = dt;
    }

    public void SetBase()
    {
        var lrBase = (Literal)Page.Master.FindControl("lrBase");
        if (lrBase != null)
            lrBase.Text = "<base href='" + CurrentPage.UrlRoot + "/'></base>";
    }

}