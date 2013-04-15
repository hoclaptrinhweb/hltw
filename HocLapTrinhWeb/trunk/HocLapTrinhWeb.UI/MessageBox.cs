using System;
using DH.Data.SqlServer;
using System.Data;

namespace HocLapTrinhWeb.UI
{
    public class MessageBox : ClassBase
    {
       
        private Connection ic;
        private string Language;
        public string TableStructureString = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            SET ANSI_PADDING ON
            GO
            CREATE TABLE [dbo].[dh_tbl_message](
	            [MessageCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	            [Message] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [Language] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

            GO
            SET ANSI_PADDING OFF";
        public MessageBox(Connection conn, string lang)
        {
            this.ic = conn;
            this.Language = lang;
        }
        public void SetLang(string lang)
        {
            this.Language = lang;
        }

        /// <summary>
        /// Lấy lên thông báo từ hệ thống
        /// </summary>
        /// <param name="msgcode"></param>
        /// <returns></returns>
        public string GetMessage(string msgcode)
        {
            try
            {
                this.ic.Open();
                var da = new DataAccess(ic, null);
                var dt = new DataTable();
                const string strSQL = "select * from dh_tbl_message where MessageCode=@msgCode and Language=@Language";
                da.AddParams("@msgCode", SqlDbType.VarChar, msgcode, ParameterDirection.Input);
                da.AddParams("@Language", SqlDbType.VarChar, this.Language, ParameterDirection.Input);
                if (!da.FillDataFromSQLQuery(strSQL, dt))
                    return "Message is error.";
                return dt.Rows.Count == 0 ? "Message not exist" : dt.Rows[0]["Message"].ToString();
            }
            catch (Exception exception)
            {
                base.AddMessage("ERR-000000", exception.Message, 0);
                return "Message is error.";
            }
            finally
            {
                this.ic.Close();
            }
        }
    }
}
