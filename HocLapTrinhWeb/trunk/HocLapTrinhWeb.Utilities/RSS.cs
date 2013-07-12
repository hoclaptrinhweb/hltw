using System.Xml;

namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Lớp thao tác RSS.
    /// Use:
    /// RSS.RssChannel channel = new RSS.RssChannel();
    /// channel.Title = "Diễn đàn đại học";
    /// channel.Link = "http://daihoc.com.vn";
    /// channel.Description = "Website Chia xẻ tài liệu.";
    /// rss.AddRssChannel(channel);
    /// RSS.RssItem item = new RSS.RssItem();
    /// item.Title = "Lập Trình";
    /// item.Link = "http://daihoc.com.vn/forum/?ID=9";
    /// item.Description = "Các Bài Viết Hướng Dẫn về lập Trình";
    /// rss.AddRssItem(item);
    /// Response.Clear();
    /// Response.ContentType = "text/xml";
    /// Response.Write(rss.RssDocument);
    /// Response.End();
    /// </summary>
    public class RSS
    {

        private XmlDocument _rss = null;

        /// <summary>
        /// Cấu trúc kênh thông tin
        /// </summary>
        public struct RssChannel
        {
            public string Title;
            public string Link;
            public string Description;
        }

        /// <summary>
        /// Cấu trúc tin cho rss
        /// </summary>
        public struct RssItem
        {
            public string Title;
            public string Link;
            public string Description;
            public string pubDate;

        }

        /// <summary>
        /// Hàm khởi tạo RSS
        /// </summary>
        public RSS()
        {
            //Khởi tạo xml trả về
            _rss = new XmlDocument();
            XmlDeclaration xmlDeclaration = _rss.CreateXmlDeclaration("1.0", "utf-8", null);
            _rss.InsertBefore(xmlDeclaration, _rss.DocumentElement);

            //Tạo elenemt rss (<rss></rss>)
            XmlElement rssElement = _rss.CreateElement("rss");
            XmlAttribute rssVersionAttribute = _rss.CreateAttribute("version");
            rssVersionAttribute.InnerText = "2.0";
            rssElement.Attributes.Append(rssVersionAttribute);
            _rss.AppendChild(rssElement);

        }

        /// <summary>
        /// Thêm channel vào rss trả về
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        private static XmlDocument addRssChannel(XmlDocument xmlDocument, RssChannel channel)
        {
            //Thêm <channel> vào <rss>
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");
            rssElement.AppendChild(channelElement);

            //Thêm <title> vào <channel>
            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = channel.Title;
            channelElement.AppendChild(titleElement);

            //Thêm <link> vào <channel>
            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = channel.Link;
            channelElement.AppendChild(linkElement);

            //Thêm <description> vào <channel>
            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = channel.Description;
            channelElement.AppendChild(descriptionElement);

            // Generator information
            XmlElement generatorElement = xmlDocument.CreateElement("generator");
            generatorElement.InnerText = "Your RSS Generator name and version ";
            channelElement.AppendChild(generatorElement);

            return xmlDocument;
        }

        /// <summary>
        /// Thêm Item vào Channel
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static XmlDocument addRssItem(XmlDocument xmlDocument, RssItem item)
        {
            //Lấy lên <channel>
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");
            //Tạo <Item> cho  <channel>
            XmlElement itemElement = xmlDocument.CreateElement("item");

            //Tạo <title> cho <item>
            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = item.Title;
            itemElement.AppendChild(titleElement);

            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = item.Link;
            itemElement.AppendChild(linkElement);

            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = item.Description;
            itemElement.AppendChild(descriptionElement);

            // append the item into channel
            channelElement.AppendChild(itemElement);

            return xmlDocument;
        }

        /// <summary>
        /// Thêm channel
        /// </summary>
        /// <param name="channel"></param>
        public void AddRssChannel(RssChannel channel)
        {
            _rss = addRssChannel(_rss, channel);
        }

        /// <summary>
        /// Thêm item
        /// </summary>
        /// <param name="item"></param>
        public void AddRssItem(RssItem item)
        {
            _rss = addRssItem(_rss, item);
        }

        /// <summary>
        /// Lấy ra xmlOuter của Rss
        /// </summary>
        public string RssDocument
        {
            get
            {
                return _rss.OuterXml;
            }
        }

        /// <summary>
        /// Lấy lên xmlRss
        /// </summary>
        public XmlDocument RssXMLDocument
        {
            get
            {
                return _rss;
            }
        }


    }
}