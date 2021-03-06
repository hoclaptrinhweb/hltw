using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// 
    /// </summary>
    public class Adapter : ClassBase
    {

        /// <summary>
        /// 
        /// </summary>
        public SqlDataAdapter Da;
        /// <summary>
        /// 
        /// </summary>
        public bool Distinct = false;
        /// <summary>
        /// cho phép phân trang sử dụng rownumber
        /// </summary>
        public bool RowNumber = true;
        /// <summary>
        /// Dòng bắt đầu
        /// </summary>
        public int StartRow = 0;
        /// <summary>
        /// Tổng số dòng trả về
        /// </summary>
        public int MaxRows = 0;
        /// <summary>
        /// Thời gian execute sqlcommand. unlimit(0),default(30s)
        /// </summary>
        public int CommandTimeout = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connect"></param>
        public Adapter(Connection connect)
        {
            IConnect = connect;
            Da = new SqlDataAdapter();
        }

        /// <summary>
        /// Xây dựng câu select
        /// </summary>
        /// <param name="top"></param>
        /// <param name="cols"></param>
        /// <param name="tableName"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="groupByClause"></param>
        /// <param name="havingClause"></param>
        /// <param name="Params"></param>
        public bool BuildSelect(int top, string[] cols, string tableName, string whereClause, string orderByClause, string groupByClause, string havingClause, params SqlParameter[] Params)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var commandText = "SELECT ";
                if (StartRow == 0 && MaxRows == 0)
                {
                    commandText += Distinct ? " Distinct " : "";
                    commandText += top != 0 ? " Top(" + top + ") " : "";
                    commandText += string.Join(",", cols);
                    commandText += " FROM " + tableName;
                    commandText += whereClause != null ? " WHERE " + whereClause : "";
                    commandText += groupByClause != null ? " GROUP BY " + groupByClause : "";
                    commandText += havingClause != null ? " HAVING " + havingClause : "";
                    commandText += orderByClause != null ? " ORDER BY " + orderByClause : "";
                }
                else
                {
                    commandText += Distinct ? " Distinct " : "";
                    commandText += top != 0 ? " Top(" + top + ") " : "";
                    commandText += " ROW_NUMBER() OVER ( " + (orderByClause != null ? " ORDER BY " + orderByClause : "") + ") AS Row,";
                    commandText += string.Join(",", cols);
                    commandText += " FROM " + tableName;
                    commandText += whereClause != null ? " WHERE " + whereClause : "";
                    commandText += groupByClause != null ? " GROUP BY " + groupByClause : "";
                    commandText += havingClause != null ? " HAVING " + havingClause : "";

                    commandText = "SELECT " + string.Join(",", cols) + " FROM (" + commandText + ") AS LogWithRowNumbers " +
                                  " WHERE Row >= " + StartRow + " AND Row <= " + (StartRow + MaxRows).ToString();
                }

                if (Params != null)
                    foreach (var op in Params)
                    {
                        command.Parameters.Add(op);
                    }
                command.CommandText = commandText;
                command.Connection = IConnect.GetConnected();
                command.CommandType = CommandType.Text;
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.SelectCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu truy vấn procedure
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public bool BuildProcedure(string procedureName, params SqlParameter[] Params)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                if (Params != null)
                {
                    foreach (var parameter in Params)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.SelectCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu truy vấn SQL
        /// </summary>
        /// <param name="stringSQL"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public bool BuildSQL(string stringSQL, params SqlParameter[] Params)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                if (Params != null)
                {
                    foreach (SqlParameter parameter in Params)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.CommandText = stringSQL;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.SelectCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu insert
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataTableSource"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public bool BuildInsert(string tableName, DataTable dataTableSource, string[] cols)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strInsert = ("INSERT INTO " + tableName + "(" + string.Join(",", cols) + ") VALUES (") + "@" + string.Join(",@", cols) + ") ";
                foreach (var t in cols)
                {
                    var param = new SqlParameter
                        {
                            ParameterName = "@" + t,
                            SourceColumn = t,
                            Direction = ParameterDirection.Input,
                            SqlDbType = ConvertType.ToSqlDbType(dataTableSource.Columns[t].DataType)
                        };
                    command.Parameters.Add(param);
                }
                command.CommandText = strInsert;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.InsertCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu insert nhiều row theo Command Builder. Không return lại id
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool BuildInsertMultiRow(string tableName)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strSQL = "Select * from " + tableName;
                command.CommandText = strSQL;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da = new SqlDataAdapter(command);
                var br = new SqlCommandBuilder(Da);
                Da.InsertCommand = br.GetInsertCommand();
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu insert nhiều row theo Command Builder. Có return lại id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataTableSource"></param>
        /// <param name="cols"></param>
        /// <param name="colsAutoIncrement"></param>
        /// <returns></returns>
        public bool BuildInsertMultiRow(string tableName, DataTable dataTableSource, string[] cols, string colsAutoIncrement)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();

                var strInsert = "DECLARE @InsertedRows TABLE (" + colsAutoIncrement + " " + ConvertType.ToSqlDbType(dataTableSource.Columns[colsAutoIncrement].DataType).ToString() + ");";
                strInsert += "INSERT " + tableName + "(" + string.Join(",", cols) + ") ";
                strInsert += "OUTPUT inserted." + colsAutoIncrement + " INTO @InsertedRows ";
                strInsert += "VALUES";
                var values = "";
                for (var j = 0; j < dataTableSource.Rows.Count; j++)
                {
                    values += "(" + "@" + j.ToString(CultureInfo.InvariantCulture) + string.Join((",@" + j.ToString(CultureInfo.InvariantCulture)), cols) + "),";
                    foreach (var t in cols)
                    {
                        var param = new SqlParameter
                            {
                                ParameterName = "@" + j + t,
                                Value = dataTableSource.Rows[j][t],
                                Direction = ParameterDirection.Input,
                                SqlDbType = ConvertType.ToSqlDbType(dataTableSource.Columns[t].DataType)
                            };
                        command.Parameters.Add(param);
                    }
                }
                values = values.Substring(0, values.Length - 1);
                strInsert = strInsert + values + "; select * from @InsertedRows;";
                command.CommandText = strInsert;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.InsertCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu lệnh update
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="cols"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool BuildUpdate(string tableName, DataTable dt, string[] cols, string[] keys)
        {
            try
            {
                int num;
                SqlParameter parameter;
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strArray = new string[cols.Length];
                var strArray2 = new string[keys.Length];
                for (num = 0; num < cols.Length; num++)
                {
                    strArray[num] = cols[num].Trim() + "=@" + cols[num];
                }
                for (num = 0; num < keys.Length; num++)
                {
                    strArray2[num] = keys[num].Trim() + " = @Original_" + keys[num];
                }
                var str = ("UPDATE " + tableName + " SET " + string.Join(",", strArray)) + ((" WHERE " + string.Join(" AND ", strArray2)));
                foreach (var str2 in cols)
                {
                    parameter = new SqlParameter
                        {
                            ParameterName = "@" + str2,
                            Direction = ParameterDirection.Input,
                            SqlDbType = ConvertType.ToSqlDbType(dt.Columns[str2.Trim()].DataType),
                            SourceVersion = DataRowVersion.Current,
                            SourceColumn = str2
                        };
                    command.Parameters.Add(parameter);
                }
                foreach (var str3 in keys)
                {
                    parameter = new SqlParameter
                        {
                            ParameterName = "@Original_" + str3,
                            Direction = ParameterDirection.Input,
                            SqlDbType = ConvertType.ToSqlDbType(dt.Columns[str3.Trim()].DataType),
                            SourceVersion = DataRowVersion.Original,
                            SourceColumn = str3
                        };
                    command.Parameters.Add(parameter);
                }
                command.CommandText = str;
                command.UpdatedRowSource = UpdateRowSource.Both;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.UpdateCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Xây dựng câu sql xóa dữ liệu
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool BuildDelete(string tableName, string[] keys)
        {
            try
            {
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strArray = new string[keys.Length];
                for (var i = 0; i < strArray.Length; i++)
                {
                    strArray[i] = keys[i].Trim() + " = @Original_" + keys[i].Trim();
                }
                var str = "DELETE " + tableName + ((" WHERE " + string.Join(" AND ", strArray)));
                foreach (var str2 in keys)
                {
                    var param = new SqlParameter
                        {
                            ParameterName = "@Original_" + str2.Trim(),
                            Direction = ParameterDirection.Input,
                            SourceVersion = DataRowVersion.Original,
                            SourceColumn = str2.Trim()
                        };
                    command.Parameters.Add(param);
                }
                command.CommandText = str;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                Da.DeleteCommand = command;
                return true;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return false;
            }
        }

        /// <summary>
        /// Đổ dữ liệu bào DataTableSource [Phân trang]
        /// </summary>
        /// <param name="dataTableSource"></param>
        /// <returns></returns>
        public bool Fill(DataTable dataTableSource)
        {

            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                if (dataTableSource != null)
                {
                    if (StartRow == 0 && MaxRows == 0)
                        Da.Fill(dataTableSource);
                    else
                    {
                        // => Dữ liệu lớn khuyên dùng sử dụng rownumber
                        if (RowNumber)
                            Da.Fill(dataTableSource);
                        else
                            Da.Fill(StartRow, MaxRows, dataTableSource); // hàm này sẽ chậm nếu số lượng dữ liệu lớn
                    }
                    Da.SelectCommand.Parameters.Clear();
                    Da.Dispose();
                    return true;
                }
                return false;
            }
            catch (SqlException exception)
            {
                AddMessage("ERR-000000", exception.Message, exception.Number);
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
        /// Fill dữ liệu từ Procedure hoặc câu sQL
        /// </summary>
        /// <param name="dataTableSource"></param>
        /// <returns></returns>
        public bool FillNonStruct(DataTable dataTableSource)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                if (dataTableSource != null)
                {
                    if (StartRow == 0 && MaxRows == 0)
                        Da.Fill(dataTableSource);
                    else
                        Da.Fill(StartRow, MaxRows, dataTableSource);
                    Da.SelectCommand.Parameters.Clear();
                    Da.Dispose();
                    return true;
                }
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
        /// Thực thi cập nhật dữ liệu (Ok)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="row"></param>
        /// <param name="cols"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool ExecuteUpdateRow(string tableName, DataRow row, string[] cols, string[] keys)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                    return false;
                int num;
                SqlParameter parameter;
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strArray = new string[cols.Length];
                var strArray2 = new string[keys.Length];
                for (num = 0; num < cols.Length; num++)
                {
                    strArray[num] = cols[num].Trim() + "=@" + cols[num].Trim();
                }
                for (num = 0; num < keys.Length; num++)
                {
                    strArray2[num] = keys[num].Trim() + " = @Original_" + keys[num].Trim();
                }
                var str = ("UPDATE " + tableName + " SET " + string.Join(",", strArray)) + ((" WHERE " + string.Join(" AND ", strArray2)));
                foreach (var str2 in cols)
                {
                    parameter = new SqlParameter
                        {
                            ParameterName = "@" + str2.Trim(),
                            Direction = ParameterDirection.Input,
                            SourceVersion = DataRowVersion.Current,
                            Value = row[str2.Trim()],
                            SqlDbType = ConvertType.ToSqlDbType(row.Table.Columns[str2.Trim()].DataType)
                        };
                    command.Parameters.Add(parameter);
                }
                foreach (var str3 in keys)
                {
                    parameter = new SqlParameter
                        {
                            ParameterName = "@Original_" + str3.Trim(),
                            Direction = ParameterDirection.Input,
                            SourceVersion = DataRowVersion.Original,
                            Value = row[str3.Trim()],
                            SqlDbType = ConvertType.ToSqlDbType(row.Table.Columns[str3.Trim()].DataType)
                        };
                    command.Parameters.Add(parameter);
                }
                command.CommandText = str;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                var num2 = command.ExecuteNonQuery();
                flag = num2 >= 0;
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
        /// Thực thi lệnh xóa dữ liệu
        /// </summary>
        /// <param name="tableName">Tên table</param>
        /// <param name="whereClause">điều kiện where</param>
        /// <param name="Params">tham số</param>
        /// <returns></returns>
        public bool ExecuteDelete(string tableName, string whereClause, params SqlParameter[] Params)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var command = new SqlCommand();
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                var strDelete = "DELETE FROM " + tableName + " " + ((whereClause != null) ? ("WHERE " + whereClause) : "");
                if (Params != null)
                    foreach (var op in Params)
                    {
                        command.Parameters.Add(op);
                    }
                command.CommandText = strDelete;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                var num = command.ExecuteNonQuery();
                flag = num >= 0;
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
        /// Thực thi câu lệnh SQL
        /// </summary>
        /// <param name="stringSQL">Chuổi SQL cần thực thi</param>
        /// <param name="Params">Tham số</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string stringSQL, params SqlParameter[] Params)
        {

            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var command = new SqlCommand();
                if (Params != null)
                {
                    foreach (var parameter in Params)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                command.CommandText = stringSQL;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                var num = command.ExecuteNonQuery();
                flag = num >= 0;
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
        /// Thực thi Procedure
        /// </summary>
        /// <param name="procedureName">Tên procedure</param>
        /// <param name="Params">Tham số</param>
        /// <returns></returns>
        public bool ExecuteProcedure(string procedureName, params SqlParameter[] Params)
        {
            bool flag;
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                var command = new SqlCommand();
                if (Params != null)
                {
                    foreach (var parameter in Params)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                if (IConnect.IsTrans())
                    command.Transaction = IConnect.GetTransaction();
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = IConnect.GetConnected();
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                var num = command.ExecuteNonQuery();
                flag = num >= 0;
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
        /// Thực thi câu lệnh. trả về DataReader
        /// </summary>
        /// <param name="top"></param>
        /// <param name="selectClause"></param>
        /// <param name="tableName"></param>
        /// <param name="whereClause"></param>
        /// <param name="orderByClause"></param>
        /// <param name="groupByClause"></param>
        /// <param name="havingClause"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(int top, string selectClause, string tableName, string whereClause, string orderByClause, string groupByClause, string havingClause, params SqlParameter[] Params)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return null;
                }
                var command = new SqlCommand();
                var commandText = "SELECT ";

                commandText += Distinct ? " Distinct " : "";
                commandText += top != 0 ? " Top(" + top + ") " : "";
                if (selectClause.Trim().ToLower() == "count(*)")
                    commandText += "*";
                else
                    commandText += selectClause;
                commandText += " FROM " + tableName;
                commandText += whereClause != null ? " WHERE " + whereClause : "";
                commandText += orderByClause != null ? " ORDER BY " + orderByClause : "";
                commandText += groupByClause != null ? " GROUP BY " + groupByClause : "";
                commandText += havingClause != null ? " HAVING " + havingClause : "";
                if (selectClause.Trim().ToLower() == "count(*)")
                    commandText = "SELECT count(*) from (" + commandText + ") t";
                if (Params != null)
                    foreach (var op in Params)
                    {
                        command.Parameters.Add(op);
                    }
                command.CommandText = commandText;
                command.Connection = IConnect.GetConnected();
                command.CommandType = CommandType.Text;
                if (CommandTimeout != -1)
                    command.CommandTimeout = CommandTimeout;
                var reader = command.ExecuteReader();
                return reader;
            }
            catch (SqlException exception2)
            {
                AddMessage("ERR-000000", exception2.Message, 0);
                return null;
            }
            catch (Exception exception)
            {
                AddMessage("ERR-000000", exception.Message, 0);
                return null;
            }
            finally
            {
                CloseConnection(opened);
            }
        }

        /// <summary>
        /// Gọi hàm Insert, Update, Delete
        /// </summary>
        /// <param name="dataTableSource">Dữ liệu cần cập nhật</param>
        /// <returns></returns>
        public bool Update(DataTable dataTableSource)
        {
            var opened = false;
            try
            {
                if (!OpenConnection(ref opened))
                {
                    return false;
                }
                if (dataTableSource != null)
                {
                    Da.Update(dataTableSource);
                    dataTableSource.AcceptChanges();
                    Da.Dispose();
                    return true;
                }
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
        /// Lấy lên id tự tăng
        /// </summary>
        /// <param name="tableName"></param>
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
    }
}
