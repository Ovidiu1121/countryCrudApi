using CountryCrduApi.Countries.Model;
using CountryCrduApi.Countries.Repository.interfaces;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.Dto;
using CountryCrduApi.System.Constant;
using CountryCrduApi.System.Exceptions;

namespace CountryCrduApi.Countries.Service
{
    public class CountryCommandService:ICountryCommandService
    {
        private ICountryRepository _repository;

        public CountryCommandService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CountryDto> CreateCountry(CreateCountryRequest request)
        {
            CountryDto country = await _repository.GetByNameAsync(request.Name);

            if (country!=null)
            {
                throw new ItemAlreadyExists(Constants.COUNTRY_ALREADY_EXIST);
            }

            country=await _repository.CreateCountry(request);
            return country;
        }

        public async Task<CountryDto> DeleteCountry(int id)
        {
            CountryDto country = await _repository.GetByIdAsync(id);

            if (country==null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            await _repository.DeleteCountryById(id);
            return country;
        }

        public async Task<CountryDto> UpdateCountry(int id,UpdateCountryRequest request)
        {
            CountryDto country = await _repository.GetByIdAsync(id);

            if (country==null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            country = await _repository.UpdateCountry(id,request);
            return country;
        }
    }
}
