using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.DataTransferObjects.Contato;
using EscolaTransparente.Application.Data.DataTransferObjects.Endereco;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao;
using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Application.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //var caracteristicaMap = CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>();
            //caracteristicaMap.ForMember(dest => dest.Caracteristica, src => src.MapFrom(fld => fld.Caracteristica.Descricao));

            // Contato mappings
            CreateMap<ContatoModel, ContatoReadDTO>();
            CreateMap<ContatoInsertDTO, ContatoModel>();
            
            // Avaliacao mappings
            CreateMap<AvaliacaoModel, AvaliacaoReadDTO>();
            CreateMap<AvaliacaoInsertDTO, AvaliacaoModel>();
            
            // Caracteristica mappings
            CreateMap<CaracteristicaInsertDTO, CaracteristicaModel>();
            CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Caracteristica.Descricao));

            CreateMap<CaracteristicasEscolaInsertDTO, CaracteristicasEscolaModel>()
                 .ForPath(dest => dest.Caracteristica.CaracteristicaId, opt => opt.MapFrom(src => src.CaracteristicaId))
                 .ForPath(dest => dest.Caracteristica.Descricao, opt => opt.MapFrom(src => src.Descricao));
            // Endereco mappings
            CreateMap<EnderecoModel, EnderecoReadDTO>();
            CreateMap<EnderecoInsertDTO, EnderecoModel>();
            
            // Escola mappings
            CreateMap<EscolaModel, EscolaReadDTO>();
            CreateMap<EscolaInsertDTO, EscolaModel>();
            
            // RespostaAvaliacao mappings
            CreateMap<RespostaAvaliacaoModel, RespostaReadAvaliacaoDTO>();
            CreateMap<RespostaAvaliacaoInsertDTO, RespostaAvaliacaoModel>();
        }
    }
}
