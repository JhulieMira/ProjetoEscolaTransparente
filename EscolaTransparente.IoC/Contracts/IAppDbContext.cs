using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.IoC.Contracts
{
    public interface IAppDbContext
    {
        //TO DO: Addcontext here
        public DbSet<EscolaModel> Escolas { get; set; }
        public DbSet<AvaliacaoModel> Avaliacoes { get; set; }
        public DbSet<CaracteristicaModel> Caracteristicas { get; set; }
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscolas { get; set; }
        public DbSet<ContatoModel> Contato { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacoes { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        
    }
}
