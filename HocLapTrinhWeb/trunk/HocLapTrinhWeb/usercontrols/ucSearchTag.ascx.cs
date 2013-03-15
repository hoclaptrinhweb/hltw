using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucSearchTag : DH.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        SetBase();
        if (!IsPostBack)
            LoadData();
    }

    #endregion

    #region Method Page

    public string TagName
    {
        get
        {
            if (Request.QueryString["TagName"] != null)
                return Request.QueryString["TagName"];
            return "";
        }
    }

    public int PageSize
    {
        get
        {
            try
            {
                return Request.QueryString["pagesize"] != null ? int.Parse(Request.QueryString["pagesize"]) : 10;
            }
            catch
            {
                return 10;
            }
        }
    }

    public int PageIndex
    {
        get
        {
            return Request.QueryString["trang"] != null ? int.Parse(Request.QueryString["trang"]) : 1;
        }
    }

    public string BindPaging(int total)
    {
        var url = CurrentPage.UrlRoot + "/tag/" + TagName + ".aspx" + (PageSize == 10 ? "?" : "?pagesize=" + PageSize + "&");

        var html = "";
        var nSumOfPage = (total - 1) / PageSize + 1;
        var nPageShow = nSumOfPage > 7 ? 7 : nSumOfPage;
        if (nSumOfPage > 1 || total > PageSize)
        {
            if (PageIndex > 1)
            {
                html += "<li><a class=\"firstPage\"  href=\"" + url + "trang=1" + "\" >|<<</a></li>";
                html += "<li><a class=\"prevPage\"  href=\"" + url + "trang=" + (PageIndex - 1) + "\"><<</a></li>";
            }
            var delta = 0;
            for (var i = 0; i < nPageShow; i++)
            {
                var number = PageIndex - 3 + i;
                if (number <= 0)
                {
                    delta = 3 + 1 - PageIndex;
                }
                if (number > nSumOfPage)
                {
                    break;
                }
                number += delta;
                if (number == PageIndex)
                {
                    html += "<li><a class=\"pagecurrent\">" + number + "</a></li>";
                }
                else
                {
                    html += "<li><a class=\"pages\" href=\"" + url + "trang=" + number + "\" >" + number + "</a></li>";
                }
            }
            if (PageIndex < nSumOfPage)
            {
                html += "<li><a class=\"nextPage\"  href=\"" + url + "trang=" + (PageIndex + 1) + "\">>></a></li>";
                html += "<li><a class=\"endPage\"  href=\"" + url + "trang=" + (nSumOfPage) + "\">>>|</a></li>";
            }
        }
        return html;
    }

    private void LoadData()
    {
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsForGridView((PageIndex - 1) * PageSize, PageSize, "NewsTypeName,Title,NewsID,RefAddress,UpdatedDate,Viewed,Thumbnail,Brief", TagName.Replace("-", " "), 1);
        rpData.DataSource = dt;
        rpData.DataBind();
        if (dt != null && dt.Count > 0)
        {
            var total = vnnNewsBll.GetAllNewsRowCount("", TagName.Replace("-", " "), 1);
            Paging.InnerHtml = BindPaging(total);
        }
        SeoConfig(TagName.Replace("-", " "), "Tìm kiếm bài viết theo " + TagName.Replace("-", " "), TagName.Replace("-", " "), "", "");
    }

    private void SeoConfig(string strTitle, string strDescription, string strKeyWords, string strImage, string strUrl)
    {
        strTitle = strTitle + (PageIndex == 1 ? "" : " - Trang " + PageIndex);
        if (strTitle.Length < 100)
            strTitle = strTitle + " - hoclaptrinhweb.com";
        CurrentPage.Title = strTitle;
        if (Page.Master == null) return;
        var metaTitle = (HtmlMeta)Page.Master.FindControl("metaTitle");
        if (metaTitle != null)
            metaTitle.Content = strTitle + (PageIndex == 1 ? "" : " - Trang " + PageIndex);
        var metaDesc = (HtmlMeta)Page.Master.FindControl("metaDesc");
        if (metaDesc != null)
            metaDesc.Content = strDescription;
        var metaDescFb = (HtmlMeta)Page.Master.FindControl("metaDescFb");
        if (metaDescFb != null)
            metaDescFb.Content = strDescription + (PageIndex == 1 ? "" : " - Trang " + PageIndex);
        var metaKeywords = (HtmlMeta)Page.Master.FindControl("metaKeywords");
        if (metaKeywords != null)
            metaKeywords.Content = strKeyWords;
        var metaUrl = (HtmlMeta)Page.Master.FindControl("metaUrl");
        if (metaUrl != null)
            metaUrl.Content = strUrl;
        var metaImage = (HtmlMeta)Page.Master.FindControl("metaImage");
        if (metaImage != null)
            metaImage.Content = strImage;
    }

    public string Title
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["title"]) == false ? Request.QueryString["title"] : "";
        }
    }

    public void SetBase()
    {
        //var include = new HtmlGenericControl("base");
        //include.Attributes.Add("href", CurrentPage.UrlRoot + "/today.aspx");
        //CurrentPage.Header.Controls.Add(include);
    }

    #endregion
}