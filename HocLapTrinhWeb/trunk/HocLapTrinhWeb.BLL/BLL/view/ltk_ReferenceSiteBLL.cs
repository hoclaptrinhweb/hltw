using System;
using System.Data;
using DH.Data.SqlServer;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class ltk_ReferenceSiteBLL : ReferenceSiteBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ltk_ReferenceSiteBLL(Connection conn)
            : base(conn)
        {
        }

        #region Admin

        /// <summary>
        /// Lấy lên danh sách cấu hình của NewsType
        /// </summary>
        /// <param name="newsTypeID"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable GetNewsTypeRefSiteForGridView(int startRowIndex, int maximumRows, int newsTypeID)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {StartRow = startRowIndex, MaxRows = maximumRows};
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.NewsTypeIDColumn.ColumnName;
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


        public dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable GetNewsTypeRefSiteForGridView(int newsTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {OrderByClause = dt.NewsTypeIDColumn.ColumnName};
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable GetNewsTypeRefSiteForDropDownList()
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {SelectClause = " DISTINCT  " + dt.RefSiteColumn.ColumnName};
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Rows.Count > 0)
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
        /// <param name="newsTypeID"></param>
        /// <returns></returns>
        public int GetNewsTypeRefSiteRowCount(int newsTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeRefSiteDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    int rowcount = _ClassBaseDAL.GetRowCount();
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
        /// Cập nhật lại trạng thái
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool UpdateStatus(dsHocLapTrinhWeb.tbl_ReferenceSiteRow row)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_ReferenceSiteDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (_ClassBaseDAL.UpdateDirect(row, dt.UpdatedDateColumn.ColumnName, dt.UpdateRowsColumn.ColumnName))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
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

        #endregion

        #region Private Methods

        #endregion
    }
}
