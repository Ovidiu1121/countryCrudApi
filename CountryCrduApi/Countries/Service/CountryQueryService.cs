using CountryCrduApi.Countries.Model;
using CountryCrduApi.Countries.Repository.interfaces;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.System.Constant;
using CountryCrduApi.System.Exceptions;

namespace CountryCrduApi.Countries.Service
{
    public class CountryQueryService:iCountryQueryService
    {
        private ICountryRepository _repository;

        public CountryQueryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            IEnumerable<Country> countries = await _repository.GetAllAsync();

            if (countries.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_COUNTRIES_EXIST);
            }

            return countries;
        }

        public async Task<Country> GetByName(string name)
        {
            Country country = await _repository.GetByNameAsync(name);

            if (country == null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            return country;
        }

        public async Task<Country> GetByPopulation(int population)
        {
            Country country = await _repository.GetByPopulationAsync(population);

            if (country == null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            return country;
        }
    }
}
