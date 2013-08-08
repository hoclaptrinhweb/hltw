using System;
using System.Collections;
using System.Web.UI.WebControls;
using HocLapTrinhWeb.BLL;

public partial class Admin_usercontrols_ucAlexaDetail : HocLapTrinhWeb.UI.UCBase
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
        var vnnAlexaDetailBll = new vnn_AlexaDetailBLL(CurrentPage.getCurrentConnection());
        e.ObjectInstance = vnnAlexaDetailBll;
    }

    protected void GvDataDataBound(object sender, EventArgs e)
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

    protected void GvDataPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
    }

    protected void BtnDeleteClick(object sender, EventArgs e)
    {
        var alexaDetailBll = new AlexaDetailBLL(CurrentPage.getCurrentConnection());
        try
        {
            var arrID = new ArrayList();
            foreach (GridViewRow row in gvData.Rows)
            {
                var chckDelete = (CheckBox)row.FindControl("chckSelect");
                if (!chckDelete.Checked) continue;
                var hdAlexaDetailID = (HiddenField)row.FindControl("hdAlexaDetailID");
                arrID.Add(hdAlexaDetailID.Value);
            }
            if (!alexaDetailBll.Delete(arrID))
            {
                SaveValidate.IsValid = false;
                SaveValidate.ErrorMessage = msg.GetMessage(alexaDetailBll.getMsgCode());
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

    bool _bGetSelectCount;
    protected void ObjDataSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        _bGetSelectCount = e.ExecutingSelectCount;
        e.InputParameters["AlexaID"] = AlexaID;
    }

    protected void ObjDataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null || e.ReturnValue == null) return;
        if (_bGetSelectCount)
            lbTotal.Text += " / " + e.ReturnValue;
        else
            lbTotal.Text = ((vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable)e.ReturnValue).Count.ToString();
    }

    #endregion

    public int AlexaID
    {
        get
        {
            try
            {
                if (Request.QueryString["AlexaID"] != null)
                    return int.Parse(Request.QueryString["AlexaID"]);
                return -1;
            }
            catch
            {
                return -1;
            }
        }
    }

    public string Test()
    {
        var strCat = "";
        var vnnAlexaDetailBll = new vnn_AlexaDetailBLL(CurrentPage.getCurrentConnection());
        var dt = vnnAlexaDetailBll.GetAllAlexaDetailForGridView(0, 50, AlexaID);
        strCat = "[";
        foreach (var r in dt)
        {
            strCat += "'" + r.UpdatedDate.ToString("dd") + "',";
        }
        strCat = strCat.TrimEnd(',');
        strCat += "]";
        return strCat;
    }

    public string Data()
    {
        var strCat = "";
        var srtVN = "";
        var srtEN = "";
        var vnnAlexaDetailBll = new vnn_AlexaDetailBLL(CurrentPage.getCurrentConnection());
        var dt = vnnAlexaDetailBll.GetAllAlexaDetailForGridView(0, 0, AlexaID);

        srtVN += "{name :'Hạng Việt Nam + ',";
        srtEN += "{name :'Hạng Thế Giới + ',";
        srtVN += "data : [";
        srtEN += "data : [";
        for (var i = (dt.Count - 1); i >= 0; i--)
        {
            srtVN += dt[i].TrafficRankVn.ToString() + ",";
            srtEN += dt[i].TrafficRank.ToString() + ",";
        }
        srtVN = srtVN.TrimEnd(',');
        srtEN = srtEN.TrimEnd(',');
        srtVN += "]}";
        srtEN += "]}";
        strCat = "[" + srtVN + "," + srtEN + "]";
        return strCat;
    }

}
