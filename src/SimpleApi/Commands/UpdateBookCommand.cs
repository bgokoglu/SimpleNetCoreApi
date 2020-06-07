using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;

namespace SimpleAPI.Commands
{
    public class UpdateBookCommand : IRequest<Response>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
