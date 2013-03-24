﻿using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.IO;
using System.Drawing;

public partial class Admin_usercontrols_ucVideo : DH.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (Session["PageSize"] == null)
            Session["PageSize"] = Global.Pagesize.ToString();
        gvData.PageSize = int.Parse(Session["PageSize"].ToString());
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnVideoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnVideoBll;
    }

    protected void ObjRefSiteObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var ltkReferenceSiteBll = new ltk_ReferenceSiteBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = ltkReferenceSiteBll;
    }

    protected void GvDataRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var isnew = (DateTime)DataBinder.Eval(e.Row.DataItem, "CreatedDate");
        var isActive = (bool)DataBinder.Eval(e.Row.DataItem, "IsActive");
        if (isnew.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy"))
            e.Row.BackColor = Color.FromName(isActive ? "#FFF3ED" : "#DBF0FA");
    }

    protected void GvDataDataBound(object sender, EventArgs e)
    {
        try
        {
            if (gvData.Rows.Count > 0)
            {
                if (hdEdit.Value == "1")
                {
                    btnSaveExpress.Visible = true;
                    var chckAllIsHot = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsHot");
                    chckAllIsHot.Enabled = true;
                    var chckAllIsActive = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsActive");
                    chckAllIsActive.Enabled = true;

                    foreach (GridViewRow row in gvData.Rows)
                    {
                        var chckIsHot = (CheckBox)row.FindControl("chckIsHot");
                        chckIsHot.Enabled = true;
                        var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                        chckIsActive.Enabled = true;
                    }
                }
                else
                    btnSaveExpress.Visible = false;
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
        var VideoBll = new t_VideoBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            var arrImage = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdVideoID = (HiddenField)row.FindControl("hdVideoID");
                arrID.Add(hdVideoID.Value);
                var hdThumbnail = (HiddenField)row.FindControl("hdThumbnail");
                if (!string.IsNullOrEmpty(hdThumbnail.Value))
                    arrImage.Add(hdThumbnail.Value);
            }
            if (VideoBll.Delete(arrID))
            {
                foreach (var t in arrImage)
                {
                    var filepath = Server.MapPath("~/" + Global.ImagesVideo + t);
                    if (File.Exists(filepath))
                        File.Delete(filepath);
                }
                gvData.DataBind();
                return;
            }
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(VideoBll.getMsgCode());
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
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdVideoID = (HiddenField)row.FindControl("hdVideoID");
                CurrentPage.GoPageWithAjax("VideoDetail.aspx?VideoId=" + hdVideoID.Value);
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
        CurrentPage.GoPage("VideoDetail.aspx");
    }

    /// <summary>
    /// dropdown Videotype
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;

    }

    protected void DropVideoTypeDataBound(object sender, EventArgs e)
    {
        dropVideoType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void DropRefSiteDataBound(object sender, EventArgs e)
    {
        dropRefSite.Items.Insert(0, new ListItem("Tất cả", ""));
    }

    //Sửa tin nhanh
    protected void BtnEditExpressClick(object sender, EventArgs e)
    {
        hdEdit.Value = "1";
        gvData.DataBind();
    }

    protected void BtnSaveExpressClick(object sender, EventArgs e)
    {
        try
        {
            if (hdEdit.Value != "1") return;
            var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
            foreach (GridViewRow row in gvData.Rows)
            {
                var r = dt.Newtbl_VideoRow();
                var hdVideoID = (HiddenField)row.FindControl("hdVideoID");
                var chckThumbnail = (CheckBox)row.FindControl("chckThumbnail");
                var chckIsHot = (CheckBox)row.FindControl("chckIsHot");
                var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                r.VideoID = int.Parse(hdVideoID.Value);
                r.IsHot = chckIsHot.Checked;
                r.IsActive = chckThumbnail.Checked && chckIsActive.Checked;
                r.CreatedDate = DateTime.Now;
                r.UpdatedBy = int.Parse(Session["UserID"].ToString());
                r.UpdatedDate = DateTime.Now;
                r.IPUpdate = DH.Utilities.Net.GetVisitorIPAddress();
                dt.Addtbl_VideoRow(r);
            }
            var ltkVideoBll = new t_VideoBLL(CurrentPage.getCurrentConnection());
            //if (!ltkVideoBll.UpdateStatus(dt, dt.IsHotColumn.ColumnName, dt.IsActiveColumn.ColumnName, dt.IPUpdateColumn.ColumnName, dt.UpdatedByColumn.ColumnName, dt.UpdatedDateColumn.ColumnName))
            //{
            //    SaveValidate.IsValid = false;
            //    SaveValidate.ErrorMessage = msg.GetMessage(ltkVideoBll.getMsgCode());
            //    return;
            //}
            hdEdit.Value = "0";
            gvData.DataBind();
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    //Tìm kiếm
    protected void BtnXemClick(object sender, EventArgs e)
    {
        Session["PageSize"] = dropPageSize.SelectedValue;
        gvData.PageSize = int.Parse(Session["PageSize"].ToString());
        gvData.DataBind();
    }


    //Chuyển tin
    protected void DrVideoTypeMoveDataBound(object sender, EventArgs e)
    {
        drVideoTypeMove.Items.Insert(0, new ListItem("Tất cả", "-1"));

    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        try
        {

            var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var r = dt.Newtbl_VideoRow();
                var hdVideoID = (HiddenField)row.FindControl("hdVideoID");
                var hdVideoTypeID = (HiddenField)row.FindControl("hdVideoTypeID");
                r.VideoID = int.Parse(hdVideoID.Value);
                r.VideoTypeID = int.Parse(drVideoTypeMove.SelectedValue);
                r.MoveFrom = int.Parse(hdVideoTypeID.Value);
                r.CreatedDate = DateTime.Now;
                r.UpdatedBy = int.Parse(Session["UserID"].ToString());
                r.UpdatedDate = DateTime.Now;
                r.IPUpdate = DH.Utilities.Net.GetVisitorIPAddress();
                dt.Addtbl_VideoRow(r);
            }
            //var ltkVideoBll = new ltk_VideoBLL(CurrentPage.getCurrentConnection());
            //if (!ltkVideoBll.MoveVideo(dt))
            //{
            //    SaveValidate1.IsValid = false;
            //    SaveValidate1.ErrorMessage = msg.GetMessage(ltkVideoBll.getMsgCode());
            //    return;
            //}
            hdIsAddSuccessful.Value = "1";

            gvData.DataBind();
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    protected void BtnCancelClick(object sender, EventArgs e)
    {
        hdIsAddSuccessful.Value = "1";
        gvData.DataBind();
    }

    bool _bGetSelectCount;
    protected void ObjDataSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        _bGetSelectCount = e.ExecutingSelectCount;
    //    e.InputParameters["VideoTypeID"] = dropVideoType.SelectedValue;
    //    e.InputParameters["IsActive"] = dropIsActive.SelectedValue;
    //    e.InputParameters["Refsite"] = dropRefSite.SelectedValue;
    //    e.InputParameters["FromDate"] = txtFromDate.Value.ToString("MM/dd/yyyy");
    //    e.InputParameters["ToDate"] = txtToDate.Value.ToString("MM/dd/yyyy");
    //    e.InputParameters["selectCol"] = "VideoID,VideoTypeID,Thumbnail,Title,VideoTypeName,CreatedByUserName,IsHot,IsActive,CreatedDate";
    }

    protected void ObjDataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        // Get total count from the ObjectDataSource
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_VideoDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

    #region Method Page

    #endregion
}