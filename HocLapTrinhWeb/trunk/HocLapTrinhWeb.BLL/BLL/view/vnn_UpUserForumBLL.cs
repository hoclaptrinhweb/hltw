using System;
using System.Data;
using HocLapTrinhWeb.DAL;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class vnn_UpUserForumBLL : UpUserForumBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public vnn_UpUserForumBLL(Connection conn)
            : base(conn)
        {

        }

        #region Admin

        /// <summary>
        /// Load UserForum len gridview
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable GetAllUserForumForGridView(int startRowIndex, int maximumRows)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {
                            StartRow = startRowIndex,
                            MaxRows = maximumRows,
                            OrderByClause = dt.UserForumIDColumn.ColumnName + " desc"
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

        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable GetAllUserForumForGridView()
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {OrderByClause = dt.UserForumIDColumn.ColumnName + " desc"};
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
        public int GetAllUserForumRowCount()
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable();
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
        public vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumRow GetUserForumByID(int id)
        {
            bool isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new vnn_dsHocLapTrinhWeb.vnn_vw_UpUserForumDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt)
                        {WhereClause = dt.UserForumIDColumn.ColumnName + "=@id"};
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

        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
