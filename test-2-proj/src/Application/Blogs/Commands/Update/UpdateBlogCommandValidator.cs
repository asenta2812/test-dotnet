using FluentValidation;

namespace Application.Blogs.Commands.Update;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(v => v.Title)
            .Must(s => !string.IsNullOrEmpty(s))
            .When(s => s.Title != null);

        RuleFor(v => v.Content)
            .Must(s => !string.IsNullOrEmpty(s))
            .When(s => s.Content != null);
        ;
    }
}