using System;
using System.Data;
using System.Collections;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class UserPermissionBLL : ClassBase
    {
        #region Variable
        ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public UserPermissionBLL(Connection conn)
        {
            IConnect = conn;
        }

        #region User Info
        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public dsHocLapTrinhWeb.tbl_UserRow GetUserInfo(int userID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
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
        /// Lấy thông tin User
        /// </summary>
        /// <param name="userName">User Name login</param>
        /// <returns></returns>
        public dsHocLapTrinhWeb.tbl_UserRow GetUserInfo(string userName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserNameColumn.ColumnName + "=@name"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@name", SqlDbType.VarChar, userName, ParameterDirection.Input);
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
        /// Kiểm tra user đã kích hoạt chưa.
        /// true: actived, false: not active, null: lỗi
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>true: actived, false: not active, null: lỗi</returns>
        public bool? CheckUserActive(int userID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0].IsActive;
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
        /// Kiểm tra user đã kích hoạt chưa.
        /// true: actived, false: not active, null: lỗi
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>true: actived, false: not active, null: lỗi</returns>
        public bool? CheckUserActive(string userName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserNameColumn.ColumnName + "=@username"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@username", SqlDbType.VarChar, userName, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0].IsActive;
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
        /// Kiểm tra user có phải admin hay không. user deactive not admin
        /// true: admin, false: not admin, null: lỗi
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>true: admin, false: not admin, null: lỗi</returns>
        public bool? CheckUserIsAdmin(int userID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai.", 0);
                            return null;
                        }
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return null;
                        }
                        return dt[0].IsAdmin;
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
        /// Kiểm tra user có phải admin hay không. user deactive not admin
        /// true: admin, false: not admin, null: lỗi
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>true: admin, false: not admin, null: lỗi</returns>
        public bool? CheckUserIsAdmin(string userName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserNameColumn.ColumnName + "=@username"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@username", SqlDbType.VarChar, userName, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai.", 0);
                            return null;
                        }
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return null;
                        }
                        return dt[0].IsAdmin;
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

        #endregion

        #region Check Role
        /// <summary>
        /// Lấy danh sách Role của user. Chưa kiểm tra user active
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public dsHocLapTrinhWeb.tbl_UserPermissionDataTable GetUserRole(int userID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        return dt;
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
        /// Kiểm tra role Anysystem cho user. 
        /// Nếu user deactive -> role = false, 
        /// nếu là admin-> anysystem. Đã kiểm tra quyền admin của user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool? CheckUserRoleAnySystem(int userID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    //Nếu user là admin --> toàn quyền hệ thống
                    var isAdmin = CheckUserIsAdmin(userID);
                    if (isAdmin == null)
                    {
                        return null;
                    }
                    if (isAdmin == true)
                    {
                        return true;
                    }
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.WhereClause += " and " + dt.RoleIDColumn.ColumnName + "=@role";
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@role", SqlDbType.VarChar, "ANYSYSTEM", ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                            return false;
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return false;
                        }
                        return true;
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
        /// Kiểm tra role cho user. Nếu user deactive -> role = false.
        /// Đã kiểm tra Role anysystem và User admin
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool? CheckUserRole(int userID, string roleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    //Nếu có quyền anysystem --> toàn quyền trên các quyền khác
                    var roleanysystem = CheckUserRoleAnySystem(userID);
                    if (roleanysystem == null)
                    {
                        return null;
                    }
                    if (roleanysystem == true)
                    {
                        return true;
                    }
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.WhereClause += " and " + dt.RoleIDColumn.ColumnName + "=@role";
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@role", SqlDbType.VarChar, roleID, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                            return false;
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return false;
                        }
                        return true;
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

        #endregion

        #region Check Permission
        /// <summary>
        /// Lấy lên danh sách permission thuộc roleid của user. Chưa kiểm tra user active
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable GetUserRolePermission(int userID, string roleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    //Kiem tra quyen tren role
                    var isAccess = CheckUserRole(userID, roleID);
                    if (isAccess == null || isAccess == false)
                    {
                        return null;
                    }
                    if (isAccess == true)
                    {
                        var dtResult = new DataTable();
                        var dt = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
                        _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                            {
                                Distinct = true,
                                SelectClause = dt.PermissionIDColumn.ColumnName,
                                WhereClause = dt.UserIDColumn.ColumnName + "=@id"
                            };
                        _classBaseDAL.WhereClause += " and " + dt.RoleIDColumn.ColumnName + " in ('ANYSYSTEM','" + roleID + "')";
                        _classBaseDAL.ClearParams();
                        _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                        if (_classBaseDAL.FillDataWithNotStructure(dtResult))
                        {
                            return dtResult;
                        }
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
        /// Kiểm tra Permission thuộc roleid của user.
        /// Nếu Permission=anysystem-> full quyền.
        /// Nếu User admin --> Full quyền
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool? CheckUserRolePermissionAnySystem(int userID, string roleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    //Nếu user là admin --> toàn quyền hệ thống
                    var isAdmin = CheckUserIsAdmin(userID);
                    if (isAdmin == null)
                    {
                        return null;
                    }
                    if (isAdmin == true)
                    {
                        return true;
                    }
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.WhereClause += " and " + dt.RoleIDColumn.ColumnName + "=@role";
                    _classBaseDAL.WhereClause += " and " + dt.PermissionIDColumn.ColumnName + "=@permission";
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@role", SqlDbType.VarChar, roleID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@permission", SqlDbType.VarChar, "ANYSYSTEM", ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                            return false;
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return false;
                        }
                        return true;
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
        /// Kiểm tra Permission thuộc roleid của user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <param name="permissionID"></param>
        /// <returns></returns>
        public bool? CheckUserRolePermission(int userID, string roleID, string permissionID)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    //Nếu có quyền anysystem --> toàn quyền trên các quyền khác
                    var permissionanysystem = CheckUserRolePermissionAnySystem(userID, roleID);
                    if (permissionanysystem == null)
                    {
                        return null;
                    }
                    if (permissionanysystem == true)
                    {
                        return true;
                    }
                    var dt = new vnn_dsHocLapTrinhWeb.ltk_vw_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.UserIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.WhereClause += " and " + dt.RoleIDColumn.ColumnName + "=@role";
                    _classBaseDAL.WhereClause += " and " + dt.PermissionIDColumn.ColumnName + "=@permission";
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.Int, userID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@role", SqlDbType.VarChar, roleID, ParameterDirection.Input);
                    _classBaseDAL.AddParams("@permission", SqlDbType.VarChar, permissionID, ParameterDirection.Input);
                    if (_classBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                            return false;
                        if (!dt[0].IsActive)
                        {
                            AddMessage("ERR-LOG007", "Tài khoản chưa kích hoạt", 0);
                            return false;
                        }
                        return true;
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

        #endregion

        #region Delete

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Delete(dsHocLapTrinhWeb.tbl_UserPermissionDataTable dt)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (_classBaseDAL.Delete(dt))
                    {
                        return true;
                    }
                    if (_classBaseDAL.getMsgNumber() == 547)//Ràng buộc dữ liệu khóa ngoại
                    {
                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000005", "Delete data fail." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000005", "Delete data fail." + ex.Message, 0);
                return false;
            }
            finally
            {

                CloseConnection(isOpen);
            }
        }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="userID"></param>
     /// <param name="permissionID"></param>
     /// <param name="roleID"></param>
     /// <returns></returns>
        public bool Delete(int userID, int permissionID, int roleID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.WhereClause = dt.UserIDColumn.ColumnName + "=@1";
                    _classBaseDAL.AddParams("@1", SqlDbType.Int, userID, ParameterDirection.Input);
                    if (_classBaseDAL.DeleteDirect())
                        return true;
                    if (_classBaseDAL.getMsgNumber() == 547)//Bi rang buoc khoa ngoai
                    {
                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000005", "Delete data fail." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000005", "Delection failed." + ex.Message, 0);
                return false;
            }
            finally
            {

                CloseConnection(isOpen);
            }
        }

        public bool Delete(ArrayList arrayID)
        {
            var isOpen = false;
            var strWhere = "";
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserPermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    for (var i = 1; i < arrayID.Count + 1; i++)
                    {

                        strWhere += "( " + dt.UserIDColumn.ColumnName + "= @User" + i + " and ";
                        strWhere += dt.PermissionIDColumn.ColumnName + " = @Permission" + i + " and ";
                        strWhere += dt.RoleIDColumn.ColumnName + " = @Role" + i + " ) or";
                        _classBaseDAL.AddParams("@User" + i, SqlDbType.Int, int.Parse(((string[])arrayID[i - 1])[0]), ParameterDirection.Input);
                        _classBaseDAL.AddParams("@Permission" + i, SqlDbType.VarChar, ((string[])arrayID[i - 1])[1], ParameterDirection.Input);
                        _classBaseDAL.AddParams("@Role" + i, SqlDbType.VarChar, ((string[])arrayID[i - 1])[2], ParameterDirection.Input);
                    }
                    _classBaseDAL.WhereClause = strWhere.Substring(0, strWhere.Length - 2);
                    if (_classBaseDAL.DeleteDirect())
                        return true;
                    if (_classBaseDAL.getMsgNumber() == 547)//Bi rang buoc khoa ngoai

                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    else
                        AddMessage("ERR-000005", "Delete data fail." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("PRI-000001", "Connect không thành công." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("PRI-000005", ex.ToString(), 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }
        #endregion

        #region Add

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Add(dsHocLapTrinhWeb.tbl_UserPermissionDataTable dt)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_UserPermissionRow row in dt.Rows)
                        row.SetAdded();
                    if (_classBaseDAL.AddNew(dt))
                    {
                        //Nếu khóa là identity. thì hàm sẽ tự động trả lại id
                        return true;
                    }
                    if (_classBaseDAL.getMsgNumber() == 2627)
                    {
                        AddMessage("ERR-000010", "Data is exist." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000003", "Add data fail." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000003", "Add data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }
        #endregion

        #endregion

        #region Private Methods

        #region Check User Info
        #endregion

        #region Check Role

        #endregion

        #region Check Permission
        #endregion

        #endregion
    }
}
