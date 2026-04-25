using SecureFileStorage.Models;
using SecureFileStorage.Repositories.Interfaces;

namespace SecureFileStorage.Services
{
    public class FileService
    {
        private readonly IFileRepository _repo;
        private readonly string _folder = "uploads";

        public FileService(IFileRepository repo)
        {
            _repo = repo;
        }
        public async Task<Guid> UploadAsync(IFormFile file, CancellationToken ct)
        {
            var id = Guid.NewGuid();
            var ext = Path.GetExtension(file.FileName);
            var storedName = id + ext;

            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            var path = Path.Combine(_folder, storedName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream, ct);
            }


            var entity = new StoredFile
            {
                Id = id,
                OriginalName = file.FileName,
                StoredName = storedName,
                ContentType = file.ContentType,
                Size = file.Length,
                StoragePath = path,
                UploadedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(entity, ct);
            return id;
        }


        public async Task<(Stream stream, StoredFile file)> DownloadAsync(Guid id, CancellationToken ct)
        {
            var file = await _repo.GetByIdAsync(id, ct);

            if (file == null)
                throw new Exception("File was not found");

            var stream = new FileStream(file.StoragePath, FileMode.Open, FileAccess.Read);

            return (stream, file);
        }


        public async Task<List<FileSummary>> GetAllAsync(CancellationToken ct)
        {
            var files = await _repo.GetAllAsync(ct);

            return files.Select(x => new FileSummary
            {
                Id = x.Id,
                OriginalName = x.OriginalName,
                Size = x.Size,
                ContentType = x.ContentType,
                UploadedAt = x.UploadedAt
            }).ToList();
        }


        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var file = await _repo.GetByIdAsync(id, ct);

            if (file == null)
                throw new Exception("File was not found");

            if (File.Exists(file.StoragePath))
                File.Delete(file.StoragePath);

            await _repo.DeleteAsync(file, ct);
        }


    }
}