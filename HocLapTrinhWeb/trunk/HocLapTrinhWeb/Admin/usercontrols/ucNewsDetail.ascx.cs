﻿using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.IO;
using System.Collections;

public partial class administrator_usercontrols_NewsDetail : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
        var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]), "NEWS");
        if (isAllow == null || isAllow == false)
            CurrentPage.GoPage("~/admin/View.aspx");

        var newsId = Request.QueryString["NewsID"];
        if (newsId == null)
        {
            //New and View
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]), "NEWS");
            if (dt == null || dt.Rows.Count == 0)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dtuser = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
            var rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ANYSYSTEM'");
            if (rows.Length == 0)
            {
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='VIEW'");
                if (rows.Length == 0)
                    CurrentPage.GoPage("~/admin/View.aspx");
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ADD'");
                if (rows.Length == 0)
                    CurrentPage.GoPage("~/admin/View.aspx");
            }
            OnLoad();
        }
        else
        {
            //Update and View
            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]), "NEWS");
            if (dt == null || dt.Rows.Count == 0)
                CurrentPage.GoPage("~/admin/View.aspx");
            var dtuser = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
            var rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ANYSYSTEM'");
            if (rows.Length == 0)
            {
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='VIEW'");
                if (rows.Length == 0)
                    CurrentPage.GoPage("~/admin/View.aspx");

                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='UPDATE'");
                if (rows.Length == 0)
                {
                    btnSaveAndNew.Visible = false;
                    btnSave.Visible = false;
                }
            }
            LoadDataEdit(int.Parse(newsId));
        }
    }

    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        if (SaveData())
            Response.Redirect("~/admin/View.aspx?action=new");
    }

    protected void BtnSaveAndNewClick(object sender, EventArgs e)
    {
        if (SaveData())
            Response.Redirect("~/Admin/View.aspx?action=newsdetail");
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/View.aspx?action=new");
    }

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Chọn Loại Tin", "-1"));
    }

    #endregion

    #region Method Page

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var newsBll = new NewsBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
            var row = dt.Newtbl_NewsRow();
            row.Title = txtTitle.Text;
            row.Brief = txtBrief.Text;
            row.Content = FCKContent.Text;
            row.Keyword = txtkeyword.Text;
            row.Viewed = int.Parse(txtSolanxem.Text);
            row.Priority = int.Parse(txtDouutien.Text);
            row.IPAddress = txtIPCreate.Text;
            row.CreatedBy = int.Parse(hdCreatedBy.Value);
            row.IsDelete = cboxDelete.Checked;
            row.UpdatedDate = DateTime.Now;
            row.IPUpdate = txtIPUpdate.Text;
            row.RefAddress = txtNguon.Text;
            if (cbxImage.Checked)
            {
                if (txtImage.Text != "")
                    try
                    {
                        var webClient = new WebClient();
                        var path = txtImage.Text;
                        var fileName = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);
                        var suffixImage = Path.GetExtension(fileName).ToLower();
                        fileName = DateTime.Now.ToString("ddMMyyyy") + "_" + XuLyChuoi.ConvertToUnSign(row.Title) + suffixImage;
                        webClient.DownloadFile(path, Server.MapPath("~/" + Global.ImagesNews + fileName));
                        row.Thumbnail = Global.ImagesNews + fileName;
                    }
                    catch (Exception)
                    {
                        row.Thumbnail = Global.ImagesNews + "noimage.jpg";
                    }
            }
            else
            {
                var pathImage = CheckUploadImageThumbnail(XuLyChuoi.ConvertToUnSign(txtTitle.Text), fileuploadThumbnail, false, Global.MaxThumbnailSize, int.Parse(dropNewsType.SelectedValue));
                row.Thumbnail = pathImage != "" ? pathImage : imgThumbnail.ImageUrl.Replace("~/", "");
            }

            row.Image = "";
            row.IsHot = cboxHot.Checked;
            row.IsShowImage = false;
            row.IsActive = cboxActive.Checked;
            var newsId = Request.QueryString["NewsID"];

            if (newsId == null)
            {
                row.CreatedDate = DateTime.Now;
                row.MoveFrom = int.Parse(dropNewsType.SelectedValue);
                row.NewsTypeID = int.Parse(dropNewsType.SelectedValue);
                row.UpdatedBy = int.Parse(Session["UserID"].ToString());
                row.CreatedBy = int.Parse(Session["UserID"].ToString());
                dt.Addtbl_NewsRow(row);
                if (newsBll.Add(dt))
                {
                    var tNewsTag = new t_NewsTagBLL(getCurrentConnection());
                    var dtNewsTag = new dsHocLapTrinhWeb.tbl_NewsTagDataTable();
                    var arrKey = txtkeyword.Text.Split(',');
                    for (var i = 0; i < arrKey.Length; i++)
                    {
                        if (arrKey[i] == "")
                            continue;
                        var rowNewsTag = dtNewsTag.Newtbl_NewsTagRow();
                        // Lưu vào tbl_Tag
                        var vTagBll = new v_TagBLL(getCurrentConnection());
                        var rowTag = vTagBll.GetTagByName(arrKey[i]);
                        if (rowTag == null)
                        {
                            var dtTag = new dsHocLapTrinhWeb.tbl_TagDataTable();
                            rowTag = dtTag.Newtbl_TagRow();
                            rowTag.TagName = arrKey[i];
                            dtTag.Addtbl_TagRow(rowTag);
                            if (vTagBll.Add(dtTag))
                                rowNewsTag.TagID = dtTag[0].TagID;
                            else
                                return false;
                        }
                        else
                            rowNewsTag.TagID = rowTag.TagID;
                        rowNewsTag.NewsID = dt[0].NewsID;
                        dtNewsTag.Addtbl_NewsTagRow(rowNewsTag);
                    }
                    tNewsTag.Add(dtNewsTag);
                    return true;
                }
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(newsBll.getMsgCode());
                return false;
            }
            row.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
            row.UpdatedBy = int.Parse(Session["UserID"].ToString());
            row.NewsTypeID = int.Parse(dropNewsType.SelectedValue);
            row.MoveFrom = int.Parse(hdNewsTypeID.Value);
            row.NewsID = int.Parse(newsId);
            row.CreatedDate = Convert.ToDateTime(txtNgaytao.Text);
            dt.Addtbl_NewsRow(row);
            if (newsBll.Update(dt))
            {
                if (imgThumbnail.ImageUrl != ("~/" + row.Thumbnail) &&
                    File.Exists(Server.MapPath(imgThumbnail.ImageUrl)))
                {
                    if (imgThumbnail.ImageUrl.ToLower() != "~/upload/image/noimage.jpg")
                        File.Delete(Server.MapPath(imgThumbnail.ImageUrl));
                }
                var tNewsTag = new t_NewsTagBLL(getCurrentConnection());
                var arrKey = txtkeyword.Text.Split(',');
                var arrDels = new ArrayList();
                for (var i = 0; i < arrKey.Length; i++)
                {
                    if (arrKey[i] == "")
                        continue;
                    var dtNewsTag = new dsHocLapTrinhWeb.tbl_NewsTagDataTable();
                    var rowNewsTag = dtNewsTag.Newtbl_NewsTagRow();
                    // Lưu vào tbl_Tag
                    var vTagBll = new v_TagBLL(getCurrentConnection());
                    var rowTag = vTagBll.GetTagByName(arrKey[i]);
                    if (rowTag == null)
                    {
                        var dtTag = new dsHocLapTrinhWeb.tbl_TagDataTable();
                        rowTag = dtTag.Newtbl_TagRow();
                        rowTag.TagName = arrKey[i];
                        dtTag.Addtbl_TagRow(rowTag);
                        if (vTagBll.Add(dtTag))
                            rowNewsTag.TagID = dtTag[0].TagID;
                    }
                    else
                        rowNewsTag.TagID = rowTag.TagID;

                    rowNewsTag.NewsID = dt[0].NewsID;

                    //Kiểm tra tag đã tồn tại trong newstag chưa
                    var rowExist = tNewsTag.Get_NewsTagByID(rowTag.TagID, dt[0].NewsID);
                    if (rowExist == null)
                    {
                        dtNewsTag.Addtbl_NewsTagRow(rowNewsTag);
                        tNewsTag.Add(dtNewsTag);
                    }

                    arrDels.Add(rowNewsTag.TagID);
                }
                //Remove not arrDels
                var dtDel = tNewsTag.Get_NewsTagByID(dt[0].NewsID, arrDels);
                tNewsTag.Delete(dtDel);
                return true;
            }
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(newsBll.getMsgCode());
            return false;

        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return false;
        }
    }

    void OnLoad()
    {
        txtIPCreate.Text = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
        txtIPUpdate.Text = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
        txtNgaytao.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        txtSolanxem.Text = "0";
        txtNgaycapnhat.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        txtDouutien.Text = "0";
        txtBrief.Text = "";
        txtkeyword.Text = "";
        txtNguon.Text = "";
        txtTitle.Text = "";
        FCKContent.Text = "";
        hdNewsID.Value = "";
        hdCreatedBy.Value = Session["UserID"].ToString();
        hdUpdateBy.Value = Session["UserID"].ToString();
    }

    public void LoadDataEdit(int newsID)
    {
        var newsBll = new vnn_NewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rNews = newsBll.GetNewsByID(newsID);
            if (rNews == null) return;
            dropNewsType.SelectedValue = rNews.NewsTypeID.ToString(CultureInfo.InvariantCulture);
            hdNewsTypeID.Value = rNews.NewsTypeID.ToString(CultureInfo.InvariantCulture);
            hdNewsID.Value = rNews.NewsID.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = rNews.Title;
            txtBrief.Text = rNews.Brief;
            FCKContent.Text = rNews.Content;
            txtMoveFrom.Text = rNews.NameMoveFrom ?? "";
            txtkeyword.Text = rNews.IsKeywordNull() ? "" : rNews.Keyword;
            txtDouutien.Text = rNews.Priority.ToString(CultureInfo.InvariantCulture);
            txtIPCreate.Text = rNews.IPAddress;
            txtIPUpdate.Text = rNews.IPUpdate;
            txtNgaycapnhat.Text = DateTime.Parse(rNews.UpdatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNgaytao.Text = DateTime.Parse(rNews.CreatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNguon.Text = rNews.RefAddress;
            txtSolanxem.Text = rNews.Viewed.ToString(CultureInfo.InvariantCulture);
            txtUserCreate.Text = rNews.CreatedByUserName;
            hdCreatedBy.Value = rNews.CreatedBy.ToString(CultureInfo.InvariantCulture);
            txtUserUpdate.Text = rNews.UpdatedByUserName;
            hdUpdateBy.Value = rNews.UpdatedBy.ToString(CultureInfo.InvariantCulture);
            cboxActive.Checked = rNews.IsActive;
            cboxDelete.Checked = rNews.IsDelete;
            cboxHot.Checked = rNews.IsHot;
            var videoBll = new t_VideoBLL(CurrentPage.getCurrentConnection());
            var rVideo = videoBll.GetVideoByID(rNews.NewsID);
            if (rNews.IsThumbnailNull() || rNews.Thumbnail == "")
                imgThumbnail.Visible = false;
            else
                imgThumbnail.Visible = true;
            imgThumbnail.ImageUrl = rNews.IsThumbnailNull() ? "" : (rNews.Thumbnail.Contains("http://") ? rNews.Thumbnail : "~/" + rNews.Thumbnail);
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
        }
    }

    public string CheckUploadImageThumbnail(string imgfilename, FileUpload fileupload, bool resizeImage, int maxSize, int newsType)
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
                                                           Server.MapPath("~/" + Global.ImagesNews + pathImage));
            else
                fileupload.SaveAs(Server.MapPath("~/" + Global.ImagesNews + pathImage));
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return "";
        }
        return Global.ImagesNews + pathImage;
    }

    public string CheckUploadImage(FileUpload fileupload, bool resizeImage, int maxWithSize, int maxHeightSize, int newsType)
    {
        if (fileupload.PostedFile.ContentLength == 0)
            return "";
        var suffixImage = Path.GetExtension(fileupload.FileName).ToLower();
        if (!HocLapTrinhWeb.Utilities.clsImage.CheckIsImage(suffixImage))
            return "";
        var imgfilename = fileupload.FileName.Substring(0, fileupload.FileName.Length - 4);
        var pathImage = imgfilename + DateTime.Now.ToString("hhmmssddMMyyyy") + suffixImage;

        try
        {
            if (resizeImage)
                HocLapTrinhWeb.Utilities.ImageResizer.ResizeFromStream(fileupload.PostedFile.InputStream, maxWithSize,
                                                           maxHeightSize,
                                                           System.Drawing.Drawing2D.InterpolationMode.
                                                               HighQualityBilinear,
                                                           HocLapTrinhWeb.Utilities.EnumImageResizer.Align.Center,
                                                           HocLapTrinhWeb.Utilities.EnumImageResizer.Valign.Middle,
                                                           Server.MapPath("~/" + Global.ImagesNews + pathImage));
            else
                fileupload.SaveAs(Server.MapPath("~/" + Global.ImagesNews + pathImage));
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return "";
        }
        return Global.ImagesNews + pathImage;
    }

    #endregion

}
