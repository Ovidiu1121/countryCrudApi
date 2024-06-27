using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;

namespace CountryCrduApi.Countries.Repository.interfaces
{
    public interface ICountryRepository
    {
        Task<ListCountryDto> GetAllAsync();
        Task<CountryDto> GetByNameAsync(string name);
        Task<CountryDto> GetByIdAsync(int id);
        Task<CountryDto> GetByPopulationAsync(int population);
        Task<CountryDto> CreateCountry(CreateCountryRequest request);
        Task<CountryDto> UpdateCountry(int id,UpdateCountryRequest request);
        Task<CountryDto> DeleteCountryById(int id);
    }
}
