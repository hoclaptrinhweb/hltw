using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRandomNews : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsForRepeater("NewsTypeName,Title,NewsID,NameMoveFrom", 100, -1, 1, "", "", "", "NEWID()", "");
        var rnd = new Random();
        var i = rnd.Next(0, 99);
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[i].NameMoveFrom) + "/" + XuLyChuoi.ConvertToUnSign(dt[i].Title) + "-hltw" + dt[i].NewsID + ".aspx");
    }
}