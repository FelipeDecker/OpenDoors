using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class TicketDiarioService
    {
        private readonly IGenericRepository<TicketDiario> _repo;

        public TicketDiarioService(IGenericRepository<TicketDiario> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<TicketDiario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<TicketDiario> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<TicketDiario> CreateAsync(TicketDiario entity) => _repo.AddAsync(entity);
        public Task<TicketDiario> UpdateAsync(TicketDiario entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
