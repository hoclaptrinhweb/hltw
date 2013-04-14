using System;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucindex : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if(IsPostBack)
            return;
        SetBase();
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        rpNewType.DataSource = vnnNewsTypeBll.GetNewsTypeByParentID("NewsTypeName,NewsTypeID", -1, new ArrayList());
        rpNewType.DataBind();

    }

    protected void rpNewType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var item = e.Item;
        if ((item.ItemType != ListItemType.Item) && (item.ItemType != ListItemType.AlternatingItem)) return;
        var rpData = (Repeater)item.FindControl("rpData");
        var row = (vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRow)((DataRowView)(e.Item.DataItem)).Row;
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        rpData.DataSource = vnnNewsBll.GetAllNewsForRepeater("NewsTypeName,Title,NewsID", 5, row.NewsTypeID, 1, "", "", "NewsId", "Desc");
        rpData.DataBind();
    }

    public void SetBase()
    {
        var include = new HtmlGenericControl("base");
        include.Attributes.Add("href", CurrentPage.UrlRoot);
        CurrentPage.Header.Controls.Add(include);
    }

}
