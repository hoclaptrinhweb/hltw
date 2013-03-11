using System;
using System.Web;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucLoginUser : DH.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;

        if (Request.UrlReferrer != null)
            Session["nextpage"] = Request.UrlReferrer.ToString();
        else
            Session["nextpage"] = "~/";
        if (Request["logout"] == null || Request["logout"] == "") return;
        Session.Remove("UserName");
        Session.Remove("UserID");
        Session.Remove("FullName");
        Session.Remove("IsAdmin");
        Response.Cookies["UserName"].Expires = DateTime.Now;
        if (Session["nextpage"] != null && Session["nextpage"].ToString().ToLower().Contains(CurrentPage.UrlRoot.ToLower()))
            CurrentPage.GoPage(Session["nextpage"].ToString());
        else
            CurrentPage.GoPage("~/");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!Login()) return;
        var aCookie1 = new HttpCookie("UserName");
        aCookie1.Values["UserName"] = txtUserName.Text;
        aCookie1.Values["Password"] = DH.Utilities.Cryptography.EncryptMD5(txtPassword.Text);
        aCookie1.Expires = DateTime.Now.AddDays(7);
        Response.Cookies.Add(aCookie1);

        if (Session["nextpage"] != null && Session["nextpage"].ToString().ToLower().Contains(CurrentPage.UrlRoot.ToLower()))
            CurrentPage.GoPage(Session["nextpage"].ToString());
        else
            CurrentPage.GoPage("~/");
    }


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
            if (row.Pass != DH.Utilities.Cryptography.EncryptMD5(txtPassword.Text))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG003");
                txtPassword.Focus();
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