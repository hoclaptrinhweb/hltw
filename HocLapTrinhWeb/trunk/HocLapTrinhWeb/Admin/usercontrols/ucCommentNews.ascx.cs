using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Collections;

public partial class Admin_usercontrols_ucCommentNews : HocLapTrinhWeb.UI.UCBase
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
        var vnnCommentNewsBll = new vnn_CommentNewsBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnCommentNewsBll;
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
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSaveExpress.Visible = true;
                    var chckAllIsActive = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsActive");
                    chckAllIsActive.Enabled = true;
                    foreach (GridViewRow row in gvData.Rows)
                    {
                        var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                        chckIsActive.Enabled = true;
                    }
                }
                else
                {
                    btnEditExpress.Text = "Chỉnh sửa nhanh";
                    btnSaveExpress.Visible = false;
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
        var commentNewsBll = new CommentNewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var commentNewsID = (HiddenField)row.FindControl("hdCommentNewsID");
                arrID.Add(commentNewsID.Value);
            }
            if (!commentNewsBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(commentNewsBll.getMsgCode());
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
                var commentNewsID = (HiddenField)row.FindControl("hdCommentNewsID");
                LoadDataEdit(Convert.ToInt16(commentNewsID.Value));
            }
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000004");
        }
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        RefreshControl();
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void DropCommentNewsTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
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
                lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable)e.ReturnValue).Count.ToString();
        }
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        if (!SaveData()) return;
        hdIsAddSuccessful.Value = "1";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    //Sửa tin nhanh
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
            var dt = new dsHocLapTrinhWeb.tbl_CommentNewsDataTable();
            foreach (GridViewRow row in gvData.Rows)
            {
                var r = dt.Newtbl_CommentNewsRow();
                var commentNewsID = (HiddenField)row.FindControl("hdCommentNewsID");
                var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                r.CommentNewsID = int.Parse(commentNewsID.Value);
                r.IsActive = chckIsActive.Checked ? 1 : 0;
                dt.Addtbl_CommentNewsRow(r);
            }
            var vnnCommentNewsBll = new vnn_CommentNewsBLL(CurrentPage.getCurrentConnection());
            if (!vnnCommentNewsBll.UpdateChange(dt, "isactive"))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(vnnCommentNewsBll.getMsgCode());
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


    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdCommentNewsID.Value = string.Empty;
        txtUsername.Text = string.Empty;
        txtContent.Text = string.Empty;
        txtContent.Text = string.Empty;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnCommentNewsBll = new vnn_CommentNewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rCommentNews = vnnCommentNewsBll.GetCommentNewsByID(pID);
            if (rCommentNews != null)
            {
                hdCommentNewsID.Value = rCommentNews.CommentNewsID.ToString();
                cbIsActive.Checked = rCommentNews.IsActive != 0;
                txtTitleNews.Text = rCommentNews.Title;
                txtTitleNews.NavigateUrl = CurrentPage.UrlRoot + "/newsdetail.aspx?newsid=" + rCommentNews.NewsID;
                txtUsername.Text = rCommentNews.UserName;
                txtContent.Text = rCommentNews.Content;
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnCommentNewsBll.getMsgCode());
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
            var commentNewsBll = new CommentNewsBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_CommentNewsDataTable();
            var row = dt.Newtbl_CommentNewsRow();
            if (hdEdit.Value == "0")
            {
                dt.Addtbl_CommentNewsRow(row);
                if (commentNewsBll.Add(dt))
                    return true;
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(commentNewsBll.getMsgCode());
                return false;
            }
            row.CommentNewsID = Convert.ToInt32(hdCommentNewsID.Value);
            row.Content = txtContent.Text;
            row.IsActive = cbIsActive.Checked ? 1 : 0;
            dt.Addtbl_CommentNewsRow(row);
            if (commentNewsBll.UpdateChange(dt, "content", "isactive"))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(commentNewsBll.getMsgCode());
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

}
