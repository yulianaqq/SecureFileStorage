using FluentValidation;
using SecureFileStorage.CQRS.Commands;

namespace SecureFileStorage.Validators
{
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required");

            RuleFor(x => x.File.Length)
                .GreaterThan(0)
                .When(x => x.File != null)
                .WithMessage("File cannot be empty");
        }
    }
}