using FluentValidation;

namespace EventStore.Application.Features.User.Password
{
    public class GetHashedPasswordMediatorQueryValidator : AbstractValidator<GetHashedPasswordMediatorQuery>
    {
        /// <summary>
        /// CTor
        /// </summary>
        public GetHashedPasswordMediatorQueryValidator()
        {
            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}