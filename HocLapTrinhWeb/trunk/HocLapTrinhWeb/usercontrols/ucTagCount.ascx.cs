using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucTagCount : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vTagCountBll = new t_TagCountBLL(getCurrentConnection());
        var vNewsBll = new vnn_NewsBLL(getCurrentConnection());
        hdNewsCount.Value = vNewsBll.GetAllNewsRowCount("", -1, -1, "", "", "").ToString();
        rpTagCount.DataSource = vTagCountBll.GetAll(30);
        rpTagCount.DataBind();
    }

    public string GetTagClass(int category)
    {
        var result = (category * 100) / int.Parse(hdNewsCount.Value);
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