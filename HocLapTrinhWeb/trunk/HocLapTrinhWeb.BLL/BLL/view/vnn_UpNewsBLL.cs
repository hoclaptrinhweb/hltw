using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_UpNewsBLL : UpNewsBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_UpNewsBLL(Connection conn)
            : base(conn)
        {
            IConnect = conn;
        }

        #region Admin

        /// <summary>
        /// Load News len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, int NewsTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {StartRow = startRowIndex, MaxRows = maximumRows};
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.NewsIDColumn.ColumnName + " desc";
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsRow GetNewsByID(int newsID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.NewsIDColumn.ColumnName + "=@NewsID"};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, newsID, ParameterDirection.Input);
                    _ClassBaseDAL.OrderByClause = dt.NewsIDColumn.ColumnName + " desc";
                    if (_ClassBaseDAL.FillData(dt))
                        return dt[0];
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
        public int GetAllNewsRowCount(int NewsTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
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

        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
