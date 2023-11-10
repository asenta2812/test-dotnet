using FluentValidation;

namespace Application.Blogs.Commands.Create;

public class CreateBlogPostValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Content).NotEmpty();
    }
}