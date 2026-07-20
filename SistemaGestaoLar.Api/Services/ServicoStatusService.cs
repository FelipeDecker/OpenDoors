using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class ServicoStatusService
    {
        private readonly IGenericRepository<ServicoStatus> _repo;

        public ServicoStatusService(IGenericRepository<ServicoStatus> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ServicoStatus>> GetAllAsync() => _repo.GetAllAsync();
        public Task<ServicoStatus> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    }
}
