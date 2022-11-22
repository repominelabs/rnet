using FluentValidation;

namespace Application.Features.Comment.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
	public CreateCommentCommandValidator()
	{
        RuleFor(v => v.PostId)
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
