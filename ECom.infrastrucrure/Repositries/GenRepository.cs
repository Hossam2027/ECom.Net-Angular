using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECom.infrastrucrure.Repositries
{
    public class GenRepository<T> : IGenericRepository<T> where T : class
    {
        public AppDbContext AppDbContext { get; }
        public GenRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }



        public async Task AddAsync(T entity)
        {
           await AppDbContext.Set<T>().AddAsync(entity);
            await AppDbContext.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var entity = await AppDbContext.Set<T>().FindAsync(id);
            AppDbContext.Set<T>().Remove(entity);
            await AppDbContext.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await AppDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll(params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = AppDbContext.Set<T>();
           
                foreach (var item in include)
                {
                    query = query.Include(item);
                }            

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await AppDbContext.Set<T>().FindAsync(id);

        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = AppDbContext.Set<T>();

          
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            var entity = await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return entity;

        }

  

        

        public async Task UpdateAsync(T entity)
        {
         AppDbContext.Entry(entity).State = EntityState.Modified;
             await AppDbContext.SaveChangesAsync();
        }
    }
}
