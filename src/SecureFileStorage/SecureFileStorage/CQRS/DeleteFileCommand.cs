using MediatR;
using Microsoft.AspNetCore.Http;

namespace SecureFileStorage.CQRS.Commands
{
    public class DeleteFileCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}