using System;

public partial class Admin_View : HocLapTrinhWeb.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Action)
        {
            case "new":
                Page.Title = "Tin tức | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucNews.ascx"));
                break;
            case "newsdetail":
                Page.Title = "Chi tiết tin tức | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucNewsDetail.ascx"));
                break;
            case "newtype":
                Page.Title = "Loại tin | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucNewsType.ascx"));
                break;
            case "tag":
                Page.Title = "Tag | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucTag.ascx"));
                break;
            case "updatenews":
                Page.Title = "Lấy tin tự động | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucUpdateNews.ascx"));
                break;
            case "video":
                Page.Title = "Video | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucVideo.ascx"));
                break;
            case "videotype":
                Page.Title = "Loại video | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucVideoType.ascx"));
                break;
            case "videodetail":
                Page.Title = "Chi tiết video | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucVideoDetail.ascx"));
                break;
            case "contact":
                Page.Title = "Quản lý liên hệ | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucContact.ascx"));
                break;
            case "commentnew":
                Page.Title = "Bình luận tin tức | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucCommentNews.ascx"));
                break;
            case "user":
                Page.Title = "Quản lý thành viên | Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucUser.ascx"));
                break;
            case "autoadv":
                Page.Title = "Quản lý quảng cáo - Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucAutoAdv.ascx"));
                break;
            case "seoconfig":
                Page.Title = "Cấu hình Seoconfig - Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucSeoConfig.ascx"));
                break;
            case "alexa":
                Page.Title = "Quản lý alexa - Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucAlexa.ascx"));
                break;
            case "alexadetail":
                Page.Title = "Chi tiết alexa - Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucAlexaDetail.ascx"));
                break;
            case "changepassword":
                Page.Title = "Thay đổi mật khẩu - Hoclaptrinhweb.com";
                pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucChangePassword.ascx"));
                break;
            default:
                Page.Title = "Bảng điều khiển | HocLapTrinhWeb.com";
                 pCenter.Controls.Add(Page.LoadControl("~/Admin/usercontrols/ucDefault.ascx"));
                break;
        }
    }

    private string Action
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["action"]) ? "default" : Request.QueryString["action"];
        }
    }
}