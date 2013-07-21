using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;
using HocLapTrinhWeb.BLL;
using HocLapTrinhWeb.DAL;

namespace AutoNews
{
    class HTLWTimer
    {
        public void StartTimer()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = int.Parse(ConfigurationManager.AppSettings["Time"]);
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var con = new Connection();
            if (con.CreateConnection(ConfigurationManager.AppSettings["cs_sqlserver"], ConfigurationManager.AppSettings["Key"], ConfigurationManager.AppSettings["ValidKey"]))
            {
                var ltk = new ltk_ReferenceSiteBLL(con);
                var dt = ltk.GetNewsTypeRefSiteForGridView(0, 0, -1, -1);
                foreach (var r in dt)
                {
                    UpdateNews(r);
                }
            }
            con.Close();
        }

        private static bool UpdateNews(vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteRow r)
        {
            try
            {
                var isSuccess = true;
                var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                var updateNewsBase = new UpdateNewsBase();
                var config = r.ConfigSite.Split(new[] { "$$$" }, StringSplitOptions.None);
                var arr = updateNewsBase.GetNewsBrief(config[0], int.Parse(config[1]), config[2], config[3], config[4]);
                var con = new Connection();
                if (!con.CreateConnection(ConfigurationManager.AppSettings["cs_sqlserver"], ConfigurationManager.AppSettings["Key"], ConfigurationManager.AppSettings["ValidKey"]))
                    return false;
                var ltkNewsBll = new ltk_NewsBLL(con);
                var arrnew = new ArrayList();
                foreach (var t in arr)
                {
                    if (!ltkNewsBll.CheckExistByRefAddress(((ArrayList)t)[0].ToString()))
                        arrnew.Add(t);
                }
                var arrConent = updateNewsBase.GetNewsByURL(arrnew, config[5]);
                if (arrConent == null)
                {
                    return false;
                }
                for (var i = 0; i < arrConent.Count; i++)
                {

                    if (ltkNewsBll.CheckExistByRefAddress(((ArrayList)arrConent[i])[0].ToString()))
                        continue;

                    var rNews = dt.Newtbl_NewsRow();
                    rNews.Title = XuLyChuoi.ConvertTitle(((ArrayList)arrConent[i])[1].ToString(),
                                                         ((ArrayList)arrConent[i])[0].ToString());
                    rNews.Brief = XuLyChuoi.ConvertBrief(((ArrayList)arrConent[i])[2].ToString(),
                                                         ((ArrayList)arrConent[i])[0].ToString());
                    if (rNews.Brief.Length > 2000)
                        rNews.Brief = rNews.Brief.Substring(0, 2000);
                    string fileName;
                    try
                    {
                        var webClient = new WebClient();
                        var path = HttpUtility.UrlDecode(((ArrayList)arrConent[i])[3].ToString()).Replace("&amp;", "&");
                        if (path.ToLower().Contains("thanhnien.com.vn"))
                        {
                            var n = path.IndexOf(";", StringComparison.Ordinal);
                            if (n > -1)
                                path = path.Substring(0, n);
                        }
                        else if (path.ToLower().Contains("http://pridio.com"))
                        {
                            path = path.Replace("-500x128", "");
                        }
                        else if (path.ToLower().Contains("vietnamnet.vn"))
                            path = path.Replace("?w=142&h=100", "");
                        fileName = path;
                        fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.Ordinal) + 1);
                        if (fileName.IndexOf('?') != -1)
                            fileName = fileName.Substring(0, fileName.IndexOf('?'));
                        if (fileName == "")
                            fileName = XuLyChuoi.ConvertToUnSign(rNews.Title) + ".jpg";
                        fileName = DateTime.Now.ToString("ddMMyyyy") + fileName;
                        webClient.DownloadFile(path, ConfigurationManager.AppSettings["Uploads"] + "\\"+ fileName);
                    }
                    catch (Exception)
                    {
                        fileName = "noimage.jpg";
                    }

                    rNews.Thumbnail = ConfigurationManager.AppSettings["HLTWUploads"] + fileName;
                    rNews.Content = XuLyChuoi.ConvertContent(((ArrayList)arrConent[i])[4].ToString(),
                                                             ((ArrayList)arrConent[i])[0].ToString());
                    if (rNews.Content.ToLower() == "không lấy được nội dung")
                        continue;
                    rNews.NewsTypeID = r.NewsTypeID;

                    rNews.Image = "";
                    rNews.RefAddress = ((ArrayList)arrConent[i])[0].ToString();
                    rNews.IsShowImage = false;
                    rNews.IsHot = false;
                    rNews.Viewed = 1;
                    rNews.Priority = i;
                    rNews.IPAddress = "127.0.0.1";
                    rNews.CreatedDate = DateTime.Now.AddMilliseconds(double.Parse(i.ToString()));
                    rNews.CreatedBy =1;
                    rNews.UpdatedDate = DateTime.Now.AddMilliseconds(double.Parse(i.ToString()));
                    rNews.UpdatedBy = 1;
                    rNews.IPUpdate = "127.0.0.1";
                    rNews.IsDelete = false;
                    rNews.IsActive =r.IsAutoRun;
                    rNews.MoveFrom = r.NewsTypeID;
                    dt.Addtbl_NewsRow(rNews);
                }

                var dtRefSite = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                var rRefSite = dtRefSite.Newtbl_ReferenceSiteRow();
                rRefSite.ReferenceSiteID = r.ReferenceSiteID;
                rRefSite.UpdatedDate = DateTime.Now;
                rRefSite.UpdateRows = dt.Count;
                isSuccess = ltkNewsBll.AddFromSite(dt, rRefSite);

                return isSuccess;
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
