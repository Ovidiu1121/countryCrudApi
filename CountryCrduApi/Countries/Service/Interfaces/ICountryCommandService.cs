using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;

namespace CountryCrduApi.Countries.Service.Interfaces
{
    public interface ICountryCommandService
    {
        Task<CountryDto> CreateCountry(CreateCountryRequest request);
        Task<CountryDto> UpdateCountry(int id,UpdateCountryRequest request);
        Task<CountryDto> DeleteCountry(int id);
    }
}
