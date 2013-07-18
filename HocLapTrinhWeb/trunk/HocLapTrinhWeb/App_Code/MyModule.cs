using System;
using System.Web;

namespace AspNetResources.Web
{
    public class MyModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.ReleaseRequestState += new EventHandler(InstallResponseFilter);
            app.BeginRequest += (new EventHandler(this.Application_BeginRequest));
            app.EndRequest += (new EventHandler(this.Application_EndRequest));
        }


        private void InstallResponseFilter(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            var app = (HttpApplication)sender;
            if (app.Context.Request.FilePath.ToLower().Contains("/contact.aspx") ||
                app.Context.Request.FilePath.ToLower().Contains(".ashx") ||
                app.Context.Request.FilePath.ToLower().Contains("/admin/") ||
                app.Context.Request.FilePath.ToLower().Contains("/code/") ||
                 app.Context.Request.FilePath.ToLower().Contains("/download/") ||
                app.Context.Request.FilePath.ToLower().Contains("/test/") ||
                app.Context.Request.FilePath.ToLower().Contains("/members/") ||
                app.Context.Request.FilePath.ToLower().Contains("/newspost.aspx") ||
                app.Context.Request.FilePath.ToLower().Contains("/ckeditor/")) return;
            if (response.ContentType == "text/html" && app.Context.Request.FilePath.ToLower().Contains(".aspx"))
                response.Filter = new PageFilter(response.Filter);
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            //var application = (HttpApplication)source;
            //var context = application.Context;
            //context.Response.Write("<h1><font color=red>HelloWorldModule: Beginning of Request</font></h1><hr>");
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            //var application = (HttpApplication)source;
            //var context = application.Context;
            //context.Response.Write("<hr><h1><font color=red>HelloWorldModule: End of Request</font></h1>");
        }

        public void Dispose()
        {

        }
    }
}
