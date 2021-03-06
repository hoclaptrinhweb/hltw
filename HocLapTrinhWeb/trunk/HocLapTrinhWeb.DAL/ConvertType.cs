using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace HocLapTrinhWeb.DAL
{

    /// <summary>
    /// Convert a base data type to another base data type
    /// </summary>
    public sealed class TypeConvertor
    {

        private struct DbTypeMapEntry
        {
            public Type Type;
            public DbType DbType;
            public SqlDbType SqlDbType;

            public DbTypeMapEntry(Type type, DbType dbType, SqlDbType sqlDbType)
            {
                Type = type;
                DbType = dbType;
                SqlDbType = sqlDbType;
            }
        };

        private static ArrayList _DbTypeList = new ArrayList();

        #region Constructors

        static TypeConvertor()
        {

            var dbTypeMapEntry = new DbTypeMapEntry(typeof(bool), DbType.Boolean, SqlDbType.Bit);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(byte), DbType.Double, SqlDbType.TinyInt);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(byte[]), DbType.Binary, SqlDbType.Image);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(DateTime), DbType.DateTime, SqlDbType.DateTime);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(Decimal), DbType.Decimal, SqlDbType.Decimal);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(double), DbType.Double, SqlDbType.Float);

            _DbTypeList.Add(dbTypeMapEntry);
            dbTypeMapEntry = new DbTypeMapEntry(typeof(Guid), DbType.Guid, SqlDbType.UniqueIdentifier);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(Int16), DbType.Int16, SqlDbType.SmallInt);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(Int32), DbType.Int32, SqlDbType.Int);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(Int64), DbType.Int64, SqlDbType.BigInt);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(object), DbType.Object, SqlDbType.Variant);

            _DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof(string), DbType.String, SqlDbType.VarChar);

            _DbTypeList.Add(dbTypeMapEntry);


        }
        private TypeConvertor()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Convert db type to .Net data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type ToNetType(DbType dbType)
        {
            var entry = Find(dbType);
            return entry.Type;
        }


        /// <summary>
        /// Convert TSQL type to .Net data type
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static Type ToNetType(SqlDbType sqlDbType)
        {
            var entry = Find(sqlDbType);
            return entry.Type;
        }

        /// <summary>
        /// Convert .Net type to Db type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            var entry = Find(type);
            return entry.DbType;
        }

        /// <summary>
        /// Convert TSQL data type to DbType
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static DbType ToDbType(SqlDbType sqlDbType)
        {
            var entry = Find(sqlDbType);
            return entry.DbType;
        }


        /// <summary>
        /// Convert .Net type to TSQL data type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(Type type)
        {
            var entry = Find(type);
            return entry.SqlDbType;
        }


        /// <summary>
        /// Convert DbType type to TSQL data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(DbType dbType)
        {
            var entry = Find(dbType);
            return entry.SqlDbType;
        }


        private static DbTypeMapEntry Find(Type type)
        {
            object retObj = null;
            foreach (var t in _DbTypeList)
            {
                var entry = (DbTypeMapEntry)t;
                if (entry.Type != type) continue;
                retObj = entry;
                break;
            }
            if (retObj == null)
            {
                throw
                new ApplicationException("Referenced an unsupported Type");
            }
            return (DbTypeMapEntry)retObj;
        }

        private static DbTypeMapEntry Find(DbType dbType)
        {
            object retObj = null;

            foreach (var t in _DbTypeList)
            {
                var entry = (DbTypeMapEntry)t;
                if (entry.DbType != dbType) continue;
                retObj = entry;
                break;
            }

            if (retObj == null)
            {
                throw new ApplicationException("Referenced an unsupported DbType");
            }
            return (DbTypeMapEntry)retObj;
        }

        private static DbTypeMapEntry Find(SqlDbType sqlDbType)
        {
            object retObj = null;

            foreach (var t in _DbTypeList)
            {
                var entry = (DbTypeMapEntry)t;
                if (entry.SqlDbType != sqlDbType) continue;
                retObj = entry;
                break;
            }

            if (retObj == null)
            {
                throw new ApplicationException("Referenced an unsupported SqlDbType");
            }
            return (DbTypeMapEntry)retObj;
        }

        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public static class ConvertType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(Type theType)
        {
            var param = new SqlParameter();
            var tc = System.ComponentModel.TypeDescriptor.GetConverter(param.DbType);
            if (tc.CanConvertFrom(theType))
            {
                var convertFrom = tc.ConvertFrom(theType.Name);
                if (convertFrom != null) param.DbType = (DbType)convertFrom;
            }
            else
            {
                // try to forcefully convert
                try
                {
                    var convertFrom = tc.ConvertFrom(theType.Name);
                    if (convertFrom != null) param.DbType = (DbType)convertFrom;
                }
                catch
                {
                    // ignore the exception
                }
            }
            return param.SqlDbType;
        }

        /// <summary>
        /// This function will populate the p_object passed in with the datarow passed in
        /// It is assumed that the datarow columns match exactly with the properties in the
        /// object passed in.
        /// </summary>
        /// <param name="p_dcc"></param>
        /// <param name="p_dr"></param>
        /// <param name="p_object"></param>
        public static void DataRowToObjectConvert(DataColumnCollection p_dcc, DataRow p_dr, Object p_object)
        {
            Type t = p_object.GetType(); //This is used to do the reflection
            for (Int32 i = 0; i <= p_dcc.Count - 1; i++)
            {
                //Don't ask it just works
                try
                {  //NOTE the datarow column names must match exactly (including case) to the object property names
                    t.InvokeMember(p_dcc[i].ColumnName, BindingFlags.SetProperty, null, p_object, new object[] { p_dr[p_dcc[i].ColumnName] });
                }
                catch (Exception ex)
                { //Usually you are getting here because a column doesn't exist or it is null
                    if (ex.ToString() != null)
                    { }
                }
            };//for i
        }


        /// <summary>
        /// This method takes in an object and by reflection takes the properties
        /// Creates a table if it doesn't already exist
        /// Adds a row to the table in a dataset.
        /// </summary>
        /// <param name="p_obj"></param>
        /// <param name="p_ds"></param>
        /// <param name="p_tableName"></param>
        public static void ObjectToTableConvert(Object p_obj, ref DataSet p_ds, String p_tableName)
        {
            //we need the type to figure out the properties
            Type t = p_obj.GetType();
            //Get the properties of our type
            PropertyInfo[] tmpP = t.GetProperties();

            //We need to create the table if it doesn't already exist
            if (p_ds.Tables[p_tableName] == null)
            {
                p_ds.Tables.Add(p_tableName);
                //Create the columns of the table based off the properties we reflected from the type
                foreach (PropertyInfo xtemp2 in tmpP)
                {
                    p_ds.Tables[p_tableName].Columns.Add(xtemp2.Name, xtemp2.PropertyType);
                } //foreach
            }
            //Now the table should exist so add records to it.

            Object[] tmpObj = new Object[tmpP.Length];

            for (Int32 i = 0; i <= tmpObj.Length - 1; i++)
            {
                //tmpObj[i] = tmpP[i].GetValue(p_obj,null);
                tmpObj[i] = t.InvokeMember(tmpP[i].Name, BindingFlags.GetProperty, null, p_obj, new object[0]);


            }
            //Add the row to the table in the dataset
            p_ds.Tables[p_tableName].LoadDataRow(tmpObj, true);
        }
    }

}

