using FluentValidation;

namespace EventStore.Application.Features.Project.Register;

public class RegisterProjectMediatorCommandValidator : AbstractValidator<RegisterProjectMediatorCommand>
{
    public RegisterProjectMediatorCommandValidator()
    {
        RuleFor(model => model.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name is required and must be maximum 100 characters.");

        RuleFor(model => model.Code)
            .NotNull()
            .NotEmpty()
            .MaximumLength(25)
            .WithMessage("Code is required and must be maximum 25 characters.");
    }
}