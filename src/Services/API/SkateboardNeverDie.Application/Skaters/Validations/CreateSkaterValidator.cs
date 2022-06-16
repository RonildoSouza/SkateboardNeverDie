using FluentValidation;
using SkateboardNeverDie.Application.Skaters.Dtos;

namespace SkateboardNeverDie.Application.Skaters.Validations
{
    public sealed class CreateSkaterValidator : AbstractValidator<CreateSkaterDto>
    {
        public CreateSkaterValidator()
        {
            RuleFor(s => s.FirstName).NotNull().NotEmpty().MaximumLength(128);

            RuleFor(s => s.LastName).NotNull().NotEmpty().MaximumLength(128);

            RuleFor(s => s.Birthdate).NotNull().NotEmpty();

            RuleFor(s => s.NaturalStance).NotNull();

            RuleFor(s => s.SkaterTricks).NotNull().NotEmpty();
        }
    }
}
