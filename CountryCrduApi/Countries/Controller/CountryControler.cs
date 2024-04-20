using CountryCrduApi.Countries.Controller.Interfaces;
using CountryCrduApi.Countries.Model;
using CountryCrduApi.Countries.Repository.interfaces;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.Dto;
using CountryCrduApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CountryCrduApi.Countries.Controller
{
    public class CountryControler: CountryApiController
    {
        private ICountryCommandService _countryCommandService;
        private iCountryQueryService _countryQueryService;

        public CountryControler(ICountryCommandService coutnryCommandService, iCountryQueryService countryQueryService)
        {
            _countryCommandService = coutnryCommandService;
            _countryQueryService = countryQueryService;
        }

        public async override Task<ActionResult<Country>> CreateCountry([FromBody] CreateCountryRequest request)
        {
            try
            {
                var countries = await _countryCommandService.CreateCountry(request);

                return Ok(countries);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async override Task<ActionResult<Country>> DeleteCountry([FromRoute] int id)
        {
            try
            {
                var countries = await _countryCommandService.DeleteCountry(id);

                return Accepted("", countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async override Task<ActionResult<IEnumerable<Country>>> GetAll()
        {
            try
            {
                var countries = await _countryQueryService.GetAllCountries();
                return Ok(countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async override Task<ActionResult<Country>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var countries = await _countryQueryService.GetByName(name);
                return Ok(countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async override Task<ActionResult<Country>> UpdateCountry([FromRoute]int id, [FromBody] UpdateCountryRequest request)
        {
            try
            {
                var countries = await _countryCommandService.UpdateCountry(id,request);

                return Ok(countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
