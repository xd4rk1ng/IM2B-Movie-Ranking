using Microsoft.EntityFrameworkCore;
using shared.Models;
using shared.Interfaces;
using context.Mappings;

namespace context.Repositories
{
    public class PapelRepository : IPapelRepository<Papel>
    {
        private readonly ApplicationContext _context;

        public PapelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Papel?> GetByIdAsync(int id) =>
            await _context.Papeis
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => p.ToModel())
                .FirstOrDefaultAsync();

        public async Task<Papel?> GetByIdsAsync(int filmeId, int atorId) =>
            await _context.Papeis
                .AsNoTracking()
                .Where(p => p.FilmeId == filmeId && p.AtorId == atorId)
                .Where(p => p.AtorId == atorId)
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
                .AsNoTracking()
                .Select(e => e.ToModel())
                .ToListAsync();
        public async Task<List<Papel>> GetAllTrackedAsync() =>
            await _context.Papeis
                .Select(e => e.ToModel())
                .ToListAsync();

        public async Task<List<Papel>> GetAllForAtorIdAsync(int id) =>
            await _context.Papeis
                .AsNoTracking()
                .Where(p => p.AtorId == id)
                .Include(p => p.Filme)
                .Select(e => e.ToModel())
                .ToListAsync();
    

        public async Task<List<Papel>> GetAllForFilmeIdAsync(int id) =>
            await _context.Papeis
                .AsNoTracking()
                .Where(p => p.FilmeId == id)
                .Include(p => p.Ator)
                .Select(e => e.ToModel())
                .ToListAsync();

        public async Task<int> AddAsync(Papel papel)
        {
            var papelEntity = papel.ToEntity();
            _context.Papeis.Add(papelEntity);
            await _context.SaveChangesAsync();
            return papelEntity.Id;
        }

        public async Task<int> UpdateAsync(Papel papel)
        {
            var papelEntity = papel.ToEntity();
            _context.Papeis.Update(papelEntity);
            await _context.SaveChangesAsync();
            return papelEntity.Id;
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
