using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Collections;

public partial class Admin_usercontrols_ucContact : DH.UI.UCBase
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
        var vnnContactBll = new vnn_ContactBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnContactBll;
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
        var contactBll = new ContactBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hiddenField = (HiddenField)row.FindControl("hdContactID");
                arrID.Add(hiddenField.Value);
            }
            if (!contactBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(contactBll.getMsgCode());
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
                var findControl = (HiddenField)row.FindControl("hdContactID");
                LoadDataEdit(Convert.ToInt16(findControl.Value));
            }
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        RefreshControl();
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void DropContactTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
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
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable)e.ReturnValue).Count.ToString();
        }
    }

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdContactID.Value = string.Empty;
        txtUsername.Text = string.Empty;
        txtContent.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtContent.Text = string.Empty;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnContactBll = new vnn_ContactBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rContact = vnnContactBll.GetContactByID(pID);
            if (rContact != null)
            {
                hdContactID.Value = rContact.ContactID.ToString();
                txtUsername.Text = rContact.UserName;
                txtEmail.Text = rContact.Email;
                txtPhone.Text = rContact.IsPhoneNull() ? "" : rContact.Phone;
                txtAddress.Text = rContact.IsAddressNull() ? "" : rContact.Address;
                txtTitle.Text = rContact.Title;
                txtContent.Text = rContact.Content;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnContactBll.getMsgCode());
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
            var contactBll = new ContactBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_ContactDataTable();
            var row = dt.Newtbl_ContactRow();
            row.Title = txtTitle.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_ContactRow(row);
                if (contactBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(contactBll.getMsgCode());
                return false;
            }
            row.ContactID = Convert.ToInt32(hdContactID.Value);
            dt.Addtbl_ContactRow(row);
            if (contactBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(contactBll.getMsgCode());
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
