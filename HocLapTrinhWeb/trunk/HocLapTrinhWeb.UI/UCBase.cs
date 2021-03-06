using System;
using System.Web.UI;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.UI
{
    public class UCBase : UserControl
    {
        protected string connectionString;
        protected PageBase CurrentPage;
        protected MessageBox msg;

        public Connection getCurrentConnection()
        {
            return this.CurrentPage.getCurrentConnection();
        }
        /// <summary>
        /// cho phep overide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            this.CurrentPage = (PageBase)this.Page;
            this.connectionString = this.CurrentPage.connectionString;
            this.msg = this.CurrentPage.msg;
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
            return CurrentPage.CreateConnection(ConnectionString, Key, ValidKey);
        }
    }
}
