using System;
using System.Data;
using System.Collections;
using System.Globalization;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class PermissionBLL : ClassBase
    {

        #region Variable
        ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public PermissionBLL(Connection conn)
        {
            IConnect = conn;
        }

        public dsHocLapTrinhWeb.tbl_PermissionRow GetPermissionByID(string id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_PermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.PermissionIDColumn.ColumnName + "=@id"};
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.AddParams("@id", SqlDbType.VarChar, id, ParameterDirection.Input);
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
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Add(dsHocLapTrinhWeb.tbl_PermissionDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_PermissionRow row in dt.Rows)
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

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Update(dsHocLapTrinhWeb.tbl_PermissionDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_PermissionRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDAL.UpdateChange(dt))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _classBaseDAL.getMessage(), _classBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000004", "Update data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Delete(dsHocLapTrinhWeb.tbl_PermissionDataTable dt)
        {
            var isOpen = false;
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
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_PermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.WhereClause = dt.PermissionIDColumn.ColumnName + "=@1";
                    _classBaseDAL.AddParams("@1", SqlDbType.VarChar, id, ParameterDirection.Input);
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

        public bool Delete(ArrayList arrayId)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_PermissionDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    var strWhereClause = dt.PermissionIDColumn.ColumnName + " in (";
                    for (int i = 1; i < arrayId.Count + 1; i++)
                    {
                        strWhereClause += "@" + i + ",";
                        _classBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.VarChar, arrayId[i - 1].ToString(), ParameterDirection.Input);
                    }
                    _classBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
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

        #region Private Methods

        #endregion
    }
}
