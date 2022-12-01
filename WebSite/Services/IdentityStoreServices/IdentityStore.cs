using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Services.IdentityStoreServices
{
    public class IdentityStore<TEntity> where TEntity : class
    {

        
        public DbContext Context { get; private set; }

        public DbSet<TEntity> DbEntitySet { get; private set; }
        public IdentityStore(DbContext context)
        {
            Context = context;

            DbEntitySet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> EntitySet
        {
            get { return DbEntitySet; }
        }

        public virtual async  Task<TEntity> GetByIdAsync(object id)
        {
            return await  DbEntitySet.FindAsync(id);
        }

        public virtual void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
