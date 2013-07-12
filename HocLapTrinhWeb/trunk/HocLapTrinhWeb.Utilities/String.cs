namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Lớp mở rộng cho hàm chuỗi
    /// </summary>
    public class StringExtension : HocLapTrinhWeb.Utilities.Cryptography
    {
        /// <summary>
        /// Hàm cắt chuỗi
        /// </summary>
        /// <param name="inputString">Chuỗi cần cắt</param>
        /// <param name="length">Độ dài</param>
        /// <returns></returns>
        public static string SubString(string inputString, int length)
        {
            try
            {
                //Nếu chuỗi nhỏ hơn chiều dài cần cắt thì return về chính chuỗi truyền vào
                if (inputString.Length <= length)
                    return inputString;

                string strResult = string.Empty;
                int iEmptyIndex = 0;

                //Nếu chỉ có 1 từ --> ko cho cắt (vô nghĩa)
                iEmptyIndex = inputString.IndexOf(" ");
                if (iEmptyIndex != -1)
                {
                    //Cắt chuỗi yêu cầu
                    strResult = inputString.Substring(0, length);

                    //Tìm index của khoảng trắng kế bên chuổi cắt. Nếu là khoảng trắng --> return kết quả là strSplited.
                    //Ngược lại: cắt chuổi ký tự thừa của strSplited
                    iEmptyIndex = inputString.IndexOf(" ", length);
                    if (iEmptyIndex == length)
                        return strResult;
                    else
                    {
                        //strResult: Cộng hò --> Cộng
                        iEmptyIndex = strResult.LastIndexOf(" ");
                        if (iEmptyIndex != -1)
                        {
                            strResult = strResult.Substring(0, iEmptyIndex);
                        }
                    }
                }
                return strResult;
            }
            catch { return null; }
        }
    }
}

