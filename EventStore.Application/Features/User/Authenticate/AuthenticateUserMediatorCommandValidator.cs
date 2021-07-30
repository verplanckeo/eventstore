using FluentValidation;

namespace EventStore.Application.Features.User.Authenticate
{
    public class AuthenticateUserMediatorCommandValidator : AbstractValidator<AuthenticateUserMediatorCommand>
    {
        public AuthenticateUserMediatorCommandValidator()
        {
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