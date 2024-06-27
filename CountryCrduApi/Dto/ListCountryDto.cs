namespace CountryCrduApi.Dto;

public class ListCountryDto
{
    public ListCountryDto()
    {
        countryList = new List<CountryDto>();
    }
    
    public List<CountryDto> countryList { get; set; }
}