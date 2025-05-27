using EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class AvaliacaoPorEscolaRequestDTO
    {
        public string NomeUsuario { get; set; }
        public List<AvaliacaoRequestDTO> Avaliacoes { get; set; }
        public bool AvaliacaoAnonima { get; set; }
    }
    public class AvaliacaoRequestDTO
    {
        public int AvaliacaoId { get; set; }
        public DateTime Data { get; set; }
        public string NomeCaracteristica { get; set; }
        public short Nota { get; set; }
        public string? ConteudoAvaliacao { get; set; }
        public RespostaReadAvaliacaoDTO RespostaAvaliacao { get; set; }
        public bool AvaliacaoAnonima { get; set; }
    }
}
