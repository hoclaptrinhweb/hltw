using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class Connection : Message
    {
        private bool _isUseTrans;
        private SqlConnection _sqlConnection;

        /// <summary>
        /// 
        /// </summary>
        public int OpenedNo = 0;

        private SqlTransaction _sqlTrans;
        /// <summary>
        /// 
        /// </summary>
        public int TransNo = 0;

        /// <summary>
        /// Mở connect
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                if (_sqlConnection == null)
                {
                    AddMessage("ERR-000000", "Connection is null.", 0);
                    return false;
                }
                if (_sqlConnection.State != ConnectionState.Open)
                {
                    _sqlConnection.Open();
                }
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Tạo 1 connect mới
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="key">Thông tin database</param>
        /// <param name="validKey">Mã hợp lệ</param>
        /// <returns></returns>
        public bool CreateConnection(string connectionString,string key,string validKey)
        {
            try
            {
                if (connectionString.IndexOf(key, StringComparison.Ordinal) == -1)
                    return false;
                var md5Hasher = new MD5CryptoServiceProvider();
                var hashedDataBytes = md5Hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HocLapTrinhWeb.DAL" + key));
                var connectKey = Convert.ToBase64String(hashedDataBytes);
                hashedDataBytes = md5Hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(connectKey));
                connectKey = Convert.ToBase64String(hashedDataBytes);
                return connectKey.Equals(validKey) && CreateConnection(connectionString);
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        private bool CreateConnection(string connectionString)
        {
            Exception exception2;
            bool flag;
            _sqlConnection = new SqlConnection {ConnectionString = connectionString};
            try
            {
                MsgMessage = null;
                _sqlConnection.Open();
                flag = true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                try
                {
                    SqlConnection.ClearPool(_sqlConnection);
                }
                catch (Exception exception3)
                {
                    exception2 = exception3;
                    AddMessage("ERR-000000", exception2.Message, 0);
                }
                flag = false;
            }
            catch (Exception exception4)
            {
                exception2 = exception4;
                AddMessage("ERR-000000", exception2.Message, 0);
                flag = false;
            }
            finally
            {
                if (_sqlConnection != null)
                    _sqlConnection.Close();
            }
            return flag;
        }

        /// <summary>
        /// Lấy lên connect hiện tại
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnected()
        {
            return _sqlConnection;
        }

        /// <summary>
        /// Đóng kết nối
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                _sqlConnection.Close();
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Lấy lên trạng thái connect
        /// </summary>
        public ConnectionState State
        {
            get
            {
                return ((_sqlConnection == null) ? ConnectionState.Closed : _sqlConnection.State);
            }
        }

        /// <summary>
        /// Khởi tạo Transaction
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            try
            {
                _sqlTrans = _sqlConnection.BeginTransaction();
                _isUseTrans = true;
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Commit 1 transaction
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            try
            {
                _sqlTrans.Commit();
                _isUseTrans = false;
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Lấy lên transaction hiện tại
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTransaction()
        {
            return _sqlTrans;
        }

        /// <summary>
        /// Đánh dấu nơi cần rollback
        /// </summary>
        /// <param name="savepointName"></param>
        /// <returns></returns>
        public bool SavePoint(string savepointName)
        {
            try
            {
                _sqlTrans.Save(savepointName);
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsTrans()
        {
            return _isUseTrans;
        }

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// <returns></returns>
        public bool Rollback()
        {
            try
            {
                _sqlTrans.Rollback();
                _isUseTrans = false;
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Rollback tại Savepoint
        /// </summary>
        /// <param name="savepointName"></param>
        /// <returns></returns>
        public bool Rollback(string savepointName)
        {
            try
            {
                _sqlTrans.Rollback(savepointName);
                _isUseTrans = false;
                return true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
        }

    }
}
