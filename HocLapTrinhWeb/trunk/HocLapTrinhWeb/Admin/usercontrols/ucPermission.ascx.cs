using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucPermission : HocLapTrinhWeb.UI.UCBase
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
        var vnnPermissionBll = new vnn_PermissionBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnPermissionBll;
    }

    private bool _bGetSelectCount;

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
            lbTotal.Text = ((dsHocLapTrinhWeb.tbl_PermissionDataTable) e.ReturnValue).Count.ToString();
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
        var permissionBll = new PermissionBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrId = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox) row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField) row.FindControl("hdPermissionID");
                arrId.Add(findControl.Value);
            }
            if (!permissionBll.Delete(arrId))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(permissionBll.getMsgCode());
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
            txtPermission.ReadOnly = true;

            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox) row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField) row.FindControl("hdPermissionID");
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
        txtPermission.ReadOnly = false;
        txtPermission.Text = "";
        txtDes.Text = "";
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pId"></param>
    private void LoadDataEdit(string pId)
    {
        var vnnPermissionBll = new vnn_PermissionBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rPermission = vnnPermissionBll.GetPermissionByID(pId);
            if (rPermission != null)
            {
                hdPermissionID.Value = rPermission.PermissionID;
                txtPermission.Text = rPermission.PermissionID;
                txtDes.Text = rPermission.IsDescriptionNull() ? "" : rPermission.Description;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnPermissionBll.getMsgCode());
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
            var permissionBll = new PermissionBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_PermissionDataTable();
            var row = dt.Newtbl_PermissionRow();
            row.Description = txtDes.Text;
            if (hdEdit.Value == "0")
            {
                row.PermissionID = txtPermission.Text;
                dt.Addtbl_PermissionRow(row);
                if (permissionBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(permissionBll.getMsgCode());
                return false;
            }
            row.PermissionID = hdPermissionID.Value;
            dt.Addtbl_PermissionRow(row);
            if (permissionBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(permissionBll.getMsgCode());
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