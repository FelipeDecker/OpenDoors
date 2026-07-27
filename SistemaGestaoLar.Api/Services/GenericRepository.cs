using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Data;

namespace SistemaGestaoLar.Api.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity == null) return false;
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetQueryableNoTracking()
        {
            return _db.Set<T>().AsNoTracking();
        }
    }
}
