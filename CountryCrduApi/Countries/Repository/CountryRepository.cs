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

        public async Task<Country> CreateCountry(CreateCountryRequest request)
        {
            var country = _mapper.Map<Country>(request);

            _context.Countries.Add(country);

            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<Country> DeleteCountryById(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            _context.Countries.Remove(country);

            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Country> GetByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(obj => obj.Name.Equals(name));
        }

        public async Task<Country> GetByPopulationAsync(int population)
        {
            return await _context.Countries.FirstOrDefaultAsync(obj => obj.Population.Equals(population));

        }

        public async Task<Country> UpdateCountry(int id,UpdateCountryRequest request)
        {
            var country = await _context.Countries.FindAsync(id);

            country.Name= request.Name ?? country.Name;
            country.Capital= request.Capital ?? country.Capital;
            country.Population=request.Population ?? country.Population;

            _context.Countries.Update(country);

            await _context.SaveChangesAsync();

            return country;
        }
    }
}
