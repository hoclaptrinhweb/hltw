using System;
using System.Globalization;
using System.Web;
using System.Web.Services;
using HocLapTrinhWeb.BLL;
using System.Web.Script.Services;

/// <summary>
/// Summary description for User
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[ScriptService]
public class User : WebService
{
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Login(string username, string password)
    {
        var strResult = "\"\"";
        var strValue = "\"\"";
        try
        {
            var con = new HocLapTrinhWeb.DAL.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {
                var userBll = new ltk_UserBLL(con);
                var row = userBll.GetUserByName(username);
                if (row == null)
                    strResult = "\"Tài khoản không tồn tại\"";
                if (row != null && !row.IsActive)
                    strResult = "\"Tài khoản chưa kích hoạt\"";

                if (row != null && row.Pass != DH.Utilities.Cryptography.EncryptMD5(password))
                    strResult = "\"Mật khẩu không đúng\"";
                strValue = "{";
                strValue += "\"id\":" + row.UserID.ToString(CultureInfo.InvariantCulture) + ",";
                strValue += "\"username\":\"" + row.UserName + "\"";
                strValue += "}";

                Session["UserID"] = row.UserID;
                Session["UserName"] = row.UserName;
                Session["FullName"] = row.FullName;
                var aCookie1 = new HttpCookie("UserName");
                aCookie1.Values["UserName"] = username;
                aCookie1.Values["Password"] = DH.Utilities.Cryptography.EncryptMD5(password);
                aCookie1.Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.Cookies.Add(aCookie1);
            }
            else
                strResult = "\"Không kết nối được dữ liệu\"";
        }
        catch (Exception)
        {
            strResult = "\"Có lỗi xảy ra\"";
        }
        return "{\"Error\":" + strResult + ",\"Value\":" + strValue + "}";
    }


    [WebMethod(EnableSession = true)]
    public string Logout()
    {
        Session.Remove("UserName");
        Session.Remove("UserID");
        Session.Remove("FullName");
        HttpContext.Current.Response.Cookies["UserName"].Expires = DateTime.Now;
        return "{\"Error\":\"\",\"Value\":\"\"}";
    }

}
