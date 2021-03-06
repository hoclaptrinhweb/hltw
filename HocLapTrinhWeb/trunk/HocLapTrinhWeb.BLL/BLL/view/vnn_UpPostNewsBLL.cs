using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_UpPostNewsBLL : ClassBase
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        #region Admin

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_UpPostNewsBLL(Connection conn)
        {
            IConnect = conn;
        }


        public vnn_dsHocLapTrinhWeb.vnn_vw_UpPostNewsDataTable GetAllPostNewsForGridView()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpPostNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {OrderByClause = dt.ForumNameColumn.ColumnName + " desc"};
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_UpPostNewsDataTable GetAllPostNewsForGridView(string categoryName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpPostNewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.CategoryNameColumn.ColumnName + "=@CategoryName"};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@CategoryName", SqlDbType.NVarChar, categoryName, ParameterDirection.Input);
                    _ClassBaseDAL.OrderByClause = dt.ForumNameColumn.ColumnName + " desc";
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
