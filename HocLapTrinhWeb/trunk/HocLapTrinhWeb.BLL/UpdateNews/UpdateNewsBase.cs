using System;
using System.Globalization;
using HtmlAgilityPack;
using System.Collections;

namespace HocLapTrinhWeb.BLL
{
    public class UpdateNewsBase
    {
        public HtmlDocument GetContentFromUrl(string Url)
        {
            try
            {
                var htmlWeb = new HtmlWeb();
                var htmlContent = htmlWeb.Load(Url);
                return htmlContent;
            }
            catch (Exception)
            { return null; }
        }


        public ArrayList GetNewsBrief(string documentURLPara, int totalPage, string paraTitle, string paraBrief, string paraImage)
        {
            try
            {
                var arrDocument = new ArrayList();
                var arrPaging = GetPaging(documentURLPara, totalPage);
                if (arrPaging != null)
                {
                    foreach (var t in arrPaging)
                    {
                        var dDocument = GetContentFromUrl(t.ToString());
                        GetDocumentsByPage(dDocument, documentURLPara, paraTitle, paraBrief, paraImage, ref arrDocument);
                    }
                    return arrDocument;
                }
                return null;
            }
            catch (Exception)
            { return null; }
        }

        public ArrayList GetNewsByURL(ArrayList arrLinkDocument, string idContent)
        {
            var arrDocument = new ArrayList();
            foreach (var t in arrLinkDocument)
            {
                var arrNews = (ArrayList)t;
                try
                {
                    var dDocument = GetContentFromUrl(arrNews[0].ToString());
                    //Lấy nội dung bài viết
                    var nodeContent = dDocument.DocumentNode.SelectSingleNode(idContent);
                    //Xử lý nội dung
                    var docA = nodeContent.SelectNodes("//a");

                    var url = new Uri(arrNews[0].ToString());
                    var strHref = url.Host;
                    strHref = strHref.Replace("www.", "");

                    if (docA != null)
                    {
                        foreach (var t1 in docA)
                        {
                            if (t1.Attributes["href"] == null)
                                continue;
                            var strLink = t1.Attributes["href"].Value;
                            //Remove all a
                            if (!strLink.Contains("http://") && !strLink.Contains("https://"))
                            {
                                t1.Attributes["href"].Value = url.Scheme + "://" + url.Host + "/" + strLink.TrimStart('/');
                            }
                            t1.Attributes["href"].Value = "http://www.hoclaptrinhweb.com" + "/checklink.aspx?url=" + t1.Attributes["href"].Value;
                            //Thêm thuộc tính không index trong google
                            t1.SetAttributeValue("rel", "nofollow");
                        }
                    }

                    var docI = nodeContent.SelectNodes("//img");
                    if (docI != null)
                    {
                        foreach (var t1 in docI)
                        {
                            if (t1.Attributes["src"] == null)
                                continue;
                            var strLink = t1.Attributes["src"].Value;
                            //Thêm thuộc tính không index trong google
                            if (!strLink.Contains("http://") && !strLink.Contains("https://"))
                                t1.Attributes["src"].Value = url.Scheme + "://" + url.Host + "/" + strLink.TrimStart('/');
                        }
                    }

                    var docScript = nodeContent.SelectNodes("//script");
                    if (docScript != null)
                    {
                        foreach (var t1 in docScript)
                            t1.Remove();
                    }

                    switch (strHref)
                    {
                        case "thongtincongnghe.com":
                            {
                                //Lấy nội dung bài viết
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='vud-widget vud-widget-like']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "thanhnien.com.vn":
                            {
                                //Lấy nội dung bài viết
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h2[@class='ms-rteElement-H2']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                var nodeP = nodeContent.SelectNodes("//p[@style='text-align:right']");
                                if (nodeP != null)
                                    nodeP[nodeP.Count - 1].Remove();
                                break;
                            }
                        case "tintoi.com":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='ratings hreview-aggregate']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='tags']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='crp_related']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "2cweb.vn":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='create-date']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//p[@class='breadcrumb']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();

