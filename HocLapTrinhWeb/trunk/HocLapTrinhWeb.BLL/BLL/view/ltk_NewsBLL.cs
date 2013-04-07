using System;
using System.Data;
using DH.Data.SqlServer;
using HocLapTrinhWeb.DAL;

namespace HocLapTrinhWeb.BLL
{
    public class ltk_NewsBLL : NewsBLL
    {
        #region Variable
        ClassBaseDAL _ClassBaseDAL;
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public ltk_NewsBLL(Connection conn)
            : base(conn)
        {
        }

        #region Admin

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rRefSie"></param>
        /// <returns></returns>
        public bool AddFromSite(dsHocLapTrinhWeb.tbl_NewsDataTable dt, dsHocLapTrinhWeb.tbl_ReferenceSiteRow rRefSie)
        {
            bool isOpen = false, isTrans = false, isCommit = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    if (!BeginTransaction(ref isTrans))
                    {
                        AddMessage("ERR-000002", "Transaction start failed." + getMessage(), getMsgNumber());
                        return false;
                    }
                    var newsBll = new NewsBLL(IConnect);
                    if (newsBll.Add(dt))
                    {
                        var ltkReferenceSiteBll = new ltk_ReferenceSiteBLL(IConnect);
                        if (ltkReferenceSiteBll.UpdateStatus(rRefSie))
                        {
                            isCommit = true;
                            return true;
                        }
                        else
                        {
                            SetMessage(ltkReferenceSiteBll);
                            return false;
                        }
                    }
                    else
                    {
                        SetMessage(newsBll);
                        return false;
                    }
                }
                else
                {
                    AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000006", "Load data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                if (isCommit)
                    Commit(isTrans);
                else
                    Rollback(isTrans);
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnsName"></param>
        /// <returns></returns>
        public bool UpdateStatus(dsHocLapTrinhWeb.tbl_NewsDataTable dt, params string[] columnsName)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
                        row.SetModified();
                    if (_ClassBaseDAL.UpdateChange(dt, columnsName))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000004", "Update data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái tin
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool MoveNews(dsHocLapTrinhWeb.tbl_NewsDataTable dt)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt);
                    dt.AcceptChanges();
                    foreach (dsHocLapTrinhWeb.tbl_NewsRow row in dt.Rows)
                        row.SetModified();
                    if (_ClassBaseDAL.UpdateChange(dt, dt.NewsTypeIDColumn.ColumnName, dt.MoveFromColumn.ColumnName, dt.UpdatedDateColumn.ColumnName, dt.UpdatedByColumn.ColumnName, dt.IPUpdateColumn.ColumnName))
                        return true;
                    AddMessage("ERR-000004", "Update data fail." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Connection failed." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000004", "Update data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        public bool CheckExistByRefAddress(string refAddress)
        {
            var isOpen = false;
            try
            {
                if (OpenConnection(ref isOpen))
                {
                    var dt = new dsHocLapTrinhWeb.tbl_NewsDataTable();
                    _ClassBaseDAL = new ClassBaseDAL(IConnect, dt) { WhereClause = dt.RefAddressColumn.ColumnName + "=@id" };
                    _ClassBaseDAL.ClearParams();
                    _ClassBaseDAL.AddParams("@id", SqlDbType.NVarChar, refAddress, ParameterDirection.Input);
                    if (_ClassBaseDAL.FillData(dt))
                    {
                        if (dt.Count == 0)
                        {
                            AddMessage("ERR-000009", "Du lieu khong ton tai." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                            return false;
                        }
                        return true;
                    }
                    AddMessage("ERR-000006", "Tải dữ liệu không thành công." + _ClassBaseDAL.getMessage(), _ClassBaseDAL.getMsgNumber());
                    return false;
                }
                AddMessage("ERR-000001", "Kết nối bị lỗi." + getMessage(), 0);
                return false;
            }
            catch (Exception ex)
            {
                AddMessage("ERR-000004", "Update data fail." + ex.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(isOpen);
            }
        }

        #endregion

        #endregion

    }
}
