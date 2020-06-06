using MediatR;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Commands
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
    }
}
