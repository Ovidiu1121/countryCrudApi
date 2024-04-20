using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;

namespace CountryCrduApi.Countries.Repository.interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByNameAsync(string name);
        Task<Country> GetByIdAsync(int id);
        Task<Country> GetByPopulationAsync(int population);
        Task<Country> CreateCountry(CreateCountryRequest request);
        Task<Country> UpdateCountry(int id,UpdateCountryRequest request);
        Task<Country> DeleteCountryById(int id);
    }
}
