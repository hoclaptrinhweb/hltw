using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_AlexaDetailBLL : AlexaDetailBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_AlexaDetailBLL(Connection conn)
            : base(conn)
        {

        }

        #region Admin

        /// <summary>
        /// Load Alexa len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable GetAllAlexaDetailForGridView(int startRowIndex, int maximumRows, int AlexaID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {StartRow = startRowIndex, MaxRows = maximumRows};
                    if (AlexaID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.AlexaIDColumn.ColumnName + "=@AlexaID";
                        _ClassBaseDAL.AddParams("@AlexaID", SqlDbType.Int, AlexaID, ParameterDirection.Input);
                    }
                    _ClassBaseDAL.OrderByClause = dt.AlexaDetailIDColumn.ColumnName + " desc";
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
        public int GetAllAlexaDetailRowCount(int AlexaID)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    if (AlexaID != -1)
                    {
                        _ClassBaseDAL.WhereClause = dt.AlexaIDColumn.ColumnName + "=@AlexaID";
                        _ClassBaseDAL.AddParams("@AlexaID", SqlDbType.Int, AlexaID, ParameterDirection.Input);
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
        /// Lấy lên chi tiết 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailRow GetAlexaDetailByID(int id)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {WhereClause = dt.AlexaIDColumn.ColumnName + "=@id"};
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailRow GetAlexaDetailByAlexaID(int alexaID, string fromDate)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.AlexaIDColumn.ColumnName + "=@AlexaID"};
                    _ClassBaseDAL.AddParams("@AlexaID", SqlDbType.Int, alexaID, ParameterDirection.Input);
                    _ClassBaseDAL.WhereClause += " and cast(CONVERT(nvarchar(10)," + dt.UpdatedDateColumn.ColumnName + ",101) AS DATETIME) = cast(@FromDate AS DATETIME)";
                    _ClassBaseDAL.AddParams("@FromDate", SqlDbType.NVarChar, fromDate, ParameterDirection.Input);

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



        public vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailRow GetAlexaByName(string linkUrl)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_AlexaDetailDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.LinkUrlColumn.ColumnName + "=@LinkUrl"};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@LinkUrl", SqlDbType.VarChar, linkUrl, ParameterDirection.Input);
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

        #endregion

        #region Private Methods

        #endregion
    }
}
