using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_NewsType : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]), "NEWSTYPE");
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]), "NEWSTYPE");
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

    protected void ObjNewsTypeObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;
    }

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("--Chọn Loại--", "-1"));
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;
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
        var newsTypeBll = new NewsTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var newsTypeID = (HiddenField)row.FindControl("hdNewsTypeID");
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
                var newsTypeID = (HiddenField)row.FindControl("hdNewsTypeID");
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
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable)e.ReturnValue).Count.ToString();
        }
    }


    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdNewsTypeID.Value = string.Empty;
        txtNewsTypeName.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rNewsType = vnnNewsTypeBll.GetNewsTypeByID(pID);
            if (rNewsType != null)
            {
                hdNewsTypeID.Value = rNewsType.NewsTypeID.ToString();
                dropNewsType.SelectedValue = rNewsType.ParentID.ToString();
                txtNewsTypeName.Text = rNewsType.NewsTypeName;
                txtPriority.Text = rNewsType.Priority.ToString();
                ImageUrl.ImageUrl = rNewsType.IsImageURLNull() ? "" : (rNewsType.ImageURL.Contains("http") ? rNewsType.ImageURL : CurrentPage.UrlRoot + "/" + rNewsType.ImageURL);
                txtDescription.Text = rNewsType.IsDescriptionNull() ? "" : rNewsType.Description;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnNewsTypeBll.getMsgCode());
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
            var newsTypeBll = new NewsTypeBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_NewsTypeDataTable();
            var row = dt.Newtbl_NewsTypeRow();
            row.NewsTypeName = txtNewsTypeName.Text;
            row.ParentID = int.Parse(dropNewsType.SelectedValue);
            row.Priority = int.Parse(txtPriority.Text);
            row.Description = txtDescription.Text;
            row.IsActive = 0;
            string pathImage = CheckUpload("newstype_", fileuploadImage, true, 1000);
            row.ImageURL = pathImage != "" ? pathImage : ImageUrl.ImageUrl.Replace("~/", "");
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_NewsTypeRow(row);
                if (newsTypeBll.Add(dt))
                {
                    HttpContext.Current.Cache.Remove("dataNewsType");
                    return true;
                }
            }
            else
            {
                row.NewsTypeID = int.Parse(hdNewsTypeID.Value);
                dt.Addtbl_NewsTypeRow(row);
                if (newsTypeBll.Update(dt))
                {
                    HttpContext.Current.Cache.Remove("dataNewsType");
                    return true;
                }
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

    public string CheckUpload(string imgfilename, FileUpload fileupload, bool resizeImage, int maxSize)
    {
        if (fileupload.PostedFile.ContentLength == 0)
            return "";
        var suffixImage = Path.GetExtension(fileupload.FileName).ToLower();
        if (!HocLapTrinhWeb.Utilities.clsImage.CheckIsImage(suffixImage))
            return "";
        var pathImage = imgfilename + DateTime.Now.ToString("hhmmssddMMyyyy") + suffixImage;
        try
        {
            if (resizeImage)
                HocLapTrinhWeb.Utilities.ImageResizer.ResizeFromStream(fileupload.PostedFile.InputStream, maxSize,
                                                           System.Drawing.Drawing2D.InterpolationMode.
                                                               HighQualityBilinear,
                                                           Server.MapPath("~/" + Global.ImagesNewsType + pathImage));
            else
                fileupload.SaveAs(Server.MapPath("~/" + Global.ImagesNewsType + pathImage));
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return "";
        }
        return Global.ImagesNewsType + pathImage;
    }


    #endregion
}
