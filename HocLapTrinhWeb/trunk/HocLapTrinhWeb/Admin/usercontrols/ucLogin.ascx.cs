using System;
using System.Web;
using HocLapTrinhWeb.BLL;

public partial class administrator_usercontrols_ucLogin : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (Session["UserName"] != null)
            CurrentPage.GoPage("~/admin/View.aspx");
        txtUserName.Focus();
    }

    protected void BtnLoginClick(object sender, EventArgs e)
    {
        var p = Request.QueryString["nextpage"] == null ? "" : Server.UrlDecode(Request.QueryString["nextpage"]);
        if (!Login()) return;
        var aCookie1 = new HttpCookie("UserName");
        aCookie1.Values["UserName"] = txtUserName.Text;
        aCookie1.Values["Password"] = HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtPass.Text);
        aCookie1.Expires = DateTime.Now.AddDays(7);
        Response.Cookies.Add(aCookie1);

        CurrentPage.GoPage(p != "" ? p : "~/admin/View.aspx");
    }

    #endregion

    #region Private Function
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool Login()
    {
        try
        {
            var userBll = new ltk_UserBLL(CurrentPage.getCurrentConnection());
            var row = userBll.GetUserByName(txtUserName.Text);
            if (row == null)
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
                txtUserName.Focus();
                return false;
            }
            if (!row.IsActive)
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG007");
                txtUserName.Focus();
                return false;
            }
            if (row.Pass != HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtPass.Text))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG003");//mat khau ko dung
                txtPass.Focus();
                return false;
            }
            Session["UserID"] = row.UserID;
            Session["UserName"] = row.UserName;
            Session["FullName"] = row.FullName;
            Session["IsAdmin"] = row.IsAdmin;
            return true;
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG001");
            return false;
        }
    }

    #endregion

}
