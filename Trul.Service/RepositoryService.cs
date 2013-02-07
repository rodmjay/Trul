using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Trul.Application;
using Trul.Domain.Core;
using Trul.Domain.Entities;
using Trul.Service.Core;
using Trul.Infrastructure.Crosscutting.Adapter;
using Trul.Framework;

namespace Trul.Service
{
    public class RepositoryService<TEntity, TDTOEntity, TId> : IRepositoryService<TDTOEntity, TId>
        where TEntity : class, IEntityWithTypedId<TId>, new()
        where TDTOEntity : class, IBaseObjectWithTypeId<TId>
    {
        protected IRepository<TEntity, TId> Repository;

        public RepositoryService(IRepository<TEntity, TId> repository)
        {
            Repository = repository;
        }

        public virtual void Add(TDTOEntity item)
        {
            Repository.Add(item.ProjectedAs<TEntity, TId>());
            Repository.UnitOfWork.Commit();
        }

        public virtual void Delete(TDTOEntity item)
        {
            Repository.Remove(item.ProjectedAs<TEntity, TId>());
            Repository.UnitOfWork.Commit();
        }

        public virtual void Update(TDTOEntity item)
        {
            Repository.Modify(item.ProjectedAs<TEntity, TId>());
            Repository.UnitOfWork.Commit();
        }

        public virtual TDTOEntity Get(TId id, params Expression<Func<TDTOEntity, object>>[] includes)
        {
            Expression<Func<TEntity, object>>[] translatedIncludes = null;
            if (includes != null)
            {
                translatedIncludes = includes.Select(inc => TranslateInclude<Func<TEntity, object>>(inc)).ToArray();
            }
            return Repository.Get(id, translatedIncludes).Single().ProjectedAs<TDTOEntity, TId>();
        }

        public virtual IList<TDTOEntity> GetAll(params Expression<Func<TDTOEntity, object>>[] includes)
        {
            Expression<Func<TEntity, object>>[] translatedIncludes = null;
            if (includes != null)
            {
                translatedIncludes = includes.Select(inc => TranslateInclude<Func<TEntity, object>>(inc)).ToArray();
            }
            return ((IEnumerable<IEntityWithTypedId<TId>>)Repository.GetAll(translatedIncludes)).ProjectedAsCollection<TDTOEntity, TId>();
        }

        public virtual IList<TDTOEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TDTOEntity, KProperty>> orderByExpression, bool ascending, params Expression<Func<TDTOEntity, object>>[] includes)
        {
            var translatedCondition = Translate<Func<TEntity, KProperty>>(orderByExpression);
            Expression<Func<TEntity, object>>[] translatedIncludes = null;
            if (includes != null)
            {
                translatedIncludes = includes.Select(inc => TranslateInclude<Func<TEntity, object>>(inc)).ToArray();
            }
            return ((IEnumerable<IEntityWithTypedId<TId>>)Repository.GetPaged<KProperty>(pageIndex, pageCount, translatedCondition, ascending, translatedIncludes)).ProjectedAsCollection<TDTOEntity, TId>();
        }

        public virtual IList<TDTOEntity> GetFiltered(Expression<Func<TDTOEntity, bool>> filter, params Expression<Func<TDTOEntity, object>>[] includes)
        {
            var translatedCondition = Translate<Func<TEntity, bool>>(filter);
            Expression<Func<TEntity, object>>[] translatedIncludes = null;
            if (includes != null)
            {
                translatedIncludes = includes.Select(inc => TranslateInclude<Func<TEntity, object>>(inc)).ToArray();
            }
            return ((IEnumerable<IEntityWithTypedId<TId>>)Repository.GetFiltered(translatedCondition, translatedIncludes)).ProjectedAsCollection<TDTOEntity, TId>();
        }

        #region Translation Helpers

        protected Expression Translate(Expression expression)
        {
            var translator = TypeAdapterFactory.CreateTranslatingExpressionVisitor<TDTOEntity, TEntity>();
            var translated = translator.Translate(expression);
            return translated;
        }

        protected Expression<T> Translate<T>(Expression expression)
        {
            var ret = Translate(expression);
            return (Expression<T>)ret;
        }

        protected Expression<T> TranslateInclude<T>(Expression expression)
        {
            var translator = TypeAdapterFactory.CreateTranslatingExpressionVisitor<TDTOEntity, TEntity>();
            var translated = translator.Translate(expression);
            return (Expression<T>)translated;
        }

        #endregion
    }
}
