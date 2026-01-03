using Microsoft.EntityFrameworkCore;
using shared.Models;
using shared.Interfaces;
using context.Mappings;

namespace context.Repositories
{
    public class PapelRepository : IGenericRepository<Papel>
    {
        private readonly ApplicationContext _context;

        public PapelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Papel?> GetByIdAsync(int id) =>
            await _context.Papeis
                .Where(f => f.Id == id)
                .Select(f => f.ToModel())
                .FirstOrDefaultAsync();

        public async Task<List<Papel>> GetAllAsync() =>
            await _context.Papeis
                .Select(e => e.ToModel())
                .ToListAsync();

        public async Task AddAsync(Papel filme)
        {
            _context.Papeis.Add(filme.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Papel filme)
        {
            _context.Papeis.Update(filme.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Papeis.FindAsync(id);
            if (entity != null)
            {
                _context.Papeis.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
