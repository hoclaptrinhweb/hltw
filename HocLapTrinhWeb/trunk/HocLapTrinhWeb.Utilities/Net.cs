using System;
using System.Collections;
using System.Net;
using System.Web;

namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Lớp mở rộng cho hàm chuỗi
    /// </summary>
    public class Net
    {
        /// <summary>
        /// Lấy lên host name của máy client
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            try
            {
                return Dns.GetHostName();
            }
            catch { return null; }
        }

        /// <summary>
        /// Lấy lên dãy IP của host Name hiện tại phía client
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetIPAddress()
        {
            try
            {
                //Get host name
                string strHostName = Dns.GetHostName();

                //Find host by name
                IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

                // Enumerate IP addresses
                ArrayList arrAddress = new ArrayList();
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    arrAddress.Add(ipaddress.ToString());
                }
                return arrAddress;
                //IPAddress[] ipAdd = Dns.GetHostAddresses(strHostName);
                //foreach (IPAddress ipaddress in ipAdd)
                //{
                //    Response.Write("IP #" + ++nIP + ": " +
                //                      ipaddress.ToString() + "\n");
                //}
            }
            catch { return null; }
        }

        /// <summary>
        /// Lấy IP của máy client khi ghé thăm website
        /// </summary>
        /// <returns></returns>
        public static string GetVisitorIPAddress()
        {
            try
            {
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(ip))
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                return ip;
            }
            catch { return null; }
        }
    }
}

