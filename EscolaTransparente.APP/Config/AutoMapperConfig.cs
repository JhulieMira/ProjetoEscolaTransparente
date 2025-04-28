using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.DataTransferObjects.Contato;
using EscolaTransparente.Application.Data.DataTransferObjects.Endereco;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Application.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ContatoModel, ContatoDTO>().ReverseMap();
            CreateMap<AvaliacaoModel, AvaliacaoDTO>().ReverseMap();
            CreateMap<CaracteristicaModel, CaracteristicaDTO>().ReverseMap();
            CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaDTO>().ReverseMap();
            CreateMap<EnderecoModel, EnderecoDTO>().ReverseMap();
            CreateMap<EscolaModel, EscolaDTO>().ReverseMap();
            CreateMap<RespostaAvaliacaoModel, RespostaAvaliacaoDTO>().ReverseMap(); 
        }
    }
}
