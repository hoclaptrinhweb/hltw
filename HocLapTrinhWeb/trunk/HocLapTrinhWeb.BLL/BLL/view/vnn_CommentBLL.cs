using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_CommentNewsBLL : CommentNewsBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_CommentNewsBLL(Connection conn)
            : base(conn)
        {

        }

        public vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsRow GetCommentNewsByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.CommentNewsIDColumn.ColumnName + "=@id"};
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
        /// Load CommentNews len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable GetAllCommentNewsForGridView(int startRowIndex, int maximumRows, int IsActive)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {StartRow = startRowIndex, MaxRows = maximumRows};
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.CommentNewsIDColumn.ColumnName + " desc";
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
        public int GetAllCommentNewsRowCount(int IsActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
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

        public int GetAllCommentNewsRowCount(int IsActive, string d)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (d != "")
                    {
                        _ClassBaseDAL.WhereClause = (_ClassBaseDAL.WhereClause == null ? "" : " and ") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) = cast(@d AS DATETIME)";
                        _ClassBaseDAL.AddParams("@d", SqlDbType.NVarChar, d, ParameterDirection.Input);
                    }
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


        #region Web UI

        public vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable GetAllCommentNewsForRepeater(int startRowIndex, int maximumRows, int IsActive, int NewsID)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable dt = new vnn_dsHocLapTrinhWeb.vnn_vw_CommentNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _ClassBaseDAL.StartRow = startRowIndex;
                    _ClassBaseDAL.MaxRows = maximumRows;
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }

                    if (NewsID != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.NewsIDColumn.ColumnName + "=@NewsID";
                        _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, NewsID, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.CommentNewsIDColumn.ColumnName + " desc";
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
        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
