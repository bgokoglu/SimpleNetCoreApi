using MediatR;
using SimpleAPI.Models;

namespace SimpleAPI.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
