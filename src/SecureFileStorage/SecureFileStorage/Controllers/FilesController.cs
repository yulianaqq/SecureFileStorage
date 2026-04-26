using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecureFileStorage.CQRS.Commands;
using SecureFileStorage.CQRS.Queries;
using SecureFileStorage.Services;

namespace SecureFileStorage.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly FileService _service;

        public FilesController(IMediator mediator, FileService service)
        {
            _mediator = mediator;
            _service = service;
        }

   
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
        {
            var id = await _mediator.Send(new UploadFileCommand { File = file }, ct);
            return Ok(id);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetFilesQuery
            {
                Page = page,
                PageSize = pageSize
            });

            return Ok(result);
        }

   
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(Guid id, CancellationToken ct)
        {
            var (stream, file) = await _service.DownloadAsync(id, ct);

            return File(stream, file.ContentType, file.OriginalName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}