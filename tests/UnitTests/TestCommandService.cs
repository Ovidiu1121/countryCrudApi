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

public class TestCommandService
{
    Mock<ICountryRepository> _mock;
    ICountryCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<ICountryRepository>();
        _service = new CountryCommandService(_mock.Object);
    }
    
    [Fact]
    public async Task Create_InvalidData()
    {
        var create = new CreateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 0
        };

        var country = TestCountryFactory.CreateCountry(5);

        _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(country);
                
        var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateCountry(create));

        Assert.Equal(Constants.COUNTRY_ALREADY_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task Create_ReturnCountry()
    {

        var create = new CreateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 12000
        };

        var country = TestCountryFactory.CreateCountry(5);

        country.Name=create.Name;
        country.Capital=create.Capital;
        country.Population=create.Population;

        _mock.Setup(repo => repo.CreateCountry(It.IsAny<CreateCountryRequest>())).ReturnsAsync(country);

        var result = await _service.CreateCountry(create);

        Assert.NotNull(result);
        Assert.Equal(result, country);
    }
    
    [Fact]
    public async Task Update_ItemDoesNotExist()
    {
        var update = new UpdateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 0
        };

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateCountry(1, update));

        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Update_InvalidData()
    {
        var update = new UpdateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 20000
        };

        _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateCountry(5, update));

        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, exception.Message);

    }
    
    
    [Fact]
    public async Task Update_ValidData()
    {
        var update = new UpdateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 20000
        };

        var country = TestCountryFactory.CreateCountry(5);

        country.Name=update.Name;
        country.Capital=update.Capital;
        country.Population=update.Population.Value;
        
        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(country);
        _mock.Setup(repoo => repoo.UpdateCountry(It.IsAny<int>(), It.IsAny<UpdateCountryRequest>())).ReturnsAsync(country);

        var result = await _service.UpdateCountry(5, update);

        Assert.NotNull(result);
        Assert.Equal(country, result);

    }
    
    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.DeleteCountryById(It.IsAny<int>())).ReturnsAsync((CountryDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteCountry(5));

        Assert.Equal(exception.Message, Constants.COUNTRY_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var country = TestCountryFactory.CreateCountry(1);

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(country);

        var result= await _service.DeleteCountry(1);

        Assert.NotNull(result);
        Assert.Equal(country, result);


    }
    
    
}