using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucTagCount : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vTagCountBll = new t_TagCountBLL(getCurrentConnection());
        var dt = vTagCountBll.GetAll(50);
        if (dt == null || dt.Count <= 0) return;
        hdNewsCount.Value = dt[0].TagCount.ToString();
        rpTagCount.DataSource = dt;
        rpTagCount.DataBind();
    }

    public string GetTagClass(int category)
    {
        var result = int.Parse(hdNewsCount.Value) - category;
        if (result <= 10)
            return "tag7";
        if (result <= 20)
            return "tag6";
        if (result <= 30)
            return "tag5";
        if (result <= 35)
            return "tag4";
        if (result <= 40)
            return "tag3";
        if (result <= 45)
            return "tag2";
        return result <= 50 ? "tag1" : "";
    }

}