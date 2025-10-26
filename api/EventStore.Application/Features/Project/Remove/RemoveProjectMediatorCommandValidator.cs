using System;
using FluentValidation;

namespace EventStore.Application.Features.Project.Remove;

public class RemoveProjectMediatorCommandValidator : AbstractValidator<RemoveProjectMediatorCommand>
{
    public RemoveProjectMediatorCommandValidator()
    {
        RuleFor(model => model.AggregateRootId)
            .NotNull()
            .NotEmpty()
            .Must(value => Guid.TryParse(value, out _))
            .WithMessage("AggregateRootId must be a valid GUID.");
    }
}