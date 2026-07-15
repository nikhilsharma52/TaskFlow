using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskFlow.API.Data;

namespace TaskFlow.API.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {

        private readonly TaskFlowDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EfRepository(TaskFlowDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }    
    }
}
