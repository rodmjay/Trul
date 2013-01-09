using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Trul.Framework
{
    /// <summary>
    /// LINQ expressionlardaki bir tipi diğer bir tipe dönüştürür. Servis ve data layer arası 
    /// LINQ Expressionları parametre olarak geçerken dönüşümde kullanılır. bkz...
    /// </summary>
    /// <remarks>
    /// Sadwce tek parametreli WHERE cümlelerindeki parametreyi değiştirmek için kullanılır.
    /// </remarks>
    /// <typeparam name="TFrom">The type of from.</typeparam>
    /// <typeparam name="TTo">The type of to.</typeparam>
    public class SingleParameterReplacingExpressionVisitor : ExpressionVisitor, ITranslatingExpressionVisitor
    {
        // TODO: bu sınıf çok daha basit olabilir. tek ihtiyaç MutateParameter metodunun gerektiğinde çağırılması.

        protected List<ParameterExpression> _mutatedParameters;
        protected ParameterExpression _newParameter;

        protected List<Type> _exceptTypes;

        public SingleParameterReplacingExpressionVisitor(ParameterExpression newParameter)
        {
            _mutatedParameters = new List<ParameterExpression>();
            _newParameter = newParameter;
            _exceptTypes = new List<Type>();
        }

        public SingleParameterReplacingExpressionVisitor(ParameterExpression newParameter, IEnumerable<Type> exceptTypes)
        {
            _mutatedParameters = new List<ParameterExpression>();
            _newParameter = newParameter;
            _exceptTypes = new List<Type>(exceptTypes);
        }

        public Expression Translate(Expression expression)
        {
            var abc = Visit(expression);
            return abc;
        }

        protected override Expression VisitLambda<T>(Expression<T> originalExpression)
        {
            return MutateLambdaExpression(( LambdaExpression )originalExpression, _mutatedParameters);
        }

        protected LambdaExpression MutateLambdaExpression(LambdaExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            var newParameters = ( from p in originalExpression.Parameters
                                  let np = MutateParameterExpression(p, mutatedParameters)
                                  select np ).ToArray();

            var newBody = MutateExpression(originalExpression.Body, mutatedParameters);

            var newType = MutateType(originalExpression.Type);

            var ret = Expression.Lambda(
                body: newBody,
                name: originalExpression.Name,
                delegateType: newType,
                tailCall: originalExpression.TailCall,
                parameters: newParameters
                );

            return ret;
        }

        protected Type MutateType(Type originalType)
        {
            return originalType;
        }

        protected MemberInfo MutateMember(MemberInfo originalMember)
        {
            return originalMember;
        }

        // aynı isimde birden fazla parametre kullanmayın.
        protected ParameterExpression MutateParameterExpression(ParameterExpression originalExpresion, IList<ParameterExpression> mutatedParameters)
        {
            if ( originalExpresion == null ) { return null; }

            if ( _exceptTypes.Contains(originalExpresion.Type) )
            {
                return originalExpresion;
            }

            return _newParameter;
        }

        protected Expression MutateMemberExpression(MemberExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var newExpression = MutateExpression(originalExpression.Expression, mutatedParameters);

            var newMember = MutateMember(originalExpression.Member);

            var ret = Expression.MakeMemberAccess(
                expression: newExpression,
                member: newMember
            );

            return ret;
        }

        protected Expression MutateExpression(Expression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            Expression ret = originalExpression;

            if ( null == originalExpression )
            {
                ret = null;
            }
            else if ( originalExpression is LambdaExpression )
            {
                ret = MutateLambdaExpression(( LambdaExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is BinaryExpression )
            {
                ret = MutateBinaryExpression(( BinaryExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is ParameterExpression )
            {
                ret = MutateParameterExpression(( ParameterExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is MemberExpression )
            {
                ret = MutateMemberExpression(( MemberExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is ConstantExpression )
            {
                ret = MutateConstantExpression(( ConstantExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is MethodCallExpression )
            {
                ret = MutateMethodCallExpression(( MethodCallExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is ConditionalExpression )
            {
                ret = MutateConditionalExpression(( ConditionalExpression )originalExpression, mutatedParameters);
            }
            else if ( originalExpression is UnaryExpression )
            {
                ret = MutateUnaryExpression(( UnaryExpression )originalExpression, mutatedParameters);
            }
            else
            {
                throw new NotImplementedException();
            }

            return ret;
        }

        private Expression MutateUnaryExpression(UnaryExpression unaryExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == unaryExpression ) { return null; }

            var expr = MutateExpression(unaryExpression.Operand, mutatedParameters);
            var ret = Expression.MakeUnary(unaryExpression.NodeType, expr, unaryExpression.Type, unaryExpression.Method);

            return ret;
        }

        private Expression MutateConditionalExpression(ConditionalExpression conditionalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == conditionalExpression ) { return null; }

            var test = MutateExpression(conditionalExpression.Test, mutatedParameters);
            var ifTrue = MutateExpression(conditionalExpression.IfTrue, mutatedParameters);
            var ifFalse = MutateExpression(conditionalExpression.IfFalse, mutatedParameters);

            var ret = Expression.Condition(test, ifTrue, ifFalse);

            return ret;
        }

        private Expression MutateMethodCallExpression(MethodCallExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var obj = MutateExpression(originalExpression.Object, mutatedParameters);
            var method = ( MethodInfo )MutateMember(originalExpression.Method);
            var f = from arg in originalExpression.Arguments
                    let mutatedArg = MutateExpression(arg, mutatedParameters)
                    select mutatedArg;

            var ret = Expression.Call(obj, method, f.ToArray());

            return ret;
        }

        protected BinaryExpression MutateBinaryExpression(BinaryExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var newExprConversion = MutateExpression(originalExpression.Conversion, mutatedParameters);
            var newExprLambdaConversion = ( LambdaExpression )newExprConversion;
            var newExprLeft = MutateExpression(originalExpression.Left, mutatedParameters);
            var newExprRight = MutateExpression(originalExpression.Right, mutatedParameters);
            var newType = MutateType(originalExpression.Type);
            var newMember = MutateMember(originalExpression.Method);
            var newMethod = ( MethodInfo )newMember;

            var ret = Expression.MakeBinary(
                binaryType: originalExpression.NodeType,
                left: newExprLeft,
                right: newExprRight,
                liftToNull: originalExpression.IsLiftedToNull,
                method: newMethod,
                conversion: newExprLambdaConversion
            );

            return ret;
        }

        protected ConstantExpression MutateConstantExpression(ConstantExpression originalExpression, IList<ParameterExpression> parameterExpressions)
        {
            if ( null == originalExpression ) { return null; }

            ConstantExpression ret;

            var newType = MutateType(originalExpression.Type);
            var newValue = originalExpression.Value;

            ret = Expression.Constant(
                value: newValue,
                type: newType
            );

            return ret;
        }
    }


}
