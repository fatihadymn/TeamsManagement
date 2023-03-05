using FluentValidation;
using TeamsManagement.Items.Models.Requests;

namespace TeamsManagement.Items.Validators
{
    public class UpdatePlayerRequestValidator : AbstractValidator<UpdatePlayerRequest>
    {
        public UpdatePlayerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MinimumLength(1)
                                .WithName("Name");

            RuleFor(x => x.Height).GreaterThan(50)
                                  .LessThanOrEqualTo(260)
                                  .WithName("Height");

            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now.Date)
                                       .WithName("Date of Birth");
        }
    }
}
