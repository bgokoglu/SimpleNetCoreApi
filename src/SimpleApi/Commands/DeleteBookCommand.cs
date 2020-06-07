using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;

namespace SimpleAPI.Commands
{
    public class DeleteBookCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
