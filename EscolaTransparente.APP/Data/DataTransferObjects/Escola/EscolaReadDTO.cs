using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.DataTransferObjects.Contato;
using EscolaTransparente.Application.Data.DataTransferObjects.Endereco;
using EscolaTransparente.Application.Data.Enums;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Escola
{
    public class EscolaReadDTO
    {
        public int EscolaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public short NotaMedia { get; set; }
        public string CNPJ { get; set; }
        public bool Verificada { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime DataCadastro { get; set; }

        public NivelEnsino NivelEnsino { get; set; }
        public TipoInstituicao TipoInstituicao { get; set; }

        public ContatoReadDTO Contato { get; set; }
        public EnderecoReadDTO Endereco { get; set; }
        public List<AvaliacaoReadDTO> Avaliacoes { get; set; }
        public List<CaracteristicasEscolaReadDTO> CaracteristicasEscola { get; set; }
    }
}
