using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class MoradorService
    {
        private readonly IGenericRepository<Morador> _repo;

        public MoradorService(IGenericRepository<Morador> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Morador>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Morador> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Morador> CreateAsync(Morador entity) => _repo.AddAsync(entity);
        public Task<Morador> UpdateAsync(Morador entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
