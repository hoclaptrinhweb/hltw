using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Web.UI.HtmlControls;

public partial class usercontrols_ucVideoDetail : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        LoadData();
    }

    private void SeoConfig(string strTitle, string strDescription, string strKeyWords, string strImage, string strUrl, string strVideo)
    {
        if (strTitle.Length < 100)
            strTitle = strTitle + " - hoclaptrinhweb.com";
        CurrentPage.Title = strTitle;
        if (Page.Master == null) return;
        var metaTitle = (HtmlMeta)Page.Master.FindControl("metaTitle");
        if (metaTitle != null)
            metaTitle.Content = strTitle;

        //Description
        var metaDesc = (HtmlMeta)Page.Master.FindControl("metaDesc");
        if (metaDesc != null)
            metaDesc.Content = strDescription;
        var metaDescFb = (HtmlMeta)Page.Master.FindControl("metaDescFb");
        if (metaDescFb != null)
            metaDescFb.Content = strDescription;
        //Keywords
        var metaKeywords = (HtmlMeta)Page.Master.FindControl("metaKeywords");
        if (metaKeywords != null)
            metaKeywords.Content = strKeyWords;
        //Url
        var metaUrl = (HtmlMeta)Page.Master.FindControl("metaUrl");
        if (metaUrl != null)
            metaUrl.Content = strUrl;
        var metaVideo = (HtmlMeta)Page.Master.FindControl("metaVideo");
        if (metaVideo != null)
            metaVideo.Content = CurrentPage.UrlRoot + "/code/jwp58l/player.swf?file=" + HttpUtility.UrlEncode(strVideo) + "&abouttext=Hoclaptrinhweb.com&aboutlink=" + HttpUtility.UrlEncode(CurrentPage.UrlRoot) + "&skin=" + HttpUtility.UrlEncode(CurrentPage.UrlRoot + "/code/jwp58l/NewTubeDark.zip") + "&controlbar.position=over&dock.position=true&logo.file=" + HttpUtility.UrlEncode(CurrentPage.UrlRoot + "/code/jwp58l/logo.png") + "&logo.hide=false&logo.position=top-right&logo.link=" + HttpUtility.UrlEncode(CurrentPage.UrlRoot);
        //Image
        var metaImage = (HtmlMeta)Page.Master.FindControl("metaImage");
        if (metaImage != null)
            metaImage.Content = strImage.Contains("http://") ? strImage : CurrentPage.UrlRoot + "/" + strImage;
    }

    private void LoadData()
    {
        var vnnVideo = new v_VideoBLL(CurrentPage.getCurrentConnection());

        var rNews = vnnVideo.GetVideoByID("", VideoID, 1);
        if (rNews == null)
        {
            Response.Redirect("~/FileNotFound.html");
            return;
        }
        hdLink.Value = rNews.VideoURL;
        SeoConfig(rNews.Title, rNews.Brief, rNews.IsKeywordNull() ? "" : rNews.Keyword, rNews.Thumbnail, CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(rNews.VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNews.Title) + "-hltw" + rNews.VideoID + ".aspx", rNews.VideoURL);
    }

    public int VideoID
    {
        get
        {
            if (Request.QueryString["VideoID"] == null)
                return -1;
            try
            {
                return int.Parse(Request.QueryString["VideoID"]);
            }
            catch
            {
                return -1;
            }
        }
    }
}