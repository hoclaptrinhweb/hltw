using System;

public partial class administrator_usercontrols_WebUserControl : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        lblFullName.Text = Session["FullName"] == null ? "Guest" : Session["FullName"].ToString();
        btnChangePass.Visible = Session["UserID"] != null;
    }

    protected void BtnLogoutClick(object sender, EventArgs e)
    {
        Session.Remove("UserName");
        Session.Remove("UserID");
        Session.Remove("FullName");
        Session.Remove("IsAdmin");
        Response.Cookies["UserName"].Expires = DateTime.Now;
        Response.Redirect("~/admin/Login.aspx");
    }

    #endregion
}
