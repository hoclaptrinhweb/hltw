using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Resize image
    /// </summary>
    public class ImageResizer
    {
        /// <summary>
        /// Resize by ratio
        /// </summary>
        /// <param name="ImageInputStream">FileUpload.PostedFile.InputStream</param>
        /// <param name="MaxSize">Image size maximum</param>
        /// <param name="ImageInterpolation">Quality Image</param>
        /// <param name="ImageSavePath">Image save path</param>
        /// <returns>True: Successfuly, False: Fail</returns>
        public static bool ResizeFromStream(Stream ImageInputStream, int MaxSize, InterpolationMode ImageInterpolation, string ImageSavePath)
        {
            try
            {
                Image imgInput = Image.FromStream(ImageInputStream);

                //Determine image format (xác định định dạng hình)
                ImageFormat fmtImageFormat = imgInput.RawFormat;

                //Get new size
                Size newSize = GetSizeByRatio(imgInput, MaxSize);

                //resize image
                Bitmap bmpResized = Resize(imgInput, newSize, ImageInterpolation);
                if (bmpResized == null)
                    return false;

                //save bitmap to disk
                bmpResized.Save(ImageSavePath, fmtImageFormat);
                imgInput.Dispose();
                bmpResized.Dispose();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// resize and crop by w+h
        /// </summary>
        /// <param name="ImageInputStream"></param>
        /// <param name="MaxWidthSize"></param>
        /// <param name="MaxHeightSize"></param>
        /// <param name="ImageSavePath">Đường dẫn lưu hình</param>
        /// <returns></returns>
        public static bool ResizeFromStream(Stream ImageInputStream, int MaxWidth, int MaxHeight, InterpolationMode ImageInterpolation, EnumImageResizer.Align ImageAlign, EnumImageResizer.Valign ImageValign, string ImageSavePath)
        {
            try
            {
                Image imgInput = Image.FromStream(ImageInputStream);

                //Determine image format (xác định định dạng hình)
                ImageFormat fmtImageFormat = imgInput.RawFormat;

                //Get new size
                Size newSize = GetSizeByRatio(imgInput, MaxWidth, MaxHeight);

                //resize image
                Bitmap bmpResized = Resize(imgInput, newSize, ImageInterpolation);
                if (bmpResized == null)
                    return false;


                //Crop image 
                int LocationX = 0, LocationY = 0;
                if (MaxWidth < newSize.Width)
                {
                    switch (ImageAlign.GetHashCode().ToString())
                    {
                        case "0"://left
                            LocationX = 0;
                            break;
                        case "1"://center
                            LocationX = Convert.ToInt32((newSize.Width - MaxWidth) / 2);
                            break;
                        case "2"://right
                            LocationX = newSize.Width - MaxWidth;
                            break;
                    }
                }
                else
                {
                    LocationX = 0;
                    MaxWidth = newSize.Width;
                }
                if (MaxHeight < newSize.Height)
                {
                    switch (ImageValign.GetHashCode().ToString())
                    {
                        case "0"://top
                            LocationY = 0;
                            break;
                        case "1"://middle
                            LocationY = Convert.ToInt32((newSize.Height - MaxHeight) / 2);
                            break;
                        case "2"://Bottom
                            LocationY = newSize.Height - MaxHeight;
                            break;
                    }
                }
                else
                {
                    LocationY = 0;
                    MaxHeight = newSize.Height;
                }
                Rectangle RectangleCrop = new Rectangle(LocationX, LocationY, MaxWidth, MaxHeight);
                Bitmap bmpcrop = bmpResized.Clone(RectangleCrop, bmpResized.PixelFormat);
                //save bitmap to disk
                bmpcrop.Save(ImageSavePath, fmtImageFormat);
                imgInput.Dispose();
                bmpResized.Dispose();
                bmpcrop.Dispose();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Resize by ratio
        /// </summary>
        /// <param name="ImageInput">System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));</param>
        /// <param name="MaxSize"></param>
        /// <param name="ImageInterpolation"></param>
        /// <param name="ImageSavePath"></param>
        /// <returns></returns>
        public static bool ResizeFromFile(Image ImageInput, int MaxSize, InterpolationMode ImageInterpolation, string ImageSavePath)
        {
            try
            {
                Image imgInput = ImageInput;

                //Determine image format (xác định định dạng hình)
                ImageFormat fmtImageFormat = imgInput.RawFormat;

                //Get new size
                Size newSize = GetSizeByRatio(imgInput, MaxSize);

                //resize image
                Bitmap bmpResized = Resize(imgInput, newSize, ImageInterpolation);
                if (bmpResized == null)
                    return false;

                //save bitmap to disk
                bmpResized.Save(ImageSavePath, fmtImageFormat);
                imgInput.Dispose();
                bmpResized.Dispose();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// resize and crop by w+h
        /// </summary>
        /// <param name="ImageInput">System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));</param>
        /// <param name="MaxWidth"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="ImageInterpolation"></param>
        /// <param name="ImageAlign"></param>
        /// <param name="ImageValign"></param>
        /// <param name="ImageSavePath"></param>
        /// <returns></returns>
        public static bool ResizeFromFile(Image ImageInput, int MaxWidth, int MaxHeight, InterpolationMode ImageInterpolation, EnumImageResizer.Align ImageAlign, EnumImageResizer.Valign ImageValign, string ImageSavePath)
        {
            try
            {
                Image imgInput = ImageInput;

                //Determine image format (xác định định dạng hình)
                ImageFormat fmtImageFormat = imgInput.RawFormat;

                //Get new size
                Size newSize = GetSizeByRatio(imgInput, MaxWidth, MaxHeight);

                //resize image
                Bitmap bmpResized = Resize(imgInput, newSize, ImageInterpolation);
                if (bmpResized == null)
                    return false;


                //Crop image 
                int LocationX = 0, LocationY = 0;
                if (MaxWidth < newSize.Width)
                {
                    switch (ImageAlign.GetHashCode().ToString())
                    {
                        case "0"://left
                            LocationX = 0;
                            break;
                        case "1"://center
                            LocationX = Convert.ToInt32((newSize.Width - MaxWidth) / 2);
                            break;
                        case "2"://right
                            LocationX = newSize.Width - MaxWidth;
                            break;
                    }
                }
                else
                {
                    LocationX = 0;
                    MaxWidth = newSize.Width;
                }
                if (MaxHeight < newSize.Height)
                {
                    switch (ImageValign.GetHashCode().ToString())
                    {
                        case "0"://top
                            LocationY = 0;
                            break;
                        case "1"://middle
                            LocationY = Convert.ToInt32((newSize.Height - MaxHeight) / 2);
                            break;
                        case "2"://Bottom
                            LocationY = newSize.Height - MaxHeight;
                            break;
                    }
                }
                else
                {
                    LocationY = 0;
                    MaxHeight = newSize.Height;
                }
                Rectangle RectangleCrop = new Rectangle(LocationX, LocationY, MaxWidth, MaxHeight);
                Bitmap bmpcrop = bmpResized.Clone(RectangleCrop, bmpResized.PixelFormat);
                //save bitmap to disk
                bmpcrop.Save(ImageSavePath, fmtImageFormat);
                imgInput.Dispose();
                bmpResized.Dispose();
                bmpcrop.Dispose();
                return true;
            }
            catch { return false; }
        }

        #region Private Method
        /// <summary>
        /// Get image size by ratio
        /// </summary>
        /// <param name="imgInput"></param>
        /// <param name="MaxSize"></param>
        /// <returns></returns>
        private static Size GetSizeByRatio(Image imgInput, int MaxSize)
        {
            Size sizeResult = new Size();
            try
            {
                int intNewWidth;
                int intNewHeight;

                //get image original width and height
                int intOldWidth = imgInput.Width;
                int intOldHeight = imgInput.Height;

                //determine if landscape(ngang) or portrait(dung)
                int intMaxSize;

                if (intOldWidth >= intOldHeight)
                {
                    intMaxSize = intOldWidth;
                }
                else
                {
                    intMaxSize = intOldHeight;
                }

                if (intMaxSize > MaxSize)
                {
                    //set new width and height
                    double dblRatio = MaxSize / (double)intMaxSize;
                    intNewWidth = Convert.ToInt32(dblRatio * intOldWidth);
                    intNewHeight = Convert.ToInt32(dblRatio * intOldHeight);
                }
                else
                {
                    intNewWidth = intOldWidth;
                    intNewHeight = intOldHeight;
                }
                sizeResult = new Size(intNewWidth, intNewHeight);
                return sizeResult;
            }
            catch { return sizeResult; }
        }

        private static Size GetSizeByRatio(Image imgInput, int MaxWidth, int MaxHeight)
        {
            Size sizeResult = new Size();
            try
            {
                int intNewWidth;
                int intNewHeight;

                //get image original width and height
                int intOldWidth = imgInput.Width;
                int intOldHeight = imgInput.Height;

                //determine if landscape(ngang) or portrait(dung)
                double dblWRatio = MaxWidth / (double)intOldWidth;
                double dblHratio = MaxHeight / (double)intOldHeight;
                double dblRatio = Math.Max(dblWRatio, dblHratio);
                dblRatio = dblRatio > 1 ? 1 : dblRatio;

                //set new width and height
                intNewWidth = Convert.ToInt32(dblRatio * intOldWidth);
                intNewHeight = Convert.ToInt32(dblRatio * intOldHeight);

                sizeResult = new Size(intNewWidth, intNewHeight);
                return sizeResult;
            }
            catch { return sizeResult; }
        }

        /// <summary>
        /// Resize from image
        /// </summary>
        /// <param name="ImageInput"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="ImageInterpolation"></param>
        /// <returns></returns>
        private static Bitmap Resize(Image ImageInput, Size ImageSize, InterpolationMode ImageInterpolation)
        {
            try
            {
                //create new bitmap
                Bitmap bmpResized = new Bitmap(ImageSize.Width, ImageSize.Height);
                Graphics grpResized = Graphics.FromImage((Image)bmpResized);
                grpResized.InterpolationMode = ImageInterpolation;
                grpResized.DrawImage(ImageInput, 0, 0, ImageSize.Width, ImageSize.Height);
                grpResized.Dispose();
                return bmpResized;
            }
            catch { return null; }
        }
        #endregion
    }
    public class EnumImageResizer
    {
        public enum Align
        {
            Left = 0,
            Center = 1,
            Right = 2,
        }
        public enum Valign
        {
            Top = 0,
            Middle = 1,
            Bottom = 2,
        }
    }


}

/*
 * goi o webpage
 *  a.ResizeFromStream("C:\\_" + FileUpload1.PostedFile.FileName, 200, FileUpload1.PostedFile.InputStream);
   yeu cau:
   * theo ty le
   * input(string filepathinput, maxsize,imagesavepath)-->true,false
   * input(Stream Buffer, maxsize,imagesavepath)-->true,false
     
   theo kich thuoc fix
   * input(string filepathinput, maxwidthsize, maxheightsize,imagesavepath)-->true,false
   * input(Stream Buffer, maxwidthsize, maxheightsize,imagesavepath)-->true,false
   */
/*
  public void ResizeFromStream(string ImageSavePath, int MaxSideSize, Stream Buffer)
    {
        int intNewWidth;
        int intNewHeight;
        Image imgInput = Image.FromStream(Buffer);
        
        //Determine image format
        ImageFormat fmtImageFormat = imgInput.RawFormat;

        //get image original width and height
        int intOldWidth = imgInput.Width;
        int intOldHeight = imgInput.Height;

        //determine if landscape or portrait
        int intMaxSide;

        if (intOldWidth >= intOldHeight)
        {
            intMaxSide = intOldWidth;
        }
        else
        {
            intMaxSide = intOldHeight;
        }


        if (intMaxSide > MaxSideSize)
        {
            //set new width and height
            double dblCoef = MaxSideSize / (double)intMaxSide;
            intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
            intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
        }
        else
        {
            intNewWidth = intOldWidth;
            intNewHeight = intOldHeight;
        }
        //Rectangle crop = new Rectangle(50, 50, intNewWidth - 50, intNewHeight - 50);

        //create new bitmap
        Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);
        
        //Bitmap bmpcrop = bmpResized.Clone(crop, bmpResized.PixelFormat);
        //save bitmap to disk
        bmpResized.Save(ImageSavePath, fmtImageFormat);
        //bmpcrop.Save("c:\\crop.jpg",fmtImageFormat);
        //release used resources
        imgInput.Dispose();
        bmpResized.Dispose();
        //Buffer.Close();
    }

    public Image cropImage(Image img, Rectangle cropArea)
    {
        Bitmap bmpImage = new Bitmap(img);
        Bitmap bmpCrop = bmpImage.Clone(cropArea,
        bmpImage.PixelFormat);
        return (Image)(bmpCrop);
    }

  

    public void Resize2(string ImageSavePath, int MaxSideSize, Stream Buffer)
    {
        Image imgInput = Image.FromStream(Buffer);
        //Determine image format
        ImageFormat fmtImageFormat = imgInput.RawFormat;
        //create new bitmap
        Size size = new Size(200, 160);
        Bitmap bmpResized =  resizeImage(imgInput, size);
        //save bitmap to disk
        bmpResized.Save(ImageSavePath, fmtImageFormat);
     
        //release used resources
        imgInput.Dispose();
        bmpResized.Dispose();
        //Buffer.Close();
    }
    private Bitmap resizeImage(Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;

        double nPercent = 0;
        double nPercentW = 0;
        double nPercentH = 0;

        nPercentW = ((double)size.Width / (double)sourceWidth);
        nPercentH = ((double)size.Height / (double)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return b;
    }
 * 
 * <%@ Page Language="C#" %>
<%@ import Namespace="System.Drawing.Imaging" %>
<script runat="server">
//Pics folder has to be created under the current folder
void UploadBtn_Click(Object sender, EventArgs e)
{      
    String UploadedFile = MyFile.PostedFile.FileName;
    int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
  
    //to retrieve only Filename from the complete path
    String UploadedFileName = UploadedFile.Substring(ExtractPos,UploadedFile.Length - ExtractPos);
       
    // Display information about posted file. Div is invisible by default   
    FileName.InnerHtml = UploadedFileName;
    
    MyContentType.InnerHtml = MyFile.PostedFile.ContentType;
    
    ContentLength.InnerHtml = MyFile.PostedFile.ContentLength.ToString();
    
    FileDetails.Visible = true; //div is made visible
 
    // Save uploaded file to server at the in the Pics folder
    MyFile.PostedFile.SaveAs(Request.PhysicalApplicationPath 
                            + "pics\\" + UploadedFileName );

   //thumbnail creation starts
   try
   {
      //Retrieve the image filename whose thumbnail has to be created
      String imageUrl =  UploadedFileName;
      //Read in the width and height
      int imageHeight =Convert.ToInt32(h.Text);
      int imageWidth  = Convert.ToInt32(w.Text); 
  
      //You may even specify a standard thumbnail size
      //int imageWidth  = 70; 
      //int imageHeight = 70;
  
      if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0 )
      {
         //We found a / or \
         Response.End();
      }
  
      //the uploaded image will be stored in the Pics folder.
      //to get resize the image, the original image has to be
      //accessed from the Pics folder
      imageUrl = "pics/" + imageUrl;
  
      System.Drawing.Image fullSizeImg 
              = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));
      System.Drawing.Image.GetThumbnailImageAbort dummyCallBack 
              = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
      System.Drawing.Image thumbNailImg 
              = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, 
                                              dummyCallBack, IntPtr.Zero);
   
      //We need to create a unique filename for each generated image
      DateTime MyDate = DateTime.Now;
  
      String MyString  = MyDate.ToString("ddMMyyhhmmss") + ".png" ;
 
      //Save the thumbnail in PNG format. 
      //You may change it to a diff format with the ImageFormat property
      thumbNailImg.Save ( Request.PhysicalApplicationPath 
                          + "pics\\" +   MyString , ImageFormat.Png);
      thumbNailImg.Dispose();
   
      //Display the original & the newly generated thumbnail
   
      Image1.AlternateText = "Original image";  
      Image1.ImageUrl="pics\\" + UploadedFileName;
      Image2.AlternateText = "Thumbnail";
      Image2.ImageUrl="pics\\" + MyString;
   }
   catch(Exception ex)
   {
      Response.Write("An error occurred - " + ex.ToString());
   }
}
  
//this function is reqd for thumbnail creation
public bool ThumbnailCallback()
{
   return false;
}
</script>
 */