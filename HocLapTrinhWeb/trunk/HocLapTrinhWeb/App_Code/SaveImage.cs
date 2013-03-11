using System;
using System.Web.Services;
using System.Net;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SaveImage : WebService
{
    [WebMethod]
    public bool PostImage(string fileName, string pathImage)
    {
        try
        {
            var webClient = new WebClient();
            webClient.DownloadFile(pathImage, Server.MapPath("~/" + Global.ImagesNews + fileName));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
