using System;
using System.Linq;
using AutoMapper;
using Trul.Infrastructure.Crosscutting.Adapter;
using Trul.Framework;

namespace Trul.Infrastructure.Crosscutting.NetFramework.Adapter
{
    public class AutomapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter();
        }

        #endregion

        public ITranslatingExpressionVisitor CreateTranslatingExpressionVisitor<TFrom, TTo>()
        {
            return new AutomapperTranslatingExpressionVisitor<TFrom, TTo>();
        }
    }
}
