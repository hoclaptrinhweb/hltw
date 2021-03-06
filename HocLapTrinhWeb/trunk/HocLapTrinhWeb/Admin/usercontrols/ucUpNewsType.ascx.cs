using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucUpNewsType : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjData_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpNewsTypeBll = new vnn_UpNewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpNewsTypeBll;
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
        var upNewsTypeBll = new UpNewsTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdUpNewsTypeID = (HiddenField)row.FindControl("hdUpNewsTypeID");
                arrID.Add(hdUpNewsTypeID.Value);
            }
            if (!upNewsTypeBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(upNewsTypeBll.getMsgCode());
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
                var hdUpNewsTypeID = (HiddenField)row.FindControl("hdUpNewsTypeID");
                LoadDataEdit(Convert.ToInt16(hdUpNewsTypeID.Value));
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
        txtNewsTypeName.Text = "";
        txtDescription.Text = "";
        hdNewsTypeID.Value = "0";
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnUpNewsTypeBll = new vnn_UpNewsTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rUpNewsType = vnnUpNewsTypeBll.GetNewsTypeByID(pID);
            if (rUpNewsType != null)
            {
                txtNewsTypeName.Text = rUpNewsType.NewsTypeName;
                txtDescription.Text = rUpNewsType.Description;
                hdNewsTypeID.Value = rUpNewsType.NewsTypeID.ToString();
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnUpNewsTypeBll.getMsgCode());
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
            var upNewsTypeBll = new UpNewsTypeBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_NewsTypeDataTable();
            var row = dt.Newup_tbl_NewsTypeRow();
            row.NewsTypeName = txtNewsTypeName.Text;
            row.Description = txtDescription.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addup_tbl_NewsTypeRow(row);
                if (upNewsTypeBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(upNewsTypeBll.getMsgCode());
                return false;
            }
            row.NewsTypeID = int.Parse(hdNewsTypeID.Value);
            dt.Addup_tbl_NewsTypeRow(row);
            if (upNewsTypeBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upNewsTypeBll.getMsgCode());
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

    #region CountRow

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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsTypeDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

}