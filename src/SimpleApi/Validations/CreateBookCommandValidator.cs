using FluentValidation;
using MediatR;
using SimpleAPI.Commands;
using SimpleAPI.Core;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Validations
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("book title is required");
        }
    }
}
