using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_UpUserBLL : UpUserBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_UpUserBLL(Connection conn)
            : base(conn)
        {

        }

        #region Admin

        /// <summary>
        /// Load user len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable GetAllUserForGridView(int startRowIndex, int maximumRows)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows,
                            OrderByClause = dt.UserIDColumn.ColumnName + " desc"
                        };
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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


        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable GetAllUserForGridView()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {OrderByClause = dt.UserIDColumn.ColumnName + " desc"};
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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
        public int GetAllUserRowCount()
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    var rowcount = _ClassBaseDAL.GetRowCount();
                    if (rowcount == -1)
                    {
                        AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    }
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
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserRow GetUserByID(int id)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@id", SqlDbType.Int, id, ParameterDirection.Input);
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserRow GetUserByName(string userName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserNameColumn.ColumnName + "=@1"};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@1", SqlDbType.VarChar, userName, ParameterDirection.Input);
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-LOG002", "Du lieu khong ton tai." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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

   
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable GetUserSearchName(string name, int top)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {Top = top, WhereClause = dt.UserNameColumn.ColumnName + " like N'%" + name + "%'"};
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        return dt;
                    }
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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

        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
