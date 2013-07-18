using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Code_SendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        var a = SendMail.SendMailFull(txtEmail.Text, txtPassword.Text, txtUrlMail.Text, txtPort.Text, txtEmailTo.Text, txtTitle.Text, txtContent.Text, cbEnableSsl.Checked);
        if (a != "")
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = a;
        }
        else
        {
            SaveValidate.IsValid = false;
            SaveValidate.ErrorMessage = "Gửi mail thành công .";
        }
    }
}