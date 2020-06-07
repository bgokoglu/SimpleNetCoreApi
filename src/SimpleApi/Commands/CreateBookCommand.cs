using FluentValidation;
using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;

namespace SimpleAPI.Commands
{
    public class CreateBookCommand : IRequest<Response>
    {
        public string Title { get; set; }
    }
}
