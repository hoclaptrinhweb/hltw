using System;
using System.Web.UI;
using System.IO;
using System.Configuration;

public partial class ImageBrowserPage : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindImageList();
        }
        Page.ClientScript.RegisterStartupScript(GetType(), "FocusImageList", "window.setTimeout(\"document.getElementById('" + ImageList.ClientID + "').focus();\", 1);", true);
    }

    protected void BindImageList()
    {
        ImageList.Items.Clear();
        var files = Directory.GetFiles(FileImageFolder, "*" + SearchTerms.Text.Replace(" ", "*") + "*");
        files = Array.FindAll(files, delegate(string f) { return IsImage(f); });

        foreach (var file in files)
        {
            if (IsImage(file))
                ImageList.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
        }

        if (files.Length > 0)
            ImageList.SelectedIndex = 0;
    }

    protected void Search(object sender, EventArgs e)
    {
        BindImageList();
    }

    protected void Upload(object sender, EventArgs e)
    {
        if (!IsImage(UploadedImageFile.FileName)) return;
        var filename = UniqueFilename(UploadedImageFile.FileName);
        UploadedImageFile.SaveAs(FileImageFolder + filename);

        var data = ImageMedia.Create(UploadedImageFile.FileBytes).Resize(960, null).ToByteArray();
        var file = File.Create(FileImageFolder + filename);
        file.Write(data, 0, data.Length);
        file.Close();

        BindImageList();
        ImageList.SelectedValue = filename;
    }

    private bool IsImage(string file)
    {
        return file.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
            file.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
            file.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase);
    }

    protected string UniqueFilename(string filename)
    {
        var newfilename = filename;

        for (var i = 1; File.Exists(FileImageFolder + newfilename); i++)
        {
            newfilename = filename.Insert(filename.LastIndexOf('.'), "(" + i + ")");
        }

        return newfilename;
    }

    protected string UniqueDirectory(string directoryname)
    {
        var newdirectoryname = directoryname;
        for (var i = 1; Directory.Exists(FileImageFolderRoot + newdirectoryname); i++)
            newdirectoryname = directoryname + "(" + i + ")";
        return newdirectoryname;
    }

    protected string ImageFolderRoot
    {
        get { return ResolveUrl(ConfigurationManager.AppSettings["ImageRoot"]); }
    }

    protected string FileImageFolderRoot
    {
        get { return Server.MapPath(ImageFolderRoot); }
    }

    protected string ImageFolder
    {
        get { return ImageFolderRoot + ViewState["folder"]; }
        set { ViewState["folder"] = value; }
    }

    protected string FileImageFolder
    {
        get { return Server.MapPath(ImageFolder); }
    }

    public string UrlRoot
    {
        get
        {
            return (this.Request.Url.Scheme + "://" + Request.Url.Host + ((Request.Url.Port == 80) ? "" : (":" + Request.Url.Port)) + ((Request.ApplicationPath == "/") ? "" : Request.ApplicationPath)) + "/" + ConfigurationManager.AppSettings["ImagesNews"];
        }
    }
}
