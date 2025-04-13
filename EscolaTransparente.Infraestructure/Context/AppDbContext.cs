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
            modelBuilder.Entity<EscolaModel>(entity =>
            {
                entity.ToTable("Escola");
                entity.HasKey(e => e.EscolaId);
                entity.Property(e => e.NivelEnsino).IsRequired();
                entity.Property(e => e.EscolaId).ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(300);
                entity.Property(e => e.NotaMedia).IsRequired();
                entity.Property(e => e.TipoInstituicao).IsRequired();
                entity.Property(e => e.CNPJ).HasMaxLength(14);
                entity.Property(e => e.Verificada).HasDefaultValue(false).IsRequired();
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Contato)
                    .WithOne(c => c.Escola)
                    .HasForeignKey<ContatoModel>(c => c.EscolaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Endereco)
                    .WithOne(c => c.Escola)
                    .HasForeignKey<EnderecoModel>(c => c.EscolaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Avaliacoes)
                      .WithOne(a => a.Escola)
                      .HasForeignKey(a => a.EscolaId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.CaracteristicasEscola)
                      .WithOne(c => c.Escola)
                      .HasForeignKey(c => c.EscolaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
    