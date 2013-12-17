using System;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for ClsGlobal
/// </summary>
public static class Global
{
    #region Variables

    public static int Pagesize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
    public static int PageButtonCount = Convert.ToInt16(ConfigurationManager.AppSettings["PageButtonCount"]);
    public static string ImagesProducts = ConfigurationManager.AppSettings["ImagesProducts"];
    public static string ImagesNews = ConfigurationManager.AppSettings["ImagesNews"];
    public static string ImagesNewsType = ConfigurationManager.AppSettings["ImagesNewsType"];
    public static string ImagesVideo = ConfigurationManager.AppSettings["ImagesVideo"];
    public static int SubTitle = int.Parse(ConfigurationManager.AppSettings["SubTitle"]);
    public static int SubBrief = int.Parse(ConfigurationManager.AppSettings["SubBrief"]);

    public static int MaxThumbnailSize = Convert.ToInt16(ConfigurationManager.AppSettings["MaxThumbnailSize"]);
    public static int MaxImageSize = Convert.ToInt16(ConfigurationManager.AppSettings["MaxImageSize"]);
    public static int MaxWithImageSize = Convert.ToInt16(ConfigurationManager.AppSettings["MaxWithImageSize"]);
    public static int MaxHeightImageSize = Convert.ToInt16(ConfigurationManager.AppSettings["MaxHeightImageSize"]);

    public static int Warning = Convert.ToInt16(ConfigurationManager.AppSettings["Warning"]);

    public static string cs_sqlserver = ConfigurationManager.ConnectionStrings["cs_sqlserver"].ToString();
    public static string Key = ConfigurationManager.AppSettings["Key"];
    public static string ValidKey = ConfigurationManager.AppSettings["ValidKey"];

    public static string Email = ConfigurationManager.AppSettings["Email"];
    public static string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
    public static string EmailCC = ConfigurationManager.AppSettings["EmailCC"];
    public static string HostMail = ConfigurationManager.AppSettings["HostMail"];
    public static string PostMail = ConfigurationManager.AppSettings["PostMail"];
    public static string MailPass = "[vnn183492760505]";

    

    #endregion

    #region Methods

    public static string GetSubContent(string pContent, int pLength)
    {
        var strResult = pContent;
        if (pContent.Length > pLength)
        {
            strResult = HocLapTrinhWeb.Utilities.StringExtension.SubString(pContent, pLength - 3) + "...";
        }
        return strResult;
    }

    /// <summary>
    /// Lấy hạng Alexa thông qua API
    /// </summary>
    /// <param name="host"></param>
    /// <param name="iTrafficRank"></param>
    /// <param name="iTrafficRankVn"></param>
    /// <param name="iSiteLink"></param>
    public static void GetAlexa(string host, ref int iTrafficRank, ref int iTrafficRankVn, ref int iSiteLink)
    {
        var html = "http://data.alexa.com/data?cli=10&dat=s&url=" + host;
        try
        {
            var xr = XmlReader.Create(html);
            var xdoc = new XmlDocument();
            xdoc.Load(xr);
            try
            {
                iTrafficRank = int.Parse(xdoc.SelectSingleNode("/ALEXA/SD/POPULARITY").Attributes["TEXT"].Value.ToString());
            }
            catch
            {
                iTrafficRank = 0;
            }
            try
            {
                iTrafficRankVn = int.Parse(xdoc.SelectNodes("/ALEXA/SD/COUNTRY").Count == 1 ? xdoc.SelectNodes("/ALEXA/SD/COUNTRY")[0].Attributes["RANK"].Value : xdoc.SelectNodes("/ALEXA/SD/COUNTRY")[1].Attributes["RANK"].Value);
            }
            catch { iTrafficRankVn = 0; }
            try
            {
                iSiteLink = int.Parse(xdoc.SelectSingleNode("/ALEXA/SD/LINKSIN").Attributes["NUM"].Value);
            }
            catch { iSiteLink = 0; }
        }
        catch
        {
            iTrafficRank = 0;
            iTrafficRankVn = 0;
            iSiteLink = 0;
        }
    }

    /// <summary>
    /// Kiểm tra mail có hợp lệ không
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool CheckEmail(string str)
    {
        var re = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        return re.IsMatch(str);
    }

    public static string Encrypt(string toEncrypt, string key, bool useHashing)
    {
        byte[] keyArray;
        var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

        if (useHashing)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
        else
            keyArray = Encoding.UTF8.GetBytes(key);

        var tdes = new TripleDESCryptoServiceProvider
            {Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};

        var cTransform = tdes.CreateEncryptor();
        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string toDecrypt, string key, bool useHashing)
    {
        byte[] keyArray;
        var toEncryptArray = Convert.FromBase64String(toDecrypt);

        if (useHashing)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
        else
            keyArray = Encoding.UTF8.GetBytes(key);

        var tdes = new TripleDESCryptoServiceProvider
            {Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};

        var cTransform = tdes.CreateDecryptor();
        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Encoding.UTF8.GetString(resultArray);
    }



    #endregion
}
