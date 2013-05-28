﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Facebook;

public partial class usercontrols_ucDownLoad : HocLapTrinhWeb.UI.UCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            var filename = System.IO.Path.GetFileName(hdUser.Value + "." + hdiType.Value);
            var fbApp = new FacebookClient(hdAccess.Value);
            var result = (IDictionary<string, object>)fbApp.Get("me/permissions");
            var objData = (Facebook.JsonArray)result["data"];
            var objd = (IDictionary<string, object>)objData[0];
            if (objd.ContainsKey("publish_stream") && objd.ContainsKey("photo_upload") && objd.ContainsKey("user_photos") && (long)objd["publish_stream"] == 1 && (long)objd["photo_upload"] == 1 && (long)objd["user_photos"] == 1)
            {

                var albumParameters = new Dictionary<string, object>();
                albumParameters.Add("message", "Được tải lên bởi ứng dụng " + UrlRoot);
                albumParameters.Add("name", "Chỗ ở hiện tại.Được tải bởi ứng dụng " + UrlRoot);
                result = (IDictionary<string, object>)fbApp.Post("/me/albums", albumParameters);

                var mediaObject = new FacebookMediaObject();
                mediaObject.ContentType = "image/jpeg";
                mediaObject.FileName = "Nhà của bạn";
                var fileBytes = File.ReadAllBytes(Server.MapPath("~/images/logo.png"));
                mediaObject.SetValue(fileBytes);
                IDictionary<string, object> upload = new Dictionary<string, object>();
                upload.Add("name", "Được tải lên bởi " + UrlRoot);
                upload.Add("image", mediaObject);
                result = (IDictionary<string, object>)fbApp.Post("/" + (string)result["id"] + "/photos", upload);
            }
        }
        catch (Exception ex)
        {
        }

    }
}