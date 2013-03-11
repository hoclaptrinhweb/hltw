using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

namespace AspNetResources.Web
{
    /// <summary>
    /// PageFilter does all the dirty work of tinkering the 
    /// outgoing HTML stream. This is a good place to add keywords and
    /// enforce some compilancy with web standards.
    /// </summary>
    public class PageFilter : Stream
    {
        Stream responseStream;
        StringBuilder responseHtml;

        public PageFilter(Stream inputStream)
        {
            responseStream = inputStream;
            responseHtml = new StringBuilder();
        }

        #region Filter overrides
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override void Close()
        {
            responseStream.Close();
        }

        public override void Flush()
        {
            responseStream.Flush();
        }

        public override long Length
        {
            get
            {
                return 0;
            }
        }

        public override long Position { get; set; }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }
        #endregion

        #region Dirty work
        public override void Write(byte[] buffer, int offset, int count)
        {
            string strBuffer = Encoding.UTF8.GetString(buffer, offset, count);

            // ---------------------------------
            // Wait for the closing </html> tag
            // ---------------------------------
            var eof = new Regex("</html>", RegexOptions.IgnoreCase);

            if (!eof.IsMatch(strBuffer))
            {
                responseHtml.Append(strBuffer);
            }
            else
            {
                responseHtml.Append(strBuffer);

                var finalHtml = responseHtml.ToString();


                // The title has an id="..." which we need to get rid of
                //re = new Regex("<title(\\s+id=.+?)>", RegexOptions.IgnoreCase);
                //finalHtml = re.Replace(finalHtml, new MatchEvaluator(TitleMatch));

                // Replace language="javascript" with script type="text/javascript"
                var re = new Regex("(?<=script\\s*)(language=\"javascript\")", RegexOptions.IgnoreCase);
                finalHtml = re.Replace(finalHtml, JavaScriptMatch);

                // Replace language="javascript" with script type="text/javascript"
                re = new Regex("(?<=meta\\s*)(name=\"description\")", RegexOptions.IgnoreCase);
                finalHtml = re.Replace(finalHtml, MetaMatch);


                // If there are still any language="javascript" are left, delete them
                finalHtml = Regex.Replace(finalHtml, "language=\"javascript\"", string.Empty, RegexOptions.IgnoreCase);

                //// Clean up images. Some images have a border property which is deprecated in XHTML
                //re = new Regex ("<img.*(border=\".*?\").*?>", RegexOptions.IgnoreCase);
                //finalHtml = re.Replace (finalHtml, new MatchEvaluator (ImageBorderMatch));

                // Wrap the __VIEWSTATE tag in a div to pass validation
                re = new Regex("(<input.*?__VIEWSTATE.*?/>)", RegexOptions.IgnoreCase);
                finalHtml = re.Replace(finalHtml, CommentMatch);

                re = new Regex("(<!--.*?-->)", RegexOptions.IgnoreCase);
                finalHtml = re.Replace(finalHtml, CommentMatch);
                finalHtml = Regex.Replace(finalHtml, "^\\s*", string.Empty, RegexOptions.Compiled |
                                  RegexOptions.Multiline);

                //Hiện tại bị lỗi khi có pre
                if (!finalHtml.Contains("<pre"))
                {
                    finalHtml = Regex.Replace(finalHtml, "\\r\\n", string.Empty, RegexOptions.Compiled |
                                      RegexOptions.Multiline);
                }
                // Write the formatted HTML back
                var data = Encoding.UTF8.GetBytes(finalHtml);

                responseStream.Write(data, 0, data.Length);
            }

        }


        private static string MetaMatch(Match m)
        {
            return m.ToString().Replace(m.Groups[1].Value, "type=\"text/javascript\"");
        }

        private static string JavaScriptMatch(Match m)
        {
            return m.ToString().Replace(m.Groups[1].Value, "type=\"text/javascript\"");
        }

        private static string CommentMatch(Match m)
        {
            return string.Concat("", "", "");
        }

        #endregion

    }
}
