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
                    From = new MailAddress(Global.Email, Global.DisplayName)
                };
            msg.To.Add(new MailAddress(mailTo));
            msg.CC.Add(new MailAddress(Global.EmailCC));
            msg.Subject = subject;

            var client = new SmtpClient(Global.HostMail, int.Parse(Global.PostMail))
                {
                    Credentials =
                        new NetworkCredential(Global.Email, Global.MailPass),
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
