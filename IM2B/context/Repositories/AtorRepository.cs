using Microsoft.EntityFrameworkCore;
using shared.Models;
using shared.Interfaces;
using context.Mappings;

namespace context.Repositories
{
    public class AtorRepository : IGenericRepository<Ator>
    {
        private readonly ApplicationContext _context;

        public AtorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Ator?> GetByIdAsync(int id) =>
            await _context.Atores
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => a.ToModel())
                .FirstOrDefaultAsync();

        public async Task<List<Ator>> GetAllAsync() =>
            await _context.Atores
                .Select(a => a.ToModel())
                .ToListAsync();

        public async Task AddAsync(Ator ator)
        {
            _context.Atores.Add(ator.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ator ator)
        {
            var atorEntity = ator.ToEntity();
            _context.Attach(atorEntity);
            _context.Entry(atorEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Atores.FindAsync(id);
            if (entity != null)
            {
                _context.Atores.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
