using System;
using HocLapTrinhWeb.BLL;

public partial class administrator_usercontrols_ucSideBar : DH.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        var strDate = DateTime.Now.ToString("MM/dd/yyyy");
        var vnnComment = new vnn_CommentNewsBLL(getCurrentConnection());
        var n = vnnComment.GetAllCommentNewsRowCount(-1, strDate);
        hpCommemnt.Text += " ( " + (n == -1 ? "0" : n.ToString()) + " )";

        var vnnContact = new vnn_ContactBLL(getCurrentConnection());
        n = vnnContact.GetAllContactRowCount(-1, strDate);
        hpContact.Text += " ( " + (n == -1 ? "0" : n.ToString()) + " )";

        var vnnNews = new vnn_NewsBLL(getCurrentConnection());
        n = vnnNews.GetAllNewsRowCount("",-1, -1,"", strDate, strDate);
        hpNews.Text += " ( " + (n == -1 ? "0" : n.ToString()) + " )";

        var vnnUser = new ltk_UserBLL(getCurrentConnection());
        n = vnnUser.GetAllUserRowCount(strDate);
        hpUser.Text += " ( " + (n == -1 ? "0" : n.ToString()) + " )";
    }

    #endregion

    #region Method Page

    protected string SetClass(params string[] defaultpage)
    {
        try
        {
            foreach (var t in defaultpage)
            {
                if (CurrentPage.GetRequestURL().ToLower().Contains(t.ToLower()))
                    return "active";
            }
            return "";
        }
        catch { return ""; }
    }

    protected string IsShow(params string[] defaultpage)
    {
        try
        {
            foreach (var t in defaultpage)
            {
                if (CurrentPage.GetRequestURL().ToLower().Contains(t.ToLower()))
                    return "show";
            }
            return "hide";
        }
        catch { return "hide"; }
    }

    #endregion
}
