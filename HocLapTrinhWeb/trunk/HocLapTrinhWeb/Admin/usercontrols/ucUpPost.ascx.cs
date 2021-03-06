using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Drawing;

public partial class Admin_usercontrols_ucUpPost : HocLapTrinhWeb.UI.UCBase
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
        var vnnUpPostBll = new vnn_UpPostBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpPostBll;
    }

    protected void GvDataDataBound(object sender, EventArgs e)
    {
        try
        {
            btnDelete.Enabled = gvData.Rows.Count > 0;
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

    protected void GvDataRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var isnew = (DateTime)DataBinder.Eval(e.Row.DataItem, "CreatedDate");
        if (isnew.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy"))
            e.Row.BackColor = Color.FromName("#FFF3ED");
    }

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var upPostBll = new UpPostBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdPostID");
                arrID.Add(findControl.Value);
            }
            if (!upPostBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(upPostBll.getMsgCode());
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

    private void RefreshControl()
    {
        txtUrl.Text = "";
        txtUser.Text = "";
        hdPostID.Value = "";
    }


    private bool SaveData()
    {
        try
        {
            var upPostBll = new UpPostBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_PostDataTable();
            var row = dt.Newup_tbl_PostRow();

            var upUserBll = new UpUserBLL(CurrentPage.getCurrentConnection());
            var rowUser = upUserBll.GetUserByName(txtUser.Text);
            row.UserID = rowUser != null ? rowUser.UserID : 3;
            row.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
            row.IsActive = 1;
            row.CreatedDate = DateTime.Now;
            row.RefAddress = txtUrl.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addup_tbl_PostRow(row);
                if (upPostBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(upPostBll.getMsgCode());
                return false;
            }
            row.PostID = Convert.ToInt32(hdPostID.Value);
            dt.Addup_tbl_PostRow(row);
            if (upPostBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upPostBll.getMsgCode());
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
        if (e.Exception == null && e.ReturnValue != null)
        {
            if (_bGetSelectCount)
                lbTotal.Text += " / " + e.ReturnValue;
            else
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_UpPostDataTable)e.ReturnValue).Count.ToString();
        }
    }

    #endregion

}

