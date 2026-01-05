namespace shared.Interfaces
{
    public interface IGenericRepository <Model>
        where Model : class
    {
        Task<Model?> GetByIdAsync(int id);
        Task<List<Model>> GetByNameAsync(string name);
        Task<List<Model>> GetAllAsync();
        Task<List<Model>> GetAllTrackedAsync();
        Task<int> AddAsync(Model objectModel);
        Task<int> UpdateAsync(Model objectModel);
        Task DeleteAsync(int id);
    }
}
