namespace shared.Interfaces
{
    public interface IGenericRepository <Model>
        where Model : class
    {
        Task<Model?> GetByIdAsync(int id);
        Task<List<Model>> GetAllAsync();
        Task<int> AddAsync(Model objectModel);
        Task UpdateAsync(Model objectModel);
        Task DeleteAsync(int id);
    }
}
