using SecureFileStorage.Data;
using SecureFileStorage.Models;
using SecureFileStorage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SecureFileStorage.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StoredFile file, CancellationToken ct)
        {
            await _context.Files.AddAsync(file, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<StoredFile?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Files
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<StoredFile>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Files.ToListAsync(ct);
        }

        public async Task DeleteAsync(StoredFile file, CancellationToken ct)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(ct);
        }
    }
}