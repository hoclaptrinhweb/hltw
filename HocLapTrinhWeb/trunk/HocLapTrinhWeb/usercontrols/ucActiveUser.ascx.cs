using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucActiveUser : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (Email != "" && Guid != "")
        {
            if (HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(Email + "hoclaptrinhweb.com") == Guid)
            {
                var userBll = new UserBLL(CurrentPage.getCurrentConnection());
                var dtUser = new dsHocLapTrinhWeb.tbl_UserDataTable();
                var rowUserAdd = userBll.GetUserByEmail(Email);
                if (rowUserAdd == null)
                {
                    lrResult.Text = "Tài khoản này không tồn tại !";
                    return;
                }
                if (rowUserAdd.IsActive)
                {
                    lrResult.Text = "Tài khoản này đã kích hoạt";
                    return;
                }
                var rowUserUpdate = dtUser.Newtbl_UserRow();
                rowUserUpdate.UserID = rowUserAdd.UserID;
                rowUserUpdate.IsActive = true;

                dtUser.Addtbl_UserRow(rowUserUpdate);
                if (userBll.Update(dtUser, "IsActive"))
                {
                    lrResult.Text = "Kích hoạt tài khoản thành công ! <br> Tài khoản : " + rowUserAdd.UserName;
                    return;
                }
            }
        }
        lrResult.Text = "Có lỗi xảy ra !";
    }

    private string Email
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["email"]) ? "" : Request.QueryString["email"];
        }
    }

    private string Guid
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["guid"]) ? "" : Request.QueryString["guid"];
        }
    }
}