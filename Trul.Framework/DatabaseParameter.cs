using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Trul.Framework
{
    public class DatabaseParameter : DbParameter
    {
        public DatabaseParameter(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
            this.SourceVersion = DataRowVersion.Current;
        }

        public DatabaseParameter(string parameterName, DbType dbType)
            : this(parameterName, null)
        {
            this.DbType = dbType;
        }

        public DatabaseParameter(string parameterName, DbType dbType, int size)
            : this(parameterName, dbType)
        {
            this.Size = size;
        }

        public DatabaseParameter(string parameterName, DbType dbType, int size, string sourceColumn)
            : this(parameterName, dbType, size)
        {
            this.SourceColumn = sourceColumn;
        }

        public DatabaseParameter(string parameterName, DbType dbType, int size, string sourceColumn, ParameterDirection direction, bool isNullable, DataRowVersion sourceVersion, object value)
            : this(parameterName, dbType, size, sourceColumn)
        {
            this.Direction = direction;
            this.IsNullable = isNullable;
            this.SourceVersion = sourceVersion;
            this.Value = value;
        }

        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; }

        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }

        public override int Size { get; set; }

        public override string SourceColumn { get; set; }

        public override bool SourceColumnNullMapping { get; set; }

        public override DataRowVersion SourceVersion { get; set; }

        public override object Value { get; set; }
    }
}
