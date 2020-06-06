using MediatR;
using SimpleAPI.Models;

namespace SimpleAPI.Commands
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
    }
}
