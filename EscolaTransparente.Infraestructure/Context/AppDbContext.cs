using EscolaTransparente.Infraestructure.Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EscolaModel> Escolas { get; set; }
        public DbSet<AvaliacaoModel> Avaliacoes { get; set; }
        public DbSet<CaracteristicaModel> Caracteristicas { get; set; }
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscolas { get; set; }
        public DbSet<ContatoModel> Contato { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
    