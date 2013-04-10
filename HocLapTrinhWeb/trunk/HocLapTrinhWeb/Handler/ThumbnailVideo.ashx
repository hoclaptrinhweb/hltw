<%@ WebHandler Language="C#" Class="Thumbnail" %>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

public class Thumbnail : IHttpHandler
{
    private int _thumbnailSize = 150;

    public void ProcessRequest(HttpContext context)
    {
        var pathPhoto = context.Request.QueryString["p"];
        if (string.IsNullOrEmpty(context.Request.QueryString["w"]) == false)
        {
            try
            {
                _thumbnailSize = int.Parse(context.Request.QueryString["w"]);
            }
            catch
            {
                _thumbnailSize = 150;
            }
        }
        else
            _thumbnailSize = 150;


        var photoName = Path.GetFileNameWithoutExtension(pathPhoto).ToLower();
        //if (_nameValidationExpression.IsMatch(photoName)) {
        //    throw new HttpException(404, "Invalid photo name.");
        //}
        var cachePath = Path.Combine(HttpRuntime.CodegenDir, photoName + "_w_" + _thumbnailSize + ".png");
        if (File.Exists(cachePath))
        {
            OutputCacheResponse(context, File.GetLastWriteTime(cachePath));
            context.Response.WriteFile(cachePath);
            return;
        }
        pathPhoto = context.Server.MapPath("~/" + Global.ImagesVideo + pathPhoto);
        Bitmap photo;
        try
        {
            photo = new Bitmap(pathPhoto);
        }
        catch (ArgumentException)
        {
            photo = new Bitmap(context.Server.MapPath("~/" + Global.ImagesVideo + "noimage.jpg"));
        }
        context.Response.ContentType = "image/png";
        int width, height;
        if (photo.Width > photo.Height)
        {
            width = _thumbnailSize;
            height = photo.Height * _thumbnailSize / photo.Width;
        }
        else
        {
            width = photo.Width * _thumbnailSize / photo.Height;
            height = _thumbnailSize;
        }
        var target = new Bitmap(width, height);
        using (var graphics = Graphics.FromImage(target))
        {
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(photo, 0, 0, width, height);

            if (width > 100)
            {
                const string strCopyright = " hoclaptrinhweb ";
                var drFont = new Font("Times New Roman", 9, FontStyle.Bold);
                var drBrush = new SolidBrush(Color.Red);
                var drPoint = new PointF(1, height - 14);
                graphics.DrawString(strCopyright, drFont, drBrush, drPoint);
            }
            using (var memoryStream = new MemoryStream())
            {
                target.Save(memoryStream, ImageFormat.Png);
                OutputCacheResponse(context, File.GetLastWriteTime(pathPhoto));
                using (var diskCacheStream = new FileStream(cachePath, FileMode.CreateNew))
                {
                    memoryStream.WriteTo(diskCacheStream);
                }
                memoryStream.WriteTo(context.Response.OutputStream);
            }
        }
    }

    private static void OutputCacheResponse(HttpContext context, DateTime lastModified)
    {
        var cachePolicy = context.Response.Cache;
        context.Response.Cookies.Clear();
        cachePolicy.SetCacheability(HttpCacheability.Public);
        cachePolicy.VaryByParams["p"] = true;
        cachePolicy.VaryByParams["w"] = true;
        cachePolicy.SetOmitVaryStar(true);
        cachePolicy.SetExpires(DateTime.Now + TimeSpan.FromDays(365));
        cachePolicy.SetValidUntilExpires(true);
        cachePolicy.SetLastModified(lastModified);
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}