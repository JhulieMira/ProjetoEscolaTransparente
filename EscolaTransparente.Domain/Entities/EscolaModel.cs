using EscolaTransparente.Infraestructure.Data.Enums;

namespace EscolaTransparente.Domain.Entities
{
    public class EscolaModel
    {
        public int EscolaId { get; set; }
        public NivelEnsino NivelEnsino { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public short NotaMedia { get; set; }
        public TipoInstituicao TipoInstituicao { get; set; }
        public string CNPJ { get; set; }
        public bool Verificada { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual ContatoModel Contato { get; set; }
        public virtual EnderecoModel Endereco { get; set; }
        public virtual List<AvaliacaoModel> Avaliacoes { get; set; }
        public virtual List<CaracteristicasEscolaModel> CaracteristicasEscola { get; set; }
    }
}
