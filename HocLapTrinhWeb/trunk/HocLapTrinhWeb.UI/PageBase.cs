using System;
using System.Configuration;
using System.Web.UI;
using DH.Data.SqlServer;
using System.Web.UI.HtmlControls;

namespace HocLapTrinhWeb.UI
{
    public class PageBase : Page
    {
        /// <summary>
        /// Chuỗi kết nối database hiện hành
        /// </summary>
        public string connectionString;
        private string lang;
        /// <summary>
        /// Lớp lấy thông báo lỗi
        /// </summary>
        public MessageBox msg;
        /// <summary>
        /// Connect hiện tại
        /// </summary>
        private ClassBase CurrentConnect;
        /// <summary>
        ///1. Khoi tao Language
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            try
            {
                if ((this.Session["Language"] == null) || this.Session["Language"].Equals(""))
                {
                    if ((ConfigurationManager.AppSettings["Language"] == null) || ConfigurationManager.AppSettings["Language"].Equals(""))
                    {
                        this.Session["Language"] = "en-US";
                    }
                    else
                    {
                        this.Session["Language"] = ConfigurationManager.AppSettings["Language"];
                    }
                }
                base.UICulture = this.Session["Language"].ToString();
                base.Culture = this.Session["Language"].ToString();
                this.lang = this.Session["Language"].ToString();
                //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo( this.Session["Language"].ToString());
                //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                //System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch
            {
                this.Session["Language"] = "en-US";
            }
        }
        /// <summary>
        /// 2. Khoi tao page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            try
            {
                this.connectionString = ConfigurationManager.ConnectionStrings["cs_sqlserver"].ConnectionString;
                //Khóa
                var key = ConfigurationManager.AppSettings["Key"];
                //Khóa hợp lệ
                var validKey = ConfigurationManager.AppSettings["ValidKey"];
                //Tạo connect mới
                CurrentConnect = new ClassBase();
                if (!CurrentConnect.IConnect.CreateConnection(this.connectionString, key, validKey))
                {
                    base.Server.Transfer(base.Request.ApplicationPath + "/Error.aspx");
                    return;
                }
                msg = new MessageBox(this.getCurrentConnection(), this.Session["Language"].ToString());
                msg.SetLang(this.Session["Language"].ToString());
            }
            catch (Exception exception)
            {
                if (CurrentConnect.IConnect.GetConnected() != null)
                    this.msg.AddMessage("ERR-000000", exception.Message, 0);
                else
                    base.Server.Transfer(base.Request.ApplicationPath + "/Error.aspx");
            }
        }

        /// <summary>
        /// Tạo lại connect mới
        /// </summary>
        /// <param name="ConnectionString">Chuổi connect</param>
        /// <param name="Key">Khóa kết nối</param>
        /// <param name="ValidKey">Khóa hợp lệ</param>
        /// <returns></returns>
        public bool CreateConnection(string ConnectionString, string Key, string ValidKey)
        {
            try
            {
                this.connectionString = ConnectionString;
                //Tạo connect mới
                CurrentConnect = new ClassBase();
                if (!CurrentConnect.IConnect.CreateConnection(this.connectionString, Key, ValidKey))
                {
                    base.Server.Transfer(base.Request.ApplicationPath + "/Error.aspx");
                    return false;
                }
                msg = new MessageBox(this.getCurrentConnection(), this.Session["Language"].ToString());
                msg.SetLang(this.Session["Language"].ToString());
                return true;
            }
            catch (Exception exception)
            {
                if (CurrentConnect.IConnect.GetConnected() != null)
                    this.msg.AddMessage("ERR-000000", exception.Message, 0);
                else
                    base.Server.Transfer(base.Request.ApplicationPath + "/Error.aspx");
                return false;
            }
        }

        /// <summary>
        /// Ngôn ngữ hiện tại của trang
        /// </summary>
        public string Language
        {
            get
            {
                return this.lang;
            }
            set
            {
                this.lang = value;
                this.Session["Language"] = this.lang;
            }
        }

        /// <summary>
        /// Load lại page
        /// </summary>
        public void Reload()
        {
            base.Response.Redirect(this.Page.Request.Url.PathAndQuery);
        }

        /// <summary>
        /// Load lại page với ajax
        /// </summary>
        public void ReloadWithAjax()
        {
            ScriptManager.RegisterClientScriptBlock((Control)this.Page, typeof(string), "jsRedirect", "window.location.href = '" + this.Page.Request.Url.PathAndQuery + "';", true);
        }

