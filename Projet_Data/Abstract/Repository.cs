using Microsoft.EntityFrameworkCore;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Abstract
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DataContext dbContext;
        public Repository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddEntity(T entity)
        {
            // provide User for collection in Dbcontext : 
            await dbContext.Set<T>().AddAsync(entity);
            // save
            await dbContext.SaveChangesAsync();

            return true;

        }        
        public async Task<bool> DeleteEntity(T entity)
        {
            
            if(entity == null)
            {
                return false;
            }
            else
            {
                dbContext.Remove<T>(entity);
                await dbContext.SaveChangesAsync();
                return true;
            
            }
        }

        public async Task<T> GetEntity(object id)
        {
            var entity = await dbContext.FindAsync<T>(id);
            return entity;
        }

        public async Task<bool> AddListEntity(List<T> list_entity)
        {
            try
            {
                await dbContext.Set<T>().AddRangeAsync(list_entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateListEntity(List<T> list_entity)
        {
            try
            {
                // Save changes to the database
                dbContext.UpdateRange(list_entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<T>>GetAllEntity()
        {
            var entity = await dbContext.Set<T>().ToListAsync();
            return entity;
        }

        public async Task<bool> UpdateEntity(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                dbContext.Update(entity);  // Mark the entity as modified
                await dbContext.SaveChangesAsync();  // Save changes to the database
                return true;
            }
        }
    }

}




