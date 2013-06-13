using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRandomNews : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var rnd = new Random();
        var n = rnd.Next(0, 2);

        if (n == 0 && Request.UrlReferrer != null && Request.UrlReferrer.Host.ToLower() == "luanvanthacsi.edu.vn")
        {
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "http://forum.hoclaptrinhweb.com/threads/slide-bai-giang-tong-quan-co-ban-ve-html-css-javascript.90/");
            return;
        }
        n = rnd.Next(0, 2);
        if (n == 0)
        {
            var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
            var dt = vnnNewsBll.GetAllNewsForRepeater("NewsTypeName,Title,NewsID,NameMoveFrom", 2, -1, 1, "", "", "", "NEWID()", "");
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[0].NameMoveFrom) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].NewsID + ".aspx");
        }
        else
        {
            var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
            var dt = vnnVideoBll.GetAllVideoForRepeater("VideoTypeName,Title,VideoID,VideoTypeName", 2, -1, 1, "", "", "", "NEWID()", "");
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(dt[0].VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].VideoID + ".aspx");
        }
    }
}