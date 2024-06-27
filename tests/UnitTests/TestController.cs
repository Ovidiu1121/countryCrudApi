using System.Threading.Tasks;
using CountryCrduApi.Countries.Controller;
using CountryCrduApi.Countries.Controller.Interfaces;
using CountryCrduApi.Countries.Service.Interfaces;
using CountryCrduApi.Dto;
using CountryCrduApi.System.Constant;
using CountryCrduApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    Mock<ICountryCommandService> _command;
    Mock<iCountryQueryService> _query;
    CountryApiController _controller;

    public TestController()
    {
        _command = new Mock<ICountryCommandService>();
        _query = new Mock<iCountryQueryService>();
        _controller = new CountryControler(_command.Object, _query.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {

        _query.Setup(repo => repo.GetAllCountries()).ThrowsAsync(new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST));
           
        var result = await _controller.GetAll();

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(404, notFound.StatusCode);
        Assert.Equal(Constants.COUNTRY_DOES_NOT_EXIST, notFound.Value);

    }
    
    [Fact]
    public async Task GetAll_ValidData()
    {

        var countries = TestCountryFactory.CreateCountries(5);

        _query.Setup(repo => repo.GetAllCountries()).ReturnsAsync(countries);

        var result = await _controller.GetAll();
        var okresult = Assert.IsType<OkObjectResult>(result.Result);
        var countriesAll = Assert.IsType<ListCountryDto>(okresult.Value);

        Assert.Equal(5, countriesAll.countryList.Count);
        Assert.Equal(200, okresult.StatusCode);

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
        
        _command.Setup(repo => repo.CreateCountry(It.IsAny<CreateCountryRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.COUNTRY_ALREADY_EXIST));

        var result = await _controller.CreateCountry(create);
        var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(400,bad.StatusCode);
        Assert.Equal(Constants.COUNTRY_ALREADY_EXIST, bad.Value);

    }
    
    [Fact]
    public async Task Create_ValidData()
    {

        var create = new CreateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 0
        };

        var country = TestCountryFactory.CreateCountry(5);

        country.Name=create.Name;
        country.Capital=create.Capital;
        country.Population=create.Population;

        _command.Setup(repo => repo.CreateCountry(create)).ReturnsAsync(country);

        var result = await _controller.CreateCountry(create);

        var okResult= Assert.IsType<CreatedResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 201);
        Assert.Equal(country, okResult.Value);

    }
    
    [Fact]
    public async Task Update_InvalidDate()
    {

        var update = new UpdateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 0
        };

        _command.Setup(repo => repo.UpdateCountry(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST));

        var result = await _controller.UpdateCountry(11, update);
        var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(bad.StatusCode, 404);
        Assert.Equal(bad.Value, Constants.COUNTRY_DOES_NOT_EXIST);

    }
    
    [Fact]
    public async Task Update_ValidData()
    {

        var update = new UpdateCountryRequest()
        {
            Name="Test",
            Capital= "test",
            Population= 23000
        };

        var country=TestCountryFactory.CreateCountry(5);
        
        country.Name=update.Name;
        country.Capital=update.Capital;
        country.Population=update.Population.Value;

        _command.Setup(repo=>repo.UpdateCountry(5,update)).ReturnsAsync(country);

        var result = await _controller.UpdateCountry(5, update);

        var okResult=Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 200);
        Assert.Equal(okResult.Value, country);

    }
    
    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _command.Setup(repo=>repo.DeleteCountry(2)).ThrowsAsync(new ItemDoesNotExist(Constants.COUNTRY_DOES_NOT_EXIST));

        var result= await _controller.DeleteCountry(2);
        var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(notfound.StatusCode, 404);
        Assert.Equal(notfound.Value, Constants.COUNTRY_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var country = TestCountryFactory.CreateCountry(1);

        _command.Setup(repo => repo.DeleteCountry(1)).ReturnsAsync(country);

        var result = await _controller.DeleteCountry(1);

        var okResult=Assert.IsType<AcceptedResult>(result.Result);

        Assert.Equal(202, okResult.StatusCode);
        Assert.Equal(country, okResult.Value);

    }
    
    
    
}