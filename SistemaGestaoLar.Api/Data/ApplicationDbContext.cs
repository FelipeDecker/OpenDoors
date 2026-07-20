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
        public DbSet<ServicoStatus> ServicosStatus { get; set; }
        public DbSet<TicketServico> TicketServicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Morador>(entity =>
            {
                entity.Property(m => m.NomeCompleto)
                    .IsRequired();

                entity.Property(m => m.DataNascimento)
                    .IsRequired(false);

                entity.Property(m => m.ContatoEmergencia)
                    .IsRequired(false);

                entity.Property(m => m.Observacoes)
                    .IsRequired(false);

                entity.Property(m => m.HistoricoAcolhimento)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Ajudante>()
                .HasMany(a => a.Grupos)
                .WithMany(g => g.Ajudantes)
                .UsingEntity(j => j.ToTable("AjudanteGrupo"));

            modelBuilder.Entity<TicketServico>()
                .HasOne(ts => ts.TicketDiario)
                .WithMany(t => t.Servicos)
                .HasForeignKey(ts => ts.TicketDiarioId);

            modelBuilder.Entity<TicketServico>()
                .HasOne(ts => ts.ServicoStatus)
                .WithMany()
                .HasForeignKey(ts => ts.ServicoStatusId);

            modelBuilder.Entity<ServicoStatus>().HasData(
                new ServicoStatus { Id = 1, Name = "Banho" },
                new ServicoStatus { Id = 2, Name = "Troca de Roupa" },
                new ServicoStatus { Id = 3, Name = "Jantar" }
            );
        }
    }
}
