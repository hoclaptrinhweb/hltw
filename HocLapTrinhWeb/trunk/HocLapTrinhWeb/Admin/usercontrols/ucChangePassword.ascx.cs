using System;
using HocLapTrinhWeb.BLL;

public partial class administrator_usercontrols_ucChangePassword : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
            txtUserName.Text = Session["UserName"].ToString();
    }


    protected void BtnSaveClick(object sender, EventArgs e)
    {
        SaveData();
    }
    protected void BtnCancelClick(object sender, EventArgs e)
    {
        CurrentPage.GoPageWithAjax("Default.aspx");
    }

    #endregion

    #region Method Page

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private void SaveData()
    {
        try
        {
            var userBll = new UserBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
            var row = userBll.GetUserByID(Convert.ToInt16(Session["UserID"].ToString()));
            if (row != null)
            {
                if (row.Pass.Equals(DH.Utilities.Cryptography.EncryptMD5(txtOldPass.Text)))
                {
                    row.Pass = DH.Utilities.Cryptography.EncryptMD5(txtPass.Text);
                    dt.ImportRow(row);
                    if (userBll.Update(dt))
                    {
                        SaveValidate.IsValid = false;
                        SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG006");
                        return;
                    }
                    SaveValidate.IsValid = false;
                    SaveValidate.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
                    return;
                }
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage("ERR-LOG005");//mat khau cu khong dung
                return;
            }
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    #endregion
}
