using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;

namespace CountryCrduApi.Countries.Service.Interfaces
{
    public interface iCountryQueryService
    {
        Task<ListCountryDto> GetAllCountries();
        Task<CountryDto> GetByName(string name);
        Task<CountryDto> GetById(int id);
        Task<CountryDto> GetByPopulation(int population);
    }
}
