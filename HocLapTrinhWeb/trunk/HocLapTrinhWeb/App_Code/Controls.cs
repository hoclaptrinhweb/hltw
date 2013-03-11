using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.Security.Application;

namespace Controls
{
    public class Ckeditor : HtmlTextArea
    {
        //http://docs.cksource.com/ckeditor_api/symbols/CKEDITOR.config.html

        /// <summary>
        /// The base href URL used to resolve relative and absolute URLs in the editor content.
        /// </summary>
        public string BaseHref
        {
            get { return ViewState["BaseHref"] as string ?? ""; }
            set { ViewState["BaseHref"] = value; }
        }

        /// <summary>
        /// The height of editing area( content ), in relative or absolute.
        /// </summary>
        public Unit Height
        {
            get { return Util.Parse<Unit>(ViewState["Height"]); }
            set { ViewState["Height"] = value; }
        }

        /// <summary>
        /// The width of editing area( content ), in relative or absolute.
        /// </summary>
        public Unit Width
        {
            get { return Util.Parse<Unit>(ViewState["Width"]); }
            set { ViewState["Width"] = value; }
        }

        /// <summary>
        /// The toolbox (alias toolbar) definition.
        /// </summary>
        public string Toolbar
        {
            get { return ViewState["Toolbar"] as string ?? ""; }
            set { ViewState["Toolbar"] = value; }
        }

        /// <summary>
        /// The CSS file(s) to be used to apply style to the contents. Specify multiple like "['content1.css','content2.css']", note ~ will not be resolved when specifying multiple.
        /// </summary>
        public string ContentsCss
        {
            get { return ViewState["ContentsCss"] as string ?? ""; }
            set { ViewState["ContentsCss"] = value; }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool res = base.LoadPostData(postDataKey, postCollection);
            Value = AntiXss.GetSafeHtmlFragment(Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&"));
            return res;
        }

        protected override void OnPreRender(EventArgs e)
        {
            var settings = new List<string> {"baseHref: '" + ResolveUrl(BaseHref) + "'"};
            if (Height != Unit.Empty)
                settings.Add("height: '" + Height + "'");
            if (Width != Unit.Empty)
                settings.Add("width: '" + Width + "'");
            if (Toolbar != "")
                settings.Add("toolbar: '" + Toolbar + "'");
            if (ContentsCss != "")
                settings.Add("contentsCss: '" + (ContentsCss.StartsWith("~") ? ResolveUrl(ContentsCss) : ContentsCss) + "'");
            
            ScriptManager.RegisterClientScriptInclude(this, GetType(), "ckeditorScript", ResolveUrl(BaseHref + (BaseHref.EndsWith("/") ? "" : "/") + "ckeditor.js"));
            ScriptManager.RegisterStartupScript(this, GetType(), "ckeditorLoad", "CKEDITOR.replace('" + ClientID + "', { " + String.Join(", ", settings.ToArray()) + " });", true);

            base.OnPreRender(e);
        }
    }
}
