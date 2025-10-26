using System;
using FluentValidation;

namespace EventStore.Application.Features.Project.Update;

public class UpdateProjectMediatorCommandValidator : AbstractValidator<UpdateProjectMediatorCommand>
{
    public UpdateProjectMediatorCommandValidator()
    {
        RuleFor(model => model.AggregateRootId)
            .NotNull()
            .NotEmpty()
            .Must(value => Guid.TryParse(value, out _))
            .WithMessage("AggregateRootId must be a valid GUID.");

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