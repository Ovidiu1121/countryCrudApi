using CountryCrduApi.Countries.Model;
using CountryCrduApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CountryCrduApi.Countries.Controller.Interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class CountryApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Country>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListCountryDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Country))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> CreateCountry([FromBody] CreateCountryRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Country))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> UpdateCountry([FromRoute]int id, [FromBody] UpdateCountryRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Country))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> DeleteCountry([FromRoute] int id);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Country))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> GetByIdRoute([FromRoute] int id);
        
        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Country))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> GetByNameRoute([FromRoute] string name);
        
        [HttpGet("population/{population}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Country))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<CountryDto>> GetByPopulationRoute([FromRoute] int population);
        
        
    }
}
