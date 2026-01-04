using Microsoft.EntityFrameworkCore;
using shared.Models;
using shared.Interfaces;
using context.Mappings;

namespace context.Repositories
{
    public class FilmeRepository : IGenericRepository<Filme>
    {
        private readonly ApplicationContext _context;

        public FilmeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Filme?> GetByIdAsync(int id) =>
            await _context.Filmes
                .Where(f => f.Id == id)
                .Select(f => f.ToModel())
                .FirstOrDefaultAsync();

        public async Task<List<Filme>> GetByNameAsync(string name) =>
            await _context.Filmes
                .AsNoTracking()
                .Where(a => a.Titulo == name)
                .Select(a => a.ToModel())
                .ToListAsync();

        public async Task<List<Filme>> GetAllAsync() =>
            await _context.Filmes
                .Select(e => e.ToModel())
                .ToListAsync();

        public async Task<int> AddAsync(Filme filme)
        {
            var filmeEntity = filme.ToEntity();
            _context.Filmes.Add(filmeEntity);
            await _context.SaveChangesAsync();
            return filmeEntity.Id;
        }

        public async Task UpdateAsync(Filme filme)
        {
            _context.Filmes.Update(filme.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Filmes.FindAsync(id);
            if (entity != null)
            {
                _context.Filmes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
