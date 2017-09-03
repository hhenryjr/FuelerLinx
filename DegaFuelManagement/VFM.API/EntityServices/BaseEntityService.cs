using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VFM.EDM;

namespace VFM.API.EntityServices
{
    public class BaseEntityService<T> where T : class
    {
        #region Protected Methods
        public virtual T Insert(T entity)
        {
            using (var context = new FuelManagementEntities())
            {
                context.Set(GetEntityType()).Add(entity);
                context.SaveChanges();
                return entity;
            }
        }

        public virtual T GetByKey(object key)
        {
            using (var context = new FuelManagementEntities())
            {
                return (T)context.Set(GetEntityType()).Find(key);
            }
        }

        public virtual T GetByKeySerializable(object key)
        {
            using (var context = new FuelManagementEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (T)context.Set(GetEntityType()).Find(key);
            }
        }

        public virtual List<T> GetList(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate, string include = "")
        {
            using (var context = new FuelManagementEntities())
            {
                if (string.IsNullOrEmpty(include))
                    return context.Set(GetEntityType()).AsQueryable().Cast<T>().Where(predicate).ToList();
                else
                    return context.Set(GetEntityType()).AsQueryable().Include(include).Cast<T>().Where(predicate).ToList();
            }
        }

        public virtual List<T> GetListSerializable(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string include = "")
        {
            using (var context = new FuelManagementEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                if (string.IsNullOrEmpty(include))
                    return context.Set(GetEntityType()).AsQueryable().Cast<T>().Where(predicate).ToList();
                else
                    return context.Set(GetEntityType()).AsQueryable().Include(include).Cast<T>().Where(predicate).ToList();
            }
        }

        protected virtual void DeleteByKey(object key)
        {
            using (var context = new FuelManagementEntities())
            {
                T entityToUpdate = (T)context.Set(GetEntityType()).Find(key);
                if (entityToUpdate == null)
                    return;
                context.Entry(entityToUpdate).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        protected virtual T UpdateByKey(T entity, object key)
        {
            using (var context = new FuelManagementEntities())
            {
                if (key == null || ((int)key) == 0)
                    return Insert(entity);
                context.Set(GetEntityType()).Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
        #endregion

        #region Private Methods
        private Type GetEntityType()
        {
            return typeof(T);
        }
        #endregion
    }
}
