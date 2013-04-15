using System;
using System.Globalization;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Web.UI.HtmlControls;

public partial class usercontrols_ucNewsDetail1 : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (Page.Master != null)
        {
            var lrScriptDetail = (Literal)Page.Master.FindControl("lrScriptDetail");
            if (lrScriptDetail != null)
            {
                lrScriptDetail.Text = Combres.WebExtensions.CombresLink("syntaxhighlighter");
                //lrScriptDetail.Text = "<script type='text/javascript' src='" + CurrentPage.UrlRoot + "/js/syntaxhighlighter.js'></script>";
                lrScriptDetail.Text += "<script type=\"text/javascript\">SyntaxHighlighter.all();</script>";
            }
        }
        if (IsPostBack) return;
        LoadData();
        ViewComment();
    }

    #endregion

    #region Method Page

    private void ViewComment()
    {
        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            pGuest.Visible = false;
            pUser.Visible = true;
        }
        else
        {
            pGuest.Visible = true;
            pUser.Visible = false;
        }
    }

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

        lbView.Text = rNews.Viewed.ToString(CultureInfo.InvariantCulture);
        lbRatingCount.Text = rNews.Viewed.ToString(CultureInfo.InvariantCulture);
        hdNewsID.Value = rNews.NewsID.ToString(CultureInfo.InvariantCulture);
        lbTitle.Text = rNews.Title;
        lbBrief.Text = rNews.Brief;
        lbContent.Text = rNews.Content;
        try
        {
            var strHref = new Uri(rNews.RefAddress).Host.Replace("www.", "");
            lbRefAddress.Text = strHref;
            if (strHref.Contains("bcdonline"))
                lbRefAddress.NavigateUrl = CurrentPage.UrlRoot + "/checklink.aspx?url=" + rNews.RefAddress;
        }
        catch
        {
            lbRefAddress.Text = "";
        }
        lbCreatedDate.Text = rNews.UpdatedDate.ToString(CultureInfo.InvariantCulture);

        //Xử lý keyword
        if (!rNews.IsKeywordNull() && rNews.Keyword != "")
        {
            var arrKeyword = rNews.Keyword.Split(',');
            for (var i = 0; i < arrKeyword.Length; i++)
            {
                lbKeyword.Text += "<a href='" + CurrentPage.UrlRoot + "/tag/" + arrKeyword[i].Replace(" ", "-") + ".aspx'>" + arrKeyword[i] + "</a>";
            }
        }
        //

        var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
        var rowUpdate = dt.Newtbl_NewsRow();
        rowUpdate.NewsID = NewsID;
        rowUpdate.Viewed = rNews.Viewed + 1;
        dt.Addtbl_NewsRow(rowUpdate);
        vnnNewsBll.UpdateChange(dt, dt.ViewedColumn.ColumnName);

        SeoConfig(rNews.Title, rNews.Brief, rNews.IsKeywordNull() ? "" : rNews.Keyword, rNews.Thumbnail, CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNews.Title) + "-hltw" + rNews.NewsID + ".aspx");
        lrRss.Text = lrRss.Text.Replace("{des}", rNews.NewsTypeName).Replace("{href}", CurrentPage.UrlRoot + "/rss/" + XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName) + "-rss" + rNews.NewsTypeID + ".aspx");
        GetTreeView(rNews.NewsTypeID);
        LoadDataOld(rNews.NewsID, rNews.NewsTypeID, rNews.CreatedDate);
        LoadDataComment();
        cmFacebook.Attributes.Add("data-href", CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(rNews.NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(rNews.Title) + "-hltw" + rNews.NewsID + ".aspx");
    }

    private void LoadDataOld(int notNewsID, int newsTypeID, DateTime currdate)
    {
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsOldForRepeater("Title,NewsID,NewsTypeName,CreatedDate", 10, notNewsID, newsTypeID, 1, currdate);
        rpDataOld.DataSource = dt;
        rpDataOld.DataBind();
        if (dt != null && dt.Count > 0)
        {
            aPrev.Visible = true;
            aPrev.HRef = CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[0].NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].NewsID + ".aspx";
            aPrev.Title = dt[0].Title.Replace("\"", "");
        }

        dt = vnnNewsBll.GetAllNewsNewForRepeater("Title,NewsID,NewsTypeName,CreatedDate", 10, notNewsID, newsTypeID, 1, currdate);
        rpDataNew.DataSource = dt.Select("", "createddate desc");
        rpDataNew.DataBind();
        if (dt.Count == 0) return;
        aNext.Visible = true;
        aNext.HRef = CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(dt[0].NewsTypeName) + "/" + XuLyChuoi.ConvertToUnSign(dt[0].Title) + "-hltw" + dt[0].NewsID + ".aspx";
        aNext.Title = dt[0].Title.Replace("\"", "");
    }

    private void LoadDataComment()
    {
        var vnnCommentNewsBll = new vnn_CommentNewsBLL(CurrentPage.getCurrentConnection());
        rpComment.DataSource = vnnCommentNewsBll.GetAllCommentNewsForRepeater(0, 10, 1, NewsID);
        rpComment.DataBind();
    }

    private void GetTreeView(int newsTypeID)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnNewsTypeBll.GetNewsTypeByID(newsTypeID);
        if (row == null)
        {
            lrTreeView.Text = lrTreeView.Text;
            return;
        }
        lrTreeView.Text = "<li typeof=\"v:Breadcrumb\"><a rel=\"v:url\" property=\"v:title\" href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(row.NewsTypeName) + "/hltw" + row.NewsTypeID + ".aspx' >" + row.NewsTypeName + "</a></li>" + lrTreeView.Text;
        if (row.ParentID != -1)
            GetTreeView(row.ParentID);
    }

    public void SetBase()
    {
        var include = new HtmlGenericControl("base");
        include.Attributes.Add("href", CurrentPage.UrlRoot + "/NewsDetail.aspx");
        CurrentPage.Header.Controls.Add(include);
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