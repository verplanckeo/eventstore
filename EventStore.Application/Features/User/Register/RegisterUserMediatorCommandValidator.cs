using FluentValidation;

namespace EventStore.Application.Features.User.Register
{
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