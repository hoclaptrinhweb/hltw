using System;
using System.Globalization;
using System.Web;
using System.Web.Services;
using HocLapTrinhWeb.BLL;
using System.Web.Script.Services;

/// <summary>
/// Summary description for CommentNews
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CommentNews : WebService
{
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PostDataGuest(string username, string email, string content, int newsid, string keyname)
    {
        try
        {
            if (HttpContext.Current.Session["captcha"] == null)
                return "timeout";

            if (keyname.ToLower() != HttpContext.Current.Session["captcha"].ToString().ToLower())
                return "nhập mã không đúng";

            if (username == "" || email == "" || content == "")
                return "Bạn phải nhập đầy đủ thông tin";

            if (!Global.CheckEmail(email))
                return "Email không hợp lệ";

            var con = new DH.Data.SqlServer.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {
                //Kiểm tra mail có trong CSDL không
                var ltkUserBll = new ltk_UserBLL(con);
                var rowUser = ltkUserBll.GetUserByEmail(email);
                if (rowUser != null)
                    return "user này đã có"; //Chuyển sang chế độ để login

                var newsBll = new NewsBLL(con);
                //Kiểm tra bài viết có trong CSDL không
                var rowNews = newsBll.GetNewsByID(newsid);
                if (rowNews == null)
                    return "Bài viết này không tồn tại";

                //Tạo accout nhưng chưa Active
                var dtUser = new dsHocLapTrinhWeb.tbl_UserDataTable();
                var rowUserAdd = dtUser.Newtbl_UserRow();
                rowUserAdd.UserName = email;
                rowUserAdd.Pass = email;
                rowUserAdd.FullName = username;
                rowUserAdd.Email = email;
                rowUserAdd.IsAdmin = false;
                rowUserAdd.CreatedDate = DateTime.Now;
                rowUserAdd.IsActive = false;
                dtUser.Addtbl_UserRow(rowUserAdd);
                if (!ltkUserBll.Add(dtUser))
                    return "Gửi không thành công !";

                var commentNewsBll = new CommentNewsBLL(con);
                var dt = new dsHocLapTrinhWeb.tbl_CommentNewsDataTable();
                var row = dt.Newtbl_CommentNewsRow();
                row.NewsID = newsid;
                row.Content = content;
                row.UserID = rowUserAdd.UserID;
                row.IsActive = 0;
                row.CreatedDate = DateTime.Now;

                dt.Addtbl_CommentNewsRow(row);
                return commentNewsBll.Add(dt) ? "Gửi bình luận thành công !" : "Gửi không thành công !";
            }
            return "Gửi không thành công !";
        }
        catch (Exception)
        {
            return "Gửi không thành công!";
        }
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PostDataUser(string content, int newsid, string keyname)
    {
        try
        {
            if (HttpContext.Current.Session["captcha"] == null)
                return "timeout";

            if (keyname.ToLower() != HttpContext.Current.Session["captcha"].ToString().ToLower())
                return "nhập mã không đúng";

            if (content == "")
                return "Bạn phải nhập đầy đủ thông tin";

            if (!Login())
                return "Có lỗi xảy ra ! Bạn nhấn F5 để tải lại trang web.";


            var con = new DH.Data.SqlServer.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {

                var newsBll = new NewsBLL(con);
                //Kiểm tra bài viết có trong CSDL không
                var rowNews = newsBll.GetNewsByID(newsid);
                if (rowNews == null)
                    return "Bài viết này không tồn tại";

                var commentNewsBll = new CommentNewsBLL(con);
                var dt = new dsHocLapTrinhWeb.tbl_CommentNewsDataTable();
                var row = dt.Newtbl_CommentNewsRow();
                row.NewsID = newsid;
                row.Content = XuLyChuoi.ConvertHtmlToText(content);
                row.UserID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                row.IsActive = 1;
                row.CreatedDate = DateTime.Now;
                dt.Addtbl_CommentNewsRow(row);
                return commentNewsBll.Add(dt) ? "Gửi bình luận thành công !" : "Gửi không thành công !";
            }
            return "Gửi không thành công !";
        }
        catch (Exception)
        {
            return "Gửi không thành công!";
        }
    }

    private bool Login()
    {
        try
        {
            if (HttpContext.Current.Session["UserName"] != null)
                return true;
            var con = new DH.Data.SqlServer.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {
                var userBll = new ltk_UserBLL(con);
                var row =
                    userBll.GetUserByName(HttpContext.Current.Request.Cookies["UserName"] != null ? HttpContext.Current.Request.Cookies["UserName"].Values["UserName"] : "");
                if (row == null)
                    return false;
                if (!row.IsActive)
                    return false;
                if (row.Pass != (HttpContext.Current.Request.Cookies["UserName"] != null ? HttpContext.Current.Request.Cookies["UserName"].Values["Password"] : ""))
                    return false;
                Session["UserName"] = row.UserName;
                Session["FullName"] = row.FullName;
                Session["UserID"] = row.UserID.ToString(CultureInfo.InvariantCulture);
                Session["IsAdmin"] = row.IsAdmin;
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PostDataEmail(string email, string keyname)
    {
        if (HttpContext.Current.Session["captcha"] == null)
            return "timeout";

        if (keyname.ToLower() != HttpContext.Current.Session["captcha"].ToString().ToLower())
            return "nhập mã không đúng";

        var guid = DH.Utilities.Cryptography.EncryptMD5(email + "hoclaptrinhweb.com");
        var link = "http://hoclaptrinhweb.com/activeuser.aspx?email=" + email + "&guid=" + guid;
        var b = SendMail.SendMailFrom(email, "Kích hoạt tài khoản trên Hoclaptrinhweb.com", "<a href='" + link + "' title='kích hoạt tài khoản' >" + link + "</a>");
        return b ? "1" : "0";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PostData(string type)
    {
        try
        {
            var con = new DH.Data.SqlServer.Connection();
            if (con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey))
            {
                var autoAdv = new AutoAdvBLL(con);
                //Kiểm tra bài viết có trong CSDL không
                var ntype = 1;
                if (type.Contains("360"))
                    ntype = 0;
                var row = autoAdv.GetAutoAdvByDate(DateTime.Now.ToString("MM/dd/yyyy"), ntype);
                if (row == null)
                    return "0";
                dsHocLapTrinhWeb.tbl_AutoAdvDataTable dt;
                dsHocLapTrinhWeb.tbl_AutoAdvRow rowUpdate;
                switch (type)
                {
                    case "top":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = row.AutoAdvID;
                        rowUpdate.TotalClickTop = row.TotalClickTop + 1;
                        rowUpdate.CurrentClickTop = row.CurrentClickTop > 1 ? row.CurrentClickTop - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                            return "1";
                        break;
                    case "":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = row.AutoAdvID;
                        rowUpdate.TotalClick = row.TotalClick + 1;
                        rowUpdate.CurrentClick = row.CurrentClick > 1 ? row.CurrentClick - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                            return "1";
                        break;
                    case "top360":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = row.AutoAdvID;
                        rowUpdate.TotalClickTop = row.TotalClickTop + 1;
                        rowUpdate.CurrentClickTop = row.CurrentClickTop > 1 ? row.CurrentClickTop - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClickTop", "CurrentClickTop", "UpdatedDate"))
                            return "1";
                        break;
                    case "360":
                        dt = new dsHocLapTrinhWeb.tbl_AutoAdvDataTable();
                        rowUpdate = dt.Newtbl_AutoAdvRow();
                        rowUpdate.AutoAdvID = row.AutoAdvID;
                        rowUpdate.TotalClick = row.TotalClick + 1;
                        rowUpdate.CurrentClick = row.CurrentClick > 1 ? row.CurrentClick - 1 : 0;
                        rowUpdate.UpdatedDate = DateTime.Now;
                        dt.Addtbl_AutoAdvRow(rowUpdate);
                        if (autoAdv.Update(dt, "TotalClick", "CurrentClick", "UpdatedDate"))
                            return "1";
                        break;
                }

                return "0";
            }
            return "0";
        }
        catch (Exception)
        {
            return "0";
        }
    }

}
