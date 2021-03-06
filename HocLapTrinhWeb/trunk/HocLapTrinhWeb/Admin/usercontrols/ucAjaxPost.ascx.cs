using System;
using HocLapTrinhWeb.BLL;
using HtmlAgilityPack;

public partial class Admin_usercontrols_ucAjaxPost : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        var categoryID = Request.QueryString["categoryid"];
        if (DoPost != null)
        {
            UpdateDate(NewsID);
            return;
        }

        if (NewsID != null && categoryID != null)
        {
            var upNewsBll = new vnn_UpNewsBLL(CurrentPage.getCurrentConnection());
            try
            {
                var rNews = upNewsBll.GetNewsByID(int.Parse(NewsID));
                if (rNews != null)
                {
                    var vnnUpPostNewsBll = new vnn_UpPostNewsBLL(CurrentPage.getCurrentConnection());
                    var rPostNews = vnnUpPostNewsBll.GetAllPostNewsForGridView(categoryID);
                    if (rPostNews != null && rPostNews.Rows.Count > 0)
                    {
                        foreach (var row in rPostNews)
                            PostNews(row, rNews);
                    }
                    else
                        Response.Write("Có lỗi xảy ra");
                }
                else
                    Response.Write("Có lỗi xảy ra");
            }
            catch (Exception)
            {
                Response.Write("Có lỗi xảy ra");
            }

        }
        else
            Response.Write("Có lỗi xảy ra");
    }

    private bool PostNews(vnn_dsHocLapTrinhWeb.vnn_vw_UpPostNewsRow row, vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsRow rNews)
    {
        var b = new BrowserSession();
        //Begin Login
        b.Get(row.ForumName);
        b.FormElements["vb_login_username"] = row.UserName;
        b.FormElements["vb_login_password"] = row.Password;
        var response = b.Post(row.ForumName.TrimEnd('/') + "/login.php?do=login");
        if (CheckLoginForum())
        {
            var linkPost = PostNews(b, row.LinkPost, rNews);
            if (linkPost != "")
                SaveDatabase(linkPost, row.UserID);
            else
                return false;
        }
        else
            return false;
        return true;
    }


    private bool CheckLoginForum()
    {
        return true;
    }

    private string CheckPostNew(HtmlDocument response)
    {
        string link;
        try
        {
            link = response.DocumentNode.SelectSingleNode("//noscript").SelectSingleNode(".//meta").Attributes[1].Value.Substring(response.DocumentNode.SelectSingleNode("//noscript").SelectSingleNode(".//meta").Attributes[1].Value.IndexOf("=") + 1).Replace("poll.php", "showthread.php");
        }
        catch
        {
            link = "";
        }
        return link;
    }

    private bool UpdateDate(string id)
    {
        try
        {
            //Lưu bài viết bài database
            var vnnUpPostBll = new vnn_UpPostBLL(CurrentPage.getCurrentConnection());
            var rUpPost = vnnUpPostBll.GetPostByID(int.Parse(id));
            //Post bai viết OK
            var b = new BrowserSession();
            b.Get(rUpPost.RefAddress.Substring(0, rUpPost.RefAddress.IndexOf("showthread")) + "login.php");
            b.FormElements["vb_login_username"] = rUpPost.UserName;
            b.FormElements["vb_login_password"] = rUpPost.Password;
            var response = b.Post(rUpPost.RefAddress.Substring(0, rUpPost.RefAddress.IndexOf("showthread")) + "login.php?do=login");

            b.Get(rUpPost.RefAddress);
            b.FormElements.Remove("preview");
            b.FormElements["title"] = "";
            b.FormElements["message"] = "Mọi người cùng tham gia nào [url]http://www.hoclaptrinhweb.com[/url]";
            b.FormElements["do"] = "postreply";
            b.FormElements.Remove("preview");
            response = b.Post(rUpPost.RefAddress.Substring(0, rUpPost.RefAddress.IndexOf("showthread")) + "newreply.php");

            var upPostBll = new UpPostBLL(CurrentPage.getCurrentConnection());

            var dt = new dsHocLapTrinhWeb.up_tbl_PostDataTable();
            var rowPost = dt.Newup_tbl_PostRow();
            rowPost.PostID = int.Parse(id);
            rowPost.CreatedDate = DateTime.Now;
            dt.Addup_tbl_PostRow(rowPost);
            return upPostBll.UpdateStatus(dt);
        }
        catch(Exception)
        {
            return false;
        }
    }

    private bool SaveDatabase(string linkPost, int userID)
    {
        //Lưu bài viết bài database
        var upPostBll = new UpPostBLL(CurrentPage.getCurrentConnection());
        var dt = new dsHocLapTrinhWeb.up_tbl_PostDataTable();
        var rowPost = dt.Newup_tbl_PostRow();
        rowPost.UserID = userID;
        rowPost.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
        rowPost.CreatedDate = DateTime.Now;
        rowPost.IsActive = 1;
        rowPost.RefAddress = linkPost.Replace("&polloptions=4", "");
        dt.Addup_tbl_PostRow(rowPost);
        return upPostBll.Add(dt);
    }

    private string PostNews(BrowserSession b, string linkPost, vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsRow rNews)
    {
        b.Get(linkPost);
        b.FormElements["subject"] = rNews.Title;
        b.FormElements["message"] = rNews.Content;
        b.FormElements["taglist"] = rNews.Tag;
        b.FormElements["do"] = "postthread";
        b.FormElements.Remove("preview");
        var response = b.Post(linkPost);
        return CheckPostNew(response);
    }

    private string DoPost
    {
        get
        {
            return Request.QueryString["doPost"];
        }
    }

    private string NewsID
    {
        get
        {
            return Request.QueryString["newsid"];
        }
    }


}

