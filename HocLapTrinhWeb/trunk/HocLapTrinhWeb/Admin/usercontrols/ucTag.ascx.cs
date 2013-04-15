using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucTag : HocLapTrinhWeb.UI.UCBase
{

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnTagBll = new v_TagBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnTagBll;
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
        var alexaBll = new t_TagBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hiddenField = (HiddenField)row.FindControl("hdTagID");
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
                var hiddenField = (HiddenField)row.FindControl("hdTagID");
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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_TagDataTable)e.ReturnValue).Count.ToString();
    }

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdTagID.Value = string.Empty;
        txtTagName.Text = "";
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnTagBll = new v_TagBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rTag = vnnTagBll.Get_TagByID(pID);
            if (rTag != null)
            {
                hdTagID.Value = rTag.TagID.ToString();
                txtTagName.Text = rTag.TagName;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnTagBll.getMsgCode());
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
            var alexaBll = new t_TagBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_TagDataTable();
            var row = dt.Newtbl_TagRow();
            row.TagName = txtTagName.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_TagRow(row);
                if (alexaBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(alexaBll.getMsgCode());
                return false;
            }

            row.TagID = Convert.ToInt32(hdTagID.Value);
            dt.Addtbl_TagRow(row);
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