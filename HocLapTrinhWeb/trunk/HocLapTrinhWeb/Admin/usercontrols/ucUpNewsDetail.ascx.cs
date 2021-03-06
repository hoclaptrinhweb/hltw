using System;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucUpNewsDetail : HocLapTrinhWeb.UI.UCBase
{

    #region Event Page

    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (IsPostBack) return;
        var newsID = Request.QueryString["NewsID"];
        if (newsID == null)
            On_Load();
        else
            LoadDataEdit(int.Parse(newsID));
    }


    protected void ObjectDataSource1ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var vnnUpNewsTypeBll = new vnn_UpNewsTypeBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnUpNewsTypeBll;

    }

    protected void DropNewsTypeDataBound(object sender, EventArgs e)
    {
        dropNewsType.Items.Insert(0, new ListItem("Tất cả", "-1"));
    }

    protected void BtnSaveClick(object sender, EventArgs e)
    {
        if (SaveData())
            CurrentPage.GoPageWithAjax("UpNews.aspx");
    }

    protected void BtnSaveAndNewClick(object sender, EventArgs e)
    {
        if (SaveData())
            CurrentPage.GoPageWithAjax("UpNewsDetail.aspx");
    }

    protected void BtnCancelAddClick(object sender, EventArgs e)
    {
        CurrentPage.GoPageWithAjax("UpNews.aspx");
    }

    #endregion

    #region Event Page

    /// <summary>
    /// Add & Edit ListPrice
    /// </summary>
    /// <returns></returns>
    private bool SaveData()
    {
        try
        {
            var upNewsBll = new UpNewsBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.up_tbl_NewsDataTable();
            var row = dt.Newup_tbl_NewsRow();
            row.Title = txtTitle.Text;
            var newsID = Request.QueryString["NewsID"];
            if (newsID == null)
            {
                row.NewsTypeID = int.Parse(dropNewsType.SelectedValue);
                row.Title = txtTitle.Text;
                row.Content = txtContent.InnerText;
                row.Tag = txtTag.Text;
                row.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                row.CreatedBy = int.Parse(Session["UserID"].ToString());
                row.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                row.IsActive = true;
                dt.Addup_tbl_NewsRow(row);
                return upNewsBll.Add(dt);
            }
            //IPUpdate
            row.NewsTypeID = int.Parse(dropNewsType.SelectedValue);
            row.NewsID = int.Parse(newsID);
            row.Title = txtTitle.Text;
            row.Content = txtContent.InnerText;
            row.Tag = txtTag.Text;
            row.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
            row.CreatedBy = int.Parse(Session["UserID"].ToString());
            row.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
            row.IsActive = true;
            dt.Addup_tbl_NewsRow(row);
            return upNewsBll.Update(dt);
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
            return false;
        }
    }

    public void On_Load()
    {
        txtTitle.Text = "";
        hdNewsID.Value = "";
        txtCreatedDate.Text = DateTime.Now.ToString();
    }

    public void LoadDataEdit(int newsID)
    {
        var upNewsBll = new vnn_UpNewsBLL(CurrentPage.getCurrentConnection());
        try
        {
            var rNews = upNewsBll.GetNewsByID(newsID);
            if (rNews == null) return;
            dropNewsType.SelectedValue = rNews.NewsTypeID.ToString();
            hdNewsTypeID.Value = rNews.NewsTypeID.ToString();
            hdNewsID.Value = rNews.NewsID.ToString();
            txtTitle.Text = rNews.Title;
            txtContent.InnerText = rNews.Content;
            txtTag.Text = rNews.IsTagNull() ? "" : rNews.Tag;
            txtCreatedDate.Text = rNews.CreatedDate.ToString();
        }
        catch (Exception)
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = msg.GetMessage("ERR-000013");
        }
    }

    #endregion

}