        /// <summary>
        /// Lấy lên kết nối hiện tại khi kế thừa PageBase
        /// </summary>
        /// <returns></returns>
        public Connection getCurrentConnection()
        {
            return this.CurrentConnect.IConnect;
        }

        /// <summary>
        /// Lấy lên địa chỉ url của page hiện hành
        /// </summary>
        /// <returns></returns>
        public string GetRequestURL()
        {
            return (base.Request.Url.PathAndQuery);
        }

        /// <summary>
        /// Chuyển tới địa chỉ url
        /// </summary>
        /// <param name="URL">URL cần chuyển tới</param>
        public void GoPage(string URL)
        {
            base.Response.Redirect(URL);
        }

        /// <summary>
        /// Chuyển tới địa chỉ url bằng script
        /// </summary>
        /// <param name="url">Url cần chuyển tới</param>
        public void GoPageWithAjax(string url)
        {
            ScriptManager.RegisterClientScriptBlock((Control)this.Page, typeof(string), "jsRedirect", "window.location.href = '" + url + "';", true);
        }

        /// <summary>
        /// 
        /// </summary>
        public string UrlRoot
        {
            get
            {
                return (base.Request.Url.Scheme + "://" + base.Request.Url.Host + ((base.Request.Url.Port == 80) ? "" : (":" + base.Request.Url.Port)) + ((base.Request.ApplicationPath == "/") ? "" : base.Request.ApplicationPath));
            }
        }

        #region Meta Management
        /// <summary>
        /// thêm metatags vào header. sẽ bị lỗi nếu trên header có dùng <%# =  %>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public void SetMetaTags(string name, string content)
        {
            try
            {
                var metaDesc = new HtmlMeta {Name = name, Content = content};
                base.Page.Header.Controls.Add(metaDesc);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gán meta description cho page hiện hành
        /// </summary>
        /// <param name="description"></param>
        public void SetMetaDescription(string description)
        {
            try
            {
                var metaDesc = new HtmlMeta {Name = "description", Content = description};
                base.Page.Header.Controls.Add(metaDesc);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gán meta keyword cho page hiện hành
        /// </summary>
        /// <param name="keyword"></param>
        public void SetMetaKeyword(string keyword)
        {
            try
            {
                var metaKey = new HtmlMeta {Name = "keywords", Content = keyword};
                base.Page.Header.Controls.Add(metaKey);
            }
            catch
            {
                throw new Exception();
            }
        }

        public void SetMetaTitle(string title)
        {
            try
            {
                var metaKey = new HtmlMeta {Name = "title", Content = title};
                base.Page.Header.Controls.Add(metaKey);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Tạo thẻ base
        /// </summary>
        /// <param name="url"></param>
        public void SetBaseTag(string url)
        {
            try
            {
                var basetag = new HtmlGenericControl("base");
                basetag.Attributes.Add("href", url);
                base.Page.Header.Controls.Add(basetag);
            }
            catch
            {
                throw new Exception();
            }
        }

        /*
            <meta NAME="ROBOTS" CONTENT="INDEX,NOFOLLOW"/>
            <meta NAME="DESCRIPTION" CONTENT="webparts in action"/>
            <meta NAME="KEYWORDS" CONTENT="webpart"/>
            <meta NAME="AUTHOR" CONTENT="DWS"/>
            <meta NAME="publication_date" CONTENT="1/26/2005"/>
            <meta NAME="distribution" CONTENT="global"/>
            <meta NAME="language" CONTENT="english"/>
            <meta NAME="rating" CONTENT="general"/>
            <meta NAME="copyright" CONTENT="2002, 2005, 2006"/>
            <meta HTTP-EQUIV="Reply_to" CONTENT="anyone@anywhere.com"/>
         */
        #endregion
    }
}
/*
    <appSettings>
    <add key="Language" value="en-US"/>
    <add key="Key" value="Data Source=calgie-ltkiet;Initial Catalog=webtest;Persist Security Info=True;User ID=admin;"/>
    <add key="ValidKey" value="0dOh0wD+V4KDcLzsjyTIsQ=="/>WjpW9LXqCXO6kSx5MeopsA==
	</appSettings>
 	<connectionStrings>
		<add name="ConnectionString" connectionString="Data Source=calgie-ltkiet;Initial Catalog=webtest;Persist Security Info=True;User ID=admin;Password=admin;Min Pool Size=10;Max Pool Size=100;"/>
	</connectionStrings>
  
 */
