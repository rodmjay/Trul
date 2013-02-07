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

        public override IQueryable<TEntity> Get(TId id, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.Get(id, includes).Where(m => !m.IsDeleted);
        }

        public override IQueryable<TEntity> AllMatching(Domain.Core.Specification.ISpecification<TEntity> specification, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.AllMatching(specification, includes).Where(e => !e.IsDeleted);
        }

        public override IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetFiltered(filter, includes).Where(m => !m.IsDeleted);
        }

        public new virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetAll(includes).Where(m => !m.IsDeleted);
        }

        public override void Remove(TEntity item)
        {
            item.IsDeleted = true;
            base.Modify(item);
        }
    }
}
