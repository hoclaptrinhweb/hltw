using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucTagCount :DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender,e);
        var vTagCountBll = new t_TagCountBLL(getCurrentConnection());
        rpTagCount.DataSource = vTagCountBll.GetAll(30);
        rpTagCount.DataBind();
    }
}