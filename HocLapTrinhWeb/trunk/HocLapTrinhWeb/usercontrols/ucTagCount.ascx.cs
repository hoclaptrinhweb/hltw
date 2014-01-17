using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucTagCount : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        var dt = (vnn_dsHocLapTrinhWeb.vnn_vw_TagCountDataTable)Cache["dataTag"];
        if (dt == null)
        {
            var vTagCountBll = new t_TagCountBLL(getCurrentConnection());
            Cache.Insert("dataTag", vTagCountBll.GetAll(100), null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
            dt = (vnn_dsHocLapTrinhWeb.vnn_vw_TagCountDataTable)Cache["dataTag"];
            if (dt == null || dt.Count <= 0) return;
        }
       
        dt.DefaultView.Sort = "tagname asc";
        rpTagCount.DataSource = dt.DefaultView;
        rpTagCount.DataBind();
    }

    public string GetTagClass(int category)
    {
        var hdNewsCount = (int?)Cache["newsCount"];
        if (hdNewsCount == null)
        {
            var vNewsBll = new vnn_NewsBLL(getCurrentConnection());
            Cache.Insert("newsCount", vNewsBll.GetAllNewsRowCount("", -1, 1, "", "", ""), null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
            hdNewsCount = (int?)Cache["newsCount"];
        }
        var result = (category * 10000) / hdNewsCount;
        if (result <= 1)
            return "tag1";
        if (result <= 4)
            return "tag2";
        if (result <= 8)
            return "tag3";
        if (result <= 12)
            return "tag4";
        if (result <= 18)
            return "tag5";
        if (result <= 30)
            return "tag6";
        return result <= 50 ? "tag7" : "";
    }

}