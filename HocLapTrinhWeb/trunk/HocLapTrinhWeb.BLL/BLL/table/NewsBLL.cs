using System;
using System.Data;
using System.Collections;
using System.Globalization;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class NewsBLL : ClassBase
    {

        #region Variable
        public ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public NewsBLL(Connection conn)
        {
            IConnect = conn;
        }

        public dsHocLapTrinhWeb.tbl_NewsRow GetNewsByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.NewsIDColumn.ColumnName + "=@id" };
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

        public dsHocLapTrinhWeb.tbl_NewsDataTable GetNewsAll(string selectCol)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selectCol != "")
                        _classBaseDAL.SelectClause = selectCol;
                    if (_classBaseDAL.FillData(dt))
                        return dt;
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetNewsAll(string selectCol, int nTop, int newsTypeID, int isActive, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _classBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _classBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _classBaseDAL.ClearParams();
                        _classBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _classBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _classBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _classBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetNewsAll(string selectCol, int nTop, int newsTypeID, int isActive, int isHot, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _classBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _classBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _classBaseDAL.ClearParams();
                        _classBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (isHot != -1)
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + dt.IsHotColumn.ColumnName + "=@isHot";
                        _classBaseDAL.AddParams("@isHot", SqlDbType.Int, isHot, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _classBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _classBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _classBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetNewsAll(string selectCol, int nTop, vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable dtNewsType, int isActive, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _classBaseDAL.SelectClause = selectCol;
                    if (dtNewsType != null && dtNewsType.Count > 0)
                    {
                        var strWhereClause = dt.NewsTypeIDColumn.ColumnName + " in (";
                        for (var i = 0; i < dtNewsType.Count; i++)
                        {
                            strWhereClause += "@" + i + ",";
                            _classBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, dtNewsType[i].NewsTypeID, ParameterDirection.Input);
                        }
                        _classBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    }
                    if (isActive != -1)
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _classBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _classBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _classBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetNewsAll(string selectCol, int nTop, int parentNewsType, int isActive, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _classBaseDAL.SelectClause = selectCol;
                    if (parentNewsType != -1)
                    {
                        _classBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _classBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, parentNewsType, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _classBaseDAL.WhereClause += (_classBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _classBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    _classBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_classBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Add(dsHocLapTrinhWeb.tbl_NewsDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
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
        public bool Update(dsHocLapTrinhWeb.tbl_NewsDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
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

        public bool Update(dsHocLapTrinhWeb.tbl_NewsDataTable dt, dsHocLapTrinhWeb.tbl_VideoDataTable dtVideo)
        {
            bool isOpen = false, isTrans = false, isCommit = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    if (!BeginTransaction(ref isTrans))
                    {
                        AddMessage("ERR-000002", "Khoi tao transaction that bai." + getMessage(), 0);
                        return false;
                    }
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDAL.UpdateChange(dt))
                    {
                        if (dtVideo.Count == 0)
                        {
                            isCommit = true;
                            return true;
                        }
                        _classBaseDAL = new ClassBaseDAL(IConnect, dtVideo);
                        if (dtVideo[0].VideoID == -1)
                        {
                            if (_classBaseDAL.AddNew(dtVideo))
                            {
                                isCommit = true;
                                return true;
                            }
                        }
                        else
                        {
                            if (_classBaseDAL.UpdateChange(dtVideo))
                            {
                                isCommit = true;
                                return true;
                            }
                        }

                        return false;
                    }
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
                if (isCommit)
                    Commit(isTrans);
                else
                    Rollback(isTrans);
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">table</param>
        /// <param name="strColumnName">các cols muốn cập nhật</param>
        /// <returns></returns>
        public bool UpdateChange(dsHocLapTrinhWeb.tbl_NewsDataTable dt, params string[] strColumnName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
                        row.SetModified();
                    if (_classBaseDAL.UpdateChange(dt, strColumnName))
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
        public bool Delete(dsHocLapTrinhWeb.tbl_NewsDataTable dt)
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
                    var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    _classBaseDAL.WhereClause = dt.NewsIDColumn.ColumnName + "=@1";
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
                    var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _classBaseDAL.ClearParams();
                    var strWhereClause = dt.NewsIDColumn.ColumnName + " in (";
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
