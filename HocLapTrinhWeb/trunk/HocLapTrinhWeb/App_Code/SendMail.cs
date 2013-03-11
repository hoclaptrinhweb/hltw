using System;
using System.Configuration;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for SendMail
/// </summary>
public static class SendMail
{

    public static bool SendMailFrom(string mailTo, string subject, string content)
    {
        try
        {
            var msg = new MailMessage
                {
                    IsBodyHtml = true,
                    Body = content,
                    From =
                        new MailAddress(ConfigurationManager.AppSettings["Email"],
                                        ConfigurationManager.AppSettings["DisplayName"])
                };
            msg.To.Add(new MailAddress(mailTo));
            msg.CC.Add(new MailAddress(ConfigurationManager.AppSettings["EmailCC"]));
            msg.Subject = subject;

            var client = new SmtpClient(ConfigurationManager.AppSettings["HostMail"], int.Parse(ConfigurationManager.AppSettings["PostMail"]))
                {
                    Credentials =
                        new NetworkCredential(ConfigurationManager.AppSettings["Email"],
                                              ConfigurationManager.AppSettings["MailPass"]),
                    EnableSsl = true
                };

            client.Send(msg);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
