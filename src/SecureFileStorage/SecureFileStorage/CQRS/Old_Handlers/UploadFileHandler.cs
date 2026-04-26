//using MediatR;
//using SecureFileStorage.CQRS.Commands;
//using SecureFileStorage.Services;

//namespace SecureFileStorage.CQRS.Handlers
//{
//    public class UploadFileHandler : IRequestHandler<UploadFileCommand, Guid>
//    {
//        private readonly FileService _service;

//        public UploadFileHandler(FileService service)
//        {
//            _service = service;
//        }

//        public async Task<Guid> Handle(UploadFileCommand request, CancellationToken ct)
//        {
//            return await _service.UploadAsync(request.File, ct);
//        }
//    }
//}