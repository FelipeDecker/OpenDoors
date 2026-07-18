using Microsoft.EntityFrameworkCore;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Morador> Moradores { get; set; }
        public DbSet<Ajudante> Ajudantes { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<TicketDiario> TicketDiarios { get; set; }
    }
}
