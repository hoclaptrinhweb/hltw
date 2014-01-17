using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRightFacebook : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var dt = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoDataTable)Cache["videoRandom"];
        if (dt == null)
        {
            var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
            Cache.Insert("videoRandom",vnnVideoBll.GetAllVideoForRepeater("VideoTypeName,VideoTypeID,Thumbnail,Title,VideoID", 5, -1, 1, "", "", "NEWID()", ""),null, DateTime.Now.AddMinutes(1d),System.Web.Caching.Cache.NoSlidingExpiration);
            dt = (vnn_dsHocLapTrinhWeb.vnn_vw_VideoDataTable)Cache["videoRandom"];
        }
        rpDataVideoViewed.DataSource = dt;
        rpDataVideoViewed.DataBind();
    }
}
