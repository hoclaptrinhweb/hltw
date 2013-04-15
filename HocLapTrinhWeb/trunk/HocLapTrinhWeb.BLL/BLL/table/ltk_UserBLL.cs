using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class ltk_UserBLL : UserBLL
    {
        #region Variable
        ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ltk_UserBLL(Connection conn)
            : base(conn)
        {

        }

        /// <summary>
        /// Load user len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable GetAllUserForGridView(int startRowIndex, int maximumRows, int IsActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows,
                            OrderByClause = dt.UserIDColumn.ColumnName + " desc"
                        };
                    if (IsActive != -1)
                    {
                        _classBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }


        public vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable GetAllUserForGridView()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { OrderByClause = dt.UserIDColumn.ColumnName + " desc" };
                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Lấy lên số dòng dữ liệu
        /// </summary>
        /// <returns></returns>
        public int GetAllUserRowCount(int IsActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (IsActive != -1)
                    {
                        _classBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    var rowcount = _classBaseDAL.GetRowCount();
                    if (rowcount == -1)
                        AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return rowcount;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return -1;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return -1;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        public int GetAllUserRowCount(string strDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (strDate != "")
                    {
                        _classBaseDAL.WhereClause = "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) = cast(@strDate AS DATETIME)";
                        _classBaseDAL.ClearParams();
                        _classBaseDAL.AddParams("@strDate", SqlDbType.NVarChar, strDate, ParameterDirection.Input);
                    }
                    var rowcount = _classBaseDAL.GetRowCount();
                    if (rowcount == -1)
                        AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return rowcount;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return -1;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return -1;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Lấy lên chi tiết 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.ltk_vw_UserRow GetUserByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.UserIDColumn.ColumnName + "=@id" };
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, id, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-000001", "Kết nối bị lỗi." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Lấy lên tài khoản cho việc login
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.ltk_vw_UserRow GetUserByName(string userName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.UserNameColumn.ColumnName + "=@1" };
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@1", SqlDbType.VarChar, userName, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-LOG002", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-LOG001", "Kết nối bị lỗi." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.ltk_vw_UserRow GetUserByEmail(string email)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.EmailColumn.ColumnName + "=@1"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@1", SqlDbType.VarChar, email, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-LOG002", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-LOG001", "Kết nối bị lỗi." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }


        /// <summary>
        /// Lấy lên tài khoản cho việc login, kiem tra user active
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.ltk_vw_UserRow GetUserByName(string userName, bool isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserNameColumn.ColumnName + "=@1"};
                    _classBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@2";
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@1", SqlDbType.VarChar, userName, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@2", SqlDbType.Bit, isActive, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-LOG002", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-LOG001", "Kết nối bị lỗi." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
