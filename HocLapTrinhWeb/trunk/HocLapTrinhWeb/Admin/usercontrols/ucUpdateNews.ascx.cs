using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Net;
using HtmlAgilityPack;

public partial class Admin_usercontrols_ucUpdateNews : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserRole(Convert.ToInt32(Session["UserID"]),"AUTONEWS");
            if (isAllow == null || isAllow == false)
            {
                CurrentPage.GoPage("~/admin/view.aspx");
            }

            var dt = userPermissionBll.GetUserRolePermission(Convert.ToInt32(Session["UserID"]),"AUTONEWS");
            if (dt == null || dt.Rows.Count == 0)
                CurrentPage.GoPage("~/admin/view.aspx");
            var dtuser = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
            var rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ANYSYSTEM'");
            if (rows.Length == 0)
            {
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='VIEW'");
                if (rows.Length == 0)
                    CurrentPage.GoPage("~/admin/view.aspx");
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='DELETE'");
                if (rows.Length == 0)
                    btnDelete.Visible = false;
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='ADD'");
                if (rows.Length == 0)
                    btnNew.Visible = false;
                rows = dt.Select(dtuser.PermissionIDColumn.ColumnName + "='UPDATE'");
                if (rows.Length == 0)
                {
                    btnUpdate.Visible = false;
                    btnRefresh.Visible = false;
                }
            }
        }
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var ltkReferenceSiteBll = new ltk_ReferenceSiteBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = ltkReferenceSiteBll;
    }

    protected void GvDataDataBound(object sender, EventArgs e)
    {
        try
        {
            btnUpdate.Enabled = gvData.Rows.Count > 0;
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    protected void GvDataRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var lblUpdatedDate = (Label)e.Row.FindControl("lblUpdatedDate");
            var imgNews = (Image)e.Row.FindControl("imgNews");
            var imgWarning = (Image)e.Row.FindControl("imgWarning");
            var dWarning = DateTime.Now.Subtract(Convert.ToDateTime(lblUpdatedDate.Text));
            lblUpdatedDate.Text += " (Cách " + dWarning.TotalHours.ToString("#,##0.#") + " giờ)";
            if (!(dWarning.TotalMinutes >= Global.Warning))
            {
                imgNews.Visible = true;
                imgWarning.Visible = false;
            }
            else
            {
                imgNews.Visible = false;
                imgWarning.Visible = true;
            }
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    protected void GvDataPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
    }

    protected void DrNewsTypeDataBound(object sender, EventArgs e)
    {
        drNewsType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void DrNewsTypeAddDataBound(object sender, EventArgs e)
    {
        drNewsTypeAdd.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void BtnUpdateClick(object sender, EventArgs e)
    {
        if (!UpdateNews()) return;
        hdIsAddSuccessful1.Value = "1";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void BtnRefreshClick(object sender, EventArgs e)
    {
        gvData.DataBind();
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
                var referenceSiteID = (HiddenField)row.FindControl("hdReferenceSiteID");
                LoadDataEdit(Convert.ToInt16(referenceSiteID.Value));
            }
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000004");
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

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var referenceSiteBll = new ReferenceSiteBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var referenceSiteID = (HiddenField)row.FindControl("hdReferenceSiteID");
                arrID.Add(referenceSiteID.Value);
            }
            if (!referenceSiteBll.Delete(arrID))
            {
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(referenceSiteBll.getMsgCode());
                return;
            }
            gvData.DataBind();
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000005");
        }
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

    protected void DrNewsTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
        gvData.DataBind();
    }

    protected void BtnUpdatesClick(object sender, EventArgs e)
    {
        var updateNewsBase = new UpdateNewsBase();
        var arr = updateNewsBase.GetNewsBrief(txtLinkPage.Text, int.Parse(txtTotalPage.Text), txtParaTitle.Text,
                                              txtParaBrief.Text, txtParaImage.Text);
        var arrConent = updateNewsBase.GetNewsByURL(arr, txtParaContent.Text);
        gvData1.DataSource = arrConent;
        gvData1.DataBind();
    }


    private bool _bGetSelectCount;

    protected void ObjDataSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        _bGetSelectCount = e.ExecutingSelectCount;
    }

    protected void ObjDataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable)e.ReturnValue).Count.ToString();
    }

    protected void ObjNewsTypeObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnNewsTypeBll;
    }

    protected void dropIsActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
        gvData.DataBind();
    }

    #endregion

    #region Method Page


    private void LoadData()
    {
        var vnnNewsTypeBll = new vnn_NewsTypeBLL(CurrentPage.getCurrentConnection());
        try
        {
            //Load NewsType
            var dtNewsType = vnnNewsTypeBll.GetAllNewsTypeForGridView();
            if (dtNewsType == null)
            {
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(vnnNewsTypeBll.getMsgCode());
                return;
            }
            drNewsType.DataSource = dtNewsType;
            drNewsType.DataBind();

            drNewsTypeAdd.DataSource = dtNewsType;
            drNewsTypeAdd.DataBind();
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
    private bool UpdateNews()
    {
        try
        {
            var isSuccess = true;
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                var chckIsAutoRun = (CheckBox)row.FindControl("chckIsAutoRun");
                if (!chckDelete.Checked) continue;
                var hdReferenceSiteId = (HiddenField)row.FindControl("hdReferenceSiteID");
                var hdNewsTypeID = (HiddenField)row.FindControl("hdNewsTypeID");
                var referenceSiteBll = new ReferenceSiteBLL(CurrentPage.getCurrentConnection());
                var r = referenceSiteBll.GetReferenceSiteByID(Convert.ToInt32(hdReferenceSiteId.Value));
                if (r == null)
                {
                    SaveValidate1.IsValid = false;
                    SaveValidate1.ErrorMessage = msg.GetMessage(referenceSiteBll.getMsgCode());
                    return false;
                }
                var ltkNewsBll = new ltk_NewsBLL(CurrentPage.getCurrentConnection());
                var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                var updateNewsBase = new UpdateNewsBase();

                var config = r.ConfigSite.Split(new[] { "$$$" }, StringSplitOptions.None);
                var arr = updateNewsBase.GetNewsBrief(config[0], int.Parse(config[1]), config[2], config[3], config[4]);
                var arrnew = new ArrayList();
                foreach (var t in arr)
                {
                    if (!ltkNewsBll.CheckExistByRefAddress(((ArrayList)t)[0].ToString()))
                        arrnew.Add(t);
                }
                var arrConent = updateNewsBase.GetNewsByURL(arrnew, config[5]);
                if (arrConent == null)
                {
                    SaveValidate1.IsValid = false;
                    SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000003");
                    continue;
                }
                for (var i = 0; i < arrConent.Count; i++)
                {

                    if (ltkNewsBll.CheckExistByRefAddress(((ArrayList)arrConent[i])[0].ToString()))
                        continue;

                    var rNews = dt.Newtbl_NewsRow();
                    rNews.Title = XuLyChuoi.ConvertTitle(((ArrayList)arrConent[i])[1].ToString(),
                                                         ((ArrayList)arrConent[i])[0].ToString());
                    rNews.Brief = XuLyChuoi.ConvertBrief(((ArrayList)arrConent[i])[2].ToString(),
                                                         ((ArrayList)arrConent[i])[0].ToString());
                    if (rNews.Brief.Length > 2000)
                        rNews.Brief = rNews.Brief.Substring(0, 2000);
                    string fileName;
                    var path = HttpUtility.UrlDecode(((ArrayList)arrConent[i])[3].ToString()).Replace("&amp;", "&");
                    if (path.ToLower().Contains("1anh.com"))
                    {
                        rNews.Thumbnail = path;
                    }
                    else {
                        try
                        {
                            var webClient = new WebClient();
                            if (path.ToLower().Contains("thanhnien.com.vn"))
                            {
                                var n = path.IndexOf(";", StringComparison.Ordinal);
                                if (n > -1)
                                    path = path.Substring(0, n);
                            }
                            else if (path.ToLower().Contains("http://pridio.com"))
                            {
                                path = path.Replace("-500x128", "");
                            }
                            else if (path.ToLower().Contains("vietnamnet.vn"))
                                path = path.Replace("?w=142&h=100", "");
                            fileName = path;
                            fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.Ordinal) + 1);
                            if (fileName.IndexOf('?') != -1)
                                fileName = fileName.Substring(0, fileName.IndexOf('?'));
                            if (fileName == "")
                                fileName = XuLyChuoi.ConvertToUnSign(rNews.Title) + ".jpg";
                            fileName = DateTime.Now.ToString("ddMMyyyy") + fileName;
                            webClient.DownloadFile(path, Server.MapPath("~/" + Global.ImagesNews + fileName));
                        }
                        catch (Exception)
                        {
                            fileName = "noimage.jpg";
                        }

                        rNews.Thumbnail = Global.ImagesNews + fileName;
                    }
                    
                    rNews.Content = XuLyChuoi.ConvertContent(((ArrayList)arrConent[i])[4].ToString(),
                                                             ((ArrayList)arrConent[i])[0].ToString());
                    if (rNews.Content.ToLower() == "không lấy được nội dung")
                        continue;
                    rNews.NewsTypeID = Convert.ToInt16(hdNewsTypeID.Value);

                    rNews.Image = "";
                    rNews.RefAddress = ((ArrayList)arrConent[i])[0].ToString();
                    rNews.IsShowImage = false;
                    rNews.IsHot = false;
                    rNews.Viewed = 1;
                    rNews.Priority = i;
                    rNews.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                    rNews.CreatedDate = DateTime.Now.AddMilliseconds(double.Parse(i.ToString()));
                    rNews.CreatedBy = Convert.ToInt16(Session["UserID"].ToString());
                    rNews.UpdatedDate = DateTime.Now.AddMilliseconds(double.Parse(i.ToString()));
                    rNews.UpdatedBy = Convert.ToInt16(Session["UserID"].ToString());
                    rNews.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                    rNews.IsDelete = false;
                    rNews.IsActive = chckIsAutoRun.Checked;
                    rNews.MoveFrom = Convert.ToInt16(hdNewsTypeID.Value);
                    dt.Addtbl_NewsRow(rNews);
                }

                var dtRefSite = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                var rRefSite = dtRefSite.Newtbl_ReferenceSiteRow();
                rRefSite.ReferenceSiteID = Convert.ToInt16(hdReferenceSiteId.Value);
                rRefSite.UpdatedDate = DateTime.Now;
                rRefSite.UpdateRows = dt.Count;
                if (ltkNewsBll.AddFromSite(dt, rRefSite)) continue;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage += "Cập nhật: " + r.RefAddress + " thất bại.\n";
                isSuccess = false;
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

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        txtLinkPage.Text = "";
        txtTotalPage.Text = "1";
        txtParaTitle.Text = "";
        txtParaImage.Text = "";
        txtParaContent.Text = "";
        cbAuto.Checked = false;
    }

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var referenceSiteBll = new ReferenceSiteBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
            var row = dt.Newtbl_ReferenceSiteRow();
            row.RefAddress = txtLinkPage.Text;
            var url = new Uri(txtLinkPage.Text);
            row.RefSite = url.Host;
            row.RefSiteDescription = "";
            row.IsAutoRun = cbAuto.Checked;
            row.UpdatedDate = DateTime.Now;
            row.UpdateRows = 0;
            row.NewsTypeID = int.Parse(drNewsTypeAdd.SelectedValue);
            row.ConfigSite = txtLinkPage.Text + "$$$" + txtTotalPage.Text + "$$$" + txtParaTitle.Text + "$$$" +
                             txtParaBrief.Text + "$$$" + txtParaImage.Text + "$$$" + txtParaContent.Text;
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_ReferenceSiteRow(row);
                if (referenceSiteBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(referenceSiteBll.getMsgCode());
                return false;
            }
            row.ReferenceSiteID = Convert.ToInt32(hdReferenceSiteID.Value);
            dt.Addtbl_ReferenceSiteRow(row);
            if (referenceSiteBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(referenceSiteBll.getMsgCode());
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
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var ltkReferenceSiteBll = new ltk_ReferenceSiteBLL(CurrentPage.getCurrentConnection());
        try
        {
            var r = ltkReferenceSiteBll.GetReferenceSiteByID(pID);
            if (r != null)
            {
                hdReferenceSiteID.Value = r.ReferenceSiteID.ToString(CultureInfo.InvariantCulture);
                txtLinkPage.Text = r.RefAddress;
                drNewsTypeAdd.SelectedValue = r.NewsTypeID.ToString(CultureInfo.InvariantCulture);
                var arr = r.ConfigSite.Split(new[] { "$$$" }, StringSplitOptions.None);
                txtLinkPage.Text = arr[0];
                txtTotalPage.Text = arr[1];
                txtParaTitle.Text = arr[2];
                txtParaBrief.Text = arr[3];
                txtParaImage.Text = arr[4];
                txtParaContent.Text = arr[5];
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(ltkReferenceSiteBll.getMsgCode());
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    public string GetNewsByURL(string arrLinkDocument, string idContent)
    {
        try
        {
            var dDocument = GetContentFromUrl(arrLinkDocument);
            //Lấy nội dung bài viết
            var nodeContent = dDocument.DocumentNode.SelectSingleNode(idContent);
            //Xử lý nội dung
            var docA = nodeContent.SelectNodes("//a");
            var url = new Uri(arrLinkDocument);
            var strHref = url.Host;
            strHref = strHref.Replace("www.", "");

            if (docA != null)
            {
                foreach (var t1 in docA)
                {
                    if (t1.Attributes["href"] == null)
                        continue;
                    var strLink = t1.Attributes["href"].Value;
                    //Remove all a
                    if (!strLink.Contains("http://") && !strLink.Contains("https://"))
                    {
                        t1.Attributes["href"].Value = url.Scheme + "://" + url.Host + "/" + strLink.TrimStart('/');
                    }
                    t1.Attributes["href"].Value = "http://www.hoclaptrinhweb.com" + "/checklink.aspx?url=" + t1.Attributes["href"].Value;
                    //Thêm thuộc tính không index trong google
                    t1.SetAttributeValue("rel", "nofollow");
                }
            }

            var docI = nodeContent.SelectNodes("//img");
            if (docI != null)
            {
                foreach (var t1 in docI)
                {
                    if (t1.Attributes["src"] == null)
                        continue;
                    var strLink = t1.Attributes["src"].Value;
                    //Thêm thuộc tính không index trong google
                    if (!strLink.Contains("http://") && !strLink.Contains("https://"))
                        t1.Attributes["src"].Value = url.Scheme + "://" + url.Host + "/" + strLink.TrimStart('/');
                }
            }

            var docScript = nodeContent.SelectNodes("//script");
            if (docScript != null)
            {
                foreach (var t1 in docScript)
                    t1.Remove();
            }

            switch (strHref)
            {
                case "thongtincongnghe.com":
                    {
                        //Lấy nội dung bài viết
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='vud-widget vud-widget-like']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "thanhnien.com.vn":
                    {
                        //Lấy nội dung bài viết
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h2[@class='ms-rteElement-H2']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "tintoi.com":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='ratings hreview-aggregate']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='tags']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='crp_related']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "2cweb.vn":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='create-date']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//p[@class='breadcrumb']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();

                        break;
                    }
                case "html5vn.net":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h1[@class='post-title']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='postdate']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//p[@class='cats']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//p[@class='tags']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "microsofttech.net":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//p[@class='tag']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "kaylaximuoi.com":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h4[@class='related_post_title']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//ul[@class='related_post']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }
                case "sharecodeweb.com":
                    {
                        HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='date']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='category']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='postMetaSingle']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-like fb-social-plugin']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-recommendations-bar fb-social-plugin']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-comments fb-social-plugin']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//noscript");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        nodeRemove = nodeContent.SelectSingleNode("//div[@class='postTags']");
                        if (nodeRemove != null)
                            nodeRemove.Remove();
                        break;
                    }


            }
            return nodeContent.InnerHtml;

        }
        catch (Exception)
        {
            return "Không lấy được nội dung";
        }

    }

    private HtmlDocument GetContentFromUrl(string Url)
    {
        try
        {
            var htmlWeb = new HtmlWeb();
            var htmlContent = htmlWeb.Load(Url);
            return htmlContent;
        }
        catch (Exception)
        { return null; }
    }


    #endregion

}
