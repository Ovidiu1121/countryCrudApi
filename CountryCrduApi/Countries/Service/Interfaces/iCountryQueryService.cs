using CountryCrduApi.Countries.Model;

namespace CountryCrduApi.Countries.Service.Interfaces
{
    public interface iCountryQueryService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetByName(string name);
        Task<Country> GetByPopulation(int population);
    }
}
