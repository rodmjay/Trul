using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework;

namespace Trul.Infrastructure.Crosscutting.Adapter
{
    public static class TypeAdapterFactory
    {
        #region Members

        static ITypeAdapterFactory _currentTypeAdapterFactory = null;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Set the current type adapter factory
        /// </summary>
        /// <param name="adapterFactory">The adapter factory to set</param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            _currentTypeAdapterFactory = adapterFactory;
        }

        /// <summary>
        /// Create a new type adapter from currect factory
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static ITypeAdapter CreateAdapter()
        {
            return _currentTypeAdapterFactory.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        public static ITranslatingExpressionVisitor CreateTranslatingExpressionVisitor<TFrom, TTo>()
        {
            return _currentTypeAdapterFactory.CreateTranslatingExpressionVisitor<TFrom, TTo>();
        }
        #endregion
    }
}
