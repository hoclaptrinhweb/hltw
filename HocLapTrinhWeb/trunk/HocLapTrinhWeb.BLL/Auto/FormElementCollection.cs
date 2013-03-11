using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

public class FormElementCollection : Dictionary<string, string>
{
    /// <summary>
    /// Constructor. Parses the HtmlDocument to get all form input elements. 
    /// </summary>
    public FormElementCollection(HtmlDocument htmlDoc)
    {
        var inputs = htmlDoc.DocumentNode.SelectNodes(".//input");
        if (inputs == null)
            return;
        foreach (var t in inputs)
        {
            var name = t.GetAttributeValue("name", "undefined");
            var value = t.GetAttributeValue("value", "");
            try
            {
                if (!name.Equals("undefined")) Add(name, value);
            }
            catch
            { }
        }

        var nodeDocument = htmlDoc.DocumentNode.SelectSingleNode("//textarea[@id='vB_Editor_001_textarea']");
        if (nodeDocument != null)
        {
            string name = nodeDocument.GetAttributeValue("name", "undefined");
            string value = nodeDocument.GetAttributeValue("value", "");
            try
            {
                if (!name.Equals("undefined")) Add(name, value);
            }
            catch
            { }
        }

        //Câu hỏi ngẫu nhiên
        HtmlNode labelhumanverify = htmlDoc.DocumentNode.SelectSingleNode("//label[@for='humanverify']");
        if (labelhumanverify != null)
        {
            try
            {
                string result = labelhumanverify.NextSibling.NextSibling.ChildNodes[1].ChildNodes[0].InnerHtml;
                if (result == "Một ngày có tổng cộng bao nhiêu giờ? (trả lời bằng số)")
                {
                    Add("humanverifyresult", "24");
                    return;
                }
                if (result == "Một năm có bao nhiêu tháng? (trả lời bằng số)")
                {
                    Add("humanverifyresult", "12");
                    return;
                }
                if (result == "Mười nhân mười bằng bao nhiêu? (trả lời bằng số)")
                {
                    Add("humanverifyresult", "100");
                    return;
                }

                result = result.Replace("=...?", "");
                string[] a;
                if (result.Contains("+"))
                {
                    a = result.Split('+');
                    result = (int.Parse(a[0]) + int.Parse(a[1])).ToString();
                }
                else
                {
                    a = result.Split('-');
                    result = (int.Parse(a[0]) - int.Parse(a[1])).ToString();
                }
                Add("humanverifyresult", result);
            }
            catch
            {

            }
        }

    }

    /// <summary>
    /// Assembles all form elements and values to POST. Also html encodes the values.  
    /// </summary>
    public string AssemblePostPayload()
    {
        var sb = new StringBuilder();
        foreach (KeyValuePair<string, string> element in this)
        {
            var value = System.Web.HttpUtility.UrlEncode(element.Value);
            sb.Append("&" + element.Key + "=" + value);
        }
        return sb.ToString().Substring(1);
    }
}
