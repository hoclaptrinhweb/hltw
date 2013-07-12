using System;
using System.Web.UI.HtmlControls;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucNews : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        GetMenuNewsType(NewsTypeID);
        LoadData();
    }

    #endregion

    #region Method Page

    public int NewsTypeID
    {
        get
        {
            if (Request.QueryString["NewsTypeID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["NewsTypeID"]);
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
                return Request.QueryString["pagesize"] != null ? int.Parse(Request.QueryString["pagesize"]) : 30;
            }
            catch
            {
                return 30;
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
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnNewsTypeBll.GetNewsTypeByID(NewsTypeID);
        var url = CurrentPage.UrlRoot + "/" + (row != null ? XuLyChuoi.ConvertToUnSign(row.NewsTypeName) : NewsTypeID.ToString()) + "/hltw" + NewsTypeID + ".aspx" + (PageSize == 30 ? "?" : "?pagesize=" + PageSize + "&");

        var html = "";
        var nSumOfPage = (total - 1) / PageSize + 1;
        if (nSumOfPage > 1 || total > PageSize)
        {
            if (PageIndex > 1)
            {
                html += "<a data-role=\"button\" data-icon=\"arrow-l\"  data-inline=\"true\" data-theme=\"b\" href=\"" + url + "trang=" + (PageIndex - 1) + "\">Sau</a>";
            }
            if (PageIndex < nSumOfPage)
            {
                html += "<a data-role=\"button\" data-icon=\"arrow-r\" data-inline=\"true\" data-theme=\"b\" data-iconpos=\"right\" href=\"" + url + "trang=" + (PageIndex + 1) + "\">Tới</a>";
            }
        }
        return html;
    }

    private void LoadData()
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var row = vnnNewsTypeBll.GetNewsTypeByID(NewsTypeID);
        if (row == null)
        {
            Response.Redirect("~/FileNotFound.html");
            return;
        }

        //Quy ve cung mot duong link
        if (Title != XuLyChuoi.ConvertToUnSign(row.NewsTypeName))
            Response.Redirect(CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(row.NewsTypeName) + "/hltw" + row.NewsTypeID + ".aspx");

        var rchildren = vnnNewsTypeBll.GetDataAllChildrenByPathID("NewsTypeName,NewsTypeID,PathID", row.PathID);
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsForGridView((PageIndex - 1) * PageSize, PageSize,true, "NewsTypeName,Title,NewsID,RefAddress,UpdatedDate,Viewed,Thumbnail,Brief", rchildren, 1, "", "", "");
        rpData.DataSource = dt;
        rpData.DataBind();
        if (dt != null && dt.Count > 0)
        {
            var total = vnnNewsBll.GetAllNewsRowCount("", rchildren, 1, "", "", "");
            divPaging.InnerHtml = BindPaging(total);
        }
        vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        var rNewsType = vnnNewsTypeBll.GetNewsTypeByID(NewsTypeID);
        if (rNewsType != null)
            SeoConfig(rNewsType.NewsTypeName, rNewsType.IsDescriptionNull() ? "" : rNewsType.Description, "", "", CurrentPage.UrlRoot + Request.RawUrl);
    }

    private void SeoConfig(string strTitle, string strDescription, string strKeyWords, string strImage, string strUrl)
    {
        strTitle = strTitle + (PageIndex == 1 ? "" : " - Trang " + PageIndex);
        if (strTitle.Length < 100)
            strTitle = strTitle + " - hoclaptrinhweb.com";
        CurrentPage.Title = strTitle;
        var lbHeaderTilte = (HtmlGenericControl)Page.Master.FindControl("ucHeader1").FindControl("hHeaderTitle");
        if (lbHeaderTilte != null)
            lbHeaderTilte.InnerText = strTitle;
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

    private void GetMenuNewsType(int productID)
    {
        try
        {
            var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
            var dt = vnnNewsTypeBll.GetDataByParentID("NewsTypeName,NewsTypeID,TotalNews", productID);
            if (dt == null || dt.Count <= 0) return;
            lbProductType.Text += "<ul  data-role=\"listview\" data-inset=\"true\" data-count-theme=\"b\">";
            lbProductType.Text += "<li data-role=\"list-divider\">Mục con</li>";
            foreach (var t in dt)
            {
                lbProductType.Text += "<li><a href='" + CurrentPage.UrlRoot + "/" + XuLyChuoi.ConvertToUnSign(t.NewsTypeName) + "/hltw" + t.NewsTypeID + ".aspx'>" + t.NewsTypeName + "<span class=\"ui-li-count\">" + t.TotalNews.ToString() + "</span></a>";
                lbProductType.Text += "</li>";
            }

            lbProductType.Text += "</ul>";
        }
        catch
        {
            return;
        }

    }

    #endregion

}
