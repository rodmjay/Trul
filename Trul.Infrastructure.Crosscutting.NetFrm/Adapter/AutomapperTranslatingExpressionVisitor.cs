using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using Trul.Framework;

namespace Trul.Infrastructure.Crosscutting.NetFramework.Adapter
{
    public class AutomapperTranslatingExpressionVisitor<TFrom, TTo> : AutomapperTranslatingExpressionVisitor
    {
        public AutomapperTranslatingExpressionVisitor()
            : base(typeof(TFrom), typeof(TTo))
        {
        }
    }

    public class AutomapperTranslatingExpressionVisitor : TranslatingExpressionVisitor
    {
        protected TypeMap TypeMap { get; set; }
        protected List<Tuple<string, string>> PropertyMap;

        public AutomapperTranslatingExpressionVisitor(Type fromType, Type toType)
            : base(fromType, toType)
        {
            TypeMap = Mapper.FindTypeMapFor(fromType, toType);
            if (TypeMap != null)
            {
                var matchingSourceNames = TypeMap.GetPropertyMaps()
                    .Where(pm => pm.DestinationProperty.Name == pm.SourceMember.Name)
                    .Select(pm => pm.SourceMember.Name);

                PropertyMap =
                    TypeMap.GetPropertyMaps()
                        .Where(pm => !matchingSourceNames.Contains(pm.SourceMember.Name))
                        .Where(pm => pm.DestinationProperty.Name != pm.SourceMember.Name)
                        .Select(pm => Tuple.Create(pm.DestinationProperty.Name, pm.SourceMember.Name))
                        .ToList();
            }
            else
            {
                PropertyMap = new List<Tuple<string, string>>();
            }
        }

        public string GetTranslatedMemberName(string originalMemberName)
        {
            return GetMutatedMemberName(originalMemberName);
        }

        protected override string GetMutatedMemberName(string originalMemberName)
        {
            if (PropertyMap == null)
            {
                return base.GetMutatedMemberName(originalMemberName);
            }

            var tuple = PropertyMap.FirstOrDefault(p => p.Item1 == originalMemberName);
            if (tuple == null)
            {
                return base.GetMutatedMemberName(originalMemberName);
            }

            return tuple.Item2;
        }

        protected override ConstantExpression MutateConstantExpression(ConstantExpression originalExpression, IList<ParameterExpression> parameterExpressions)
        {
            /* NOTE: base'deki metod çağrılmaz! */

            if (null == originalExpression) { return null; }

            // if type is compiler generated, get first property of the value object.
            ConstantExpression ret;

            var valueType = originalExpression.Value.GetType();

            bool isCompilerGenerated = valueType.IsDefined(typeof(CompilerGeneratedAttribute), false);
            if (isCompilerGenerated)
            {
                var fields = valueType.GetFields();
                if (fields != null
                    && fields.Where(f => f.FieldType == valueType).FirstOrDefault() != null)
                {
                    var newValue = fields[0].GetValue(originalExpression.Value);

                    if (newValue != null && newValue.GetType() == _fromType)
                    {
                        /* Constant değer map edilir. */
                        newValue = Mapper.Map(newValue, _fromType, _toType);
                    }

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

                if (newValue != null && newValue.GetType() == _fromType)
                {
                    /* Constant değer map edilir. */
                    newValue = Mapper.Map(newValue, _fromType, _toType);
                }

                ret = Expression.Constant(
                    value: newValue,
                    type: newType
                );
            }

            return ret;
        }

        private class FakeMemberAccessor : IMemberAccessor
        {
            // bu sınıfın sadece Name propertysi kullanılabilir.

            public string Name { get; set; }

            public void SetValue(object destination, object value)
            {
                throw new NotImplementedException();
            }

            public object GetValue(object source)
            {
                throw new NotImplementedException();
            }

            public System.Reflection.MemberInfo MemberInfo
            {
                get { throw new NotImplementedException(); }
            }



            public Type MemberType
            {
                get { throw new NotImplementedException(); }
            }

            public ResolutionResult Resolve(ResolutionResult source)
            {
                throw new NotImplementedException();
            }

            public object[] GetCustomAttributes(bool inherit)
            {
                throw new NotImplementedException();
            }

            public object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }
        }
    }
}
