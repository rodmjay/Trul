using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Trul.Framework
{
    public interface ITranslatingExpressionVisitor
    {
        Expression Translate(Expression expression);
    }
}
