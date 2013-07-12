
using System;

namespace HocLapTrinhWeb.Utilities.Text
{
    /// <summary>
    /// Lớp thao tác với PDF files
    /// </summary>
    public class PDF
    {
        #region Watermark image
        /// <summary>
        /// Đóng dấu file pdf
        /// </summary>
        /// <param name="sourcePathFile">Đường dẫn file nguồn</param>
        /// <param name="outputPathFile">Đường dẫn lưu file</param>
        /// <param name="watermarkImagePathFile">Đường dẫn hình đóng dấu</param>
        /// <param name="isBackground">Đóng dấu dạng background hoặc stamp</param>
        /// <param name="WatermarkAlign">Vị trí X</param>
        /// <param name="WatermarkValign">Vị trí Y</param>
        public static bool AddWatermarkImage(string sourcePathFile, string outputPathFile, string watermarkImagePathFile, bool isBackground, EnumWatermarkPDF.Align WatermarkAlign, EnumWatermarkPDF.Valign WatermarkValign)
        {
            iTextSharp.text.pdf.PdfReader reader = null;
            iTextSharp.text.pdf.PdfStamper stamper = null;
            iTextSharp.text.Image img = null;
            iTextSharp.text.pdf.PdfContentByte underContent = null;
            iTextSharp.text.Rectangle rect = null;
            float X = 0, Y = 0;
            int pageCount = 0;
            bool isScaleImage = false;
            try
            {
                if (System.IO.File.Exists(sourcePathFile))
                {
                    reader = new iTextSharp.text.pdf.PdfReader(sourcePathFile);
                    rect = reader.GetPageSizeWithRotation(1);
                    stamper = new iTextSharp.text.pdf.PdfStamper(reader, new System.IO.FileStream(outputPathFile, System.IO.FileMode.Create));
                    img = iTextSharp.text.Image.GetInstance(watermarkImagePathFile);
                    if (img.Width > rect.Width || img.Height > rect.Height)
                    {
                        img.ScaleToFit(rect.Width, rect.Height);
                        isScaleImage = true;
                    }
                    switch (WatermarkAlign.GetHashCode().ToString())
                    {
                        case "0"://left
                            X = (float)WatermarkAlign.GetHashCode();
                            break;
                        case "1"://center
                            X = (float)(isScaleImage ? ((rect.Width - img.ScaledWidth) / 2) : ((rect.Width - img.Width) / 2));
                            break;
                        case "2"://right
                            X = (float)(isScaleImage ? (rect.Width - img.ScaledWidth) : (rect.Width - img.Width));
                            break;
                    }

                    switch (WatermarkValign.GetHashCode().ToString())
                    {
                        case "0"://Bottom
                            Y = (float)WatermarkValign.GetHashCode();
                            break;
                        case "1"://middle
                            Y = (float)(isScaleImage ? ((rect.Height - img.ScaledHeight) / 2) : ((rect.Height - img.Height) / 2));
                            break;
                        case "2"://Top
                            Y = (float)(isScaleImage ? (rect.Height - img.ScaledHeight) : (rect.Height - img.Height));
                            break;
                    }

                    img.SetAbsolutePosition(X, Y);
                    pageCount = reader.NumberOfPages;
                    for (int i = 1; i <= pageCount; i++)
                    {
                        underContent = isBackground ? stamper.GetUnderContent(i) : stamper.GetOverContent(i);
                        underContent.AddImage(img);
                    }
                    stamper.Close();
                    reader.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        #endregion
    }

    /// <summary>
    /// Enum Watermark Pdf
    /// </summary>
    public class EnumWatermarkPDF
    {
        public enum Align
        {
            Left = 0,
            Center = 1,
            Right = 2,
        }
        public enum Valign
        {
            Top = 2,
            Middle = 1,
            Bottom = 0,
        }
    }
}

