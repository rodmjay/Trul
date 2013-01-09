using AutoMapper;
using Trul.Infrastructure.Crosscutting.Adapter;
using System.Linq.Expressions;
using System;

namespace Trul.Infrastructure.Crosscutting.NetFramework.Adapter
{
    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter
        : ITypeAdapter
    {
        #region ITypeAdapter Members

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);
        }

        #endregion
    }
}
