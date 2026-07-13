using TaskFlow.API.Data;

namespace TaskFlow.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskFlowDbContext _context;

        public UnitOfWork(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
