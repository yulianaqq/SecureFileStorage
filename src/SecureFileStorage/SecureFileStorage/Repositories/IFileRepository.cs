using SecureFileStorage.Models;

namespace SecureFileStorage.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task AddAsync(StoredFile file, CancellationToken ct);

        Task<StoredFile?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<List<StoredFile>> GetAllAsync(CancellationToken ct);

        Task DeleteAsync(StoredFile file, CancellationToken ct);
    }
}