using System;
using System.Globalization;
using System.Web;
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

        var rVideo = vnnVideo.GetVideoByID("", VideoID, 1);
        if (rVideo == null)
        {
            Response.Redirect("~/FileNotFound.html");
            return;
        }
        hdLink.Value = rVideo.VideoURL;
        lbView.Text = rVideo.Viewed.ToString(CultureInfo.InvariantCulture);
        lbRatingCount.Text = rVideo.Viewed.ToString(CultureInfo.InvariantCulture);
        lbTitle.Text = rVideo.Title;
        lbBrief.Text = rVideo.Brief;
        try
        {
            var strHref = new Uri(rVideo.RefAddress).Host.Replace("www.", "");
            lbRefAddress.Text = strHref;
            if (strHref.Contains("bcdonline"))
                lbRefAddress.NavigateUrl = CurrentPage.UrlRoot + "/checklink.aspx?url=" + rVideo.RefAddress;
        }
        catch
        {
            lbRefAddress.Text = "";
        }
        lbCreatedDate.Text = rVideo.UpdatedDate.ToString(CultureInfo.InvariantCulture);
        SeoConfig(rVideo.Title, rVideo.Brief, rVideo.IsKeywordNull() ? "" : rVideo.Keyword, rVideo.Thumbnail, CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(rVideo.VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rVideo.Title) + "-hltw" + rVideo.VideoID + ".aspx", rVideo.VideoURL);
        GetTreeView(rVideo.VideoTypeID);
        var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
        var rowUpdate = dt.Newtbl_VideoRow();
        rowUpdate.VideoID = VideoID;
        rowUpdate.Viewed = rVideo.Viewed + 1;
        dt.Addtbl_VideoRow(rowUpdate);
        vnnVideo.UpdateStatus(dt, dt.ViewedColumn.ColumnName);
        LoadDataOld(rVideo.VideoID, rVideo.VideoTypeID, rVideo.CreatedDate);
        cmFacebook.Attributes.Add("data-href", CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(rVideo.VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rVideo.Title) + "-hltw" + rVideo.VideoID + ".aspx");

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

    private void GetTreeView(int newsTypeID)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnVideoTypeBll.GetVideoTypeByID(newsTypeID);
        if (row == null)
        {
            lrTreeView.Text = lrTreeView.Text;
            return;
        }
        lrTreeView.Text = "<li typeof=\"v:Breadcrumb\"><a rel=\"v:url\" property=\"v:title\" href='" + CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(row.VideoTypeName) + "/hltw" + row.VideoTypeID + ".aspx' >" + row.VideoTypeName + "</a></li>" + lrTreeView.Text;
        if (row.ParentID != -1)
            GetTreeView(row.ParentID);
    }

    private void LoadDataOld(int notVideoID, int newsTypeID, DateTime currdate)
    {
        var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        var dt = vnnVideoBll.GetAllVideoOldForRepeater("Title,VideoID,VideoTypeName,CreatedDate,Thumbnail", 6, notVideoID, newsTypeID, 1, currdate);
        rpDataOld.DataSource = dt;
        rpDataOld.DataBind();
        if (dt != null && dt.Count > 0)
        {
            aPrev.Visible = true;
            aPrev.HRef = CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[0].VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].VideoID + ".aspx";
            aPrev.Title = dt[0].Title.Replace("\"", "");
        }
        dt = vnnVideoBll.GetAllVideoNewForRepeater("Title,VideoID,VideoTypeName,CreatedDate,Thumbnail", 6, notVideoID, newsTypeID, 1, currdate);
        if (dt == null || dt.Count == 0) return;
        rpDataNew.DataSource = dt.Select("", "createddate desc");
        rpDataNew.DataBind();
        aNext.Visible = true;
        aNext.HRef = CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[0].VideoTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].VideoID + ".aspx";
        aNext.Title = dt[0].Title.Replace("\"", "");
    }
}