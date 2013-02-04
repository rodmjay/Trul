using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Trul.Data.Core;
using Trul.Domain.Core;
using Trul.Framework;
using Trul.Framework.Helper;

namespace Trul.Data.EntityFramework
{
    public abstract class DelRepository<TEntity, TId> : Repository<TEntity, TId>
        where TEntity : class, IEntityWithTypedId<TId>, IDelEntity, new()
    {
        public DelRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override void Add(TEntity item)
        {
            item.IsDeleted = false;
            base.Add(item);
        }

        public override TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.Get(id, includes);
        }

        public override IEnumerable<TEntity> AllMatching(Domain.Core.Specification.ISpecification<TEntity> specification, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.AllMatching(specification, includes).Where(e => !e.IsDeleted);
        }

        public override IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {

            Expression<Func<TEntity, bool>> isDeletedCondition = vm => vm.IsDeleted == false;

            if (filter == null)
            {
                return base.GetFiltered(isDeletedCondition, includes);
            }

            var sprev = new SingleParameterReplacingExpressionVisitor(filter.Parameters.First());
            var translated = (Expression<Func<TEntity, bool>>)sprev.Translate(isDeletedCondition);

            var combinedWhereCondition = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(filter.Body, translated.Body),
                filter.Parameters);

            return base.GetFiltered(combinedWhereCondition, includes);
        }

        public new virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            Expression<Func<TEntity, bool>> isDeletedCondition = vm => vm.IsDeleted == false;
            return base.GetFiltered(isDeletedCondition, includes);
        }

        public override void Remove(TEntity item)
        {
            item.IsDeleted = true;
            base.Modify(item);
        }
    }
}
