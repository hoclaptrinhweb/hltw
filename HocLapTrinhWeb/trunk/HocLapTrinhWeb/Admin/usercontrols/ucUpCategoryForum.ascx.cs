using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucUpCategoryForum : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjCategoryObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpCategoryBll = new vnn_UpCategoryBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpCategoryBll;
    }

    protected void DropCategoryDataBound(object sender, EventArgs e)
    {
        dropCategory.Items.Insert(0, new ListItem("", "-1"));
    }

    protected void ObjDataObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpCategoryForumBll = new vnn_UpCategoryForumBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpCategoryForumBll;
    }

    protected void ObjForumObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpForumBll = new vnn_UpForumBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpForumBll;
    }

    protected void DropForumDataBound(object sender, EventArgs e)
    {
        dropForum.Items.Insert(0, new ListItem("", "-1"));
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
        var upCategoryForumBll = new UpCategoryForumBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var categoryForumID = (HiddenField)row.FindControl("hdCategoryForumID");
                arrID.Add(categoryForumID.Value);
            }
            if (!upCategoryForumBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(upCategoryForumBll.getMsgCode());
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
                var categoryForumID = (HiddenField)row.FindControl("hdCategoryForumID");
                LoadDataEdit(Convert.ToInt16(categoryForumID.Value));
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

    #endregion

    #region Method Page

    /// <summary>
    /// 
    /// </summary>
    private void RefreshControl()
    {
        hdCategoryForumID.Value = string.Empty;
    }

    /// <summary>
    /// Load lên level cần chỉnh sửa
    /// </summary>
    /// <param name="pID"></param>
    private void LoadDataEdit(int pID)
    {
        var vnnUpCategoryForumBll = new vnn_UpCategoryForumBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rCategoryForum = vnnUpCategoryForumBll.GetCategoryForumByID(pID);
            if (rCategoryForum != null)
            {
                hdCategoryForumID.Value = rCategoryForum.CategoryForumID.ToString();
                return;
            }
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(vnnUpCategoryForumBll.getMsgCode());
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
            var upCategoryForumBll = new UpCategoryForumBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_CategoryForumDataTable();
            var row = dt.Newup_tbl_CategoryForumRow();
            if (hdEdit.Value == "0")
            {
                var upCategoryBll = new UpCategoryBLL(CurrentPage.getCurrentConnection());
                var rowCategory = upCategoryBll.GetCategoryByID(hdCategory.Value);
                var upForumBll = new UpForumBLL(CurrentPage.getCurrentConnection());
                var rowForum = upForumBll.GetForumByName(hdForum.Value);

                row.LinkPost = txtLinkPost.Text;
                row.CategoryID = rowCategory.CategoryID;
                row.ForumID = rowForum.ForumID;
                row.Description = txtDescription.Text;
                dt.Addup_tbl_CategoryForumRow(row);
                if (upCategoryForumBll.Add(dt))
                    return true;
               SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = msg.GetMessage(upCategoryForumBll.getMsgCode());
                return false;
            }
            row.CategoryForumID = Convert.ToInt32(hdCategoryForumID.Value);
            dt.Addup_tbl_CategoryForumRow(row);
            if (upCategoryForumBll.Update(dt))
                return true;
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = msg.GetMessage(upCategoryForumBll.getMsgCode());
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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_UpCategoryForumDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion
}
