using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;


/// <summary>
/// Summary description for XuLyChuoi
/// </summary>
public class XuLyChuoi
{

    /// <summary>
    /// Bỏ dấu tiếng việt cho việc rewrite url
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ConvertToUnSign(string s)
    {
        s = HttpUtility.UrlEncode(s);
        s = s.ToLower().Replace("%e2%80%93", "").Replace("%c2%", "");
        s = HttpUtility.UrlDecode(s);
        var stFormD = s.Trim().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var t in stFormD)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }
        //URL REWRITE

        sb = sb.Replace('Đ', 'D');
        sb = sb.Replace('đ', 'd');
        sb = sb.Replace('!', ' ');
        sb = sb.Replace('$', ' ');
        sb = sb.Replace('#', ' ');
        sb = sb.Replace(':', ' ');
        sb = sb.Replace(';', ' ');
        sb = sb.Replace('&', ' ');
        sb = sb.Replace('=', ' ');
        sb = sb.Replace('/', ' ');
        sb = sb.Replace('|', ' ');
        sb = sb.Replace('*', ' ');
        sb = sb.Replace('%', ' ');
        sb = sb.Replace('.', ' ');
        sb = sb.Replace(',', ' ');
        sb = sb.Replace('"', ' ');
        sb = sb.Replace('“', ' ');
        sb = sb.Replace('”', ' ');
        sb = sb.Replace('\'', ' ');
        sb = sb.Replace('?', ' ');
        sb = sb.Replace('-', ' ');
        sb = sb.Replace('+', ' ');
        sb = sb.Replace('(', ' ');
        sb = sb.Replace(')', ' ');
        sb = sb.Replace('[', ' ');
        sb = sb.Replace(']', ' ');
        sb = sb.Replace('.', ' ');
        sb = sb.Replace(' ', '-');
        return TrimSpace(sb.ToString().ToLower().Trim('-').Normalize(NormalizationForm.FormD), "-");
    }

    /// <summary>
    /// Bỏ dấu tiếng việt cho việc đóng dấu
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ConvertToUnSignWater(string s)
    {
        var stFormD = s.Trim().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var t in stFormD)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }

        sb = sb.Replace('Đ', 'D');
        sb = sb.Replace('đ', 'd');
        sb = sb.Replace('#', ' ');
        sb = sb.Replace(':', ' ');
        sb = sb.Replace(';', ' ');
        sb = sb.Replace('&', ' ');
        sb = sb.Replace('=', ' ');
        sb = sb.Replace('/', ' ');
        sb = sb.Replace('*', ' ');
        sb = sb.Replace('%', ' ');
        sb = sb.Replace('.', ' ');
        sb = sb.Replace(',', ' ');
        sb = sb.Replace('"', ' ');
        sb = sb.Replace('“', ' ');
        sb = sb.Replace('”', ' ');
        sb = sb.Replace('\'', ' ');
        sb = sb.Replace('?', ' ');
        sb = sb.Replace('-', ' ');

        return TrimSpace(sb.ToString().Trim(' ').Normalize(NormalizationForm.FormD), " ");
    }

    /// <summary> 
    /// Ham dung de bo dau tieng viet
    /// Dung cho ham search trang chu
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ConvertToStringNoSymbol(string s)
    {
        var stFormD = s.Trim().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var t in stFormD)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }
        sb = sb.Replace('Đ', 'D');
        sb = sb.Replace('đ', 'd');
        sb = sb.Replace('#', ' ');
        sb = sb.Replace(':', ' ');
        sb = sb.Replace(';', ' ');
        sb = sb.Replace('&', ' ');
        sb = sb.Replace('@', ' ');
        sb = sb.Replace('=', ' ');
        sb = sb.Replace('/', ' ');
        sb = sb.Replace('*', ' ');
        sb = sb.Replace('%', ' ');
        sb = sb.Replace('.', ' ');
        sb = sb.Replace(',', ' ');
        sb = sb.Replace('"', ' ');
        sb = sb.Replace('“', ' ');
        sb = sb.Replace('”', ' ');
        sb = sb.Replace('\'', ' ');
        sb = sb.Replace('?', ' ');
        sb = sb.Replace('!', ' ');
        sb = sb.Replace('-', ' ');
        sb = sb.Replace('+', ' ');
        sb = sb.Replace('(', ' ');
        sb = sb.Replace(')', ' ');
        return TrimSpace(sb.ToString(), " ");

    }

    public static string TrimSpace(string s, string strSplit)
    {

        s = s.Trim();
        s = s.Trim('-');
        try
        {
            for (var i = 0; i < s.Length - 1; i++)
            {
                while (s.Substring(i, 1) == strSplit && s.Substring(i + 1, 1) == strSplit)
                    s = s.Remove(i, 1);
            }
        }
        catch
        {
            return s;
        }

        return s;
    }

    public static string ConvertApiJson(string sb)
    {
        sb = sb.Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\"", "").Replace("*", "");
        return sb;
    }

    public static string StripTagsRegex(string source)
    {
        var tmp = Regex.Replace(source, "<!--(.|\n)*?-->", string.Empty);
        tmp = Regex.Replace(tmp, "<(.|\n)*?>", string.Empty);
        tmp = TrimSpace(tmp.Replace("&nbsp;", " ").Replace("\r\n", " "), " ");
        return tmp;
    }

    public static string ConvertTitle(string strTitle, string strHref)
    {
        var url = new Uri(strHref);
        strHref = url.Host;
        strHref = strHref.Replace("www.", "");
        switch (strHref)
        {
            case "learn.tech-web.vn":
                {
                    strTitle = strTitle.Replace("\r\n", "").Trim();
                    var n = strTitle.ToLower().IndexOf("cập nhật lúc", StringComparison.Ordinal);
                    if (n > 1)
                        strTitle = strTitle.Substring(0, n);
                    break;
                }
        }
        strTitle = HttpUtility.HtmlDecode(strTitle);
        strTitle = strTitle.Replace("\r\n", "").Trim();
        return strTitle;
    }

    public static string ConvertBrief(string strBrief, string strHref)
    {
        int n;
        var url = new Uri(strHref);
        strHref = url.Host;
        strHref = strHref.Replace("www.", "");
        switch (strHref)
        {
            case "learn.tech-web.vn":
                {
                    strBrief = strBrief.Replace("\r\n", "").Trim();
                    n = strBrief.ToLower().IndexOf("cập nhật lúc", StringComparison.Ordinal);
                    if (n > 1)
                        strBrief = strBrief.Substring(0, n);
                    break;
                }
            case "yinyangit.wordpress.com":
                {
                    n = strBrief.IndexOf("\n Continue reading &rarr;\n\t\t\t\t\t", StringComparison.Ordinal);
                    if (n > 1)
                        strBrief = strBrief.Substring(0, n);
                    n = strBrief.IndexOf("\n\t\t\t", StringComparison.Ordinal);
                    if (n == 0)
                        strBrief = strBrief.Substring(4);
                    break;
                }
            case "xahoithongtin.com.vn":
                {
                    strBrief = strBrief.Replace("\r\n      XHTTOnline:&nbsp;", "");
                    break;
                }
            case "thongtincongnghe.com":
                {
                    strBrief = strBrief.Replace("\r\n", "").Trim();
                    var tmp = strBrief.Substring(0, 5);
                    var arr = tmp.Split(':');
                    if (arr.Length == 2)
                    {
                          //  n = int.Parse(arr[0]);
                          //  n = int.Parse(arr[1]);
                        strBrief = strBrief.Length >= 6 ? strBrief.Substring(6) : "";
                    }
                    break;
                }
            case "tintoi.com":
                {
                    if (strBrief.Substring(strBrief.Length - 8).ToLower() == "xem tiếp")
                        strBrief = strBrief.Substring(0, strBrief.Length - 8);
                    break;
                }
        }
        strBrief = HttpUtility.HtmlDecode(strBrief);
        strBrief = strBrief.Trim();
        return strBrief;
    }

    public static string ConvertContent(string strContent, string strHref)
    {
        int n;
        var url = new Uri(strHref);
        strHref = url.Host;
        strHref = strHref.Replace("www.", "");
        switch (strHref)
        {
            case "gamek.vn":
                {
                    n = strContent.IndexOf("<div class=\"foot\">", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "yinyangit.wordpress.com":
                {
                    n = strContent.IndexOf("<div id=\"jp-post-flair\"", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "vietseo.net":
                {
                    n = strContent.IndexOf("<h4>Bài viết cùng chủ đề liên quan</h4>", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    n = strContent.IndexOf("<script type=\"text/javascript\">AKPC_IDS += \"114,\";</script>", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    strContent = strContent.Replace("<!--GA-->\r\n<script type=\"text/javascript\"><!--\r\ngoogle_ad_client = \"ca-pub-9778311710856237\";\r\n/* VietSEO - TopPost-468x60 */\r\ngoogle_ad_slot = \"7996076419\";\r\ngoogle_ad_width = 468;\r\ngoogle_ad_height = 60;\r\n//-->\r\n</script>\r\n<script type=\"text/javascript\" src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\">\r\n</script>", "");
                    break;
                }
            case "bcdonline.net":
                {
                    n = strContent.IndexOf("<div class=\"ratingblock \"", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "tinhte.vn":
                {
                    n = strContent.IndexOf("<span class=\"attachedFiles", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    n = strContent.IndexOf("<span class=\"useressLastEdit", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    n = strContent.IndexOf("<div class=\"likesSummary secondaryContent\">", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "nmhblog.wordpress.com":
                {
                    n = strContent.IndexOf("<div id=\"jp-post-flair\"", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "learn.tech-web.vn":
                {
                    n = strContent.LastIndexOf("<div class=\"tinlq\">", StringComparison.Ordinal);
                    if (n > 1)
                        strContent = strContent.Substring(0, n);
                    break;
                }
            case "qhonline.info":
                {
                    n = strContent.IndexOf("<div style='border:1px dashed green; padding:8px;'>", StringComparison.Ordinal);
                    if (n > 0)
                        strContent = strContent.Substring(0, n);
                    break;
                }
        }

        strContent = strContent.Trim();
        return strContent;
    }

    public static string ConvertHtmlToText(string strText)
    {
        strText = strText.Replace("<", "&lt;");
        strText = strText.Replace(">", "&gt;");
        return strText;
    }
}
