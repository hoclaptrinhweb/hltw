using System;
using System.Web.UI.HtmlControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucNewsDetail : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (Page.Master != null)
        {
            //Xu ly code cho dep
        }
        if (IsPostBack) return;
        LoadData();
    }

    #endregion

    #region Method Page



    private void SeoConfig(string strTitle, string strDescription, string strKeyWords, string strImage, string strUrl)
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
        //Image
        var metaImage = (HtmlMeta)Page.Master.FindControl("metaImage");
        if (metaImage != null)
            metaImage.Content = CurrentPage.UrlRoot + "/" + strImage;
        //metaImage.Content = CurrentPage.UrlRoot + "/images/" + strImage.ToLower().Replace(Global.ImagesNews.ToLower(), "") + ".ashx?w=300";
    }

    private void LoadData()
    {
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());

        var rNews = vnnNewsBll.GetNewsAndNewsTypeByID("Title,Thumbnail,NewsTypeID,NewsTypeName,NewsID,Viewed,Brief,Content,CreatedDate,Keyword,UpdatedDate,RefAddress", NewsID, 1);
        if (rNews == null)
        {
            Response.Redirect("~/FileNotFound.html");
            return;
        }
        if (Title != XuLyChuoi.ConvertToUnSign(rNews.Title) || CatName != XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName))
            Response.Redirect(CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNews.Title) + "-hltw" + rNews.NewsID + ".aspx");

        lbTitle.Text = rNews.Title;
        lbBrief.Text = rNews.Brief;
        lbContent.Text = rNews.Content;


        var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
        var rowUpdate = dt.Newtbl_NewsRow();
        rowUpdate.NewsID = NewsID;
        rowUpdate.Viewed = rNews.Viewed + 1;
        dt.Addtbl_NewsRow(rowUpdate);
        vnnNewsBll.UpdateChange(dt, dt.ViewedColumn.ColumnName);

        SeoConfig(rNews.Title, rNews.Brief, rNews.IsKeywordNull() ? "" : rNews.Keyword, rNews.Thumbnail, CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNews.Title) + "-hltw" + rNews.NewsID + ".aspx");

    }


    public int NewsID
    {
        get
        {
            if (Request.QueryString["NewsID"] == null)
                return -1;
            try
            {
                return int.Parse(Request.QueryString["NewsID"]);
            }
            catch
            {
                return -1;
            }
        }
    }

    public string Title
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["title"]) == false ? Request.QueryString["title"] : "";
        }
    }

    public string CatName
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["catname"]) == false ? Request.QueryString["catname"] : "";
        }
    }


    #endregion
}