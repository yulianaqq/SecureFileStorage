using MediatR;
using Microsoft.AspNetCore.Http;

namespace SecureFileStorage.CQRS.Commands
{
    public class UploadFileCommand : IRequest<Guid>
    {
        public IFormFile File { get; set; }
    }
}