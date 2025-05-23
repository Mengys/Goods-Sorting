using System.Threading.Tasks;

namespace _Project.Code.Services.DataPersistence
{
    public interface IDataPersistence<T>
    {
        Task SaveAsync(T data);
        Task<T> LoadAsync();
        Task ClearAsync();
    }
}