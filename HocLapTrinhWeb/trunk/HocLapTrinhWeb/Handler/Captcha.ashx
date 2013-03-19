<%@ WebHandler Language="C#" Class="Captcha" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private int keylen = 7;
    private int width = 100;
    private int height = 25;
    private readonly Random random = new Random();

    public void ProcessRequest(HttpContext context)
    {
        // Some chars was deleted (i.e. l, 0, 0) to avoid misunderstanding
        //const string mapcar = "123456789abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
        const string mapcar = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        var randomGenerator = new Random();
        // generate the keyword
        var keyword = "";
        for (var i = 0; i < keylen; i++)
        {
            keyword += mapcar.Substring(randomGenerator.Next(mapcar.Length), 1);
        }
        context.Session["captcha"] = keyword;
        var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        var g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var rect = new Rectangle(0, 0, width, height);
        var hatchBrush = new HatchBrush(HatchStyle.Min, Color.LightGray, Color.White);
        g.FillRectangle(hatchBrush, rect);
        SizeF size;
        var fontSize = rect.Height + 1;
        Font font;

        do
        {
            fontSize--;
            font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
            size = g.MeasureString(keyword, font);
        } while (size.Width > rect.Width);

        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        var path = new GraphicsPath();

        path.AddString(keyword, font.FontFamily, (int)font.Style, 20, rect, format);
        var v = 4F;
        PointF[] points = 
        {
              new PointF(random.Next(rect.Width) / v, random.Next(
                 rect.Height) / v),
              new PointF(rect.Width - random.Next(rect.Width) / v, 
                  random.Next(rect.Height) / v),
              new PointF(random.Next(rect.Width) / v, 
                  rect.Height - random.Next(rect.Height) / v),
              new PointF(rect.Width - random.Next(rect.Width) / v,
                  rect.Height - random.Next(rect.Height) / v)
        };
        var matrix = new Matrix();
        matrix.Translate(0F, 0F);
        path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        hatchBrush = new HatchBrush(HatchStyle.Max, Color.Black, Color.Black);
        g.FillPath(hatchBrush, path);
        //var m = Math.Max(rect.Width, rect.Height);
        //for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
        //{
        //    var x = random.Next(rect.Width);
        //    var y = random.Next(rect.Height);
        //    var w = random.Next(m / 50);
        //    var h = random.Next(m / 50);
        //    g.FillEllipse(hatchBrush, x, y, w, h);
        //}
        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();

        //Sử dụng timeout
        //HttpContext.Current.Cache.Insert(sGuid, keyword, null, DateTime.Now.AddSeconds(dTimeout), TimeSpan.Zero, CacheItemPriority.High, null);

        context.Response.ContentType = "image/jpeg";
        bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        bitmap.Dispose();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}