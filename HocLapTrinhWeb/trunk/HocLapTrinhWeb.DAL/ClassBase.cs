using System.Data;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class ClassBase : Message
    {
        /// <summary>
        /// 
        /// </summary>
        public Connection IConnect = new Connection();

        /// <summary>
        /// Khởi tạo Transaction
        /// </summary>
        /// <returns></returns>
        protected bool BeginTransaction()
        {
            if (!IConnect.IsTrans())
            {
                if (!IConnect.BeginTransaction())
                {
                    SetMessage(IConnect);
                    return false;
                }
                IConnect.TransNo = 1;
            }
            else
            {
                IConnect.TransNo++;
            }
            return true;
        }

        /// <summary>
        /// Khởi tạo Transaction
        /// </summary>
        /// <param name="existsTrans"></param>
        /// <returns></returns>
        protected bool BeginTransaction(ref bool existsTrans)
        {
            existsTrans = IConnect.IsTrans();
            if (!existsTrans && !IConnect.BeginTransaction())
            {
                SetMessage(IConnect);
                return false;
            }
            return true;
        }

        protected bool SavePoint(string savepointName)
        {
            if (!IConnect.IsTrans())
                return false;
            if (!IConnect.SavePoint(savepointName))
            {
                SetMessage(IConnect);
                return false;
            }
            return true;
        }

        protected void CloseConnection()
        {
            if (IConnect.OpenedNo == 1)
            {
                if (IConnect.Close())
                    IConnect.OpenedNo = 0;
            }
            else
                IConnect.OpenedNo--;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opened">false: sẽ close connect. True: ko close connect</param>
        protected void CloseConnection(bool opened)
        {
            if (opened) return;
            IConnect.Close();
        }

        protected bool Commit()
        {
            if (IConnect.TransNo == 1)
            {
                if (!IConnect.Commit())
                {
                    SetMessage(IConnect);
                    return false;
                }
                IConnect.TransNo = 0;
            }
            else
                IConnect.TransNo--;
            return true;
        }

        protected bool Commit(bool existsTrans)
        {
            if (!existsTrans && !IConnect.Commit())
            {
                SetMessage(IConnect);
                return false;
            }
            return true;
        }

        protected bool OpenConnection()
        {
            if (IConnect.State != ConnectionState.Open)
            {
                if (!IConnect.Open())
                {
                    SetMessage(IConnect);
                    return false;
                }
                IConnect.OpenedNo = 1;
                return true;
            }
            IConnect.OpenedNo++;
            return true;
        }

        /// <summary>
        /// opened=true: Connect đã mở trước đó.
        /// opened=false: Connect được mở
        /// </summary>
        /// <param name="opened"></param>
        /// <returns></returns>
        protected bool OpenConnection(ref bool opened)
        {
            opened = IConnect.State == ConnectionState.Open;
            if (!opened)
            {
                if (!IConnect.Open())
                {
                    SetMessage(IConnect);
                    return false;
                }
                return true;
            }
            return true;
        }

        protected bool Rollback()
        {
            if (IConnect.TransNo == 1)
            {
                if (!IConnect.Rollback())
                {
                    SetMessage(IConnect);
                    return false;
                }
                IConnect.TransNo = 0;
            }
            else
                IConnect.TransNo--;
            return true;
        }

        protected bool Rollback(bool existsTrans)
        {
            if (!existsTrans && !IConnect.Rollback())
            {
                SetMessage(IConnect);
                return false;
            }
            return true;
        }

        protected bool Rollback(bool existsTrans, string savepointName)
        {
            if (!existsTrans && !IConnect.Rollback(savepointName))
            {
                SetMessage(IConnect);
                return false;
            }
            return true;
        }
    }
}
