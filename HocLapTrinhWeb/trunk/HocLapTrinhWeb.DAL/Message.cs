using System.Collections;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class Message
    {
        protected string MsgMessage;
        protected string MsgCode;
        protected int MsgNumber;
        protected ArrayList ListMessage = new ArrayList();

        /// <summary>
        /// Thêm MsgMessage
        /// </summary>
        /// <param name="pMsgCode">Mã: 10 ký tự</param>
        /// <param name="pMessage">Thông báo</param>
        /// <param name="pMsgNumber"></param>
        public void AddMessage(string pMsgCode, string pMessage, int pMsgNumber)
        {
            MsgCode = pMsgCode;
            MsgMessage = pMessage;
            MsgNumber = pMsgNumber;
            var mess = new ErrorMessage(pMsgCode, pMessage, pMsgNumber);
            ListMessage.Add(mess);
        }

        /// <summary>
        /// Gán lại Messeage từ 1 class khác được triệu gọi
        /// </summary>
        /// <param name="msg"></param>
        public void SetMessage(Message msg)
        {
            if (msg == null) return;
            ListMessage = msg.ListMessage;
            MsgMessage = msg.getMessage();
            MsgNumber = msg.getMsgNumber();
            MsgCode = msg.getMsgCode();
        }

        /// <summary>
        /// Xóa MsgMessage
        /// </summary>
        public void Clear()
        {
            MsgCode = null;
            MsgMessage = null;
            MsgNumber = 0;
        }

        /// <summary>
        /// Xóa toàn bộ MsgMessage và ListMessage
        /// </summary>
        public void ClearAll()
        {
            MsgCode = null;
            MsgMessage = null;
            MsgNumber = 0;
            ListMessage.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getMessage()
        {
            return MsgMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getMsgCode()
        {
            return MsgCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getMsgNumber()
        {
            return MsgNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayList getListMessage()
        {
            return ListMessage;
        }
    }
}
