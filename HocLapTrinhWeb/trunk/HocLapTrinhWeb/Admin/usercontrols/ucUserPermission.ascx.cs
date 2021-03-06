using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class dhadmincp_usercontrols_ucUserPermission : HocLapTrinhWeb.UI.UCBase
{
    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var isAllow = userPermissionBll.CheckUserIsAdmin(Convert.ToInt32(Session["UserID"]));
            if (isAllow == null || isAllow == false)
                CurrentPage.GoPage("~/admin/view.aspx");
        }
        gvData.PageSize = Global.Pagesize;
        gvData.PagerSettings.PageButtonCount = Global.PageButtonCount;
    }

    protected void ObjData_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUserPermissionBll = new vnn_UserPermissionBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUserPermissionBll;
    }

    protected void ObjectUser_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var ltkUserBll = new ltk_UserBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = ltkUserBll;
    }

    protected void ObjectRole_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnRoleBll = new vnn_RoleBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnRoleBll;
    }

    protected void ObjectDataSource3Add_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnPermissionBll = new vnn_PermissionBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnPermissionBll;
    }

    protected void gvData_DataBound(object sender, EventArgs e)
    {
        try
        {
            btnDelete.Enabled = gvData.Rows.Count > 0;
        }
        catch
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000006");
        }
    }

    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrId = new ArrayList();

            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var key = new string[3];
                var hdUserId = (HiddenField)row.FindControl("hdUserID");
                var hdPermissionId = (HiddenField)row.FindControl("hdPermissionID");
                var hdRoleId = (HiddenField)row.FindControl("hdRoleID");
                key[0] = hdUserId.Value;
                key[1] = hdPermissionId.Value;
                key[2] = hdRoleId.Value;
                arrId.Add(key);
            }

            if (!userPermissionBll.Delete(arrId))
            {
                this.SaveValidate.IsValid = false;
                this.SaveValidate.ErrorMessage = msg.GetMessage(userPermissionBll.getMsgCode());
                return;
            }
            gvData.DataBind();
        }
        catch
        {
            this.SaveValidate.IsValid = false;
            this.SaveValidate.ErrorMessage = msg.GetMessage("ERR-000005");
        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }


    #endregion

    #region Method Page

    #endregion

    protected void dropUser_DataBound(object sender, EventArgs e)
    {
        dropUser.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void dropRole_DataBound(object sender, EventArgs e)
    {
        dropRole.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void dropRoleAdd_DataBound(object sender, EventArgs e)
    {
        dropRoleAdd.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void dropPermission_DataBound(object sender, EventArgs e)
    {
        dropPermissionAdd.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!SaveData()) return;
        hdIsAddSuccessful.Value = "1";
        hdEdit.Value = "0";
        ObjData.Page.DataBind();
        gvData.DataBind();
    }

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var userPermissionBll = new UserPermissionBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
            var row = dt.Newtbl_UserPermissionRow();
            row.UserID = int.Parse(hdUserIDAdd.Value);
            row.RoleID = dropRoleAdd.SelectedValue;
            row.PermissionID = dropPermissionAdd.SelectedValue;
            dt.Addtbl_UserPermissionRow(row);
            if (userPermissionBll.Add(dt))
                return true;
            this.SaveValidate1.IsValid = false;
            this.SaveValidate1.ErrorMessage = msg.GetMessage(userPermissionBll.getMsgCode());
            return false;
        }
        catch
        {
            this.SaveValidate1.IsValid = false;
            this.SaveValidate1.ErrorMessage = msg.GetMessage("ERR-000003");
            return false;
        }
    }


}
