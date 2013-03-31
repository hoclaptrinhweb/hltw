using System;
using System.Collections;
using HocLapTrinhWeb.BLL;

public partial class Code_DangKhoa_YouTuBe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var update = new UpdateNewsBase();
        var arr = new ArrayList();
        for (var i = 1; i <= 6; i++)
        {
            var doc = update.GetContentFromUrl("http://www.youtube.com/playlist?list=UUwd4Q0VFfbt-JANv6ZhehxA&page=" + i);
            var node = doc.DocumentNode.SelectNodes("//li//h3[@class='video-title-container']//a");
            if (node != null)
                foreach (var t in node)
                    arr.Add("http://www.youtube.com" + t.Attributes["href"].Value);
        }
    }
}