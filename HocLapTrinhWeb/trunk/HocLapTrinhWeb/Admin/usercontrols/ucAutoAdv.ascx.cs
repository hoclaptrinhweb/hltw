using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucAutoAdv : HocLapTrinhWeb.UI.UCBase
{


    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]), "AUTOADV");
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]), "AUTOADV");
            if (dt == null || dt.Rows.Count == 0)
            {
                CurrentPage.GoPage("~/admin/View.aspx");
                return;
            }
            var dtuser = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
            var rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ANYSYSTEM'");
            if (rows.Length == 0)
            {
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='VIEW'");
                if (rows.Length == 0)
                    CurrentPage.GoPage("~/admin/View.aspx");
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='DELETE'");
                if (rows.Length == 0)
                    btnDelete.Visible = false;
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ADD'");
                if (rows.Length == 0)
                    btnNew.Visible = false;
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='UPDATE'");
                if (rows.Length == 0)
                    btnEdit.Visible = false;
            }

        }
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnAutoAdvBll = new vnn_AutoAdvBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnAutoAdvBll;
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
        var alexaBll = new AutoAdvBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hiddenField = (HiddenField)row.FindControl("hdAutoAdvID");
                arrID.Add(hiddenField.Value);
            }
            if (!alexaBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(alexaBll.getMsgCode());
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
                var hiddenField = (HiddenField)row.FindControl("hdAutoAdvID");
                LoadDataEdit(Convert.ToInt16(hiddenField.Value));
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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_AutoAdvDataTable)e.ReturnValue).Count.ToString();
    }



    #region Method Page

    private void RefreshControl()
    {
        hdAutoAdvID.Value = string.Empty;
        txtCurrentClick.Text = "50";
        txtCurrentClickTop.Text = "50";
        txtCreatedDate.Value = DateTime.Now;
        txtTotalClick.Text = "0";
        txtTotalClickTop.Text = "0";
        cbIsActive.Checked = false;
        cbIsActiveTop.Checked = false;
        txtTimeLimit.Text = "0";
        txtTypeID.Text = "0";
    }

    private void LoadDataEdit(int pID)
    {
        var vnnAutoAdvBll = new vnn_AutoAdvBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rAutoAdv = vnnAutoAdvBll.GetAutoAdvByID(pID);
            if (rAutoAdv != null)
            {
                hdAutoAdvID.Value = rAutoAdv.AutoAdvID.ToString();
                txtCurrentClick.Text = rAutoAdv.CurrentClick.ToString();
                txtTotalClick.Text = rAutoAdv.TotalClick.ToString();
                cbIsActive.Checked = rAutoAdv.IsAcitve == 1;
                txtCurrentClickTop.Text = rAutoAdv.CurrentClickTop.ToString();
                txtTotalClickTop.Text = rAutoAdv.TotalClickTop.ToString();
                cbIsActiveTop.Checked = rAutoAdv.IsAcitveTop == 1;
                txtCreatedDate.Value = rAutoAdv.CreatedDate;
                txtTypeID.Text = rAutoAdv.IsTypeIDNull() ? "0" : rAutoAdv.TypeID.ToString();
                txtTimeLimit.Text = rAutoAdv.IsTimeLimitNull() ? "0" : rAutoAdv.TimeLimit.ToString();
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnAutoAdvBll.getMsgCode());
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
            var alexaBll = new AutoAdvBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
            var row = dt.Newtbl_AutoAdvRow();
            row.TotalClick = int.Parse(txtTotalClick.Text);
            row.TotalClickTop = int.Parse(txtTotalClickTop.Text);
            row.CurrentClick = int.Parse(txtCurrentClick.Text);
            row.CurrentClickTop = int.Parse(txtCurrentClickTop.Text);
            row.TypeID = int.Parse(txtTypeID.Text);
            row.TimeLimit = int.Parse(txtTimeLimit.Text);
            row.CreatedDate = txtCreatedDate.Value;
            row.IsAcitve = cbIsActive.Checked ? 1 : 0;
            row.IsAcitveTop = cbIsActiveTop.Checked ? 1 : 0;
            row.UpdatedDate = DateTime.Now;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_AutoAdvRow(row);
                if (alexaBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(alexaBll.getMsgCode());
                return false;
            }

            row.AutoAdvID = Convert.ToInt32(hdAutoAdvID.Value);
            dt.Addtbl_AutoAdvRow(row);
            if (alexaBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(alexaBll.getMsgCode());
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