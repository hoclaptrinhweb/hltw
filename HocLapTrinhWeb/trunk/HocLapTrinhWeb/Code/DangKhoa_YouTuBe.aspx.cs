using System;
using System.Collections;
using DH.Data.SqlServer;
using HocLapTrinhWeb.BLL;

public partial class Code_DangKhoa_YouTuBe : DH.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //var con = new Connection();
        //con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
        //var videoBll = new v_VideoBLL(con);
        //var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
        //var update = new UpdateNewsBase();
        //var arr = new ArrayList();
        //for (var i = 1; i <= 2; i++)
        //{
        //    var doc = update.GetContentFromUrl("http://www.youtube.com/playlist?list=UUddE7OQGYier3M7MRYhCmEg&page=" + i);
        //    var node = doc.DocumentNode.SelectNodes("//li//h3[@class='video-title-container']//a");
        //    if (node != null)
        //        foreach (var t in node)
        //        {
        //            // arr.Add("http://www.youtube.com" + t.Attributes["href"].Value);
        //            var rowEx = videoBll.GetVideoByRefAddress("http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20), -1);
        //            if (rowEx != null)
        //                continue;
        //            var row = dt.Newtbl_VideoRow();
        //            doc = update.GetContentFromUrl("http://www.youtube.com" + t.Attributes["href"].Value);
        //            row.Title = doc.DocumentNode.SelectSingleNode("//span[@id='eow-title']").InnerText.Replace("\n", "").Trim();
        //            var brief = doc.DocumentNode.SelectSingleNode("//p[@id='eow-description']").InnerText;
        //            row.Brief = (brief == "Không có mô tả nào." ? "" : brief);
        //            row.Thumbnail = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
        //            row.RefAddress = "http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20);
        //            row.CreatedDate = DateTime.Now;
        //            row.UpdatedDate = DateTime.Now;
        //            row.CreatedBy = 1;
        //            row.UpdatedBy = 1;
        //            //Chú ý
        //            row.VideoTypeID = 10;
        //            row.VideoURL = "http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20);
        //            row.IPAddress = DH.Utilities.Net.GetVisitorIPAddress();
        //            row.IPUpdate = DH.Utilities.Net.GetVisitorIPAddress();
        //            row.Priority = 0;
        //            row.Viewed = 0;
        //            row.IsActive = false;
        //            row.IsHot = false;
        //            row.IsDelete = false;
        //            dt.Addtbl_VideoRow(row);
        //        }
        //}
        //var b = videoBll.Add(dt);
    }
}