using System;
using System.Data;
using System.Collections;
using System.Globalization;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_VideoTypeBLL : VideoTypeBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_VideoTypeBLL(Connection conn)
            : base(conn)
        {

        }

        #region Admin

        /// <summary>
        /// Load VideoType len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable GetAllVideoTypeForGridView(int startRowIndex, int maximumRows)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows,
                            OrderByClause = dt.PathColumn.ColumnName + " asc"
                        };
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
        /// 
        /// </summary>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable GetAllVideoTypeForGridView()
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { OrderByClause = dt.PathColumn.ColumnName + " asc" };
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
        public int GetAllVideoTypeRowCount()
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
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

        /// <summary>
        /// Lấy lên chi tiết 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeRow GetVideoTypeByID(int id)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.VideoTypeIDColumn.ColumnName + "=@id" };
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
        /// 
        /// </summary>
        /// <param name="selCol"> </param>
        /// <param name="parentID"></param>
        /// <param name="arrayListNotVideoTypeID"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable GetVideoTypeByParentID(string selCol, int parentID, ArrayList arrayListNotVideoTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (selCol != "")
                        _ClassBaseDAL.SelectClause = selCol;
                    var strWhereClause = "";
                    if (arrayListNotVideoTypeID.Count > 0)
                    {
                        strWhereClause = dt.VideoTypeIDColumn.ColumnName + " not in (";
                        for (var i = 1; i < arrayListNotVideoTypeID.Count + 1; i++)
                        {
                            strWhereClause += "@" + i + ",";
                            _ClassBaseDAL.AddParams(i.ToString(CultureInfo.InvariantCulture), SqlDbType.Int, int.Parse(arrayListNotVideoTypeID[i - 1].ToString()), ParameterDirection.Input);
                        }
                        _ClassBaseDAL.WhereClause += strWhereClause.Substring(0, strWhereClause.Length - 1) + ")";
                    }
                    _ClassBaseDAL.WhereClause += (strWhereClause == "" ? "" : " and ") + dt.ParentIDColumn.ColumnName + "=@ParentID";
                    _ClassBaseDAL.AddParams("@ParentID", SqlDbType.Int, parentID, ParameterDirection.Input);
                    _ClassBaseDAL.OrderByClause = dt.PathColumn.ColumnName + " asc";
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        return dt;
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
        /// Lấy lên tài khoản cho việc login
        /// </summary>
        /// <param name="newsTypeName"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeRow GetVideoTypeByName(string newsTypeName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.VideoTypeNameColumn.ColumnName + "=@1" };
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@1", SqlDbType.VarChar, newsTypeName, ParameterDirection.Input);
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-LOG002", "Du lieu khong ton tai." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                            return null;
                        }
                        return dt[0];
                    }
                    AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return null;
                }
                AddMessage("ERR-LOG001", "Kết nối bị lỗi." + getMessage(), 0);
                return null;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-LOG001", "Tải dữ liệu không thành công." + ex.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        #endregion

        #region Web Layout

        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable GetDataByParentID(string selCol, int productTypeID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.ParentIDColumn.ColumnName + "=@pID" };
                    if (selCol != "")
                        _ClassBaseDAL.SelectClause = selCol;
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@pID", SqlDbType.Int, productTypeID, ParameterDirection.Input);
                    _ClassBaseDAL.OrderByClause = dt.PriorityColumn.ColumnName + " asc";
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable GetDataAllChildrenByPathID(string selCol, string strPathID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_VideoTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.PathIDColumn.ColumnName + " like '" + strPathID + "/%' or " + dt.PathIDColumn.ColumnName + " = '" + strPathID + "'" };
                    if (selCol != "")
                        _ClassBaseDAL.SelectClause = selCol;
                    _ClassBaseDAL.OrderByClause = dt.PathIDColumn.ColumnName + " asc";
                    if (_ClassBaseDAL.FillData(dt))
                        return dt;
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

        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
