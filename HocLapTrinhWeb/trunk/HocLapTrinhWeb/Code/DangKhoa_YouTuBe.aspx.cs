﻿using System;
using System.Collections;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.BLL;
using System.Net;
using System.IO;

public partial class Code_DangKhoa_YouTuBe : HocLapTrinhWeb.UI.PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDocument();
    }

    public string strUrl
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["url"]) ? "" : Request.QueryString["url"];
        }
    }

    public void GetImage()
    {
        var videoBll = new v_VideoBLL(getCurrentConnection());
        var dtUpdate = new dsHocLapTrinhWeb.tbl_VideoDataTable();
        var dt = videoBll.GetAllVideoForGridView(0, 2000, "", -1, -1, "", "", "");
        if (dt != null && dt.Count > 0)
            for (var i = 0; i < dt.Count; i++)
            {
                var row = dtUpdate.Newtbl_VideoRow();
                row.VideoID = dt[i].VideoID;
                string fileName;
                try
                {
                    var webClient = new WebClient();
                    var path = dt[i].Thumbnail.Replace("?feature=og", "").Replace("mqdefault.jpg", "hqdefault.jpg");
                    fileName = path;
                    fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    var suffixImage = Path.GetExtension(fileName).ToLower();
                    fileName = DateTime.Now.ToString("ddMMyyyy") + "_" + XuLyChuoi.ConvertToUnSign(dt[i].Title) + "_" + i +
                               suffixImage;
                    webClient.DownloadFile(path, Server.MapPath("~/" + Global.ImagesVideo + fileName));
                }
                catch (Exception)
                {
                    fileName = "noimage.jpg";
                }
                row.Thumbnail = Global.ImagesVideo + fileName;
                dtUpdate.Addtbl_VideoRow(row);
            }
        var b = videoBll.UpdateStatus(dtUpdate, dt.ThumbnailColumn.ColumnName);
    }

    public void GetURL()
    {
        if(strUrl == "")
            return;
        var con = new Connection();
        con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
        var videoBll = new v_VideoBLL(con);
        var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
        var update = new UpdateNewsBase();
        var arr = new ArrayList();
        var doc = update.GetContentFromUrl(strUrl);
        var node = doc.DocumentNode.SelectNodes("//li//h3[@class='video-title-container']//a");
        if (node != null)
            foreach (var t in node)
            {
                // arr.Add("http://www.youtube.com" + t.Attributes["href"].Value);
                var rowEx = videoBll.GetVideoByRefAddress("http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20), -1);
                if (rowEx != null)
                    continue;
                var row = dt.Newtbl_VideoRow();
                doc = update.GetContentFromUrl("http://www.youtube.com" + t.Attributes["href"].Value);
                row.Title = doc.DocumentNode.SelectSingleNode("//span[@id='eow-title']").InnerText.Replace("\n", "").Trim();
                var brief = doc.DocumentNode.SelectSingleNode("//p[@id='eow-description']").InnerText;
                row.Brief = (brief == "Không có mô tả nào." ? "" : (brief.Length > 250 ? brief.Substring(0,250) : brief));
                string fileName;
                try
                {
                    var webClient = new WebClient();
                    var path = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value.Replace("?feature=og", "");
                    fileName = path;
                    fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    fileName = DateTime.Now.ToString("ddMMyyyy") + "_" + XuLyChuoi.ConvertToUnSign(row.Title) + "_" + fileName;
                    webClient.DownloadFile(path, Server.MapPath("~/" + Global.ImagesVideo + fileName));
                }
                catch (Exception)
                {
                    fileName = "noimage.jpg";
                }
                row.Thumbnail = Global.ImagesVideo + fileName;
                row.RefAddress = "http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20);
                row.CreatedDate = DateTime.Now;
                row.UpdatedDate = DateTime.Now;
                row.CreatedBy = 1;
                row.UpdatedBy = 1;
                //Chú ý
                row.VideoTypeID = 10;
                row.VideoURL = "http://www.youtube.com" + t.Attributes["href"].Value.Substring(0, 20);
                row.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                row.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                row.Priority = 0;
                row.Viewed = 0;
                row.IsActive = false;
                row.IsHot = false;
                row.IsDelete = false;
                dt.Addtbl_VideoRow(row);
            }
        var b = videoBll.Add(dt);
    }

    public void GetDocument()
    {
        if(strUrl == "")
            return;
        var con = new Connection();
        con.CreateConnection(Global.cs_sqlserver, Global.Key, Global.ValidKey);
        var videoBll = new v_VideoBLL(con);
        var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
        var update = new UpdateNewsBase();
        var arr = new ArrayList();

        var doc = update.GetContentFromUrl(this.UrlRoot + "/code/YouTube.html");
        var node = doc.DocumentNode.SelectNodes("//li//span[@class='content-item-detail']//a");
        if (node != null)
            foreach (var t in node)
            {
                var rowEx = videoBll.GetVideoByRefAddress(t.Attributes["href"].Value, -1);
                if (rowEx != null)
                    continue;
                var row = dt.Newtbl_VideoRow();
                doc = update.GetContentFromUrl(t.Attributes["href"].Value);
                row.Title = doc.DocumentNode.SelectSingleNode("//span[@id='eow-title']").InnerText.Replace("\n", "").Trim();
                var brief = doc.DocumentNode.SelectSingleNode("//p[@id='eow-description']").InnerText;
                row.Brief = (brief == "Không có mô tả nào." ? "" : (brief.Length > 250 ? brief.Substring(0, 250) : brief));
                string fileName;
                try
                {
                    var webClient = new WebClient();
                    var path = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value.Replace("?feature=og", "");
                    fileName = path;
                    fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    fileName = DateTime.Now.ToString("ddMMyyyy") + "_" + XuLyChuoi.ConvertToUnSign(row.Title) + "_" + fileName;
                    webClient.DownloadFile(path, Server.MapPath("~/" + Global.ImagesVideo + fileName));
                }
                catch (Exception)
                {
                    fileName = "noimage.jpg";
                }
                row.Thumbnail = Global.ImagesVideo + fileName;
                row.RefAddress = t.Attributes["href"].Value;
                row.CreatedDate = DateTime.Now;
                row.UpdatedDate = DateTime.Now;
                row.CreatedBy = 1;
                row.UpdatedBy = 1;
                //Chú ý
                row.VideoTypeID = 10;
                row.VideoURL = t.Attributes["href"].Value;
                row.IPAddress = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                row.IPUpdate = HocLapTrinhWeb.Utilities.Net.GetVisitorIPAddress();
                row.Priority = 0;
                row.Viewed = 0;
                row.IsActive = false;
                row.IsHot = false;
                row.IsDelete = false;
                dt.Addtbl_VideoRow(row);

            }
        var b = videoBll.Add(dt);
    }
}