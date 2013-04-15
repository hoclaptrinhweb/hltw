using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

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
    }

}

