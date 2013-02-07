using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Framework.Rules
{
    public class Field : IField
    {
        public Field(object value)
        {
            Value = value;
        }

        public string FieldName { get; set; }

        public object Value { get; set; }
    }

    public class CompareField : ICompareField
    {
        public CompareField(object value1, object value2)
        {
            Value = value1;
            RightValue = value2;
        }

        public string FieldName { get; set; }

        public object Value { get; set; }

        public string RightFieldName { get; set; }

        public object RightValue { get; set; }
    }
}
