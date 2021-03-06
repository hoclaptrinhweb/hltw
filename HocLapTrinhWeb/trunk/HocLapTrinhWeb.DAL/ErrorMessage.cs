namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message;
        /// <summary>
        /// 
        /// </summary>
        public string MsgCode;
        /// <summary>
        /// 
        /// </summary>
        public int MsgNumber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pMsgCode"></param>
        /// <param name="pMessage"></param>
        /// <param name="pMsgNumber"></param>
        public ErrorMessage(string pMsgCode, string pMessage, int pMsgNumber)
        {
            MsgCode = pMsgCode;
            Message = pMessage;
            MsgNumber = pMsgNumber;
        }
    }
}
