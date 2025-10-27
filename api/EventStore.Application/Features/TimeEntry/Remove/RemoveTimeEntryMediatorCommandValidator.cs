using FluentValidation;

namespace EventStore.Application.Features.TimeEntry.Remove;

public class RemoveTimeEntryMediatorCommandValidator : AbstractValidator<RemoveTimeEntryMediatorCommand>
{
    public RemoveTimeEntryMediatorCommandValidator()
    {
        RuleFor(x => x.TimeEntryId)
            .NotEmpty()
            .WithMessage("TimeEntryId is required.")
            .MinimumLength(36)
            .WithMessage("TimeEntryId must be a valid GUID.");
    }
}