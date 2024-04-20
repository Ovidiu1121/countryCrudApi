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

        public async Task<Country> CreateCountry(CreateCountryRequest request)
        {
            Country country = await _repository.GetByNameAsync(request.Name);

            if (country!=null)
            {
                throw new ItemAlreadyExists(Constants.COUNTRY_ALREADY_EXIST);
            }

            country=await _repository.CreateCountry(request);
            return country;
        }

        public async Task<Country> DeleteCountry(int id)
        {
            Country country = await _repository.GetByIdAsync(id);

            if (country==null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            await _repository.DeleteCountryById(id);
            return country;
        }

        public async Task<Country> UpdateCountry(int id,UpdateCountryRequest request)
        {
            Country country = await _repository.GetByIdAsync(id);

            if (country==null)
            {
                throw new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST);
            }

            country = await _repository.UpdateCountry(id,request);
            return country;
        }
    }
}
