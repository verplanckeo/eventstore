using FluentValidation;

namespace EventStore.Application.Features.TimeEntry.Update;

public class UpdateTimeEntryMediatorCommandValidator : AbstractValidator<UpdateTimeEntryMediatorCommand>
{
    public UpdateTimeEntryMediatorCommandValidator()
    {
        RuleFor(x => x.TimeEntryId)
            .NotEmpty()
            .WithMessage("TimeEntryId is required.")
            .MinimumLength(36)
            .WithMessage("TimeEntryId must be a valid GUID.");

        RuleFor(x => x.From)
            .NotEmpty()
            .WithMessage("From date is required.");

        RuleFor(x => x.Until)
            .NotEmpty()
            .WithMessage("Until date is required.")
            .GreaterThan(x => x.From)
            .WithMessage("Until date must be after From date.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .MinimumLength(36)
            .WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId is required.")
            .MinimumLength(36)
            .WithMessage("ProjectId must be a valid GUID.");

        RuleFor(x => x.ActivityType)
            .NotEmpty()
            .WithMessage("ActivityType is required.");

        RuleFor(x => x.Comment)
            .MaximumLength(100)
            .WithMessage("Comment cannot exceed 100 characters.");
    }
}