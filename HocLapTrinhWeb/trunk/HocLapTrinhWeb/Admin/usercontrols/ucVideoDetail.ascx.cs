using System;
using System.Globalization;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.IO;
using System.Collections;


public partial class Admin_usercontrols_ucVideoDetail : DH.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        if (VideoID == -1)
            OnLoad();
        else
            LoadDataEdit(VideoID);
    }

    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;
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

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Chọn Loại Tin", "-1"));
    }

    #endregion

    #region Method Page

    private int VideoID
    {
        get
        {
            if (!string.IsNullOrEmpty(Request.QueryString["VideoID"]))
                try
                { return int.Parse(Request.QueryString["VideoID"]); }
                catch { return -1; }
            return -1;
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
            var videoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
            var row = dt.Newtbl_VideoRow();
            row.Title = txtTitle.Text;
            row.Brief = txtBrief.Text;
            row.VideoURL = txtLinkVideo.Text;
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
            var pathImage = CheckUploadImageThumbnail(XuLyChuoi.ConvertToUnSign(txtTitle.Text), fileuploadThumbnail, false, Global.MaxThumbnailSize, int.Parse(dropNewsType.SelectedValue));
            row.Thumbnail = pathImage != "" ? pathImage : imgThumbnail.ImageUrl.Replace("~/", "");
            row.IsHot = false;
            row.IsActive = cboxActive.Checked;
            if (VideoID == -1)
            {
                row.CreatedDate = DateTime.Now;
                row.MoveFrom = int.Parse(dropNewsType.SelectedValue);
                row.VideoTypeID = int.Parse(dropNewsType.SelectedValue);
                row.UpdatedBy = int.Parse(Session["UserID"].ToString());
                row.CreatedBy = int.Parse(Session["UserID"].ToString());
                dt.Addtbl_VideoRow(row);
                return videoBll.Add(dt);
            }
            row.IPUpdate = DH.Utilities.Net.GetVisitorIPAddress();
            row.UpdatedBy = int.Parse(Session["UserID"].ToString());
            row.VideoTypeID = int.Parse(dropNewsType.SelectedValue);
            row.MoveFrom = int.Parse(hdVideoTypeID.Value);
            row.VideoID = VideoID;
            row.CreatedDate = Convert.ToDateTime(txtNgaytao.Text);
            dt.Addtbl_VideoRow(row);
            if (videoBll.Update(dt))
            {
                if (imgThumbnail.ImageUrl != ("~/" + row.Thumbnail) &&
                    File.Exists(Server.MapPath(imgThumbnail.ImageUrl)))
                {
                    if (imgThumbnail.ImageUrl.ToLower() != "~/upload/image/noimage.jpg")
                        File.Delete(Server.MapPath(imgThumbnail.ImageUrl));
                }
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
        hdVideoTypeID.Value = "";
        hdCreatedBy.Value = Session["UserID"].ToString();
        hdUpdateBy.Value = Session["UserID"].ToString();
    }

    public void LoadDataEdit(int newsID)
    {
        var videoBll = new v_VideoBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rNews = videoBll.GetVideoByID(newsID);
            if (rNews == null) return;
            dropNewsType.SelectedValue = rNews.VideoTypeID.ToString(CultureInfo.InvariantCulture);
            hdVideoTypeID.Value = rNews.VideoTypeID.ToString(CultureInfo.InvariantCulture);
            hdVideoID.Value = rNews.VideoID.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = rNews.Title;
            txtLinkVideo.Text = rNews.VideoURL;
            txtBrief.Text = rNews.Brief;
            FCKContent.Text = rNews.Content;
            //   txtMoveFrom.Text = rNews.NameMoveFrom ?? "";
            txtkeyword.Text = rNews.IsKeywordNull() ? "" : rNews.Keyword;
            txtDouutien.Text = rNews.Priority.ToString(CultureInfo.InvariantCulture);
            txtIPCreate.Text = rNews.IPAddress;
            txtIPUpdate.Text = rNews.IPUpdate;
            txtNgaycapnhat.Text = DateTime.Parse(rNews.UpdatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNgaytao.Text = DateTime.Parse(rNews.CreatedDate.ToString(), new CultureInfo(CurrentPage.Language), DateTimeStyles.None).ToString();
            txtNguon.Text = rNews.IsRefAddressNull() ? "" : rNews.RefAddress;
            txtSolanxem.Text = rNews.Viewed.ToString(CultureInfo.InvariantCulture);
            //    txtUserCreate.Text = rNews.CreatedByUserName;
            hdCreatedBy.Value = rNews.CreatedBy.ToString(CultureInfo.InvariantCulture);
            //    txtUserUpdate.Text = rNews.UpdatedByUserName;
            hdUpdateBy.Value = rNews.UpdatedBy.ToString(CultureInfo.InvariantCulture);
            cboxActive.Checked = rNews.IsActive;
            cboxDelete.Checked = rNews.IsDelete;
            cboxHot.Checked = rNews.IsHot;
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
        if (suffixImage != ".jpg" && suffixImage != ".jpeg" && suffixImage != ".tif" && suffixImage != ".png" &&
            suffixImage != ".gif" && suffixImage != ".bmp")
            return "";
        var pathImage = imgfilename + "-" + DateTime.Now.ToString("hhmmssddMMyyyy") + suffixImage;
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

    #endregion

}