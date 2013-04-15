using System;
using System.Collections;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucMenuRight : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        GetTreeView(-1);
    }

    private void GetTreeView(int newsTypeID)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsTypeBll.GetNewsTypeByParentID("NewsTypeName,NewsTypeID", newsTypeID, new ArrayList());
        if (dt == null || dt.Count == 0)
        {
            return;
        }
        lrTreeView.Text += "<ul>";
        foreach (var t in dt)
        {
            lrTreeView.Text += "<li><a href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(t.NewsTypeName) + "/hltw" + t.NewsTypeID + ".aspx' >" + t.NewsTypeName + "</a>";
            GetTreeView(t.NewsTypeID);
            lrTreeView.Text += "</li>";
        }
        lrTreeView.Text += "</ul>";

    }
}