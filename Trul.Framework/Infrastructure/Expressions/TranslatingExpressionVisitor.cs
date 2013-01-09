using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace Trul.Framework
{
    /// <summary>
    /// LINQ expressionlardaki bir tipi diğer bir tipe dönüştürür. Servis ve data layer arası 
    /// LINQ Expressionları parametre olarak geçerken dönüşümde kullanılır. bkz...
    /// </summary>
    /// <remarks>
    /// Sınıf tüm durumları kapsamaz, tiplerle ilgili exception verdiğinde ilgili 
    /// LINQ expressionını destekliyorsak, MutateType metoduna ilgili tip eklenerek sorun çözülEBİLİr.
    /// </remarks>
    /// <typeparam name="TFrom">The type of from.</typeparam>
    /// <typeparam name="TTo">The type of to.</typeparam>
    public abstract class TranslatingExpressionVisitor<TFrom, TTo> : TranslatingExpressionVisitor
    {
        public TranslatingExpressionVisitor()
            : base(typeof(TFrom), typeof(TTo))
        {
        }
    }

    public abstract class TranslatingExpressionVisitor :
        ExpressionVisitor,
        ITranslatingExpressionVisitor
    {
        protected List<ParameterExpression> _mutatedParameters;
        protected Type _fromType;
        protected Type _toType;

        public TranslatingExpressionVisitor(Type fromType, Type toType)
        {
            _mutatedParameters = new List<ParameterExpression>();
            _fromType = fromType;
            _toType = toType;
        }

        public virtual Expression Translate(Expression expression)
        {
            var abc = Visit(expression);
            return abc;
        }

        protected override Expression VisitLambda<T>(Expression<T> originalExpression)
        {
            return MutateLambdaExpression(( LambdaExpression )originalExpression, _mutatedParameters);
        }

        protected virtual LambdaExpression MutateLambdaExpression(LambdaExpression originalExpression, IList<ParameterExpression> mutatedParameters)
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

        protected virtual Type MutateType(Type originalType)
        {
            return MutateType(originalType, _fromType, _toType);
        }

        protected static Type MutateType(Type originalType, Type fromType, Type toType)
        {
            if ( null == originalType ) { return null; }

            if ( originalType == fromType )
            {
                return toType;
            }

            // Func
            // is generic?
            if ( originalType.IsGenericType )
            {
                Type genericFunc = typeof(Func<,>);
                var genericType = originalType.GetGenericTypeDefinition();
                if ( genericFunc == genericType )
                {
                    if ( originalType.GetGenericArguments()[0] == fromType )
                    {
                        // Func<T, a>
                        Type objectToFunc = genericFunc.MakeGenericType(toType, originalType.GetGenericArguments()[1]);
                        return objectToFunc;
                    }
                }
            }

            return originalType;
        }

        protected virtual MemberInfo MutateMember(MemberInfo originalMember, Type memberTargetType)
        {
            return MutateMember(originalMember, _fromType, _toType, memberTargetType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalMember"></param>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <param name="targetType"></param>
        /// <remarks>originalMember'dan ViewModelBase gibi bir base class gelebilir. Bu durumda farklı bir 
        /// ViewModel'in Id'si ile karşılaştırma yapıldığında, eşitliğin her iki tarafı da toType'a dönüştürülür. 
        /// Bu hatalı olduğu için targetType parametresine originalMember'ın eriştiği Type verilebilir.</remarks>
        /// <returns></returns>
        protected virtual MemberInfo MutateMember(MemberInfo originalMember, Type fromType, Type toType, Type memberTargetType)
        {
            if ( null == originalMember ) { return null; }

            /*
             * Authentication modellerimizde IIdentity kullanılıyor. IIdentity interfaceini aynen geçir.
             * */
            if ( originalMember.DeclaringType == typeof(System.Security.Principal.IIdentity) )
            {
                return originalMember;
            }
            else if ( memberTargetType != null && memberTargetType != fromType )
            {
                return originalMember;
            }
            else if ( originalMember.DeclaringType == fromType /* tip kendisi eşit */
                || fromType.GetInterfaces().Contains(originalMember.DeclaringType) /* interface inheritance yolu ile */
                || fromType.IsSubclassOf(originalMember.DeclaringType) /* inheritance yolu ile */)
            {
                var memberArray = toType.GetMember(GetMutatedMemberName(originalMember.Name), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if ( memberArray.Length == 0 )
                    throw new ConfigurationException(string.Format("Unmapped property name used in a LINQ expression. Cannot resolve property name: {0}.", originalMember.Name));

                return memberArray[0];
            }
            else
            {
                return originalMember;
            }
        }

        protected virtual string GetMutatedMemberName(string originalMemberName)
        {
            return originalMemberName;
        }

        // aynı isimde birden fazla parametre kullanmayın.
        protected virtual ParameterExpression MutateParameterExpression(ParameterExpression originalExpresion, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpresion ) { return null; }

            if ( mutatedParameters == null )
            {
                mutatedParameters = new List<ParameterExpression>();
            }

            var existing = mutatedParameters.Where(mp => mp.Type == _toType).FirstOrDefault();
            if ( existing != null )
            {
                return existing;
            }

            var param = Expression.Parameter(_toType, originalExpresion.Name);
            mutatedParameters.Add(param);

            return param;
        }

        protected virtual Expression MutateMemberExpression(MemberExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var newExpression = MutateExpression(originalExpression.Expression, mutatedParameters);

            MemberInfo newMember;
            var newExpressionMember = newExpression as MemberExpression;
            if ( newExpressionMember == null )
            {
                /* IPrimaryKey<> */
                if ( originalExpression.Expression.Type != _fromType
                    && _fromType.GetInterfaces().Contains(originalExpression.Expression.Type)
                    && newExpression.NodeType == ExpressionType.Convert )
                {
                    newMember = MutateMember(originalExpression.Member, _fromType);
                    newExpression = ( newExpression as UnaryExpression ).Operand;
                }
                else
                {
                    newMember = MutateMember(originalExpression.Member, originalExpression.Expression.Type);
                }
            }
            else
            {
                // u.User.Name == 1
                var pi = newExpressionMember.Member as PropertyInfo;
                if ( pi == null )
                {
                    throw new NotImplementedException();
                }

                var origMemberExp = originalExpression.Expression as MemberExpression;
                if ( origMemberExp == null )
                {
                    throw new NotImplementedException();
                }

                var piOrig = origMemberExp.Member as PropertyInfo;
                if ( piOrig == null )
                {
                    throw new NotImplementedException();
                }

                newMember = MutateMember(originalExpression.Member, piOrig.PropertyType, pi.PropertyType, originalExpression.Expression.Type);
            }

            /* Bu noktada newMember değeri hesaplanmıştır. */

            var isNewConstant = newExpression as ConstantExpression;
            if ( isNewConstant != null )
            {
                var mutatedConstant = MutateExpression(isNewConstant, mutatedParameters) as ConstantExpression;

                if ( mutatedConstant == null )
                {
                    /* mutate olan ConstantExpression tipinde olmalı. */
                    throw new NotSupportedException();
                }

                var piNew = newMember as PropertyInfo;
                if ( piNew != null )
                {
                    return Expression.Constant(piNew.GetValue(mutatedConstant.Value, null));
                }

                var fiNew = newMember as FieldInfo;
                if ( fiNew != null )
                {
                    return Expression.Constant(fiNew.GetValue(mutatedConstant.Value));
                }

                throw new NotSupportedException();
            }

            var ret = Expression.MakeMemberAccess(
                expression: newExpression,
                member: newMember
            );

            return ret;
        }

        protected virtual Expression MutateExpression(Expression originalExpression, IList<ParameterExpression> mutatedParameters)
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
            else if ( originalExpression is UnaryExpression )
            {
                ret = MutateUnaryExpression(( UnaryExpression )originalExpression, mutatedParameters);
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
            else
            {
                throw new NotImplementedException();
            }

            return ret;
        }

        private Expression MutateConditionalExpression(ConditionalExpression conditionalExpression, IList<ParameterExpression> mutatedParameters)
        {
            return conditionalExpression;
            /* TODO: LangConditionNormalizerdan gelen expressionlar için özel bir sınıf yazılabilir. */

            if ( null == conditionalExpression ) { return null; }

            var test = MutateExpression(conditionalExpression.Test, mutatedParameters);
            var ifTrue = MutateExpression(conditionalExpression.IfTrue, mutatedParameters);
            var ifFalse = MutateExpression(conditionalExpression.IfFalse, mutatedParameters);

            var ret = Expression.Condition(test, ifTrue, ifFalse);

            return ret;
        }

        protected virtual Expression MutateMethodCallExpression(MethodCallExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var obj = MutateExpression(originalExpression.Object, mutatedParameters);
            var method = ( MethodInfo )MutateMember(originalExpression.Method, originalExpression.Method.ReturnType);
            var f = from arg in originalExpression.Arguments
                    let mutatedArg = MutateExpression(arg, mutatedParameters)
                    select mutatedArg;

            var ret = Expression.Call(obj, method, f.ToArray());

            return ret;
        }

        protected virtual BinaryExpression MutateBinaryExpression(BinaryExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var newExprConversion = MutateExpression(originalExpression.Conversion, mutatedParameters);
            var newExprLambdaConversion = ( LambdaExpression )newExprConversion;
            var newExprLeft = MutateExpression(originalExpression.Left, mutatedParameters);
            var newExprRight = MutateExpression(originalExpression.Right, mutatedParameters);
            var newType = MutateType(originalExpression.Type);
            var newMember = MutateMember(originalExpression.Method, null);
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

        protected virtual UnaryExpression MutateUnaryExpression(UnaryExpression originalExpression, IList<ParameterExpression> mutatedParameters)
        {
            if ( null == originalExpression ) { return null; }

            var newOperand = MutateExpression(originalExpression.Operand, mutatedParameters);
            var newType = MutateType(originalExpression.Type);
            var newMember = MutateMember(originalExpression.Method, null);
            var newMethod = ( MethodInfo )newMember;

            var ret = Expression.MakeUnary(
                unaryType: originalExpression.NodeType,
                operand: newOperand,
                type: newType,
                method: newMethod
            );

            return ret;
        }

        protected virtual ConstantExpression MutateConstantExpression(ConstantExpression originalExpression, IList<ParameterExpression> parameterExpressions)
        {
            if ( null == originalExpression ) { return null; }

            // if type is compiler generated, get first property of the value object.
            ConstantExpression ret;

            var valueType = originalExpression.Value.GetType();

            bool isCompilerGenerated = valueType.IsDefined(typeof(CompilerGeneratedAttribute), false);
            if ( isCompilerGenerated )
            {
                var fields = valueType.GetFields();
                if ( fields != null
                    && fields.Where(f => f.FieldType == valueType).FirstOrDefault() != null )
                {
                    var newValue = fields[0].GetValue(originalExpression.Value);
                    var newType = MutateType(newValue.GetType());

                    ret = Expression.Constant(
                        value: newValue,
                        type: newType
                        );
                }
                else
                {
                    // desteklenmeyen yapı.
                    //throw new NotSupportedException();
                    ret = Expression.Constant(
                        type: MutateType(originalExpression.Type),
                        value: originalExpression.Value
                    );
                }
            }
            else
            {
                var newType = MutateType(originalExpression.Type);
                var newValue = originalExpression.Value;

                ret = Expression.Constant(
                    value: newValue,
                    type: newType
                );
            }

            return ret;
        }
    }
}
