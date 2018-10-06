using MVCCodeFirst.Data.Contract;
using MVCCodeFirst.Data.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Repositories.Repository
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        protected DataContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public DataRepository(DataContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected virtual DbSet<T> GetDbSet<T>() where T : class
        {
            return this.DbContext.Set<T>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            this.DbContext.Entry(entity).State = entityState;
        }

        /// <summary>
        /// Get List of Type T
        /// </summary>
        /// <returns>IQueryable T</returns>
        public virtual IQueryable<T> GetAll()
        {
            return (IQueryable<T>)this.GetDbSet<T>();
        }

        /// <summary>
        /// Get Type T from id
        /// </summary>
        /// <returns>int id</returns>
        public virtual T GetById(int Id)
        {
            return (T)DbContext.Set(typeof(T)).Find(Id);
        }

        /// <summary>
        /// Add Type T to DbContext
        /// </summary>
        /// <param name="entity">Type T</param>
        public virtual void Add(T entity)
        {
            try
            {
                DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    this.DbContext.Set(typeof(T)).Add(entity);
                }
            }
            catch (Exception)
            { }

        }

        /// <summary>
        /// Update Type T To DBContext
        /// </summary>
        /// <param name="entity">Type T</param>
        public virtual void Update(T entity)
        {
            try
            {
                var entry = DbContext.Entry<T>(entity);

                // Retreive the Id through reflection
                object pkey;
                pkey = DbSet.Create().GetType().GetProperty("ID").GetValue(entity);

                if (entry.State == EntityState.Detached)
                {
                    var set = DbContext.Set<T>();
                    T attachedEntity = set.Find(pkey);  // You need to have access to key
                    if (attachedEntity != null)
                    {
                        var attachedEntry = DbContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified; // This should attach entity
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Delete Type T from DBContext
        /// </summary>
        /// <param name="entity">Type T</param>
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {

                this.DbContext.Set(typeof(T)).Attach(entity);
                this.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                DbContext.Set(typeof(T)).Attach(entity);

                DbContext.Set(typeof(T)).Remove(entity);
            }
        }

        /// <summary>
        /// Delete Type T from DBContext using id
        /// </summary>
        /// <param name="entity">int id</param>
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
    }
}
