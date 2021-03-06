using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Xml;

public partial class Admin_Forumcontrols_ucUpForum : HocLapTrinhWeb.UI.UCBase
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
        var vnnUpForumBll = new vnn_UpForumBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpForumBll;
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
        var upForumBll = new UpForumBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var findControl = (HiddenField)row.FindControl("hdForumID");
                arrID.Add(findControl.Value);
            }
            if (!upForumBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(upForumBll.getMsgCode());
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
                var findControl = (HiddenField)row.FindControl("hdForumID");
                LoadDataEdit(Convert.ToInt16(findControl.Value));
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

    protected void BtnUpLoadClick(object sender, EventArgs e)
    {
        CheckUpload(fileImport);
    }

    protected void BtnAddNewUserClick(object sender, EventArgs e)
    {
        if (!SaveUserData()) return;
        hdIsAddSuccessful.Value = "1";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdForumID.Value = string.Empty;
        txtForumName.Text = string.Empty;
        txtAlexa.Text = "0";
        txtLogin.Text = "0";
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnUpForumBll = new vnn_UpForumBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rForum = vnnUpForumBll.GetForumByID(pID);
            if (rForum != null)
            {
                hdForumID.Value = rForum.ForumID.ToString();
                txtForumName.Text = rForum.ForumName;
                txtAlexa.Text = GetAlexa(txtForumName.Text);
                txtLogin.Text = rForum.Login.ToString();
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnUpForumBll.getMsgCode());
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
            var upForumBll = new UpForumBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_ForumDataTable();
            var row = dt.Newup_tbl_ForumRow();
            row.ForumName = txtForumName.Text;
            if (hdEdit.Value == "0")
            {
                row.Login = int.Parse(txtLogin.Text);
                row.Alexa = int.Parse(GetAlexa(txtForumName.Text));
                dt.Addup_tbl_ForumRow(row);
                if (upForumBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(upForumBll.getMsgCode());
                return false;
            }
            row.ForumID = Convert.ToInt32(hdForumID.Value);
            row.Login = int.Parse(txtLogin.Text);
            row.Alexa = int.Parse(txtAlexa.Text);
            dt.Addup_tbl_ForumRow(row);
            if (upForumBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upForumBll.getMsgCode());
            return false;
        }
        catch (Exception)
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000003");
            return false;
        }
    }

    private bool SaveUserData()
    {
        try
        {
            var upUserForumBll = new UpUserForumBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_UserForumDataTable();
            var row = dt.Newup_tbl_UserForumRow();

            foreach (GridViewRow rowgv in gvData.Rows)
            {
                var chckDelete = (CheckBox)rowgv.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var forumID = (HiddenField)rowgv.FindControl("hdForumID");
                var upUserBll = new UpUserBLL(CurrentPage.getCurrentConnection());
                var rowUser = upUserBll.GetUserByName(txtUserName.Text);
                row.IsActive = 0;
                row.UserID = rowUser.UserID;
                row.ForumID = Convert.ToInt16(forumID.Value);
                dt.Addup_tbl_UserForumRow(row);
            }
            if (upUserForumBll.Add(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upUserForumBll.getMsgCode());
            return false;
        }
        catch
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000003");
            return false;
        }
    }

    public string GetAlexa(string host)
    {
        string str;
        var html = "http://data.alexa.com/data?cli=10&dat=s&url=" + host;
        try
        {
            var xr = XmlReader.Create(html);
            var xdoc = new XmlDocument();
            xdoc.Load(xr);
            var node = xdoc.SelectSingleNode("/ALEXA/SD/POPULARITY");
            str = node.Attributes["TEXT"].Value;
        }
        catch
        {
            str = "0";
        }
        return str;
    }

    public string CheckUpload(FileUpload fileupload)
    {
        if (fileImport.PostedFile.ContentLength == 0)
            return "";
        var suffixImage = System.IO.Path.GetExtension(fileImport.FileName).ToLower();

        if (suffixImage != ".txt")
            return "";
        try
        {
            fileupload.SaveAs(Server.MapPath("~/App_Data/" + fileImport.FileName));
            InsertTxtToDatabase(Server.MapPath("~/App_Data/" + fileImport.FileName));
        }
        catch (Exception)
        {
            return "";
        }
        return "App_Data/" + fileImport.FileName;
    }

    private void InsertTxtToDatabase(string filePath)
    {
        var counter = 0;
        string line;

        var file =new System.IO.StreamReader(filePath);
        while ((line = file.ReadLine()) != null)
        {
            if (line != "")
            {
                var upForumBll = new UpForumBLL(CurrentPage.getCurrentConnection());
                var dt = new dsHocLapTrinhWeb.up_tbl_ForumDataTable();
                var row = dt.Newup_tbl_ForumRow();
                row.ForumName = line;
                row.Login = 1;
                row.Alexa = int.Parse(GetAlexa(line));
                dt.Addup_tbl_ForumRow(row);
                upForumBll.Add(dt);
            }
            counter++;
        }

        file.Close();
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
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_UpForumDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

}