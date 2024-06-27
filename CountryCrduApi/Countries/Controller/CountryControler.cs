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

        public async override Task<ActionResult<CountryDto>> CreateCountry([FromBody] CreateCountryRequest request)
        {
            try
            {
                var countries = await _countryCommandService.CreateCountry(request);

                return Created("Tara a fost adaugata",countries);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async override Task<ActionResult<CountryDto>> DeleteCountry([FromRoute] int id)
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

        public async override Task<ActionResult<ListCountryDto>> GetAll()
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

        public async override Task<ActionResult<CountryDto>> GetByIdRoute([FromRoute] int id)
        {
            try
            {
                var countries = await _countryQueryService.GetById(id);
                return Ok(countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public async override Task<ActionResult<CountryDto>> GetByPopulationRoute([FromRoute] int population)
        {
            try
            {
                var countries = await _countryQueryService.GetByPopulation(population);
                return Ok(countries);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        public async override Task<ActionResult<CountryDto>> GetByNameRoute([FromRoute] string name)
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

        public async override Task<ActionResult<CountryDto>> UpdateCountry([FromRoute]int id, [FromBody] UpdateCountryRequest request)
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
