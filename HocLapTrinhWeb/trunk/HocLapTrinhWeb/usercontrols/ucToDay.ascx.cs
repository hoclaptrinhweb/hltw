using System;
using System.Globalization;
using HocLapTrinhWeb.BLL;
using System.Web.UI.HtmlControls;

public partial class usercontrols_ucToDay : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
            if (StrDate == "" || StrDate.Length != 8)
                Response.Redirect(CurrentPage.UrlRoot + "/daily/" + DateTime.Now.ToString("MMddyyyy") + ".aspx");
            else
            {
                SetBase();
                LoadData();
            }
    }

    public int NewsTypeID
    {
        get
        {
            if (Request.QueryString["NewsTypeID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["NewsTypeID"].ToString(CultureInfo.InvariantCulture));
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
            return Request.QueryString["pagesize"] != null ? int.Parse(Request.QueryString["pagesize"].ToString(CultureInfo.InvariantCulture)) : 10;
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
        var url = CurrentPage.UrlRoot + "/daily/" + StrDate + ".aspx?";

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
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetAllNewsForRepeater("NewsTypeName,Title,NewsID,RefAddress,UpdatedDate,Viewed,Thumbnail,Brief", (PageIndex - 1) * 10, 10, -1, 1, StrDate.Insert(2, "/").Insert(5, "/"), StrDate.Insert(2, "/").Insert(5, "/"), "NewsID", "Desc");
        rpData.DataSource = dt;
        rpData.DataBind();
        if (dt != null && dt.Count > 0)
        {
            var total = vnnNewsBll.GetAllNewsRowCount("",-1, 1,"", StrDate.Insert(2, "/").Insert(5, "/"), StrDate.Insert(2, "/").Insert(5, "/"));
            Paging.InnerHtml = BindPaging(total);
        }
        SeoConfig("Tin trong ngày " + StrDate.Substring(2, 2) + " tháng " + StrDate.Substring(0, 2) + " năm " + StrDate.Substring(4), "Chuyên về lĩnh vực Lập trình - Thiết kế website Graphic - HTML/CSS - Jquery - Website -Photography - Tin công nghệ - Game. Hoclaptrinhweb.com là một website tổng hợp thông tin hoàn toàn được điều khiển tự động bởi máy tính. Mỗi ngày  tin tức từ  nhiều nguồn chính thức của các web điện tử và trang tin được Hoclaptrinhweb.com tự động tổng hợp, phân loại, phát hiện các bài đăng lại....", "web online, hoc lap trinh web, hoc lap trinh, học lập trình web, lập trình web", "", CurrentPage.UrlRoot + Request.RawUrl);
    }

    private void SeoConfig(string strTitle, string strDescription, string strKeyWords, string strImage, string strUrl)
    {
        //Title
        strTitle = strTitle + (PageIndex == 1 ? "" : " - Trang " + PageIndex);
        if (strTitle.Length < 100)
            strTitle = strTitle + " - Hoclaptrinhweb.com";
        CurrentPage.Title = strTitle;
        if (Page.Master != null)
        {
            var metaTitle = (HtmlMeta)Page.Master.FindControl("metaTitle");
            if (metaTitle != null)
                metaTitle.Content = strTitle + (PageIndex == 1 ? "" : string.Format(" - Trang {0}", PageIndex));

            //Description
            var metaDesc = (HtmlMeta)Page.Master.FindControl("metaDesc");
            if (metaDesc != null)
                metaDesc.Content = strDescription;
            var metaDescFb = (HtmlMeta)Page.Master.FindControl("metaDescFb");
            if (metaDescFb != null)
                metaDescFb.Content = strDescription + (PageIndex == 1 ? "" : string.Format(" - Trang {0}", PageIndex));
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
                metaImage.Content = strImage;
        }
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

    private string StrDate
    {
        get
        {
            return !string.IsNullOrEmpty(Request.QueryString["ngay"]) ? Request.QueryString["ngay"] : "";
        }
    }
}
