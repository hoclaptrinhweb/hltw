using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucIndex : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        GetMenuNewsType(-1);
    }
    private void GetMenuNewsType(int productID)
    {
        try
        {
            var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
            var dt = vnnNewsTypeBll.GetDataByParentID("NewsTypeName,NewsTypeID,TotalNews,ImageURL", productID);
            if (dt == null || dt.Count <= 0) return;
            if (productID == -1)
                lbProductType.Text += "<ul data-role=\"listview\"  data-count-theme=\"b\" data-filter=\"true\" data-filter-placeholder=\"Tìm nhanh loại tin...\" data-inset=\"true\" >";
            else
                lbProductType.Text += "<ul  data-role=\"listview\">";

            foreach (var t in dt)
            {
                lbProductType.Text += "<li>" +
                    "<a href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(t.NewsTypeName) + "/hltw" + t.NewsTypeID + ".aspx'>" +
                    "<img src='" + (t.IsImageURLNull() ? "" : t.ImageURL) + "' />"
                     + "<h2>" + t.NewsTypeName + "</h2>"
                      + "<span class=\"ui-li-count\">" + t.TotalNews.ToString() + "</span></a>";
                lbProductType.Text += "</li>";
            }

            lbProductType.Text += "</ul>";
        }
        catch
        {
            return;
        }

    }
}