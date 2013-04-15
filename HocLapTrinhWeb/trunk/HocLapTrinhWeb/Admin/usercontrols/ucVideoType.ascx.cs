using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucVideoType : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjVideoTypeObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnVideoTypeBll;
    }

    protected void DropVideoTypeDataBound(object sender, EventArgs e)
    {
        dropVideoType.Items.Insert(0, new ListItem("--Chọn Loại--", "-1"));
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnVideoTypeBll;
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
        var newsTypeBll = new VideoTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var newsTypeID = (HiddenField)row.FindControl("hdVideoTypeID");
                arrID.Add(newsTypeID.Value);
            }
            if (!newsTypeBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(newsTypeBll.getMsgCode());
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
                var newsTypeID = (HiddenField)row.FindControl("hdVideoTypeID");
                LoadDataEdit(Convert.ToInt16(newsTypeID.Value));
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
        if (e.Exception == null && e.ReturnValue != null)
        {
            if (_bGetSelectCount)
                lbTotal.Text += " / " + e.ReturnValue;
            else
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable)e.ReturnValue).Count.ToString();
        }
    }


    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdVideoTypeID.Value = string.Empty;
        txtVideoTypeName.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rVideoType = vnnVideoTypeBll.GetVideoTypeByID(pID);
            if (rVideoType != null)
            {
                hdVideoTypeID.Value = rVideoType.VideoTypeID.ToString();
                dropVideoType.SelectedValue = rVideoType.ParentID.ToString();
                txtVideoTypeName.Text = rVideoType.VideoTypeName;
                txtPriority.Text = rVideoType.Priority.ToString();
                txtDescription.Text = rVideoType.IsDescriptionNull() ? "" : rVideoType.Description;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnVideoTypeBll.getMsgCode());
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
            var newsTypeBll = new VideoTypeBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_VideoTypeDataTable();
            var row = dt.Newtbl_VideoTypeRow();
            row.VideoTypeName = txtVideoTypeName.Text;
            row.ParentID = int.Parse(dropVideoType.SelectedValue);
            row.Priority = int.Parse(txtPriority.Text);
            row.Description = txtDescription.Text;
            row.IsActive = 0;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_VideoTypeRow(row);
                if (newsTypeBll.Add(dt))
                    return true;
            }
            else
            {
                row.VideoTypeID = int.Parse(hdVideoTypeID.Value);
                dt.Addtbl_VideoTypeRow(row);
                if (newsTypeBll.Update(dt))
                    return true;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(newsTypeBll.getMsgCode());
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