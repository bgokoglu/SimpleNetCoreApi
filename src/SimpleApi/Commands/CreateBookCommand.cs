using MediatR;
using SimpleAPI.Models;

namespace SimpleAPI.Commands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
    }
}
