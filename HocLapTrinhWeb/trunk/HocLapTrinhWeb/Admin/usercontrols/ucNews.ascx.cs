using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.IO;
using System.Drawing;


public partial class administrator_usercontrols_ucNews : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]),"NEWS");
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]),"NEWS");
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
                    btnEditExpress.Visible = false;
                    btnMoveNews.Visible = false;
                    btnEdit.Visible = false;
                }

            }

        }
        if (Session["PageSize"] == null)
            Session["PageSize"] = Global.Pagesize.ToString();
        gvData.PageSize = int.Parse(Session["PageSize"].ToString());
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsBll;
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
                    btnEditExpress.Text = "Hủy bỏ";
                    btnNew.Visible = false;
                    btnEdit.Visible = false;
                    btnMoveNews.Visible = false;
                    btnDelete.Visible = false;
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
                {
                    btnEditExpress.Text = "Chỉnh sửa nhanh";
                    btnSaveExpress.Visible = false;
                    btnNew.Visible = true;
                    btnMoveNews.Visible = true;
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
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
        var newsBll = new NewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrId = new ArrayList();
            var arrImage = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdNewsId = (HiddenField)row.FindControl("hdNewsID");
                arrId.Add(hdNewsId.Value);
                var hdImage = (HiddenField)row.FindControl("hdImage");
                var hdThumbnail = (HiddenField)row.FindControl("hdThumbnail");
                if (!string.IsNullOrEmpty(hdImage.Value))
                    arrImage.Add(hdImage.Value);
                if (!string.IsNullOrEmpty(hdThumbnail.Value))
                    arrImage.Add(hdThumbnail.Value);
            }
            if (newsBll.Delete(arrId))
            {
                foreach (var t in arrImage)
                {
                    var filepath = Server.MapPath("~/" + Global.ImagesNews + t);
                    if (File.Exists(filepath))
                        File.Delete(filepath);
                }
                gvData.DataBind();
                return;
            }
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(newsBll.getMsgCode());
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
                var hdNewsId = (HiddenField)row.FindControl("hdNewsID");
                CurrentPage.GoPageWithAjax("~/Admin/View.aspx?action=newsdetail&NewsId=" + hdNewsId.Value);
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
        CurrentPage.GoPage("~/Admin/View.aspx?action=newsdetail");
    }

    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;

    }

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void DropRefSiteDataBound(object sender, EventArgs e)
    {
        dropRefSite.Items.Insert(0, new ListItem("Tất cả", ""));
    }

    protected void BtnEditExpressClick(object sender, EventArgs e)
    {
        hdEdit.Value = btnEditExpress.Text != "Hủy bỏ" ? "1" : "0";
        gvData.DataBind();
    }

    protected void BtnSaveExpressClick(object sender, EventArgs e)
    {
        try
        {
            if (hdEdit.Value != "1") return;
            var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
            foreach (GridViewRow row in gvData.Rows)
            {
                var r = dt.Newtbl_NewsRow();
                var hdNewsId = (HiddenField)row.FindControl("hdNewsID");
                var chckThumbnail = (CheckBox)row.FindControl("chckThumbnail");
                var chckIsHot = (CheckBox)row.FindControl("chckIsHot");
                var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                r.NewsID = int.Parse(hdNewsId.Value);
                r.IsHot = chckIsHot.Checked;
                r.IsActive = chckThumbnail.Checked && chckIsActive.Checked;
                r.CreatedDate = DateTime.Now;
                r.UpdatedBy = int.Parse(Session["UserID"].ToString());
                r.UpdatedDate = DateTime.Now;
                r.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                dt.Addtbl_NewsRow(r);
            }
            var ltkNewsBll = new ltk_NewsBLL(CurrentPage.getCurrentConnection());
            if (!ltkNewsBll.UpdateStatus(dt, dt.IsHotColumn.ColumnName, dt.IsActiveColumn.ColumnName, dt.IPUpdateColumn.ColumnName, dt.UpdatedByColumn.ColumnName, dt.UpdatedDateColumn.ColumnName))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(ltkNewsBll.getMsgCode());
                return;
            }
            hdEdit.Value = "0";
            gvData.DataBind();
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    protected void BtnXemClick(object sender, EventArgs e)
    {
        Session["PageSize"] = dropPageSize.SelectedValue;
        gvData.PageSize = int.Parse(Session["PageSize"].ToString());
        gvData.DataBind();
    }

    protected void DrNewsTypeMoveDataBound(object sender, EventArgs e)
    {
        drNewsTypeMove.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        try
        {

            var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var r = dt.Newtbl_NewsRow();
                var hdNewsId = (HiddenField)row.FindControl("hdNewsID");
                var hdNewsTypeId = (HiddenField)row.FindControl("hdNewsTypeID");
                r.NewsID = int.Parse(hdNewsId.Value);
                r.NewsTypeID = int.Parse(drNewsTypeMove.SelectedValue);
                r.MoveFrom = int.Parse(hdNewsTypeId.Value);
                r.CreatedDate = DateTime.Now;
                r.UpdatedBy = int.Parse(Session["UserID"].ToString());
                r.UpdatedDate = DateTime.Now;
                r.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                dt.Addtbl_NewsRow(r);
            }
            var ltkNewsBll = new ltk_NewsBLL(CurrentPage.getCurrentConnection());
            if (!ltkNewsBll.MoveNews(dt))
            {
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(ltkNewsBll.getMsgCode());
                return;
            }
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
        e.InputParameters["NewsTypeID"] = dropNewsType.SelectedValue;
        e.InputParameters["IsActive"] = dropIsActive.SelectedValue;
        e.InputParameters["Refsite"] = dropRefSite.SelectedValue;
        e.InputParameters["Tag"] = dropTag.SelectedValue;
        e.InputParameters["FromDate"] = txtFromDate.Value.ToString("MM/dd/yyyy");
        e.InputParameters["ToDate"] = txtToDate.Value.AddDays(1).ToString("MM/dd/yyyy");
        e.InputParameters["selectCol"] = "NewsID,NewsTypeID,Thumbnail,Title,NewsTypeName,CreatedByUserName,IsHot,IsActive,CreatedDate,Keyword,UpdatedDate";
        e.InputParameters["orderByName"] = "UpdatedDate";
        e.InputParameters["orderByValue"] = "desc";
    }

    protected void ObjDataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

    #region Method Page

    #endregion

}
