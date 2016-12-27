using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using WebDev.Data.Base;

namespace WebDev.Data.SqlCe.Base
{
    public class BaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected PersonDbContext Context
        {
            get { return (PersonDbContext)this.UnitOfWork; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            this.UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            this.Context.Entry(entity).State = entityState;
        }

        /// <summary>
        /// To be used for changing the proxies back to original POCO.
        /// Source: http://stackoverflow.com/questions/25770369/get-underlying-entity-object-from-entity-framework-proxy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxyObject"></param>
        /// <returns></returns>
        protected T UnProxy<T>(T proxyObject) where T : class
        {
            var proxyCreationEnabled = Context.Configuration.ProxyCreationEnabled;
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                T poco = Context.Entry(proxyObject).CurrentValues.ToObject() as T;
                return poco;
            }
            finally
            {
                Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            }
        }
    }
}
