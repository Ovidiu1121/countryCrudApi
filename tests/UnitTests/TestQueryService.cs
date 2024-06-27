using System.Threading.Tasks;
using CountryCrduApi.Countries.Repository.interfaces;
using CountryCrduApi.Countries.Service;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.Dto;
using CountryCrduApi.System.Constant;
using CountryCrduApi.System.Exceptions;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestQueryService
{
    
    Mock<ICountryRepository> _mock;
    iCountryQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<ICountryRepository>();
        _service=new CountryQueryService(_mock.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {
        _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListCountryDto());

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllCountries());

        Assert.Equal(exception.Message, Constants.NO_COUNTRIES_EXIST);       

    }
    
    [Fact]
    public async Task GetAll_ReturnAllCountries()
    {

        var countries = TestCountryFactory.CreateCountries(5);

        _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(countries);

        var result = await _service.GetAllCountries();

        Assert.NotNull(result);
        Assert.Contains(countries.countryList[1], result.countryList);

    }
    
    [Fact]
    public async Task GetById_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetById_ReturnCountry()
    {

        var country = TestCountryFactory.CreateCountry(5);

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(country);

        var result = await _service.GetById(5);

        Assert.NotNull(result);
        Assert.Equal(country, result);

    }
    
    [Fact]
    public async Task GetByName_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByName_ReturnCountry()
    {

        var country=TestCountryFactory.CreateCountry(5);

        country.Name="test";

        _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(country);

        var result = await _service.GetByName("test");

        Assert.NotNull(result);
        Assert.Equal(country, result);

    }
    
    [Fact]
    public async Task GetByPopulation_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByPopulationAsync(1)).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByPopulation_ReturnCountry()
    {

        var country = TestCountryFactory.CreateCountry(5);

        _mock.Setup(repo => repo.GetByPopulationAsync(2000)).ReturnsAsync(country);

        var result = await _service.GetByPopulation(2000);

        Assert.NotNull(result);
        Assert.Equal(country, result);

    }
    
}