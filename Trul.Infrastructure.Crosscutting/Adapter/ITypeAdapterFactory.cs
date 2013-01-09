using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework;

namespace Trul.Infrastructure.Crosscutting.Adapter
{
    /// <summary>
    /// Base contract for adapter factory
    /// </summary>
    public interface  ITypeAdapterFactory
    {
        /// <summary>
        /// Create a type adater
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <returns></returns>
        ITranslatingExpressionVisitor CreateTranslatingExpressionVisitor<TFrom, TTo>();
    }
}
