using AutoMapper;
using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;

namespace CountryCrduApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {

            CreateMap<CreateCountryRequest, Country>();
            CreateMap<UpdateCountryRequest, Country>();
            CreateMap<CountryDto, Country>().ReverseMap();

        }

    }
}
