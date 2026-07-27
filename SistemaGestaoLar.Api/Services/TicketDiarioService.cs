using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<TicketDiario>> GetAllAsync()
        {
            return await _repo.GetQueryableNoTracking()
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoTicket)
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoStatus)
                .ToListAsync();
        }

        public async Task<TicketDiario> GetByIdAsync(int id)
        {
            return await _repo.GetQueryableNoTracking()
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoTicket)
                .Include(t => t.Servicos)
                    .ThenInclude(x => x.ServicoStatus)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<TicketDiario> CreateAsync(TicketDiario entity) => _repo.AddAsync(entity);
        public Task<TicketDiario> UpdateAsync(TicketDiario entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
