using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucUpUser : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpUserBll = new vnn_UpUserBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpUserBll;
    }

    protected void GvDataDataBound(object sender, EventArgs e)
    {
        try
        {
            if (gvData.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    protected void GvDataPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
    }

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var upUserBll = new UpUserBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdUserID");
                arrID.Add(findControl.Value);
            }
            if (!upUserBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(upUserBll.getMsgCode());
                return;
            }
            gvData.DataBind();
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000005");
        }
    }

    protected void BtnEditClick(object sender, EventArgs e)
    {
        try
        {
            RefreshControl();
            hdEdit.Value = "1";
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdUserID");
                LoadDataEdit(Convert.ToInt16(findControl.Value));
            }
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    protected void BtnNewClick(object sender, EventArgs e)
    {
        RefreshControl();
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        if (!SaveData()) return;
        hdIsAddSuccessful.Value = "1";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void BtnSaveAndNewClick(object sender, EventArgs e)
    {
        if (!SaveData()) return;
        RefreshControl();
        hdIsAddSuccessful.Value = "0";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        RefreshControl();
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdUserID.Value = string.Empty;
        txtUserName.Text = string.Empty;
        txtUserName.Enabled = true;
        hdPass.Value = string.Empty;
        txtPass.Text = string.Empty;
        txtPassConfirm.Text = string.Empty;
        txtPass.Enabled = true;
        txtPassConfirm.Enabled = true;
        rfvPass.Visible = true;
        cvPass.Visible = true;
        rqvConfirmPass.Visible = true;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnUpUserBll = new vnn_UpUserBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rUser = vnnUpUserBll.GetUserByID(pID);
            if (rUser != null)
            {
                hdUserID.Value = rUser.UserID.ToString();
                txtUserName.Text = rUser.UserName;
                txtUserName.Enabled = false;
                hdPass.Value = rUser.Password;
                txtPass.Enabled = false;
                txtPassConfirm.Enabled = false;
                rfvPass.Visible = false;
                cvPass.Visible = false;
                rqvConfirmPass.Visible = false;
                txtEmail.Text = rUser.Email;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnUpUserBll.getMsgCode());
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var upUserBll = new UpUserBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_UserDataTable();
            var row = dt.Newup_tbl_UserRow();
            row.UserName = txtUserName.Text;
            row.Email = txtEmail.Text;
            row.Password = txtPass.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addup_tbl_UserRow(row);
                if (upUserBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(upUserBll.getMsgCode());
                return false;
            }
            row.UserID = Convert.ToInt32(hdUserID.Value);
            dt.Addup_tbl_UserRow(row);
            if (upUserBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upUserBll.getMsgCode());
            return false;
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000003");
            return false;
        }
    }

    #endregion

}
