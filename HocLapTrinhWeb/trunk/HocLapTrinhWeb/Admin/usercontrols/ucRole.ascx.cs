using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class dhadmincp_usercontrols_ucRole : HocLapTrinhWeb.UI.UCBase
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
        var vnnRoleBll = new vnn_RoleBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnRoleBll;
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
            lbTotal.Text = ((dsHocLapTrinhWeb.tbl_RoleDataTable)e.ReturnValue).Count.ToString();
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
        var roleBll = new RoleBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdRoleID");
                arrID.Add(findControl.Value);
            }
            if (!roleBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(roleBll.getMsgCode());
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
            txtRole.ReadOnly = true;

            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdRoleID");
                LoadDataEdit(findControl.Value);
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

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        txtRole.ReadOnly = false;
        txtRole.Text = "";
        txtDes.Text = "";
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(string pID)
    {
        var vnnRoleBll = new vnn_RoleBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rRole = vnnRoleBll.GetRoleByID(pID);
            if (rRole != null)
            {
                hdRoleID.Value = rRole.RoleID;
                txtRole.Text = rRole.RoleID;
                txtDes.Text = rRole.IsDescriptionNull() ? "" : rRole.Description;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnRoleBll.getMsgCode());
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
            var roleBll = new RoleBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_RoleDataTable();
            var row = dt.Newtbl_RoleRow();
            row.Description = txtDes.Text;
            if (hdEdit.Value == "0")
            {
                row.RoleID = txtRole.Text;
                dt.Addtbl_RoleRow(row);
                if (roleBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(roleBll.getMsgCode());
                return false;
            }
            row.RoleID = hdRoleID.Value;
            dt.Addtbl_RoleRow(row);
            if (roleBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(roleBll.getMsgCode());
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
