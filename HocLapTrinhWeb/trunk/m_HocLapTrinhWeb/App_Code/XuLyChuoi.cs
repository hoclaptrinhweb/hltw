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
   
   
    public static string ConvertHtmlToText(string strText)
    {
        strText = strText.Replace("<", "&lt;");
        strText = strText.Replace(">", "&gt;");
        return strText;
    }
}
