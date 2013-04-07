using System;
using System.Data;
using System.Collections;
using DH.Data.SqlServer;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class t_VideoBLL : ClassBase
    {
        #region Variable
        ClassBaseDAL _classBaseDal;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public t_VideoBLL(Connection conn)
        {
            IConnect = conn;
        }

        public dsHocLapTrinhWeb.tbl_VideoRow GetVideoByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
                    _classBaseDal = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.VideoIDColumn.ColumnName + "=@id"};
                    _classBaseDal.ClearParams();
                    _classBaseDal.AddParams("@id", SqlDbType.Int, id, ParameterDirection.Input);
                    if (_classBaseDal.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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
        public bool Add(dsHocLapTrinhWeb.tbl_VideoDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_VideoRow row in dt.Rows)
                        row.SetAdded();
                    if (_classBaseDal.AddNew(dt))
                        return true;
                    if (_classBaseDal.getMsgNumber() == 2627)
                    {
                        AddMessage("ERR-000010", "Data is exist." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000003", "Add data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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
        public bool Update(dsHocLapTrinhWeb.tbl_VideoDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_VideoRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDal.UpdateChange(dt))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnsName"></param>
        /// <returns></returns>
        public bool UpdateStatus(dsHocLapTrinhWeb.tbl_VideoDataTable dt, params string[] columnsName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_VideoRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDal.UpdateChange(dt, columnsName))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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
        public bool Delete(dsHocLapTrinhWeb.tbl_VideoDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var classBaseDal = new ClassBaseDAL(IConnect, dt);
                    if (classBaseDal.Delete(dt))
                        return true;
                    if (classBaseDal.getMsgNumber() == 547)//Ràng buộc dữ liệu khóa ngoại
                    {
                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + classBaseDal.getMessage(), classBaseDal.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000005", "Delete data fail." + classBaseDal.getMessage(), classBaseDal.getMsgNumber());
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
        public bool Delete(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    _classBaseDal.ClearParams();
                    _classBaseDal.WhereClause = dt.VideoIDColumn.ColumnName + "=@1";
                    _classBaseDal.AddParams("@1", SqlDbType.Int, id, ParameterDirection.Input);
                    if (_classBaseDal.DeleteDirect())
                        return true;
                    if (_classBaseDal.getMsgNumber() == 547)//Bi rang buoc khoa ngoai
                    {
                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
                        return false;
                    }
                    AddMessage("ERR-000005", "Delete data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_VideoDataTable();
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    _classBaseDal.ClearParams();
                    var strWhereClause = dt.VideoIDColumn.ColumnName + " in (";
                    for (var i = 1; i < arrayID.Count + 1; i++)
                    {
                        strWhereClause += "@" + i + ",";
                        _classBaseDal.AddParams(i.ToString(), SqlDbType.Int, Convert.ToInt16(arrayID[i - 1].ToString()), ParameterDirection.Input);
                    }
                    _classBaseDal.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    if (_classBaseDal.DeleteDirect())
                        return true;
                    if (_classBaseDal.getMsgNumber() == 547)//Bi rang buoc khoa ngoai

                        AddMessage("ERR-000011", "Bi rang buoc khoa ngoai." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
                    else
                        AddMessage("ERR-000005", "Delete data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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


        /// <summary>
        /// Cập nhật trạng thái tin
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool MoveVideo(dsHocLapTrinhWeb.tbl_VideoDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDal = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_VideoRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDal.UpdateChange(dt, dt.VideoTypeIDColumn.ColumnName, dt.MoveFromColumn.ColumnName, dt.UpdatedDateColumn.ColumnName, dt.UpdatedByColumn.ColumnName, dt.IPUpdateColumn.ColumnName))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _classBaseDal.getMessage(), _classBaseDal.getMsgNumber());
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

        #endregion

        #region Private Methods

        #endregion
    }
}
