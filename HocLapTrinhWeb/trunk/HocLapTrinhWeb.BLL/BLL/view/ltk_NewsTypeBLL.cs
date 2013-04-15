using System;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class ltk_NewsTypeBLL : NewsTypeBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ltk_NewsTypeBLL(Connection conn)
            : base(conn)
        {
        }

        #region Admin


        public vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable GetAllNewsType()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_NewsTypeDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) {OrderByClause = dt.NewsTypeIDColumn.ColumnName};
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
