using CountryCrduApi.Dto;

namespace tests.Helpers;

public class TestCountryFactory
{
    public static CountryDto CreateCountry(int id)
    {
        return new CountryDto
        {
            Id = id,
            Name="Romania"+id,
            Capital= "Balotesti"+id,
            Population= 20000+id
        };
    }

    public static ListCountryDto CreateCountries(int count)
    {
        ListCountryDto countries=new ListCountryDto();
            
        for(int i = 0; i<count; i++)
        {
            countries.countryList.Add(CreateCountry(i));
        }
        return countries;
    }
}