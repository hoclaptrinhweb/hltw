using System;
using System.Text;
using System.Security.Cryptography;

namespace HocLapTrinhWeb.Utilities
{
    /// <summary>
    /// Mã hóa
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Mã hóa chuỗi theo chuẩn md5 (Không có ký tự đặc biệt)
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptMD5(string phrase)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider();
                byte[] hashedDataBytes = md5hasher.ComputeHash(encoder.GetBytes(phrase));
                return byteArrayToString(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Mã hóa chuỗi theo chuẩn md5 (có ký tự đặc biệt +.=?&)
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptMD5_Ext(string phrase)
        {
            try
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedDataBytes = md5Hasher.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(phrase));
                return Convert.ToBase64String(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Mã hóa chuỗi theo chuẩn Sha1
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptSHA1(string phrase)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
                byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(phrase));
                return byteArrayToString(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Mã hóa chuỗi theo chuẩn SHA256
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptSHA256(string phrase)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                SHA256Managed sha256hasher = new SHA256Managed();
                byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
                return byteArrayToString(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Mã hóa chuỗi theo chuẩn SHA384
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptSHA384(string phrase)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                SHA384Managed sha384hasher = new SHA384Managed();
                byte[] hashedDataBytes = sha384hasher.ComputeHash(encoder.GetBytes(phrase));
                return byteArrayToString(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Mã hóa chuỗi theo chuẩn sha512
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string EncryptSHA512(string phrase)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                SHA512Managed sha512hasher = new SHA512Managed();
                byte[] hashedDataBytes = sha512hasher.ComputeHash(encoder.GetBytes(phrase));
                return byteArrayToString(hashedDataBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputArray"></param>
        /// <returns></returns>
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

    }

    ///// <summary>
    ///// Utility for encrypting and decrypting data.
    ///// </summary>
    //public static class Crypto
    //{
    //    #region Fields
    //    private static bool _Initialized = false;
    //    private static MethodInfo _EncDecMethod = null;
    //    #endregion

    //    #region Methods
    //    /// <summary>
    //    /// Loads methods for encryption using reflection.
    //    /// </summary>
    //    /// <remarks>
    //    /// This method is executed only once, thus reducing
    //    /// the load of using reflection to a minimum.
    //    /// </remarks>
    //    public static void Initialize()
    //    {
    //        // Exit if methods are already initialized
    //        if (_Initialized) return;

    //        try
    //        {
    //            // This class is made public but the methods
    //            // we requiere are made private or internal.
    //            Type machineKeySection = typeof(System.Web.Configuration.MachineKeySection);

    //            // Get encryption / decryption method
    //            // You can look up all the method using Reflector
    //            // or a simillar tool.
    //            _EncDecMethod = machineKeySection.GetMethod("EncryptOrDecryptData",
    //                BindingFlags.NonPublic | BindingFlags.Static, Type.DefaultBinder,
    //                new[] { typeof(bool), typeof(byte[]), typeof(byte[]), typeof(int), typeof(int) },
    //                null);

    //            // Set the init flag to true
    //            _Initialized = true;
    //        }
    //        catch
    //        {
    //            throw new Exception("Failed to initialized base encryption method.");
    //        }
    //    }

    //    /// <summary>
    //    /// Encrypts a string.
    //    /// </summary>
    //    /// <param name="data">Data to be encrypted.</param>
    //    /// <returns>Encrypted string.</returns>
    //    public static string Encrypt(string data)
    //    {
    //        // Make sure encryption method is loaded
    //        Initialize();

    //        // Try to encrypt data
    //        try
    //        {
    //            // Convert strig to byte array
    //            byte[] bytes = Encoding.UTF8.GetBytes(data);

    //            // Invoke the encryption method.
    //            // The method is basicaly the same for both
    //            // encryption and decription. You just have to
    //            // specify the the correct fEncrypt parameter
    //            // value.
    //            byte[] encData = (byte[])_EncDecMethod.Invoke(null, new object[] { true, bytes, null, 0, bytes.Length });

    //            // Return a re-usable string
    //            return Convert.ToBase64String(encData);
    //        }
    //        catch
    //        {
    //            // Data was not valid, encryption failed.
    //            throw new Exception("Failed to encrypt data.");
    //        }
    //    }

    //    /// <summary>
    //    /// Decrypts a string.
    //    /// </summary>
    //    /// <param name="data">Data to be decrypted.</param>
    //    /// <returns>Decrypted string.</returns>
    //    public static string Decrypt(string data)
    //    {
    //        // Make sure decryption method is loaded
    //        Initialize();

    //        // Try to decrypt data
    //        try
    //        {
    //            // Convert strig to byte array
    //            byte[] bytes = Convert.FromBase64String(data);

    //            // Invoke the decryption method.
    //            // The method is basicaly the same for both
    //            // encryption and decription. You just have to
    //            // specify the the correct fEncrypt parameter
    //            // value.
    //            byte[] decData = (byte[])_EncDecMethod.Invoke(null, new object[] { false, bytes, null, 0, bytes.Length });

    //            // Return a re-usable string
    //            return Encoding.UTF8.GetString(decData);
    //        }
    //        catch
    //        {
    //            // Data was not valid, encryption failed.
    //            throw new Exception("Failed to encrypt data.");
    //        }
    //    }
    //    #endregion
    //}



    public class SymmCrypto
    {
        /// <remarks>

        /// Supported .Net intrinsic SymmetricAlgorithm classes.

        /// </remarks>

        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael
        }

        private SymmetricAlgorithm mobjCryptoService;

        /// <remarks>

        /// Constructor for using an intrinsic .Net SymmetricAlgorithm class.

        /// </remarks>

        public SymmCrypto(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmProvEnum.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmProvEnum.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;
            }
        }

        /// <remarks>

        /// Constructor for using a customized SymmetricAlgorithm class.

        /// </remarks>

        public SymmCrypto(SymmetricAlgorithm ServiceProvider)
        {
            mobjCryptoService = ServiceProvider;
        }

        /// <remarks>

        /// Depending on the legal key size limitations of a specific CryptoService provider

        /// and length of the private key provided, padding the secret key with space character

        /// to meet the legal size of the algorithm.

        /// </remarks>

        private byte[] GetLegalKey(string Key)
        {
            string sTemp;
            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits

                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;

            // convert the secret key to byte array

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        public string Encrypting(string Source, string Key)
        {
            try
            {
                byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
                // create a MemoryStream so that the process can be done without I/O files

                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                byte[] bytKey = GetLegalKey(Key);

                // set the private key

                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;

                // create an Encryptor from the Provider Service instance

                ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

                // create Crypto Stream that transforms a stream using the encryption

                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                // write out encrypted content into MemoryStream

                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();

                // get the output and trim the '\0' bytes

                byte[] bytOut = ms.GetBuffer();
                int i = 0;
                for (i = 0; i < bytOut.Length; i++)
                    if (bytOut[i] == 0)
                        break;

                // convert into Base64 so that the result can be used in xml

                return System.Convert.ToBase64String(bytOut, 0, i);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public string Decrypting(string Source, string Key)
        {
            try
            {
                // convert from Base64 to binary

                byte[] bytIn = System.Convert.FromBase64String(Source);
                // create a MemoryStream with the input

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

                byte[] bytKey = GetLegalKey(Key);

                // set the private key

                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;

                // create a Decryptor from the Provider Service instance

                ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

                // create Crypto Stream that transforms a stream using the decryption

                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

                // read out the result from the Crypto Stream

                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
    }

}
