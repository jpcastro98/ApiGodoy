using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiGodoy.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> GetBy(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }

}
