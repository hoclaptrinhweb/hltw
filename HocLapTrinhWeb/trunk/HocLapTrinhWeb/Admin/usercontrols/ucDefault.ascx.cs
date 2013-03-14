using System;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucDefault : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var vNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
            var n = vNewsBll.GetAllNewsRowCount("", -1, -1, "", "", "");
            lbNews.Text += " ( " + n + " ) ";
            
            var vCommentBll = new vnn_CommentNewsBLL(getCurrentConnection());
            n = vCommentBll.GetAllCommentNewsRowCount(-1);
            lbCommentNews.Text += " ( " + n + " ) ";

            var vContactBll = new vnn_ContactBLL(getCurrentConnection());
            n = vContactBll.GetAllContactRowCount(-1);
            lbContact.Text += " ( " + n + " ) ";

            var vUserBll = new ltk_UserBLL(getCurrentConnection());
            n = vUserBll.GetAllUserRowCount(-1);
            lbUser.Text += " ( " + n + " ) ";

            var vNewsType = new vnn_NewsTypeBLL(getCurrentConnection());
            n = vNewsType.GetAllNewsTypeRowCount();
            lbNewsType.Text += " ( " + n + " ) ";
        }
    }
}
