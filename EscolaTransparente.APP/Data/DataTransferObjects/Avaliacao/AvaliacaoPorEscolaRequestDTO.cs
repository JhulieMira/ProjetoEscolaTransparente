namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
        public class AvaliacaoPorEscolaRequestDTO
        {
            public string NomeUsuario { get; set; }
            public List<AvaliacaoRequestDTO> Avaliacoes { get; set; }
        }
        public class AvaliacaoRequestDTO
        {
            public DateTime Data { get; set; }
            public string NomeCaracteristica { get; set; }
            public short Nota { get; set; }
            public string? ConteudoAvaliacao { get; set; }
        }
}
