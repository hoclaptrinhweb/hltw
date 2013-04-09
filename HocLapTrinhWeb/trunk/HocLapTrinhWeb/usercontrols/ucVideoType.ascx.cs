using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucVideoType : DH.UI.UCBase
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

    public int VideoTypeID
    {
        get
        {
            if (Request.QueryString["VideoTypeID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["VideoTypeID"]);
                }
                catch { return -1; }
            }
            return -1;
        }
    }

    public int PageSize
    {
        get
        {
            try
            {
                return Request.QueryString["pagesize"] != null ? int.Parse(Request.QueryString["pagesize"]) : 24;
            }
            catch
            {
                return 24;
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
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnVideoTypeBll.GetVideoTypeByID(VideoTypeID);
        var url = CurrentPage.UrlRoot + "/video/" + (row != null ? XuLyChuoi.ConvertToUnSign(row.VideoTypeName) : VideoTypeID.ToString()) + "/hltw" + VideoTypeID + ".aspx" + (PageSize == 10 ? "?" : "?pagesize=" + PageSize + "&");

        var html = "";
        var nSumOfPage = (total - 1) / PageSize + 1;
        var nPageShow = nSumOfPage > 7 ? 7 : nSumOfPage;
        if (nSumOfPage > 1 || total > PageSize)
        {
            if (PageIndex > 1)
            {
                html += "<li class=\"first\"><a href=\"" + url + "trang=1" + "\" ><< Đầu tiên</a></li>";
                html += "<li class=\"previous\"><a href=\"" + url + "trang=" + (PageIndex - 1) + "\">< Trước</a></li>";
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
                    html += "<li class=\"pages selected\"><a>" + number + "</a></li>";
                }
                else
                {
                    html += "<li class=\"pages\"><a href=\"" + url + "trang=" + number + "\" >" + number + "</a></li>";
                }
            }
            if (PageIndex < nSumOfPage)
            {
                html += "<li class=\"next\"><a href=\"" + url + "trang=" + (PageIndex + 1) + "\">Tiếp theo ></a></li>";
                html += "<li class=\"last\"><a href=\"" + url + "trang=" + (nSumOfPage) + "\">Cuối >></a></li>";
            }
        }
        return html;
    }

    private void LoadData()
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnVideoTypeBll.GetVideoTypeByID(VideoTypeID);
        if (row == null)
            return;

        //Quy ve cung mot duong link
        if (Title != XuLyChuoi.ConvertToUnSign(row.VideoTypeName))
            Response.Redirect(CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(row.VideoTypeName) + "/hltw" + row.VideoTypeID + ".aspx");

        GetTreeView(row.VideoTypeID);
        var rchildren = vnnVideoTypeBll.GetDataAllChildrenByPathID("VideoTypeName,VideoTypeID,PathID", row.PathID);
        var vnnNewsBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllVideoForGridView((PageIndex - 1) * PageSize, PageSize, "", VideoTypeID, 1, "", "", "");
        rpData.DataSource = dt;
        rpData.DataBind();
        if (dt != null && dt.Count > 0)
        {
            var total = vnnNewsBll.GetAllVideoRowCount("", VideoTypeID, 1, "", "", "");
            Paging.InnerHtml = BindPaging(total);
        }
        vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var rVideoType = vnnVideoTypeBll.GetVideoTypeByID(VideoTypeID);
        if (rVideoType != null)
            SeoConfig(rVideoType.VideoTypeName, rVideoType.IsDescriptionNull() ? "" : rVideoType.Description, "", "", CurrentPage.UrlRoot + Request.RawUrl);
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
        var include = new HtmlGenericControl("base");
        include.Attributes.Add("href", CurrentPage.UrlRoot + "/today.aspx");
        CurrentPage.Header.Controls.Add(include);
    }

    public string PostAuthor(string strTemp)
    {
        try
        {
            var url = new Uri(strTemp);
            var strHref = url.Host;
            strHref = strHref.Replace("www.", "");
            return strHref;
        }
        catch
        {
            return "";
        }
    }

    private void GetTreeView(int VideoTypeid)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnVideoTypeBll.GetVideoTypeByID(VideoTypeid);
        if (row == null)
        {
            lrTreeView.Text = lrTreeView.Text;
            return;
        }
        lrTreeView.Text = "<li typeof=\"v:Breadcrumb\"><a rel=\"v:url\" property=\"v:title\" href='" + CurrentPage.UrlRoot + "/video/" + XuLyChuoi.ConvertToUnSign(row.VideoTypeName) + "/hltw" + row.VideoTypeID + ".aspx' >" + row.VideoTypeName + "</a></li>" + lrTreeView.Text;
        if (row.ParentID != -1)
            GetTreeView(row.ParentID);
    }

    #endregion
}