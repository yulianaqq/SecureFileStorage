using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecureFileStorage.CQRS.Queries;
using SecureFileStorage.Repositories.Interfaces;

namespace SecureFileStorage.CQRS.Handlers
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileQuery, FileStreamResult>
    {
        private readonly IFileRepository _repo;

        public DownloadFileHandler(IFileRepository repo)
        {
            _repo = repo;
        }

        public async Task<FileStreamResult> Handle(DownloadFileQuery request, CancellationToken ct)
        {
            var file = await _repo.GetByIdAsync(request.Id, ct);

            if (file == null)
                throw new Exception("File was not found");

            var stream = new FileStream(file.StoragePath, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(stream, file.ContentType)
            {
                FileDownloadName = file.OriginalName
            };
        }
    }
}