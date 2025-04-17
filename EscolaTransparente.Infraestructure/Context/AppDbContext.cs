using EscolaTransparente.Infraestructure.Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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

            modelBuilder.Entity<CaracteristicasEscolaModel>(entity =>
            {
                entity.ToTable("CaracteristicasEscola");
                entity.HasKey(ce => ce.CaracteristicasEscolaId);

                entity.Property(ce => ce.CaracteristicasEscolaId).ValueGeneratedOnAdd();
                entity.Property(ce => ce.CaracteristicaId).IsRequired();
                entity.Property(ce => ce.EscolaId).IsRequired();
                entity.Property(ce => ce.NotaMedia).IsRequired();

                entity.HasOne(ce => ce.Escola)
                      .WithMany(e => e.CaracteristicasEscola)
                      .HasForeignKey(ce => ce.EscolaId);

                entity.HasOne(ce => ce.Caracteristica)
                      .WithMany()
                      .HasForeignKey(ce => ce.CaracteristicaId);
            });

            modelBuilder.Entity<AvaliacaoModel>(entity =>
            {
                entity.ToTable("Avaliacao");

                entity.HasKey(e => e.AvaliacaoId);

                entity.Property(e => e.AvaliacaoId).ValueGeneratedOnAdd();
                entity.Property(e => e.Nota).IsRequired();
                entity.Property(e => e.EscolaId).IsRequired();
                entity.Property(e => e.UsuarioId).IsRequired();
                entity.Property(e => e.CaracteristicaId).IsRequired();
                entity.Property(e => e.Data).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Escola)
                    .WithMany(c => c.Avaliacoes)
                    .HasForeignKey(c => c.EscolaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Caracteristica)
                    .WithMany()
                    .HasForeignKey(e => e.CaracteristicaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RespostaAvaliacao)
                      .WithOne(a => a.Avaliacao)
                      .HasForeignKey<RespostaAvaliacaoModel>(a => a.RespostaId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CaracteristicaModel>(entity =>
            {
                entity.ToTable("Caracteristica");

                entity.HasKey(c => c.CaracteristicaId);

                entity.Property(c => c.CaracteristicaId).ValueGeneratedOnAdd();
                entity.Property(c => c.Descricao).IsRequired();
            });

            modelBuilder.Entity<ContatoModel>(entity =>
            {
                entity.ToTable("Contato");

                entity.HasKey(c => c.ContatoId);

                entity.Property(c => c.ContatoId)
                    .ValueGeneratedOnAdd();
                entity.Property(c => c.Email)
                    .HasMaxLength(100);
                entity.Property(c => c.UrlSite)
                    .HasMaxLength(200);
                entity.Property(c => c.NumeroCelular)
                    .HasMaxLength(20)
                    .IsRequired();
                entity.Property(c => c.NumeroFixo)
                    .HasMaxLength(20);
                entity.Property(c => c.EscolaId)
                    .IsRequired();

                entity.HasOne(c => c.Escola)
                    .WithOne(e => e.Contato)
                    .HasForeignKey<ContatoModel>(c => c.EscolaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EnderecoModel>(entity =>
            {
                entity.ToTable("Endereco");

                entity.HasKey(e => e.EnderecoId);

                entity.Property(e => e.EnderecoId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Endereco)
                    .IsRequired();
                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(e => e.Estado)
                    .IsRequired(); 
                entity.Property(e => e.CEP)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsFixedLength();  
                entity.Property(e => e.Latitude)
                    .HasMaxLength(20);
                entity.Property(e => e.Longitude)
                    .HasMaxLength(20);

                entity.HasOne(e => e.Escola)
                    .WithOne(esc => esc.Endereco)
                    .HasForeignKey<EnderecoModel>(e => e.EscolaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RespostaAvaliacaoModel>(entity =>
            {
                entity.ToTable("RespostaAvaliacao");

                entity.HasKey(r => r.RespostaId);
                entity.Property(r => r.RespostaId)
                    .ValueGeneratedOnAdd();
                entity.Property(r => r.AvaliacaoId)
                    .IsRequired();
                entity.Property(r => r.UsuarioId)
                    .IsRequired()
                    .HasMaxLength(450); 
                entity.Property(r => r.Resposta)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(r => r.Avaliacao)
                    .WithOne(a => a.RespostaAvaliacao)
                    .HasForeignKey<RespostaAvaliacaoModel>(a => a.AvaliacaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
    