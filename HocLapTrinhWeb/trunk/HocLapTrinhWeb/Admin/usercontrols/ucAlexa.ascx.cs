using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucAlexa : HocLapTrinhWeb.UI.UCBase
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
        var vnnAlexaBll = new vnn_AlexaBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnAlexaBll;
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
        var alexaBll = new AlexaBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hiddenField = (HiddenField)row.FindControl("hdAlexaID");
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
                var hiddenField = (HiddenField)row.FindControl("hdAlexaID");
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

    protected void BtnUpdateClick(object sender, EventArgs e)
    {
        if (UpdateNews())
            hdPopup.Value = "1";
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
                lbTotal.Text += " / " + e.ReturnValue.ToString();
            else
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDataTable)e.ReturnValue).Count.ToString();
        }
    }

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdAlexaID.Value = string.Empty;
        txtLinkUrl.Text = string.Empty;
        txtLinkUrl.Enabled = true;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnAlexaBll = new vnn_AlexaBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rAlexa = vnnAlexaBll.GetAlexaByID(pID);
            if (rAlexa != null)
            {
                hdAlexaID.Value = rAlexa.AlexaID.ToString();
                txtLinkUrl.Text = rAlexa.LinkUrl;
                txtCreatedDate.Value = rAlexa.CreatedDate;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnAlexaBll.getMsgCode());
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
            var alexaBll = new AlexaBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_AlexaDataTable();
            var row = dt.Newtbl_AlexaRow();
            row.LinkUrl = txtLinkUrl.Text;
            row.CreatedDate = txtCreatedDate.Value;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_AlexaRow(row);
                if (alexaBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(alexaBll.getMsgCode());
                return false;
            }
            
            row.AlexaID = Convert.ToInt32(hdAlexaID.Value);
            dt.Addtbl_AlexaRow(row);
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

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool UpdateNews()
    {
        try
        {
            const bool isSuccess = true;
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hiddenField = (HiddenField)row.FindControl("hdAlexaID");
                var lbLinkUrl = (Label)row.FindControl("lbLinkUrl");
                var vnnAlexaDetailBll = new vnn_AlexaDetailBLL(CurrentPage.getCurrentConnection());
                var rAlexaDetail = vnnAlexaDetailBll.GetAlexaDetailByAlexaID(int.Parse(hiddenField.Value), DateTime.Now.ToString("MM/dd/yyyy"));
                var dtDetail = new dsHocLapTrinhWeb.tbl_AlexaDetailDataTable();
                var rowAlexa = dtDetail.Newtbl_AlexaDetailRow();
                var iTrafficRank = 0;
                var iTrafficRankVn = 0;
                var iSiteLink = 0;
                Global.GetAlexa(lbLinkUrl.Text, ref iTrafficRank, ref iTrafficRankVn, ref iSiteLink);
                rowAlexa.TrafficRank = iTrafficRank;
                rowAlexa.TrafficRankVn = iTrafficRankVn;
                rowAlexa.SiteLink = iSiteLink;
                rowAlexa.AlexaID = int.Parse(hiddenField.Value);
                if (rAlexaDetail != null)
                {
                    rowAlexa.AlexaDetailID = rAlexaDetail.AlexaDetailID;
                    rowAlexa.UpdatedDate = rAlexaDetail.UpdatedDate;
                    dtDetail.Addtbl_AlexaDetailRow(rowAlexa);
                     vnnAlexaDetailBll.Update(dtDetail);
                }
                else
                {
                    rowAlexa.UpdatedDate = DateTime.Now;
                    dtDetail.Addtbl_AlexaDetailRow(rowAlexa);
                    vnnAlexaDetailBll.Add(dtDetail);
                }
            }
            return isSuccess;
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