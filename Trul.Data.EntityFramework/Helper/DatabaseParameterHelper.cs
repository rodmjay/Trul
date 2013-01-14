using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Trul.Framework;

namespace Trul.Data.EntityFramework.Helper
{
    public static class DatabaseParameterHelper
    {
        public static SqlParameter[] ToSqlParameters(this DatabaseParameter[] databaseParameters)
        {
            if (databaseParameters == null) return null;

            var result = new List<SqlParameter>();
            foreach (DatabaseParameter dbParameterItem in databaseParameters)
            {
                result.Add(dbParameterItem.ToSqlParameter());
            }
            return result.ToArray();
        }

        public static SqlParameter ToSqlParameter(this DatabaseParameter databaseParameter)
        {
            if (databaseParameter == null) return null;

            var sqlParameter = new SqlParameter();
            sqlParameter.DbType = databaseParameter.DbType;
            sqlParameter.Direction = databaseParameter.Direction;
            sqlParameter.IsNullable = databaseParameter.IsNullable;
            sqlParameter.ParameterName = databaseParameter.ParameterName;
            sqlParameter.Size = databaseParameter.Size;
            sqlParameter.SourceColumn = databaseParameter.SourceColumn;
            sqlParameter.SourceColumnNullMapping = databaseParameter.SourceColumnNullMapping;
            sqlParameter.SourceVersion = databaseParameter.SourceVersion;
            sqlParameter.Value = databaseParameter.Value;
            return sqlParameter;
        }
    }
}
