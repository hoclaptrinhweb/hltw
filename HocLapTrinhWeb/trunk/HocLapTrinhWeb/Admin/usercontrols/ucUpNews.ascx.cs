using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;
using System.Drawing;


public partial class administrator_usercontrols_ucUpNews : HocLapTrinhWeb.UI.UCBase
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
        var vnnUpNewsBll = new vnn_UpNewsBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpNewsBll;
    }

    protected void GvDataDataBound(object sender, EventArgs e)
    {
        try
        {
            if (gvData.Rows.Count > 0)
            {
                if (hdEdit.Value == "1")
                {
                    var chckAllIsHot = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsHot");
                    chckAllIsHot.Enabled = true;
                    var chckAllIsDelete = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsDelete");
                    chckAllIsDelete.Enabled = true;
                    var chckAllIsActive = (CheckBox)gvData.HeaderRow.FindControl("chckAllIsActive");
                    chckAllIsActive.Enabled = true;

                    foreach (GridViewRow row in gvData.Rows)
                    {
                        var chckIsHot = (CheckBox)row.FindControl("chckIsHot");
                        chckIsHot.Enabled = true;
                        var chckIsDelete = (CheckBox)row.FindControl("chckIsDelete");
                        chckIsDelete.Enabled = true;
                        var chckIsActive = (CheckBox)row.FindControl("chckIsActive");
                        chckIsActive.Enabled = true;
                        var chckIsShowImage = (CheckBox)row.FindControl("chckIsShowImage");
                        chckIsShowImage.Enabled = true;
                    }
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

    protected void ObjCategoryObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpCategoryBll = new vnn_UpCategoryBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpCategoryBll;
    }

    protected void DropCategoryDataBound(object sender, EventArgs e)
    {
        // dropCategory.Items.Insert(0, new ListItem("", "-1"));
    }

    protected void GvDataPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
    }

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var upNewsBll = new UpNewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdNewsID = (HiddenField)row.FindControl("hdNewsID");
                arrID.Add(hdNewsID.Value);
            }
            if (upNewsBll.Delete(arrID))
            {
                gvData.DataBind();
                return;
            }
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage(upNewsBll.getMsgCode());
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
                var hdNewsID = (HiddenField)row.FindControl("hdNewsID");
                CurrentPage.GoPageWithAjax("UpNewsDetail.aspx?NewsId=" + hdNewsID.Value);
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
        CurrentPage.GoPageWithAjax("upNewsDetail.aspx");
    }

    /// <summary>
    /// dropdown newstype
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpNewsTypeBll = new vnn_UpNewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpNewsTypeBll;

    }

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    //Sửa tin nhanh
    protected void BtnEditExpressClick(object sender, EventArgs e)
    {
        hdEdit.Value = "1";
        gvData.DataBind();
    }

    //Chuyển tin
    protected void DrNewsTypeMoveDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void DropNewsTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.PageIndex = 0;
        gvData.DataBind();
    }

    protected void GvDataRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var isnew = (DateTime)DataBinder.Eval(e.Row.DataItem, "CreatedDate");
        var isActive = (bool)DataBinder.Eval(e.Row.DataItem, "IsActive");
        if (isnew.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy"))
            e.Row.BackColor = Color.FromName(isActive ? "#FFF3ED" : "#DBF0FA");
    }

    #endregion

    #region Method Page

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
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

}
