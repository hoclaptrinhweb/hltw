using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRightFacebook : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender,e);
        var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        var dt = vnnVideoBll.GetAllVideoForRepeater("VideoTypeName,VideoTypeID,Thumbnail,Title,VideoID", 5, -1, 1, "", "", "NEWID()", "");
        rpDataVideoViewed.DataSource = dt;
        rpDataVideoViewed.DataBind();
    }
}
