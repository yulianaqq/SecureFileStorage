using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecureFileStorage.CQRS.Commands;
using SecureFileStorage.CQRS.Queries;
using SecureFileStorage.Models;
using SecureFileStorage.Repositories.Interfaces;
using SecureFileStorage.Services;

namespace SecureFileStorage.CQRS.Handlers
{
    public class FileOperationsHandler :
        IRequestHandler<UploadFileCommand, Guid>,
        IRequestHandler<GetFilesQuery, List<FileSummary>>,
        IRequestHandler<DownloadFileQuery, FileStreamResult>,
        IRequestHandler<DeleteFileCommand>
    {
        private readonly FileService _service;
        private readonly IFileRepository _repo;

        public FileOperationsHandler(FileService service, IFileRepository repo)
        {
            _service = service;
            _repo = repo;
        }

        //Upload
        public async Task<Guid> Handle(UploadFileCommand request, CancellationToken ct)
        {
            return await _service.UploadAsync(request.File, ct);
        }

        //GetFiles
        public async Task<List<FileSummary>> Handle(GetFilesQuery request, CancellationToken ct)
        {
            var files = await _repo.GetAllAsync(ct);

            return files
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FileSummary
                {
                    Id = x.Id,
                    OriginalName = x.OriginalName,
                    Size = x.Size,
                    ContentType = x.ContentType,
                    UploadedAt = x.UploadedAt
                })
                .ToList();
        }

        //Download
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

        //Delete
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