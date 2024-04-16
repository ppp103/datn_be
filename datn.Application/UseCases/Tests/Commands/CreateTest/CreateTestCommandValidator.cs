using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(v => v.TestName)
                .NotEmpty().WithMessage("Tiêu đề không được để trống");
        }
    }
}
