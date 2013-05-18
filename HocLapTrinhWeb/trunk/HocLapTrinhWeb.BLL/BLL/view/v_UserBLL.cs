using System;
using System.Data;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class v_UserBLL : UserBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public v_UserBLL(Connection conn): base(conn)
        {

        }

        public dsHocLapTrinhWeb.tbl_UserRow GetUserByName(string strName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.UserNameColumn.ColumnName + " =  @strName " };
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@strName", SqlDbType.NVarChar, strName, ParameterDirection.Input);
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

        public dsHocLapTrinhWeb.tbl_UserDataTable SearchUserByName(int top, string strName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_UserDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserNameColumn.ColumnName + " like '%' + @strName + '%' ", Top = top};
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@strName", SqlDbType.NVarChar, strName, ParameterDirection.Input);
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

    }
}
    