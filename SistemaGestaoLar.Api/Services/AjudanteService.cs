using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class AjudanteService
    {
        private readonly IGenericRepository<Ajudante> _repo;

        public AjudanteService(IGenericRepository<Ajudante> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Ajudante>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Ajudante> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Ajudante> CreateAsync(Ajudante entity) => _repo.AddAsync(entity);
        public Task<Ajudante> UpdateAsync(Ajudante entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
