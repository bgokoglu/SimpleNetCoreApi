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
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
    }
}
