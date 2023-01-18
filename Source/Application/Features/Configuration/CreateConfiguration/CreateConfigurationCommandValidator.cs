using FluentValidation;

namespace Application.Features.Configuration.CreateConfiguration;

public class CreateConfigurationCommandValidator : AbstractValidator<CreateConfigurationCommand>
{
	public CreateConfigurationCommandValidator()
	{
        //RuleFor(v => v.PostId)
        //    .NotEmpty();

        //RuleFor(v => v.Name)
        //    .MaximumLength(200)
        //    .NotEmpty();
    }
}
