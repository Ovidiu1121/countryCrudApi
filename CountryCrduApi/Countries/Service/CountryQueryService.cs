using CountryCrduApi.Countries.Model;
using CountryCrduApi.Countries.Repository.interfaces;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.Dto;
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

        public async Task<ListCountryDto> GetAllCountries()
        {
            ListCountryDto countries = await _repository.GetAllAsync();

            if (countries.countryList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_COUNTRIES_EXIST);
            }

            return countries;
        }

        public async Task<CountryDto> GetByName(string name)
        {
            CountryDto country = await _repository.GetByNameAsync(name);

            if (country == null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            return country;
        }

        public async Task<CountryDto> GetByPopulation(int population)
        {
            CountryDto country = await _repository.GetByPopulationAsync(population);

            if (country == null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            return country;
        }
        
        public async Task<CountryDto> GetById(int id)
        {
            CountryDto country = await _repository.GetByIdAsync(id);

            if (country == null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            return country;
        }
        
    }
}
