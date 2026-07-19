using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Data;
using SistemaGestaoLar.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoLar.Api.Services
{
    public class GrupoService
    {
        private readonly IGenericRepository<Grupo> _repo;
        private readonly ApplicationDbContext _db;

        public GrupoService(IGenericRepository<Grupo> repo, ApplicationDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        public Task<IEnumerable<Grupo>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Grupo> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Grupo> CreateAsync(Grupo entity) => _repo.AddAsync(entity);
        public Task<Grupo> UpdateAsync(Grupo entity) => _repo.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<List<Ajudante>> GetAjudantesDoGrupoAsync(int grupoId)
        {
            return await _db.Grupos
                .Where(g => g.Id == grupoId)
                .SelectMany(g => g.Ajudantes)
                .ToListAsync();
        }

        public async Task<List<Ajudante>> GetAjudantesDisponiveisAsync(int grupoId)
        {
            var idsVinculados = await _db.Grupos
                .Where(g => g.Id == grupoId)
                .SelectMany(g => g.Ajudantes)
                .Select(a => a.Id)
                .ToListAsync();

            return await _db.Ajudantes
                .Where(a => !idsVinculados.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<bool> AdicionarAjudanteAsync(int grupoId, int ajudanteId)
        {
            var grupo = await _db.Grupos.Include(g => g.Ajudantes).FirstOrDefaultAsync(g => g.Id == grupoId);
            var ajudante = await _db.Ajudantes.FindAsync(ajudanteId);
            if (grupo == null || ajudante == null) return false;

            if (!grupo.Ajudantes.Any(a => a.Id == ajudanteId))
            {
                grupo.Ajudantes.Add(ajudante);
                await _db.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoverAjudanteAsync(int grupoId, int ajudanteId)
        {
            var grupo = await _db.Grupos.Include(g => g.Ajudantes).FirstOrDefaultAsync(g => g.Id == grupoId);
            if (grupo == null) return false;

            var ajudante = grupo.Ajudantes.FirstOrDefault(a => a.Id == ajudanteId);
            if (ajudante == null) return false;

            grupo.Ajudantes.Remove(ajudante);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
