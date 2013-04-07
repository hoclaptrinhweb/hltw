﻿using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.IO;


public partial class Admin_usercontrols_ucVideoDetail : DH.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        var videoID = Request.QueryString["VideoID"];
        if (videoID == null)
            OnLoad();
        else
            LoadDataEdit(int.Parse(videoID));
    }

    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnVideoTypeBll = new vnn_VideoTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnVideoTypeBll;
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        if (SaveData())
            Response.Redirect("Video.aspx");
    }

    protected void BtnSaveAndNewClick(object sender, EventArgs e)
    {
        if (SaveData())
            Response.Redirect("VideoDetail.aspx");
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        Response.Redirect("Video.aspx");
    }

    protected void DropVideoTypeDataBound(object sender, EventArgs e)
    {
        dropVideoType.Items.Insert(0, new ListItem("Chọn Loại Tin", "-1"));
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
            var videoBll = new t_VideoBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
            var row = dt.Newtbl_VideoRow();
            row.Title = txtTitle.Text;
            row.Brief = txtBrief.Text;
            row.Content = FCKContent.Text;
            row.VideoURL = txtLinkVideo.Text;
            row.Keyword = txtkeyword.Text;
            row.Viewed = int.Parse(txtSolanxem.Text);
            row.Priority = int.Parse(txtDouutien.Text);
            row.IPAddress = txtIPCreate.Text;
            row.CreatedBy = int.Parse(hdCreatedBy.Value);
            row.IsDelete = cboxDelete.Checked;
            row.UpdatedDate = DateTime.Now;
            row.IPUpdate = txtIPUpdate.Text;
            row.RefAddress = txtNguon.Text;
            var pathImage = CheckUploadImageThumbnail(XuLyChuoi.ConvertToUnSign(txtTitle.Text), fileuploadThumbnail, false, Global.MaxThumbnailSize, int.Parse(dropVideoType.SelectedValue));
            row.Thumbnail = pathImage != "" ? pathImage : imgThumbnail.ImageUrl.Replace("~/", "");
            row.IsHot = false;
            row.IsActive = cboxActive.Checked;
            var videoId = Request.QueryString["VideoID"];
            if (videoId == null)
            {
                row.CreatedDate = DateTime.Now;
                row.MoveFrom = int.Parse(dropVideoType.SelectedValue);
                row.VideoTypeID = int.Parse(dropVideoType.SelectedValue);
                row.UpdatedBy = int.Parse(Session["UserID"].ToString());
                row.CreatedBy = int.Parse(Session["UserID"].ToString());
                dt.Addtbl_VideoRow(row);
                if (videoBll.Add(dt))
                {
                    var tVideoTag = new t_VideoTagBLL(getCurrentConnection());
                    var dtVideoTag = new dsHocLapTrinhWeb.tbl_VideoTagDataTable();
                    var arrKey = txtkeyword.Text.Split(',');
                    for (var i = 0; i < arrKey.Length; i++)
                    {
                        if (arrKey[i] == "")
                            continue;
                        var rowVideoTag = dtVideoTag.Newtbl_VideoTagRow();
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
                                rowVideoTag.TagID = dtTag[0].TagID;
                            else
                                return false;
                        }
                        else
                            rowVideoTag.TagID = rowTag.TagID;
                        rowVideoTag.VideoID = dt[0].VideoID;
                        dtVideoTag.Addtbl_VideoTagRow(rowVideoTag);
                    }
                    tVideoTag.Add(dtVideoTag);
                    return true;
                }
                else
                    return false;
            }
            row.IPUpdate = DH.Utilities.Net.GetVisitorIPAddress();
            row.UpdatedBy = int.Parse(Session["UserID"].ToString());
            row.VideoTypeID = int.Parse(dropVideoType.SelectedValue);
            row.MoveFrom = int.Parse(hdVideoTypeID.Value);
            row.VideoID = int.Parse(videoId);
            row.CreatedDate = Convert.ToDateTime(txtNgaytao.Text);
            dt.Addtbl_VideoRow(row);
            if (videoBll.Update(dt))
            {
                if (!imgThumbnail.ImageUrl.Contains("http://") && imgThumbnail.ImageUrl != ("~/" + row.Thumbnail) && File.Exists(Server.MapPath(imgThumbnail.ImageUrl)))
                {
                    if (imgThumbnail.ImageUrl.ToLower() != "~/upload/image/noimage.jpg")
                        File.Delete(Server.MapPath(imgThumbnail.ImageUrl));
                }
                var tVideoTag = new t_VideoTagBLL(getCurrentConnection());
                var arrKey = txtkeyword.Text.Split(',');
                var arrDels = new ArrayList();
                for (var i = 0; i < arrKey.Length; i++)
                {
                    if (arrKey[i] == "")
                        continue;
                    var dtVideoTag = new dsHocLapTrinhWeb.tbl_VideoTagDataTable();
                    var rowVideoTag = dtVideoTag.Newtbl_VideoTagRow();
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
                            rowVideoTag.TagID = dtTag[0].TagID;
                    }
                    else
                        rowVideoTag.TagID = rowTag.TagID;

                    rowVideoTag.VideoID = dt[0].VideoID;

                    //Kiểm tra tag đã tồn tại trong videotag chưa
                    var rowExist = tVideoTag.Get_VideoTagByID(rowTag.TagID, dt[0].VideoID);
                    if (rowExist == null)
                    {
                        dtVideoTag.Addtbl_VideoTagRow(rowVideoTag);
                        tVideoTag.Add(dtVideoTag);
                    }

                    arrDels.Add(rowVideoTag.TagID);
                }
                //Remove not arrDels
                var dtDel = tVideoTag.Get_VideoTagByID(dt[0].VideoID, arrDels);
                tVideoTag.Delete(dtDel);
                return true;
            }
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
        txtIPCreate.Text = DH.Utilities.Net.GetVisitorIPAddress();
        txtIPUpdate.Text = DH.Utilities.Net.GetVisitorIPAddress();
        txtNgaytao.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        txtSolanxem.Text = "0";
        txtNgaycapnhat.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        txtDouutien.Text = "0";
        txtBrief.Text = "";
        txtkeyword.Text = "";
        txtNguon.Text = "";
        txtTitle.Text = "";
        FCKContent.Text = "";
        hdVideoID.Value = "";
        hdCreatedBy.Value = Session["UserID"].ToString();
        hdUpdateBy.Value = Session["UserID"].ToString();
    }

    public void LoadDataEdit(int videoID)
    {
        var videoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rVideo = videoBll.GetVideoByID(videoID);
            if (rVideo == null) return;
            dropVideoType.SelectedValue = rVideo.VideoTypeID.ToString(CultureInfo.InvariantCulture);
            hdVideoTypeID.Value = rVideo.VideoTypeID.ToString(CultureInfo.InvariantCulture);
            hdVideoID.Value = rVideo.VideoID.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = rVideo.Title;
            txtBrief.Text = rVideo.Brief;
            FCKContent.Text = rVideo.IsContentNull() ? "" : rVideo.Content;
            txtLinkVideo.Text = rVideo.VideoURL;
            txtkeyword.Text = rVideo.IsKeywordNull() ? "" : rVideo.Keyword;
            txtDouutien.Text = rVideo.Priority.ToString(CultureInfo.InvariantCulture);
            txtIPCreate.Text = rVideo.IPAddress;
            txtIPUpdate.Text = rVideo.IPUpdate;
            txtNgaycapnhat.Text = DateTime.Parse(rVideo.UpdatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNgaytao.Text = DateTime.Parse(rVideo.CreatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNguon.Text = rVideo.RefAddress;
            txtSolanxem.Text = rVideo.Viewed.ToString(CultureInfo.InvariantCulture);
            hdCreatedBy.Value = rVideo.CreatedBy.ToString(CultureInfo.InvariantCulture);
            hdUpdateBy.Value = rVideo.UpdatedBy.ToString(CultureInfo.InvariantCulture);
            cboxActive.Checked = rVideo.IsActive;
            cboxDelete.Checked = rVideo.IsDelete;
            cboxHot.Checked = rVideo.IsHot;
            if (rVideo.IsThumbnailNull() || rVideo.Thumbnail == "")
                imgThumbnail.Visible = false;
            else
                imgThumbnail.Visible = true;
            imgThumbnail.ImageUrl = rVideo.IsThumbnailNull() ? "" : (rVideo.Thumbnail.Contains("http://") ? rVideo.Thumbnail : "~/" + rVideo.Thumbnail);
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
        }
    }

    public string CheckUploadImageThumbnail(string imgfilename, FileUpload fileupload, bool resizeImage, int maxSize, int videoType)
    {
        if (fileupload.PostedFile.ContentLength == 0)
            return "";
        var suffixImage = Path.GetExtension(fileupload.FileName).ToLower();
        if (suffixImage != ".jpg" && suffixImage != ".jpeg" && suffixImage != ".tif" && suffixImage != ".png" &&
            suffixImage != ".gif" && suffixImage != ".bmp")
            return "";
        var pathImage = imgfilename + DateTime.Now.ToString("hhmmssddMMyyyy") + suffixImage;
        try
        {
            if (resizeImage)
                DH.Utilities.ImageResizer.ResizeFromStream(fileupload.PostedFile.InputStream, maxSize,
                                                           System.Drawing.Drawing2D.InterpolationMode.
                                                               HighQualityBilinear,
                                                           Server.MapPath("~/" + Global.ImagesVideo + pathImage));
            else
                fileupload.SaveAs(Server.MapPath("~/" + Global.ImagesVideo + pathImage));
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return "";
        }
        return Global.ImagesVideo + pathImage;
    }

    public string CheckUploadImage(FileUpload fileupload, bool resizeImage, int maxWithSize, int maxHeightSize, int videoType)
    {
        if (fileupload.PostedFile.ContentLength == 0)
            return "";
        var suffixImage = Path.GetExtension(fileupload.FileName).ToLower();
        if (suffixImage != ".jpg" && suffixImage != ".jpeg" && suffixImage != ".tif" && suffixImage != ".png" &&
            suffixImage != ".gif" && suffixImage != ".bmp")
            return "";
        var imgfilename = fileupload.FileName.Substring(0, fileupload.FileName.Length - 4);
        var pathImage = imgfilename + DateTime.Now.ToString("hhmmssddMMyyyy") + suffixImage;

        try
        {
            if (resizeImage)
                DH.Utilities.ImageResizer.ResizeFromStream(fileupload.PostedFile.InputStream, maxWithSize,
                                                           maxHeightSize,
                                                           System.Drawing.Drawing2D.InterpolationMode.
                                                               HighQualityBilinear,
                                                           DH.Utilities.EnumImageResizer.Align.Center,
                                                           DH.Utilities.EnumImageResizer.Valign.Middle,
                                                           Server.MapPath("~/" + Global.ImagesVideo + pathImage));
            else
                fileupload.SaveAs(Server.MapPath("~/" + Global.ImagesVideo + pathImage));
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return "";
        }
        return Global.ImagesVideo + pathImage;
    }

    #endregion
}