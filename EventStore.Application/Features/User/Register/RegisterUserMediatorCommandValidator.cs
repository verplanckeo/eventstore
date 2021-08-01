using FluentValidation;
using FluentValidation.Results;

namespace EventStore.Application.Features.User.Register
{
    //TODO: Abstragate Validator even more so the rest of the application is not aware of Fluent validation.
    //      Right now we return FluentValidationResults but it should be a custom model specific to the application to avoid dependencies on this lib.
    public class RegisterUserMediatorCommandValidator : AbstractValidator<RegisterUserMediatorCommand>
    {
        public RegisterUserMediatorCommandValidator()   
        {
            RuleFor(model => model.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(model => model.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(model => model.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}