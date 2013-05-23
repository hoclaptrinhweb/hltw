using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucAdmin : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        CheckLogin();
        lblFullName.Text = Session["FullName"] == null ? "Guest" : Session["FullName"].ToString();
        hpEdit.NavigateUrl = "~/Admin/View.aspx?action=newsdetail&NewsID=" + NewsID.ToString();

    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("UserName");
        Session.Remove("UserID");
        Session.Remove("FullName");
        Session.Remove("IsAdmin");
        Response.Cookies["UserName"].Expires = DateTime.Now;
        Response.Redirect("~/admin/Login.aspx");
    }

    #endregion

    #region Method Page

    /// <summary>
    /// Show Hide Dashboard Admin
    /// </summary>
    private void CheckLogin()
    {
        if (Session["UserName"] == null)
        {
            if (Request.Cookies["UserName"] != null)
            {
                if (!Login())
                    pUser.Visible = false;
                else
                    pGuest.Visible = false;
                return;
            }
            pUser.Visible = false;
        }
        else
        {
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
                liAdmin.Visible = false;
            pGuest.Visible = false;
        }
    }

    /// <summary>
    /// Check login bang cookie
    /// </summary>
    /// <returns></returns>
    private bool Login()
    {
        try
        {
            var userBll = new ltk_UserBLL(CurrentPage.getCurrentConnection());
            var row = userBll.GetUserByName(Request.Cookies["UserName"].Values["UserName"]);
            if (row == null)
            {
                return false;
            }
            if (!row.IsActive)
            {
                return false;
            }
            if (!row.IsAdmin)
                liAdmin.Visible = false;

            if (row.Pass != Request.Cookies["UserName"].Values["Password"])
            {
                return false;
            }
            Session["UserName"] = row.UserName;
            Session["FullName"] = row.FullName;
            Session["UserID"] = row.UserID.ToString();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public int NewsID
    {
        get
        {
            if (Request.QueryString["NewsID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["NewsID"]);
                }
                catch { return -1; }
            }
            return -1;
        }
    }

    #endregion

}
