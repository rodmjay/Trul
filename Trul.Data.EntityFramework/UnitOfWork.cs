using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using Trul.Data.Core;
using Trul.Domain.Entities;
using Trul.Data.EntityFramework.Mapping;
using Trul.Domain.Core;
using Trul.Framework;

namespace Trul.Data.EntityFramework
{
    public class UnitOfWork
        : DbContext, IQueryableUnitOfWork
    {
        public UnitOfWork()
            : base("Db")
        {
            Database.SetInitializer<UnitOfWork>(null);
            base.Configuration.LazyLoadingEnabled = false;
        }

        #region IDbSet Members

        public IDbSet<Menu> Menus { get { return base.Set<Menu>(); } }

        public IDbSet<Person> Persons { get { return base.Set<Person>(); } }

        public IDbSet<Country> Countries { get { return base.Set<Country>(); } }

        public IDbSet<User> Users { get { return base.Set<User>(); } }

        public IDbSet<Role> Roles { get { return base.Set<Role>(); } }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IEntityWithTypedId<TId> GetContextEntity<TEntity, TId>(TEntity item) where TEntity : class, IEntityWithTypedId<TId>
        {
            var original = base.Set<TEntity>().Find(item.ID);
            return original;
        }

        public TEntity Attach<TEntity, TId>(TEntity item)
            where TEntity : class, IEntityWithTypedId<TId>
        {
            if (base.Entry<TEntity>(item).State == System.Data.EntityState.Detached)
            {
                item = base.Set<TEntity>().Find(item.ID);
            }
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = System.Data.EntityState.Unchanged;
            return item;
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = System.Data.EntityState.Unchanged;
        }

        public void SetModified<TEntity, TId>(TEntity item)
            where TEntity : class, IEntityWithTypedId<TId>, new()
        {
            if (base.Entry<TEntity>(item).State == System.Data.EntityState.Detached)
            {
                var oldItem = base.Set<TEntity>().Find(item.ID);
                ApplyCurrentValues<TEntity>(oldItem, item);
            }
            else
            {
                //this operation also attach item in object state manager
                base.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
            }
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }

        #region ISql Members

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new MenuEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PersonEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new CountryEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new RoleEntityTypeConfiguration());
        }
        #endregion
    }
}
