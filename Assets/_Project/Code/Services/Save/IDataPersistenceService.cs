using System.Threading.Tasks;

public interface IDataPersistenceService<T> where T : class
{
    Task SaveAsync(T data);
    Task<T> LoadAsync();
}