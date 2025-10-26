using System;
using FluentValidation;

namespace EventStore.Application.Features.Project.LoadSingleProject;

public class LoadSingleProjectMediatorQueryValidator : AbstractValidator<LoadSingleProjectMediatorQuery>
{
    public LoadSingleProjectMediatorQueryValidator()   
    {
        RuleFor(model => model.AggregateRootId)
            .NotNull()
            .NotEmpty()
            .MinimumLength(36)
            .Must(value => Guid.TryParse(value, out _))
            .WithMessage("AggregateRootId must be a valid GUID.");
    }
}