using System;
using System.Data;
using System.Collections;
using System.Globalization;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_NewsBLL : NewsBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_NewsBLL(Connection conn)
            : base(conn)
        {
            IConnect = conn;
        }

        #region Admin

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, string selectCol, int NewsTypeID, int IsActive, string Refsite, string FromDate, string ToDate, string Tag, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows, RowNumber = true };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "createddate >= @FromDate and createddate <= @ToDate ";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
                    }
                    if (Tag != "-1")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and (" : "") + dt.KeywordColumn.ColumnName + " " + (Tag == "0" ? "= '' or Keyword IS NULL)" : "<> '')");
                    }
                    _ClassBaseDAL.OrderByClause = orderByName + " " + orderByValue;
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

        public int GetAllNewsRowCount(string selectCol, int NewsTypeID, int IsActive, string Refsite, string FromDate, string ToDate, string Tag, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
                    }
                    if (Tag != "-1")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and (" : "") + dt.KeywordColumn.ColumnName + " " + (Tag == "0" ? "= '' or Keyword IS NULL)" : "<> '')");
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, string selectCol, int NewsTypeID, int IsActive, string Refsite, string FromDate, string ToDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows, RowNumber = true };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
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

        public int GetAllNewsRowCount(string selectCol, int NewsTypeID, int IsActive, string Refsite, string FromDate, string ToDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (NewsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, NewsTypeID, ParameterDirection.Input);
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView_Show(int Top, ArrayList ArrayListNotNewsTypeID, int NotNewsID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = Top };
                    _ClassBaseDAL.ClearParams();
                    var strWhereClause = dt.NewsTypeIDColumn.ColumnName + " in (";
                    for (var i = 1; i < ArrayListNotNewsTypeID.Count + 1; i++)
                    {
                        strWhereClause += "@" + i + ",";
                        _ClassBaseDAL.AddParams(i.ToString(), SqlDbType.Int, int.Parse(ArrayListNotNewsTypeID[i - 1].ToString()), ParameterDirection.Input);
                    }
                    _ClassBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";

                    _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                    _ClassBaseDAL.WhereClause += " and " + dt.NewsIDColumn.ColumnName + " != @NewsID";
                    _ClassBaseDAL.WhereClause += " and " + dt.IsDeleteColumn.ColumnName + "=@IsDelete";
                    _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Bit, true, ParameterDirection.Input);
                    _ClassBaseDAL.AddParams("@IsDelete", SqlDbType.Bit, false, ParameterDirection.Input);
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, NotNewsID, ParameterDirection.Input);

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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsRow GetNewsByID(int newsID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.NewsIDColumn.ColumnName + "=@NewsID" };
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

        #endregion


        #region WebLayOut

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsRow GetNewsByID(string selectCol, int newsID, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.NewsIDColumn.ColumnName + "=@NewsID" };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, newsID, ParameterDirection.Input);
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }

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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, string selectCol, string TagName, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows };
                    _ClassBaseDAL.WhereClause = dt.NewsIDColumn.ColumnName + " in ( select t.NewsID from tbl_NewsTag t where t.TagID = ( select t1.TagID from tbl_tag t1 where TagName =  @TagName ) )";
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    _ClassBaseDAL.AddParams("@TagName", SqlDbType.NVarChar, TagName, ParameterDirection.Input);
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeRow GetNewsAndNewsTypeByID(string selectCol, int newsID, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.NewsIDColumn.ColumnName + "=@NewsID" };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, newsID, ParameterDirection.Input);
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }

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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsRow GetNewsByRefAddress(string refAddress, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.RefAddressColumn.ColumnName + "=@RefAddress" };
                    _ClassBaseDAL.AddParams("@RefAddress", SqlDbType.NVarChar, refAddress, ParameterDirection.Input);
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }

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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForRepeater(int newsTypeID, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForRepeater(int nTop, int newsTypeID, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetAllNewsOldForRepeater(string selectCol, int nTop, int notNewsID, int newsTypeID, int isActive, DateTime currDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.CreatedDateColumn.ColumnName + " < @CurrDate";
                    _ClassBaseDAL.AddParams("@CurrDate", SqlDbType.DateTime, currDate, ParameterDirection.Input);

                    _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.NewsIDColumn.ColumnName + " != @NewsID";
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, notNewsID, ParameterDirection.Input);

                    _ClassBaseDAL.OrderByClause = dt.CreatedDateColumn.ColumnName + " desc";
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable GetAllNewsNewForRepeater(string selectCol, int nTop, int notNewsID, int newsTypeID, int isActive, DateTime currDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_News_and_NewsTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.CreatedDateColumn.ColumnName + " > @CurrDate";
                    _ClassBaseDAL.AddParams("@CurrDate", SqlDbType.DateTime, currDate, ParameterDirection.Input);

                    _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != "" ? " and " : "") + dt.NewsIDColumn.ColumnName + " != @NewsID";
                    _ClassBaseDAL.AddParams("@NewsID", SqlDbType.Int, notNewsID, ParameterDirection.Input);

                    _ClassBaseDAL.OrderByClause = dt.CreatedDateColumn.ColumnName + " asc";
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForSiteMap(string selectCol, int startRowIndex, int maximumRows, int newsTypeID, int isActive, string fromDate, string toDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.NewsIDColumn.ColumnName + " asc";
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsHot(int nTop, int newsTypeID, int isActive, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };

                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }

                    _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsHotColumn.ColumnName + "=@IsHot";
                    _ClassBaseDAL.AddParams("@IsHot", SqlDbType.Int, 1, ParameterDirection.Input);

                    _ClassBaseDAL.OrderByClause = orderByName + " " + orderByValue;

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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForRepeater(string selectCol, int nTop, int newsTypeID, int isActive, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { Top = nTop };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "createddate >= @FromDate and reateddate <= @ToDate";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForRepeater(string selectCol, int startRowIndex, int maximumRows, int newsTypeID, int isActive, string fromDate, string toDate, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForRepeater(string selectCol, int nTop, int newsTypeID, int isActive, string fromDate, string toDate, string strWhere, string orderByName, string orderByValue)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    _ClassBaseDAL.Top = nTop;

                    if (newsTypeID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.NewsTypeIDColumn.ColumnName + "=@NewsTypeID";
                        _ClassBaseDAL.ClearParams();
                        _ClassBaseDAL.AddParams("@NewsTypeID", SqlDbType.Int, newsTypeID, ParameterDirection.Input);
                    }
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
                    }
                    if (strWhere != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + strWhere;

                    }
                    if (fromDate != "" && toDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, toDate, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = orderByName + " " + orderByValue;

                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
                    if (dt.Count > 0)
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, bool rowNumber, string selectCol, vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable dtNewsType, int IsActive, string Refsite, string FromDate, string ToDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows, RowNumber = rowNumber };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (dtNewsType != null && dtNewsType.Count > 0)
                    {
                        var strWhereClause = dt.NewsTypeIDColumn.ColumnName + " in (";
                        for (var i = 0; i < dtNewsType.Count; i++)
                        {
                            strWhereClause += "@" + i + ",";
                            _ClassBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, dtNewsType[i].NewsTypeID, ParameterDirection.Input);
                        }
                        _ClassBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable GetAllNewsForGridView(int startRowIndex, int maximumRows, bool rowNumber, string selectCol, vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable dtNewsType, int IsActive, string Refsite, string FromDate, string ToDate, bool view)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { StartRow = startRowIndex, MaxRows = maximumRows, RowNumber = rowNumber };
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (dtNewsType != null && dtNewsType.Count > 0)
                    {
                        var strWhereClause = dt.NewsTypeIDColumn.ColumnName + " in (";
                        for (var i = 0; i < dtNewsType.Count; i++)
                        {
                            strWhereClause += "@" + i + ",";
                            _ClassBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, dtNewsType[i].NewsTypeID, ParameterDirection.Input);
                        }
                        _ClassBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
                    }
                    if (view)
                        _ClassBaseDAL.OrderByClause = dt.ViewedColumn.ColumnName + " desc";
                    else
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

        public int GetAllNewsRowCount(string selectCol, vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable dtNewsType, int IsActive, string Refsite, string FromDate, string ToDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    if (dtNewsType != null && dtNewsType.Count > 0)
                    {
                        var strWhereClause = dt.NewsTypeIDColumn.ColumnName + " in (";
                        for (var i = 0; i < dtNewsType.Count; i++)
                        {
                            strWhereClause += "@" + i + ",";
                            _ClassBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, dtNewsType[i].NewsTypeID, ParameterDirection.Input);
                        }
                        _ClassBaseDAL.WhereClause = strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    }
                    if (IsActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, IsActive, ParameterDirection.Input);
                    }
                    if (FromDate != "" && ToDate != "")
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + "cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) >= cast(@FromDate AS DATETIME) and cast(CONVERT(nvarchar(10),createddate,101) AS DATETIME) <= cast(@ToDate AS DATETIME)";
                        _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, FromDate, ParameterDirection.Input);
                        _ClassBaseDAL.AddParams("@ToDate", SqlDbType.NVarChar, ToDate, ParameterDirection.Input);
                    }
                    if (string.IsNullOrEmpty(Refsite) == false)
                    {
                        _ClassBaseDAL.WhereClause += (_ClassBaseDAL.WhereClause != null ? " and " : "") + dt.RefAddressColumn.ColumnName + " like '%" + Refsite + "%'";
                        _ClassBaseDAL.AddParams("@Refsite", SqlDbType.NVarChar, Refsite, ParameterDirection.Input);
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

        public int GetAllNewsRowCount(string selectCol, string TagName, int isActive)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    _ClassBaseDAL.WhereClause = dt.NewsIDColumn.ColumnName + " in ( select t.NewsID from tbl_NewsTag t where t.TagID = ( select t1.TagID from tbl_tag t1 where TagName =  @TagName ) )";
                    if (selectCol != "")
                        _ClassBaseDAL.SelectClause = selectCol;
                    _ClassBaseDAL.AddParams("@TagName", SqlDbType.NVarChar, TagName, ParameterDirection.Input);
                    if (isActive != -1)
                    {
                        _ClassBaseDAL.WhereClause += " and " + dt.IsActiveColumn.ColumnName + "=@IsActive";
                        _ClassBaseDAL.AddParams("@IsActive", SqlDbType.Int, isActive, ParameterDirection.Input);
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
