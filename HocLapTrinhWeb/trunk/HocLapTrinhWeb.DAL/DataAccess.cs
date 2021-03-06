using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class DataAccess : ClassBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SqlDataAdapter Da = new SqlDataAdapter();

        /// <summary>
        /// 
        /// </summary>
        public string TableName = null;
        /// <summary>
        /// 
        /// </summary>
        public string SelectClause = null;
        /// <summary>
        /// 
        /// </summary>
        public bool Distinct = false;
        /// <summary>
        /// 
        /// </summary>
        public int Top = 0;
        /// <summary>
        /// 
        /// </summary>
        public string WhereClause = null;
        /// <summary>
        /// 
        /// </summary>
        public string OrderByClause = null;
        /// <summary>
        /// 
        /// </summary>
        public string GroupByClause = null;
        /// <summary>
        /// 
        /// </summary>
        public string HavingClause = null;
        /// <summary>
        /// 
        /// </summary>
        public bool RowNumber = true;
        public int StartRow;
        /// <summary>
        /// 
        /// </summary>
        public int MaxRows;
        /// <summary>
        /// Thời gian execute sqlcommand. unlimit(0),default(30s)
        /// </summary>
        public int CommandTimeout = -1;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="tabName"></param>
        public DataAccess(Connection connect, string tabName)
        {
            IConnect = connect;
            TableName = tabName;
        }

        #region Parameter (Ok)

        /// <summary>
        /// Thêm SqlParameter
        /// </summary>
        /// <param name="op">SqlParameter cần thêm</param>
        public void AddParams(SqlParameter op)
        {
            try
            {
                if (SqlParameters == null)
                    SqlParameters = new ArrayList();
                SqlParameters.Add(op);
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
            }
        }
        /// <summary>
        /// Thêm SqlParameter
        /// </summary>
        /// <param name="paramName">@Param Name</param>
        /// <param name="paramType">SqlDbType</param>
        /// <param name="paramValue">Giá trị</param>
        /// <param name="dir">Parameter Direction</param>
        public void AddParams(string paramName, SqlDbType paramType, object paramValue, ParameterDirection dir)
        {
            try
            {
                if (SqlParameters == null)
                    SqlParameters = new ArrayList();
                var parameter = new SqlParameter(paramName, paramType) { Value = paramValue, Direction = dir };
                SqlParameters.Add(parameter);
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
            }
        }

        /// <summary>
        /// Xóa tất cả SQLParameter
        /// </summary>
        public void ClearParams()
        {
            if (SqlParameters != null)
            {
                SqlParameters.Clear();
            }
        }

        /// <summary>
        /// Danh sách SQLParameter
        /// </summary>
        public ArrayList SqlParameters { get; private set; }

        #endregion

        #region Select (Ok)
        /// <summary>
        /// Fill dữ liệu có cấu trúc
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public bool FillData(DataTable objData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { Distinct = Distinct, MaxRows = MaxRows, StartRow = StartRow, CommandTimeout = CommandTimeout,RowNumber = RowNumber};
                if (TableName == null)
                    TableName = objData.TableName;
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                var cols = new string[objData.Columns.Count];
                if (SelectClause != null)
                    cols = SelectClause.Split(new[] { ',' });
                else
                {
                    for (var i = 0; i < objData.Columns.Count; i++)
                        cols[i] = objData.Columns[i].ColumnName;
                }
                if (!cb.BuildSelect(Top, cols, TableName, WhereClause, OrderByClause, GroupByClause, HavingClause, Params))
                    SetMessage(cb);
                Da = cb.Da;
                if (cb.Fill(objData))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Fill dữ liệu không có cấu trúc. Phải chỉ rõ SelectClause
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public bool FillDataWithNotStructure(DataTable objData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect)
                    {
                        Distinct = Distinct,
                        MaxRows = MaxRows,
                        StartRow = StartRow,
                        CommandTimeout = CommandTimeout
                    };
                if (TableName == null)
                    TableName = objData.TableName;
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                string[] cols;
                if (SelectClause != null)
                {
                    cols = SelectClause.Split(new[] { ',' });
                }
                else
                {
                    return false;
                }
                if (!cb.BuildSelect(Top, cols, TableName, WhereClause, OrderByClause, GroupByClause, HavingClause, Params))
                    SetMessage(cb);
                Da = cb.Da;
                if (cb.Fill(objData))
                {
                    return true;
                }
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Lấy lên số dòng dữ liệu
        /// </summary>
        /// <returns></returns>
        public int GetRowCount()
        {
            SqlDataReader reader = null;
            int rowNum;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return -1;
                }
                SelectClause = "count(*)";
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout, Distinct = Distinct };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                reader = cb.ExecuteReader(Top, SelectClause, TableName, WhereClause, OrderByClause, GroupByClause, HavingClause, Params);
                reader.Read();
                rowNum = Convert.ToInt32(reader[0]);
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                rowNum = -1;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                CloseConnection(opened);
            }
            return rowNum;
        }
        #endregion

        #region Insert (Ok)

        /// <summary>
        /// Thêm dữ liệu. Nếu 1 row sẽ trã về Identity. Unique Index/Constriant Violation( ErrorNumber: 2627)
        /// </summary>
        /// <param name="objData">Dữ liệu cần thêm</param>
        /// <returns></returns>
        public bool AddNew(DataTable objData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                TableName = objData.TableName;
                //ObjData.AcceptChanges();
                foreach (DataRow row in objData.Rows)
                {
                    if (row.RowState != DataRowState.Added)
                    {
                        row.AcceptChanges();
                        row.SetAdded();
                    }
                }
                var arrCols = new ArrayList();
                for (var i = 0; i < objData.Columns.Count; i++)
                {
                    //Neu cot tu tang thi ko dua vao menh de insert
                    if (objData.Columns[i].AutoIncrement)
                        continue;
                    arrCols.Add(objData.Columns[i].ColumnName);

                }
                var cols = (string[])arrCols.ToArray(typeof(string));
                if (!cb.BuildInsert(TableName, objData, cols))
                    SetMessage(cb);
                Da = cb.Da;

                if (cb.Update(objData))
                {
                    //Khi row=1 thi gan autoincrement cho row (ko add cho truong hop nhieu rows)
                    //Gan key cho nhung field AutoIncrement
                    if (objData.Rows.Count == 1)
                    {
                        foreach (var t in objData.PrimaryKey)
                        {
                            if (!t.AutoIncrement) continue;
                            var id = cb.Get_IDENT_CURRENT(TableName);
                            if (id == -1)
                            {
                                SetMessage(cb);
                                return false;
                            }
                            else
                            {
                                var rowEdit = objData.Rows[0];
                                rowEdit.BeginEdit();
                                objData.Columns[t.ColumnName].ReadOnly = false;
                                rowEdit[t.ColumnName] = id;
                                rowEdit.AcceptChanges();
                                rowEdit.EndEdit();
                            }
                            break;
                        }
                    }
                    return true;
                }
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Thêm dữ liệu theo Rowstate. Unique Index/Constriant Violation(ErrorNumber: 2627)
        /// </summary>
        /// <param name="objData">Dữ liệu cần thêm</param>
        /// <returns></returns>
        public bool AddNewNonReturnKey(DataTable objData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                if (TableName == null)
                    TableName = objData.TableName;
                foreach (DataRow row in objData.Rows)
                {
                    if (row.RowState == DataRowState.Added) continue;
                    row.AcceptChanges();
                    row.SetAdded();
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                if (!cb.BuildInsertMultiRow(TableName))
                    SetMessage(cb);
                Da = cb.Da;

                if (cb.Update(objData))
                {
                    return true;
                }
                SetMessage(cb);
                return false;
            }
            catch (SqlException ex)
            {
                AddMessage("ERR-000000", ex.Message, ex.Number);
                return false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return false;
            }
            finally
            {
                CloseConnection(opened);
            }
        }

        /// <summary>
        /// Insert multirow có return lại key. Chỉ sử dụng khi table có primarykey và AutoIncrement trên primarykey
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public bool AddNewMultiRow(DataTable objData)
        {
            if (objData.PrimaryKey.Length == 0)
            {
                AddMessage("ERR-000000", "no primarykey", 0);
                return false;
            }
            var iAutoIncrement = 0;
            foreach (var t in objData.PrimaryKey)
            {
                if (t.AutoIncrement)
                {
                    iAutoIncrement++;
                }
            }
            if (iAutoIncrement <= 0)
            {
                AddMessage("ERR-000000", "primarykey non AutoIncrement", 0);
                return false;
            }


            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                if (TableName == null)
                    TableName = objData.TableName;
                //ObjData.AcceptChanges();
                foreach (DataRow row in objData.Rows)
                {
                    if (row.RowState == DataRowState.Added) continue;
                    row.AcceptChanges();
                    row.SetAdded();
                }
                var arrCols = new ArrayList();
                for (var i = 0; i < objData.Columns.Count; i++)
                {
                    //Neu cot tu tang thi ko dua vao menh de insert
                    if (objData.Columns[i].AutoIncrement)
                        continue;
                    arrCols.Add(objData.Columns[i].ColumnName);

                }
                var cols = (string[])arrCols.ToArray(typeof(string));
                if (!cb.BuildInsertMultiRow(TableName, objData, cols, objData.PrimaryKey[0].ColumnName))
                    SetMessage(cb);
                Da = cb.Da;
                using (var reader = cb.Da.InsertCommand.ExecuteReader())
                {
                    var dtreader = new DataTable();
                    dtreader.Load(reader);
                    for (var i = 0; i < dtreader.Rows.Count; i++)
                    {
                        var rowEdit = objData.Rows[i];
                        rowEdit.BeginEdit();
                        objData.Columns[objData.PrimaryKey[0].ColumnName].ReadOnly = false;
                        rowEdit[objData.PrimaryKey[0].ColumnName] = (int)dtreader.Rows[i][0];
                        rowEdit.AcceptChanges();
                        rowEdit.EndEdit();
                    }
                    //while (reader.Read())
                    //{
                    //    int id = (int)reader[ObjData.PrimaryKey[0].ColumnName];
                    //}
                }
                flag = true;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                flag = false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                flag = false;
            }
            finally
            {
                CloseConnection(opened);
            }
            return flag;
        }

        /// <summary>
        /// Lấy lên id tự tăng
        /// </summary>
        /// <param name="tableName">Tên bảng </param>
        /// <returns></returns>
        public int Get_IDENT_CURRENT(string tableName)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return -1;
                }
                var strSQL = "SELECT IDENT_CURRENT('" + tableName + "')";
                var cmd = new SqlCommand(strSQL, IConnect.GetConnected());
                if (CommandTimeout != -1)
                {
                    cmd.CommandTimeout = CommandTimeout;
                }
                if (IConnect.IsTrans())
                    cmd.Transaction = IConnect.GetTransaction();
                var result = cmd.ExecuteScalar();
                int index;
                return int.TryParse(result.ToString(), out index) ? index : 0;
            }
            catch (SqlException ex)
            {
                AddMessage("ERR-000000", ex.Message, ex.Number);
                return -1;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return -1;
            }
            finally
            {
                CloseConnection(opened);
            }
        }
        #endregion

        #region Update (Ok)
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="objData">Dữ liệu cập nhật</param>
        /// <returns></returns>
        public bool UpdateChange(DataTable objData)
        {
            var opened = false;
            try
            {
                int num;
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                TableName = objData.TableName;
                foreach (DataRow row in objData.Rows)
                {
                    if (row.RowState == DataRowState.Modified) continue;
                    row.AcceptChanges();
                    row.SetModified();
                }
                var arrPrimarykey = new ArrayList();
                var arrCols = new ArrayList();
                foreach (var t in objData.PrimaryKey)
                {
                    arrPrimarykey.Add(t.ColumnName);
                }
                //Lấy cột update
                for (num = 0; num < objData.Columns.Count; num++)
                {
                    if (objData.Columns[num].AutoIncrement)
                        continue;
                    if (!arrPrimarykey.Contains(objData.Columns[num].ColumnName))
                        arrCols.Add(objData.Columns[num].ColumnName);
                }
                var keys = (string[])arrPrimarykey.ToArray(typeof(string));
                var cols = (string[])arrCols.ToArray(typeof(string));
                cb.BuildUpdate(TableName, objData, cols, keys);
                Da = cb.Da;
                if (cb.Update(objData))
                {
                    return true;
                }
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Cập nhật dữ liệu theo column
        /// </summary>
        /// <param name="objData">Dữ liệu cập nhật</param>
        /// <param name="columnsName">Column cần cập nhật. Nếu là Null-->cập nhật tất cả</param>
        /// <returns></returns>
        public bool UpdateChange(DataTable objData, params string[] columnsName)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                if (TableName == null)
                {
                    TableName = objData.TableName;
                }
                var keys = new string[objData.PrimaryKey.Length];
                var arrKeys = new ArrayList();
                for (var i = 0; i < objData.PrimaryKey.Length; i++)
                {
                    keys[i] = objData.PrimaryKey[i].ColumnName;
                    arrKeys.Add(objData.PrimaryKey[i].ColumnName);

                }
                var cols = columnsName;
                //Nếu không truyền column thì update tat ca column
                var arrCols = new ArrayList();
                if (cols == null || cols.Length == 0)
                {
                    for (var num = 0; num < objData.Columns.Count; num++)
                    {
                        if (objData.Columns[num].AutoIncrement)
                            continue;
                        if (!arrKeys.Contains(objData.Columns[num].ColumnName))
                            arrCols.Add(objData.Columns[num].ColumnName);
                    }
                    cols = (string[])arrCols.ToArray(typeof(string));
                }
                cb.BuildUpdate(TableName, objData, cols, keys);
                Da = cb.Da;
                if (cb.Update(objData))
                {
                    return true;
                }
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Cập nhật 1 dòng dữ liệu
        /// </summary>
        /// <param name="objDataRow">Dòng dữ liệu cần cập nhật</param>
        /// <param name="columnsName">Những trường cần cập nhật. Nếu là Null-->cập nhật tất cả</param>
        /// <returns></returns>
        public bool UpdateDirect(DataRow objDataRow, params string[] columnsName)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                if (TableName == null)
                {
                    TableName = objDataRow.Table.TableName;
                }
                var keys = new string[objDataRow.Table.PrimaryKey.Length];
                var arrKeys = new ArrayList();
                for (var i = 0; i < objDataRow.Table.PrimaryKey.Length; i++)
                {
                    keys[i] = objDataRow.Table.PrimaryKey[i].ColumnName;
                    arrKeys.Add(objDataRow.Table.PrimaryKey[i].ColumnName);
                }
                var cols = columnsName;
                //Nếu không truyền column thì update tat ca column
                if (cols == null || cols.Length == 0)
                {
                    var arrCols = new ArrayList();
                    for (var num = 0; num < objDataRow.Table.Columns.Count; num++)
                    {
                        if (objDataRow.Table.Columns[num].AutoIncrement)
                            continue;
                        if (!arrKeys.Contains(objDataRow.Table.Columns[num].ColumnName))
                            arrCols.Add(objDataRow.Table.Columns[num].ColumnName);
                    }
                    cols = (string[])arrCols.ToArray(typeof(string));
                }
                if (cb.ExecuteUpdateRow(TableName, objDataRow, cols, keys))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        #endregion

        #region Delete (Ok)

        /// <summary>
        /// Xóa dữ liệu. ForeignKey Violation(ErrorNumber: 547)
        /// </summary>
        /// <param name="objData">Dữ liệu cần xóa</param>
        /// <returns></returns>
        public bool Delete(DataTable objData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                if (TableName == null)
                {
                    TableName = objData.TableName;
                }
                foreach (DataRow row in objData.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;
                    row.AcceptChanges();
                    row.Delete();
                }
                var keys = new string[objData.PrimaryKey.Length];
                for (var i = 0; i < objData.PrimaryKey.Length; i++)
                {
                    keys[i] = objData.PrimaryKey[i].ColumnName;
                }
                cb.BuildDelete(TableName, keys);
                Da = cb.Da;
                if (cb.Update(objData))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Xóa dữ liệu. ForeignKey Violation(ErrorNumber: 547)
        /// </summary>
        /// <returns></returns>
        public bool DeleteDirect()
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                if (cb.ExecuteDelete(TableName, WhereClause, Params))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }
        #endregion

        #region Procedure (Ok)
        /// <summary>
        /// Thực thi 1 Procedure hay Funtion
        /// </summary>
        /// <param name="procedureName">Tên procedure</param>
        /// <returns></returns>
        public bool ExecuteProcedure(string procedureName)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;

                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                flag = cb.ExecuteProcedure(procedureName, Params);
                if (flag == false)
                    SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                flag = false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                flag = false;
            }
            finally
            {
                CloseConnection(opened);
            }
            return flag;
        }

        /// <summary>
        /// Fill dữ liệu từ 1 procedure
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="objsData"></param>
        /// <returns></returns>
        public bool FillDataFromProcedure(string procedureName, DataTable objsData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout, MaxRows = MaxRows, StartRow = StartRow };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                cb.BuildProcedure(procedureName, Params);
                if (cb.FillNonStruct(objsData))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }
        #endregion

        #region Execute Query (Ok)
        /// <summary>
        /// Fill dữ liệu từ câu sql.
        /// </summary>
        /// <param name="stringSQL"></param>
        /// <param name="objsData"></param>
        /// <returns></returns>
        public bool FillDataFromSQLQuery(string stringSQL, DataTable objsData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;

                var cb = new Adapter(IConnect) { MaxRows = MaxRows, StartRow = StartRow, CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                cb.BuildSQL(stringSQL, Params);
                if (cb.FillNonStruct(objsData))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }

        /// <summary>
        /// Thực thi câu sql
        /// </summary>
        /// <param name="stringSQL"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string stringSQL)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                flag = cb.ExecuteNonQuery(stringSQL, Params);
                if (flag == false)
                    SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                flag = false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                flag = false;
            }
            finally
            {
                CloseConnection(opened);
            }
            return flag;
        }

        /// <summary>
        /// Fill dữ liệu từ câu lệnh sql. có phân trang
        /// </summary>
        /// <param name="sqlBlock"></param>
        /// <param name="objsData"></param>
        /// <returns></returns>
        public bool FillDataFromSQLBlock(string sqlBlock, DataTable objsData)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;

                var cb = new Adapter(IConnect) { MaxRows = MaxRows, StartRow = StartRow, CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                cb.BuildSQL(sqlBlock, Params);
                if (cb.FillNonStruct(objsData))
                    return true;
                SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
            }
            finally
            {
                CloseConnection(opened);
            }
            return false;
        }
        /// <summary>
        /// Thực thi câu sql. Có dùng param
        /// </summary>
        /// <param name="sqlBlock"></param>
        /// <returns></returns>
        public bool ExecuteSQLBlock(string sqlBlock)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                var cb = new Adapter(IConnect) { CommandTimeout = CommandTimeout };
                var Params = (SqlParameters != null) ? ((SqlParameter[])SqlParameters.ToArray(typeof(SqlParameter))) : null;
                flag = cb.ExecuteNonQuery(sqlBlock, Params);
                if (flag == false)
                    SetMessage(cb);
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
                flag = false;
            }
            catch (Exception exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                flag = false;
            }
            finally
            {
                CloseConnection(opened);
            }
            return flag;
        }

        #endregion
    }
}
