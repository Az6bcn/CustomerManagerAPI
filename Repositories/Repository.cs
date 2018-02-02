using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class //  where TEnity: class --> means the generic interface will be implemented as a generic class
    {
        protected readonly DbContext CustomContextForQuery;
        private DbSet<TEntity> _dbEntity;
        
        //accepts a database context instance and initializes the entity set variable:
        public Repository(DbContext context)
        {
            CustomContextForQuery = context;

            /*
            context.DbSet === context.Set<Table>, 
            Context.Set<Table> ->declares the table to work with straight away on which we can start performing action and queries.
            */
            
             _dbEntity = CustomContextForQuery.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbEntity.ToList();
        }

      
        public void addCustomerToDB(TEntity entity)
        {
             _dbEntity.Add(entity);
        }
       
    }
}
