using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_ContactBLL : ContactBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_ContactBLL(Connection conn)
            : base(conn)
        {

        }

        /// <summary>
        /// Load Contact len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable GetAllContactForGridView(int startRowIndex, int maximumRows, int Type)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {StartRow = startRowIndex, MaxRows = maximumRows};
                    if (Type != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.TypeColumn.ColumnName + "=@Type";
                        _ClassBaseDAL.AddParams("@Type", SqlDbType.Int, Type, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.ContactIDColumn.ColumnName + " desc";
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


        public vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable GetAllContactForGridView()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {OrderByClause = dt.ContactIDColumn.ColumnName + " desc"};
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
        public int GetAllContactRowCount(int Type)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (Type != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.TypeColumn.ColumnName + "=@Type";
                        _ClassBaseDAL.AddParams("@Type", SqlDbType.Int, Type, ParameterDirection.Input);
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
        /// Lấy lên số dòng dữ liệu
        /// </summary>
        /// <returns></returns>
        public int GetAllContactRowCount(int Type, string d)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_ContactDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (Type != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.TypeColumn.ColumnName + "=@Type";
                        _ClassBaseDAL.AddParams("@Type", SqlDbType.Int, Type, ParameterDirection.Input);
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

        #endregion

        #region Private Methods

        #endregion
    }
}
