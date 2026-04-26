using MediatR;
using SecureFileStorage.CQRS.Queries;
using SecureFileStorage.Models;
using SecureFileStorage.Repositories.Interfaces;

namespace SecureFileStorage.CQRS.Handlers
{
    public class GetFilesHandler : IRequestHandler<GetFilesQuery, List<FileSummary>>
    {
        private readonly IFileRepository _repo;

        public GetFilesHandler(IFileRepository repo)
        {
            _repo = repo;
        }

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
    }
}