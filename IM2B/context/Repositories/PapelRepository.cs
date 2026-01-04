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
                .Where(p => p.Id == id)
                .Select(p => p.ToModel())
                .FirstOrDefaultAsync();

        public async Task<List<Papel>> GetByNameAsync(string name) =>
            await _context.Papeis
                .AsNoTracking()
                .Where(a => a.Personagem == name)
                .Select(a => a.ToModel())
                .ToListAsync();

        public async Task<List<Papel>> GetAllAsync() =>
            await _context.Papeis
                .Select(e => e.ToModel())
                .ToListAsync();

        public async Task<int> AddAsync(Papel papel)
        {
            var papelEntity = papel.ToEntity();
            _context.Papeis.Add(papelEntity);
            await _context.SaveChangesAsync();
            return papelEntity.Id;
        }

        public async Task UpdateAsync(Papel papel)
        {
            _context.Papeis.Update(papel.ToEntity());
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
