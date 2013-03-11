using System;
using System.Web;

namespace AspNetResources.Web
{
    public class MyModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.ReleaseRequestState += new EventHandler(InstallResponseFilter);
            app.EndRequest += new EventHandler(InstallEndRequestFilter);
        }


        private void InstallResponseFilter(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            var app = (HttpApplication)sender;
            if (app.Context.Request.FilePath.ToLower().Contains("/contact.aspx") ||
                app.Context.Request.FilePath.ToLower().Contains(".ashx") ||
                app.Context.Request.FilePath.ToLower().Contains("/admin/") ||
                app.Context.Request.FilePath.ToLower().Contains("/test/") ||
                app.Context.Request.FilePath.ToLower().Contains("/members/") ||
                app.Context.Request.FilePath.ToLower().Contains("/ckeditor/")) return;
            if (response.ContentType == "text/html")
                response.Filter = new PageFilter(response.Filter);
        }

        private void InstallEndRequestFilter(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (!context.Request.Url.ToString().StartsWith("http://m.hoclaptrinhweb.com")) return;
            if (context.Response.ContentType != "image/png") return;
            context.Request.Cookies.Clear();
            context.Response.Cookies.Clear();
            //List<string> cookiesToClear = new List<string>();
            //foreach (string cookieName in context.Request.Cookies)
            //{
            //    HttpCookie cookie = context.Request.Cookies[cookieName];
            //    cookiesToClear.Add(cookie.Name);
            //}

            //foreach (string name in cookiesToClear)
            //{
            //    HttpCookie cookie = new HttpCookie(name, string.Empty);
            //    cookie.Expires = DateTime.Today.AddYears(-1);
            //    context.Response.Cookies.Set(cookie);
            //}
        }


        public void Dispose()
        {

        }
    }
}
