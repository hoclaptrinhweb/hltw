using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRegisterUser : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }
    protected void BtnRegisterClick(object sender, EventArgs e)
    {
        if (txtImgVerify.Text != ImageVerifier1.Text)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = "Mã xác nhận nhập không đúng, hãy nhập lại!";
            return;
        }
        if (!SaveData()) return;
        var guid = HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtEmail.Text + "hoclaptrinhweb.com");
        var link = CurrentPage.UrlRoot + "/activeuser.aspx?email=" + txtEmail.Text + "&guid=" + guid;
        SendMail.SendMailFrom(txtEmail.Text, "Kích hoạt tài khoản trên Hoclaptrinhweb.com", "<a href='" + link + "' title='kích hoạt tài khoản' >" + link + "</a>");
        ClearText();
        SaveValidate.IsValid = false;
        SaveValidate.ErrorMessage = "Bạn hãy mở mail để kích hoạt tài khoản !";
    }

    private void ClearText()
    {
        txtUserName.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtAddress.Text = "";
        txtFullName.Text = "";
        txtWebsite.Text = "";
        txtImgVerify.Text = "";
    }

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var userBll = new UserBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
            var row = dt.Newtbl_UserRow();
            row.UserName = txtUserName.Text;
            row.Email = txtEmail.Text;
            row.Phone = txtPhone.Text;
            row.Address = txtAddress.Text;
            row.FullName = txtFullName.Text;
            row.IsAdmin = false;
            row.IsActive = false;
            row.CreatedDate = DateTime.Now;
            row.IpAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
            row.HomePage = txtWebsite.Text;
            row.Pass = HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtPass.Text);
            dt.Addtbl_UserRow(row);
            if (userBll.Add(dt))
                return true;
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
            return false;


        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000003");
            return false;
        }
    }
}