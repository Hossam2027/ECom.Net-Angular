using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public interface IGenericRepository <T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        //ID
        Task<T> GetByIdAsync(int id);
       Task <T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);
        //GetAll
       Task< IReadOnlyList <T>> GetAll();
        Task<IReadOnlyList<T>> GetAll(params Expression<Func<T, object>>[] include);

    }
}
