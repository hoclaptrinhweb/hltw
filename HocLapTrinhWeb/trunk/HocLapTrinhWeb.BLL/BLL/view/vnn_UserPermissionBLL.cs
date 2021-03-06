using System;
using System.Data;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_UserPermissionBLL : UserPermissionBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cnn"></param>
        public vnn_UserPermissionBLL(Connection conn)
            : base(conn)
        {
            IConnect = conn;
        }

        /// <summary>
        /// Load UserPermission len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable GetAllUserPermissionForGridView(int startRowIndex, int maximumRows)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows,
                            OrderByClause = dt.UserIDColumn.ColumnName + " desc"
                        };
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return null;
                }
                this.AddMessage("ERR-000001", "Connection failed." + this.getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        public vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable GetAllUserPermissionForGridView(int startRowIndex, int maximumRows, int UserID, string RoleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows
                        };
                    if (UserID != -1 )
                    {
                        _ClassBaseDAL.WhereClause = dt.UserIDColumn.ColumnName + "=@UserID";
                        _ClassBaseDAL.AddParams("@UserID", SqlDbType.Int, UserID, ParameterDirection.Input);
                    }
                    if (RoleID != "-1")
                    {
                        _ClassBaseDAL.WhereClause += UserID == -1 ? dt.RoleIDColumn.ColumnName + "=@RoleID" : " and " + dt.RoleIDColumn.ColumnName + "=@RoleID";
                        _ClassBaseDAL.AddParams("@RoleID", SqlDbType.VarChar, RoleID, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.PermissionIDColumn.ColumnName;
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return null;
                }
                this.AddMessage("ERR-000001", "Connection failed." + this.getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
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
        public int GetAllUserPermissionRowCount()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt);
                    int rowcount = _ClassBaseDAL.GetRowCount();
                    if (rowcount == -1)
                    {
                        this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    }
                    return rowcount;
                }
                this.AddMessage("ERR-000001", "Connection failed." + this.getMessage(), 0);
                return -1;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return -1;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        public int GetAllUserPermissionRowCount(int UserID , string RoleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt);
                    if (UserID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.UserIDColumn.ColumnName + "=@UserID";
                        _ClassBaseDAL.AddParams("@UserID", SqlDbType.Int, UserID, ParameterDirection.Input);
                    }
                    if (RoleID != "-1")
                    {
                        _ClassBaseDAL.WhereClause += UserID == -1 ? dt.RoleIDColumn.ColumnName + "=@RoleID" : " and " + dt.RoleIDColumn.ColumnName + "=@RoleID";
                        _ClassBaseDAL.AddParams("@RoleID", SqlDbType.VarChar, RoleID, ParameterDirection.Input);
                    }
                    int rowcount = _ClassBaseDAL.GetRowCount();
                    if (rowcount == -1)
                    {
                        this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    }
                    return rowcount;
                }
                this.AddMessage("ERR-000001", "Connection failed." + this.getMessage(), 0);
                return -1;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
                return -1;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái tin
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool UpdateStatus(dsHocLapTrinhWeb.tbl_UserPermissionDataTable dt)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_UserPermissionRow row in dt.Rows)
                        row.SetModified();
                    if (_ClassBaseDAL.UpdateChange(dt, dt.UserIDColumn.ColumnName, dt.PermissionIDColumn.ColumnName, dt.RoleIDColumn.ColumnName))
                        return true;
                    this.AddMessage("ERR-000004", "Update data fail." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return false;
                }
                this.AddMessage("ERR-000001", "Connection failed." + this.getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000004", "Update data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        public vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionRow GetUserPermissionByID(int UserID, string PermissionID, string RoleID)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UserPermissionDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(this.IConnect, dt)
                        {
                            WhereClause = dt.UserIDColumn.ColumnName + "=@UserID"
                        };
                    _ClassBaseDAL.WhereClause = dt.PermissionIDColumn.ColumnName + "=@PermissionID";
                    _ClassBaseDAL.WhereClause = dt.RoleIDColumn.ColumnName + "=@RoleID";
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@UserID", SqlDbType.Int, UserID, ParameterDirection.Input);
                    _ClassBaseDAL.AddParams("@PermissionID", SqlDbType.VarChar, PermissionID, ParameterDirection.Input);
                    _ClassBaseDAL.AddParams("@RoleID", SqlDbType.VarChar, RoleID, ParameterDirection.Input);
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            this.AddMessage("ERR-000009", "Du lieu khong ton tai." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return null;
                }
                this.AddMessage("ERR-000001", "Kết nối bị lỗi." + this.getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                this.AddMessage("ERR-000006", "Tải dữ liệu không thành công." + ex.Message, 0);
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
