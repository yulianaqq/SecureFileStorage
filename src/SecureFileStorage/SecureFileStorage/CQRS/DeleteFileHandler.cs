using MediatR;
using SecureFileStorage.CQRS.Commands;
using SecureFileStorage.Repositories.Interfaces;

namespace SecureFileStorage.CQRS.Handlers
{
    public class DeleteFileHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly IFileRepository _repo;

        public DeleteFileHandler(IFileRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteFileCommand request, CancellationToken ct)
        {
            var file = await _repo.GetByIdAsync(request.Id, ct);

            if (file == null)
                throw new Exception("File was not found");

            if (File.Exists(file.StoragePath))
                File.Delete(file.StoragePath);

            await _repo.DeleteAsync(file, ct);
        }
    }
}