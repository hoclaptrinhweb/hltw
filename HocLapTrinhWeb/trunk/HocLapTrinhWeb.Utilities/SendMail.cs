using System;
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
                From = new MailAddress("Global.Email", "Global.DisplayName")
            };
            msg.To.Add(new MailAddress(mailTo));
            msg.CC.Add(new MailAddress("Global.EmailCC"));
            msg.Subject = subject;

            var client = new SmtpClient("Global.HostMail", int.Parse("Global.PostMail"))
            {
                Credentials =
                    new NetworkCredential("Global.Email", "Global.MailPass"),
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

    public static string SendMailFull(string mailFrom, string mailpass, string host, string port, string mailTo, string subject, string content, bool enableSsl)
    {
        try
        {
            var msg = new MailMessage
            {
                IsBodyHtml = true,
                Body = content,
                From = new MailAddress(mailFrom, mailFrom)
            };
            msg.To.Add(new MailAddress(mailTo));
            msg.Subject = subject;

            var client = new SmtpClient(host, int.Parse(port))
            {
                Credentials =
                    new NetworkCredential(mailFrom, mailpass),
                EnableSsl = enableSsl
            };

            client.Send(msg);

            return "";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

}
