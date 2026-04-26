using MediatR;
using SecureFileStorage.Models;

namespace SecureFileStorage.CQRS.Queries
{
    public class GetFilesQuery : IRequest<List<FileSummary>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}