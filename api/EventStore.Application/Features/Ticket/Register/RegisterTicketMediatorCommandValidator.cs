using System;
using FluentValidation;

namespace EventStore.Application.Features.Ticket.Register
{
    public class RegisterTicketMediatorCommandValidator : AbstractValidator<RegisterTicketMediatorCommand>
    {
        /// <summary>
        /// CTor
        /// </summary>
        public RegisterTicketMediatorCommandValidator()
        {
            RuleFor(model => model.UserId)
                .MinimumLength(10)
                .Must(value => Guid.TryParse(value, out _));

            RuleFor(model => model.Title)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(model => model.Description)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}