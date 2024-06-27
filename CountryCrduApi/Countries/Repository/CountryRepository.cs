using AutoMapper;
using CountryCrduApi.Countries.Model;
using CountryCrduApi.Data;
using CountryCrduApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace CountryCrduApi.Countries.Repository.interfaces
{
    public class CountryRepository : ICountryRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryDto> CreateCountry(CreateCountryRequest request)
        {
            var country = _mapper.Map<Country>(request);

            _context.Countries.Add(country);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> DeleteCountryById(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            _context.Countries.Remove(country);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryDto>(country);
        }

        public async Task<ListCountryDto> GetAllAsync()
        {
            List<Country> result = await _context.Countries.ToListAsync();
            
            ListCountryDto listCountryDto = new ListCountryDto()
            {
                countryList = _mapper.Map<List<CountryDto>>(result)
            };

            return listCountryDto;
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var country = await _context.Countries.Where(c => c.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> GetByNameAsync(string name)
        {
            var country = await _context.Countries.Where(c => c.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> GetByPopulationAsync(int population)
        {
            var country = await _context.Countries.Where(c => c.Population==population).FirstOrDefaultAsync();
            
            return _mapper.Map<CountryDto>(country);

        }

        public async Task<CountryDto> UpdateCountry(int id,UpdateCountryRequest request)
        {
            var country = await _context.Countries.FindAsync(id);

            country.Name= request.Name ?? country.Name;
            country.Capital= request.Capital ?? country.Capital;
            country.Population=request.Population ?? country.Population;

            _context.Countries.Update(country);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryDto>(country);
        }
    }
}
