using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator() {
            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("'Content' field is required")
                .MaximumLength(200).WithMessage("'Content' must less than 200 characters");
        }
    }
}
