using AutoMapper;
using ConsoleApp1.Models;

namespace ConsoleApp1.Domain.Mapper
{
    public class IsinDataMapper : Profile
    {
        public IsinDataMapper()
        {
            CreateMap<DataProviderIsinResponse, IsinModel>()
                .ForMember(desting => desting.DataCreate, origin => origin.MapFrom(x => x.DataCreateOfRequest))
                .ReverseMap();

            CreateMap<DataProviderIsinResponse, IsinResponse>()
                .ForMember(desting => desting.DataCreate, origin => origin.MapFrom(x => x.DataCreateOfRequest))
                .ReverseMap();
        }


    }
}
