using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class GrupoService
    {
        private readonly IGenericRepository<Grupo> _repo;

        public GrupoService(IGenericRepository<Grupo> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Grupo>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Grupo> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Grupo> CreateAsync(Grupo entity) => _repo.AddAsync(entity);
        public Task<Grupo> UpdateAsync(Grupo entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
