using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecureFileStorage.Models;

namespace SecureFileStorage.CQRS.Queries
{
    public class DownloadFileQuery : IRequest<FileStreamResult>
    {
        public Guid Id { get; set; }
    }
}