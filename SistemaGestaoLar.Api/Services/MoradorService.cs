using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Services
{
    public class MoradorService
    {
        private readonly IGenericRepository<Morador> _repo;
        private readonly IGenericRepository<TicketDiario> _ticketRepo;

        public MoradorService(IGenericRepository<Morador> repo, IGenericRepository<TicketDiario> ticketRepo)
        {
            _repo = repo;
            _ticketRepo = ticketRepo;
        }

        public Task<IEnumerable<Morador>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Morador> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Morador> CreateAsync(Morador entity)
        {
            entity.DataCadastro = DateTime.Now;
            return _repo.AddAsync(entity);
        }
        public Task<Morador> UpdateAsync(Morador entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<bool> ExisteAsync(int id) => _repo.GetQueryableNoTracking().AnyAsync(m => m.Id == id);

        public Task<bool> PossuiTicketsAsync(int id) => _ticketRepo.GetQueryableNoTracking().AnyAsync(t => t.MoradorId == id);
    }
}
