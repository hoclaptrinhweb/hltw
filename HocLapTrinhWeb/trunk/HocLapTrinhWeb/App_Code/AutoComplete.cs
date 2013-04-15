using System.Web.Services;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.BLL;
using System.Collections.Generic;


/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//Cho phép Script call Server
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    [WebMethod]
    public string[] GetForumNameList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }

        var items = new List<string>(count);
        var con = new Connection();
        con.CreateConnection("Data Source=.\\SQLExpress;Initial Catalog=HocLapTrinhWeb_Hoclaptrinhweb;User ID=vnn;Password=123456;", "Hoclaptrinhweb", "mCN6dnkTLCVo/DavzAmpng==");
        var vnnUpForumBll = new vnn_UpForumBLL(con);
        var dtUpForum = vnnUpForumBll.GetForumSearchName(prefixText,count);
        if (dtUpForum != null && dtUpForum.Rows.Count > 0)
        {
            foreach (var row in dtUpForum)
            {
                items.Add(row.ForumName);
            }
        }

        return items.ToArray();
    }

    [WebMethod]
    public string[] GetUserNameList(string prefixText, int count)
    {
        if (count == 0)
            count = 10;

        var items = new List<string>(count);
        var con = new Connection();
        con.CreateConnection(Global.cs_sqlserver, Global.Key,Global.ValidKey);
        var vnnUpUserBll = new vnn_UpUserBLL(con);
        var dtUpUser = vnnUpUserBll.GetUserSearchName(prefixText, count);
        if (dtUpUser != null && dtUpUser.Rows.Count > 0)
        {
            foreach (var row in dtUpUser)
            {
                items.Add(row.UserName);
            }
        }

        return items.ToArray();
    }

}

