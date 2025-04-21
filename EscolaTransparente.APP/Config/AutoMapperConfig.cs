using AutoMapper;

namespace EscolaTransparente.Application.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Domain.Entities., Data.DataTransferObjects.Escola.EscolaDTO>()
                .ForMember(dest => dest.Contato, opt => opt.MapFrom(src => src.Contato))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
            CreateMap<Data.DataTransferObjects.Escola.EscolaDTO, Domain.Entities.Escola>()
                .ForMember(dest => dest.Contato, opt => opt.MapFrom(src => src.Contato))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
        }
    }
    {
    }
}
