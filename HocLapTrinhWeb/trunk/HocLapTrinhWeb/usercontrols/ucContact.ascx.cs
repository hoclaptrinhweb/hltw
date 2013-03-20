using System;
using HocLapTrinhWeb.BLL;

public partial class usercontrols_ucContact : DH.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    public void ClearText()
    {
        txtUsername.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtContent.Text = string.Empty;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtImgVerify.Text != ImageVerifier1.Text)
            {
                SaveValidate1.IsValid = false;
                SaveValidate1.ErrorMessage = "Mã xác nhận nhập không đúng, hãy nhập lại!";
                return;
            }
            var contactBll = new ContactBLL(CurrentPage.getCurrentConnection());
            var dt = new dsHocLapTrinhWeb.tbl_ContactDataTable();
            var row = dt.Newtbl_ContactRow();
            row.UserName = txtUsername.Text;
            row.Email = txtEmail.Text;
            row.Phone = txtPhone.Text;
            row.Address = txtAddress.Text;
            row.Title = txtTitle.Text;
            row.Content = txtContent.Text;
            row.CreatedDate = DateTime.Now;
            row.Type = 1;
            row.Status = 0;
            row.CreatedDate = DateTime.Now;
            SendMail.SendMailFrom("vonhatnam100689@mail.com", txtTitle.Text + " - " + txtEmail.Text, txtContent.Text);
            dt.Addtbl_ContactRow(row);
            if (!contactBll.Add(dt)) return;
            Page.RegisterStartupScript("THANHCONG", "<script language='javascript' type='text/javascript'> alert('Gửi liên hệ thành công!')</script>");
            ClearText();
        }
        catch (Exception)
        {
            SaveValidate1.IsValid = false;
            SaveValidate1.ErrorMessage = "Gửi không thành công!";
        }
    }

}
