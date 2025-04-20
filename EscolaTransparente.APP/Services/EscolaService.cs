using System.Threading.Tasks;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces.Services;
using EscolaTransparente.Domain.Interfaces.Repositories;

namespace EscolaTransparente.Application.Services
{
    internal class EscolaService : IEscolaService
    {
        private readonly IEscolaRepository _escolaRepository;

        public EscolaService(IEscolaRepository escolaRepository)
        {
            _escolaRepository = escolaRepository;
        }
        public void AdicionarEscola(EscolaDTO escola)
        {

        }

        public async Task<EscolaDTO?> ObterEscolaPorId(int escolaId)
        {
            var escola = await _escolaRepository.GetByIdAsync(escolaId);
            if (escola is null) return null;

            return new EscolaDTO
            {
                EscolaId = escola.EscolaId,
                Nome = escola.Nome,
                Descricao = escola.Descricao,
                NotaMedia = escola.NotaMedia,
                CNPJ = escola.CNPJ,
                Verificada = escola.Verificada,
                CriadaEm = escola.CriadaEm,
                DataCadastro = escola.DataCadastro,
                NivelEnsino = Data.Enums.NivelEnsino.EducacaoInfantil,
                TipoInstituicao = Data.Enums.TipoInstituicao.Privada,
                Contato = new Data.DataTransferObjects.Contato.ContatoDTO
                {
                   ContatoId = escola.Contato.ContatoId,
                    UrlSite = escola.Contato.UrlSite,
                    Email = escola.Contato.Email,
                    NumeroCelular = escola.Contato.NumeroCelular,
                    EscolaId = escola.Contato.EscolaId,
                    NumeroFixo = escola.Contato.NumeroFixo,
                },
                Endereco = new Data.DataTransferObjects.Endereco.EnderecoDTO
                {
                    EnderecoId = escola.Endereco.EnderecoId,
                    CEP = escola.Endereco.CEP,
                    Cidade = escola.Endereco.Cidade,
                    Endereco = escola.Endereco.Endereco,
                    Latitude = escola.Endereco.Latitude,
                    Longitude = escola.Endereco.Longitude,
                    Estado = escola.Endereco.Estado,
                    EscolaId = escola.Endereco.EscolaId,  
                }
            };

        }
    }
}
