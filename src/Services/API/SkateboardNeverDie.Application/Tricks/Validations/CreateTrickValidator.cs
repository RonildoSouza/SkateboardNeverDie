using FluentValidation;
using SkateboardNeverDie.Application.Tricks.Dtos;

namespace SkateboardNeverDie.Application.Tricks.Validations
{
    public sealed class CreateTrickValidator : AbstractValidator<CreateTrickDto>
    {
        public CreateTrickValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().MaximumLength(128);
        }
    }
}
