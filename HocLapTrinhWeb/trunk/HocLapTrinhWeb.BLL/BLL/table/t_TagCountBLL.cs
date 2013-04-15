using System;
using System.Collections;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class t_TagCountBLL : ClassBase
    {
        #region Variable
        ClassBaseDAL _classBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public t_TagCountBLL(Connection conn)
        {
            IConnect = conn;
        }

        public vnn_dsHocLapTrinhWeb.vnn_vw_TagCountDataTable GetAll(int iTop)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_TagCountDataTable();
                    _classBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {Top = iTop, OrderByClause = dt.TagCountColumn.ColumnName + " desc"};
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
       
        #endregion

        #region Private Methods

        #endregion
    }
}
