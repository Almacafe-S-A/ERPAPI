using ERP.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace ERPAPI.Contexts
{
    public class appCore<TEntity> where TEntity : class
    {
        public ApplicationDbContext _context = null;
        private readonly ILogger _logger;
        public DbSet<TEntity> DBSet { get; set; }


        public appCore(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            DBSet = this._context.Set<TEntity>();
            appAuditor._context = context;
            appAuditor._logger = logger;
        }

        public virtual void SaveChanges() => this._context.SaveChangesAsync();


        public virtual void DisposeChanges() => this._context.Dispose();


        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderExpression = null)
        {
            List<TEntity> lst = new List<TEntity>();
            var l = this.GetQuery(predicate, orderExpression).AsQueryable();
            if (l == null)
                return null;

            return l;
        }

        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderExpression = null) //, Func<IAsyncEnumerable<TEntity>, IOrderedQueryable<TEntity>> orderExpression = null)
        {
            try
            {
                //throw new Exception("Exception controlada");
                IQueryable<TEntity> qry = this.DBSet;

                if (predicate != null)
                    qry = qry.Where(predicate);

                if (orderExpression != null)
                    return orderExpression(qry);

                return qry;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderExpression = null)
        {
            return Find(predicate, orderExpression).AsQueryable();
        }


        public virtual void Insert<T>(T entity) where T : class
        {
            try
            {
                DbSet<T> dbSet = this._context.Set<T>();
                dbSet.Add(entity);
                foreach (EntityEntry pEntry in _context.ChangeTracker.Entries())
                {
                    pEntry.State = EntityState.Added;
                    appAuditor.SetAuditoria(pEntry, "");
                }
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public virtual void Update<T>(T entity) where T : class
        {
            try
            {
                DbSet<T> dbSet = this._context.Set<T>();
                dbSet.Attach(entity);
                this._context.Entry(entity).State = EntityState.Modified;

                foreach (EntityEntry pEntry in _context.ChangeTracker.Entries())
                {
                    pEntry.State = EntityState.Modified;
                    appAuditor.SetAuditoria(pEntry, "");
                }
                SaveChanges();
            }
            catch (Exception ex)
            {
                DisposeChanges();
                throw ex;
            }

        }


        public virtual void Delete<T>(T entity) where T : class
        {
            try
            {
                DbSet<T> dbSet = this._context.Set<T>();
                if (this._context.Entry(entity).State == EntityState.Detached)
                    dbSet.Attach(entity);

                this._context.Entry(entity).State = EntityState.Modified;
                foreach (EntityEntry pEntry in _context.ChangeTracker.Entries())
                {
                    pEntry.State = EntityState.Deleted;
                    appAuditor.SetAuditoria(pEntry, "");
                }
                dbSet.Remove(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
