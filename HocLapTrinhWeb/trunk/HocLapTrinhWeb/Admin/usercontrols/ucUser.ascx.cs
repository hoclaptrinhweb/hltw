using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class administrator_usercontrols_ucUser : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserIsAdmin(Convert.ToInt32(Session["UserID"]));
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/view.aspx");
        }
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var ltkUserBll = new ltk_UserBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = ltkUserBll;
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

    bool _bGetSelectCount;
    protected void ObjDataSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        _bGetSelectCount = e.ExecutingSelectCount;
    }

    protected void ObjDataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable)e.ReturnValue).Count.ToString();
    }

    protected void dropIsActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
        gvData.DataBind();
    }

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var userBll = new UserBLL(CurrentPage.getCurrentConnection());
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
            if (!userBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
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
        catch
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

    protected void ChckChangePassCheckedChanged(object sender, EventArgs e)
    {
        txtPass.Enabled = chckChangePass.Checked;
        txtPassConfirm.Enabled = chckChangePass.Checked;
        rfvPass.Visible = chckChangePass.Checked;
        cvPass.Visible = chckChangePass.Checked;
        rqvConfirmPass.Visible = chckChangePass.Checked;
    }

    #endregion

    #region Method Page

    private void RefreshControl()
    {
        chckChangePass.Visible = false;
        chckChangePass.Checked = false;
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
        txtFullName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtAddress.Text = string.Empty;
        chckIsAdmin.Checked = false;
        chckIsActive.Checked = true;
        txtCreatedDate.SetNullValueDate = false;
        txtCreatedDate.Value = DateTime.Now;
        txtCurrentDate.SetNullValueDate = false;
        txtCurrentDate.Value = DateTime.Now;
        txtHomePage.Text = "";
        txtIpAddress.Text = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
    }

    private void LoadDataEdit(int pID)
    {
        var ltkUserBll = new ltk_UserBLL(CurrentPage.getCurrentConnection());
        try
        {
            chckChangePass.Visible = true;
            var rUser = ltkUserBll.GetUserByID(pID);
            if (rUser != null)
            {
                hdUserID.Value = rUser.UserID.ToString();
                txtUserName.Text = rUser.UserName;
                txtUserName.Enabled = false;
                hdPass.Value = rUser.Pass;
                txtPass.Enabled = false;
                txtPassConfirm.Enabled = false;
                rfvPass.Visible = false;
                cvPass.Visible = false;
                rqvConfirmPass.Visible = false;
                txtFullName.Text = rUser.IsFullNameNull() ? "" : rUser.FullName;
                txtEmail.Text = rUser.Email;
                txtPhone.Text = rUser.IsPhoneNull() ? "" : rUser.Phone;
                txtAddress.Text = rUser.IsAddressNull() ? "" : rUser.Address;
                chckIsAdmin.Checked = rUser.IsAdmin;
                chckIsActive.Checked = rUser.IsActive;
                txtCreatedDate.Value = rUser.CreatedDate;
                txtIpAddress.Text = rUser.IsIpAddressNull() ? "" : rUser.IpAddress;
                txtHomePage.Text = rUser.IsHomePageNull() ? "" : rUser.HomePage;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(ltkUserBll.getMsgCode());
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

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
            row.IsAdmin = chckIsAdmin.Checked;
            row.IsActive = chckIsActive.Checked;
            row.CreatedDate = txtCreatedDate.Value;
            row.IpAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
            row.HomePage = txtHomePage.Text;
            if (hdEdit.Value == "0")
            {
                row.Pass = HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtPass.Text);
                dt.Addtbl_UserRow(row);
                if (userBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
                return false;
            }
            row.Pass = chckChangePass.Checked ? HocLapTrinhWeb.Utilities.Cryptography.EncryptMD5(txtPass.Text) : hdPass.Value;
            row.UserID = Convert.ToInt32(hdUserID.Value);
            dt.Addtbl_UserRow(row);
            if (userBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(userBll.getMsgCode());
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