                                break;
                            }
                        case "html5vn.net":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h1[@class='post-title']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='postdate']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//p[@class='cats']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//p[@class='tags']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "microsofttech.net":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//p[@class='tag']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "kaylaximuoi.com":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//h4[@class='related_post_title']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//ul[@class='related_post']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "garena.vn":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='blog_more']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//table[@class='contentpaneopen']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@id='fb-root']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//fb");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//a[@class='fbLink']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                var nodeP = nodeContent.SelectNodes("//p");
                                if (nodeP != null)
                                    nodeP[0].Remove();
                                break;
                            }
                        case "sharecodeweb.com":
                            {
                                HtmlNode nodeRemove = nodeContent.SelectSingleNode("//div[@class='date']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='category']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='postMetaSingle']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-like fb-social-plugin']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-recommendations-bar fb-social-plugin']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='fb-comments fb-social-plugin']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//noscript");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = nodeContent.SelectSingleNode("//div[@class='postTags']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }


                    }
                    arrNews.Add(nodeContent.InnerHtml);
                    arrDocument.Add(arrNews);
                }
                catch (Exception)
                {
                    arrNews.Add("Không lấy được nội dung");
                    arrDocument.Add(arrNews);
                }
            }
            return arrDocument;

        }

        /// <summary>
        /// Lay len phan trang theo topic url
        /// </summary>
        /// <param name="documentURL"></param>
        /// <param name="totalPage"> </param>
        /// <returns></returns>
        private ArrayList GetPaging(string documentURL, int totalPage)
        {
            var arrPaging = new ArrayList();
            try
            {
                for (var i = 1; i < totalPage; i++)
                    arrPaging.Add(documentURL.Replace("{paraPage}", i.ToString(CultureInfo.InvariantCulture)));
                return arrPaging;
            }
            catch (Exception)
            { return null; }
        }

        private void GetDocumentsByPage(HtmlDocument htmlContent, string documentURLPara, string paraTitle, string paraBrief, string paraImage, ref ArrayList arrDocument)
        {
            try
            {
                var docTitle = htmlContent.DocumentNode.SelectNodes(paraTitle);
                var docImgae = htmlContent.DocumentNode.SelectNodes(paraImage);
                var docBrief = htmlContent.DocumentNode.SelectNodes(paraBrief);
                var url = new Uri(documentURLPara);

                for (var i = 0; i < docTitle.Count; i++)
                {
                    var arrNews = new ArrayList();
                    var strLink = "";
                    var strTitle = "";
                    if (!paraTitle.ToLower().Contains("//a"))
                    {
                        strLink = docTitle[i].SelectSingleNode(".//a").Attributes["href"].Value;
                        strTitle = docTitle[i].SelectSingleNode(".//a").InnerText.Replace("\r\n", "");
                    }
                    else
                    {
                        strLink = docTitle[i].Attributes["href"].Value;
                        strTitle = docTitle[i].InnerText.Replace("\r\n", "");
                    }
                    if (!strLink.Contains("http://") && !strLink.Contains("https://"))
                    {
                        strLink = url.Scheme + "://" + url.Host + "/" + strLink.TrimStart('.').TrimStart('/');
                    }
                    arrNews.Add(strLink);

                    if (strTitle.Trim() == "")
                        strTitle = docTitle[i].InnerText;
                    arrNews.Add(strTitle);

                    //Xử lý nội dung tóm tắt
                    var strHref = url.Host;
                    strHref = strHref.Replace("www.", "");
                    switch (strHref)
                    {
                        case "2cweb.vn":
                            {
                                var nodeRemove = docBrief[i].SelectSingleNode("//div[@class='create-date']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h2");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//p[@class='breadcrumb']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "izwebz.com":
                            {
                                var nodeRemove = docBrief[i].SelectSingleNode("//div[@class='metadata']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h1");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "qhonline.info":
                            {
                                var nodeRemove = docBrief[i].SelectSingleNode("//p");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h3");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();

                                // ?????????
                                //Viet 2 lan moi xoa duoc
                                nodeRemove = docBrief[i].SelectSingleNode("//p");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h3");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                break;
                            }
                        case "kaylaximuoi.com":
                            {
                                var nodeRemove = docBrief[i].SelectSingleNode("//p[class='tag']");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h3");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h4");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();
                                nodeRemove = docBrief[i].SelectSingleNode("//h5");
                                if (nodeRemove != null)
                                    nodeRemove.Remove();

                                break;
                            }
                    }
                    string strBrief = docBrief[i].InnerText.Length >= 500 ? docBrief[i].InnerText.Substring(0, 500) : docBrief[i].InnerText;
                    arrNews.Add(strBrief);

                    try
                    {
                        var strImage = paraImage.Contains("//img") ? docImgae[i].Attributes["src"].Value : docImgae[i].SelectSingleNode(".//img").Attributes["src"].Value;
                        if (!strImage.Contains("http://") && !strImage.Contains("https://"))
                        {
                            strImage = url.Scheme + "://" + url.Host + "/" + strImage.TrimStart('.').TrimStart('/');
                        }
                        arrNews.Add(strImage);
                    }
                    catch
                    {
                        arrNews.Add("");
                    }
                    if (arrDocument.IndexOf(arrNews) == -1)
                        arrDocument.Add(arrNews);
                    else
                        continue;

                }


            }
            catch
            { }
        }
    }
}
