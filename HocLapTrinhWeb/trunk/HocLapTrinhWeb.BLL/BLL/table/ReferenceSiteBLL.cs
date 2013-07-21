using System;
using System.Data;
using System.Collections;
using System.Globalization;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class ReferenceSiteBLL : ClassBase
    {
        #region Variable
        ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ReferenceSiteBLL(Connection conn)
        {
            IConnect = conn;
        }

        /// <summary>
        /// Lấy lên chi tiết 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dsHocLapTrinhWeb.tbl_ReferenceSiteRow GetReferenceSiteByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.ReferenceSiteIDColumn.ColumnName + "=@id"};
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
        /// Lấy lên chi tiết 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dsHocLapTrinhWeb.tbl_ReferenceSiteRow GetReferenceSiteByNewsType(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@id" };
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
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Add(dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_ReferenceSiteRow row in dt.Rows)
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
        public bool Update(dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_ReferenceSiteRow row in dt.Rows)
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
        public bool Delete(dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable dt)
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
        public bool Delete(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.WhereClause = dt.ReferenceSiteIDColumn.ColumnName + "=@1";
                    _classBaseDAL.AddParams("@1", SqlDbType.Int, id, ParameterDirection.Input);
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
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    var strWhereClause = dt.ReferenceSiteIDColumn.ColumnName + " in (";
                    for (var i = 1; i < arrayID.Count + 1; i++)
                    {
                        strWhereClause += "@" + i + ",";
                        _classBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, int.Parse(arrayID[i - 1].ToString()), ParameterDirection.Input);
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
