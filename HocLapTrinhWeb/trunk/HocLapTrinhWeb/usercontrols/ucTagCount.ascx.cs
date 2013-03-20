using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucTagCount : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vTagCountBll = new t_TagCountBLL(getCurrentConnection());
        var dt = vTagCountBll.GetAll(30);
        if (dt != null && dt.Count > 0)
        {
            hdNewsCount.Value = dt[0].TagCount.ToString();
            rpTagCount.DataSource = dt;
            rpTagCount.DataBind();
        }
    }

    public string GetTagClass(int category)
    {
        var result = int.Parse(hdNewsCount.Value) - category;
        if (result <= 100)
            return "tag7";
        if (result <= 200)
            return "tag6";
        if (result <= 300)
            return "tag5";
        if (result <= 350)
            return "tag4";
        if (result <= 400)
            return "tag3";
        if (result <= 450)
            return "tag2";
        return result <= 500 ? "tag1" : "";
    }

}