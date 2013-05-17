using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucSeoConfig : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]), "SEOCONFIG");
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]), "SEOCONFIG");
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
                {
                    btnEdit.Visible = false;
                }

            }

        }
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnSeoConfigBll = new vnn_SeoConfigBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnSeoConfigBll;
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
        var seoConfigBll = new SeoConfigBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var seoConfigID = (HiddenField)row.FindControl("hdSeoConfigID");
                arrID.Add(seoConfigID.Value);
            }
            if (!seoConfigBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(seoConfigBll.getMsgCode());
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
                var seoConfigID = (HiddenField)row.FindControl("hdSeoConfigID");
                LoadDataEdit(Convert.ToInt16(seoConfigID.Value));
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

    protected void BtnOkClick(object sender, EventArgs e)
    {
        CurrentPage.ReloadWithAjax();
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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_SeoConfigDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdSeoConfigID.Value = string.Empty;
        txtLinkUrl.Text = string.Empty;
        txtLinkUrl.Enabled = true;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnSeoConfigBll = new vnn_SeoConfigBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rSeoConfig = vnnSeoConfigBll.GetSeoConfigByID(pID);
            if (rSeoConfig != null)
            {
                hdSeoConfigID.Value = rSeoConfig.SeoConfigID.ToString();
                txtLinkUrl.Text = rSeoConfig.LinkUrl;
                txtTitle.Text = rSeoConfig.Title;
                txtDescription.Text = rSeoConfig.Description;
                txtKeyword.Text = rSeoConfig.Keyword;
                cbIsActive.Checked = rSeoConfig.IsActive != 0;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnSeoConfigBll.getMsgCode());
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
            var seoConfigBll = new SeoConfigBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_SeoConfigDataTable();
            var row = dt.Newtbl_SeoConfigRow();
            row.LinkUrl = txtLinkUrl.Text;
            row.Title = txtTitle.Text;
            row.Description = txtDescription.Text;
            row.Keyword = txtKeyword.Text;
            row.Image = "";
            row.IsActive = cbIsActive.Checked ? 1 : 0;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_SeoConfigRow(row);
                if (seoConfigBll.Add(dt))
                    return true;
            }
            else
            {
                row.SeoConfigID = Convert.ToInt32(hdSeoConfigID.Value);
                dt.Addtbl_SeoConfigRow(row);
                if (seoConfigBll.Update(dt))
                    return true;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(seoConfigBll.getMsgCode());
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