using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucRightNews1 : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
            LoadData();
    }

    public void LoadData()
    {
        var vnnNewsBll = new NewsBLL(CurrentPage.getCurrentConnection());
        var dt = vnnNewsBll.GetNewsAll("NewsTypeName,NewsTypeID,Thumbnail,Title,NewsID", 5, NewsTypeID, 1, "", "", "NEWID()", "");
        rpDataNewsRandom.DataSource = dt;
        rpDataNewsRandom.DataBind();
    }

    public int NewsTypeID
    {
        get
        {
            if (Request.QueryString["NewsTypeID"] != null)
            {
                try
                {
                    return int.Parse(Request.QueryString["NewsTypeID"]);
                }
                catch { return -1; }
            }

            if (Request.QueryString["NewsID"] == null)
                return -1;
            try
            {
                var vNewsBll = new vnn_NewsBLL(getCurrentConnection());

                var rowNews = vNewsBll.GetNewsByID("", int.Parse(Request.QueryString["NewsID"].ToString()), 1);
                if (rowNews != null)
                    return rowNews.NewsTypeID;
                return -1;
            }
            catch
            {
                return -1;
            }
        }
    }
}