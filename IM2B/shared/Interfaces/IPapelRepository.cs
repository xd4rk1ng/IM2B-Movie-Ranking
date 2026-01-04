using shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Interfaces
{
    public interface IPapelRepository<Model> : IGenericRepository<Model>
        where Model : class
    {
        Task<List<Papel>> GetAllForAtorIdAsync(int id);
        Task<List<Papel>> GetAllForFilmeIdAsync(int id);
    }
}
